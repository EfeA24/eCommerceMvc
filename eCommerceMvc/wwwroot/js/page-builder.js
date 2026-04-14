(() => {
    const cfg = window.pageBuilderConfig;
    if (!cfg) {
        return;
    }

    const canvas = document.getElementById("builderCanvas");
    const propertiesPanel = document.getElementById("blockProperties");
    const layoutJsonInput = document.getElementById("layoutJsonInput");
    const addSectionButton = document.getElementById("addSectionButton");
    const addBlockButtons = document.querySelectorAll("[data-add-block]");
    const form = document.getElementById("pageBuilderForm");

    let selectedBlockId = null;
    let state = parseLayout(cfg.initialLayoutJson);

    if (!state.sections.length) {
        state.sections.push(createSection());
    }

    function parseLayout(layoutJson) {
        try {
            const parsed = JSON.parse(layoutJson || "{}");
            if (!Array.isArray(parsed.sections)) {
                return { sections: [] };
            }

            return parsed;
        } catch {
            return { sections: [] };
        }
    }

    function createSection() {
        return {
            id: crypto.randomUUID(),
            columns: [
                { id: crypto.randomUUID(), width: 6, blocks: [] },
                { id: crypto.randomUUID(), width: 6, blocks: [] }
            ]
        };
    }

    function createBlock(type) {
        const base = {
            id: crypto.randomUUID(),
            type,
            style: {
                fontFamily: "Arial",
                fontSize: 16,
                fontWeight: "normal",
                fontStyle: "normal",
                textAlign: "left",
                width: 240,
                height: 120
            },
            data: {}
        };

        if (type === "text") {
            base.data.text = "Type text";
        } else if (type === "image") {
            base.data.src = "";
            base.data.alt = "image";
        } else if (type === "button") {
            base.data.text = "Button";
            base.data.href = "#";
        }

        return base;
    }

    function findBlock(blockId) {
        for (const section of state.sections) {
            for (const column of section.columns) {
                const block = column.blocks.find(x => x.id === blockId);
                if (block) {
                    return { section, column, block };
                }
            }
        }

        return null;
    }

    function renderCanvas() {
        canvas.innerHTML = "";
        state.sections.forEach(section => {
            const sectionEl = document.createElement("div");
            sectionEl.className = "pb-section";
            sectionEl.dataset.sectionId = section.id;

            const row = document.createElement("div");
            row.className = "row g-2";

            section.columns.forEach(column => {
                const col = document.createElement("div");
                col.className = `col-md-${Math.min(Math.max(column.width || 12, 1), 12)}`;

                const columnEl = document.createElement("div");
                columnEl.className = "pb-column";
                columnEl.dataset.columnId = column.id;
                columnEl.addEventListener("dragover", onColumnDragOver);
                columnEl.addEventListener("drop", onColumnDrop);

                column.blocks.forEach(block => {
                    const blockEl = createBlockElement(block);
                    columnEl.appendChild(blockEl);
                });

                col.appendChild(columnEl);
                row.appendChild(col);
            });

            sectionEl.appendChild(row);
            canvas.appendChild(sectionEl);
        });

        persistState();
        renderProperties();
    }

    function createBlockElement(block) {
        const blockEl = document.createElement("div");
        blockEl.className = "pb-block";
        blockEl.draggable = true;
        blockEl.dataset.blockId = block.id;
        blockEl.style.width = `${block.style.width}px`;
        blockEl.style.height = `${block.style.height}px`;
        blockEl.style.fontFamily = block.style.fontFamily;
        blockEl.style.fontSize = `${block.style.fontSize}px`;
        blockEl.style.fontWeight = block.style.fontWeight;
        blockEl.style.fontStyle = block.style.fontStyle;
        blockEl.style.textAlign = block.style.textAlign;
        blockEl.style.position = "relative";

        if (selectedBlockId === block.id) {
            blockEl.classList.add("selected");
        }

        blockEl.addEventListener("click", () => {
            selectedBlockId = block.id;
            renderCanvas();
        });

        blockEl.addEventListener("dragstart", () => {
            blockEl.classList.add("dragging");
            blockEl.dataset.dragging = "1";
        });

        blockEl.addEventListener("dragend", () => {
            blockEl.classList.remove("dragging");
            delete blockEl.dataset.dragging;
        });

        const inner = document.createElement("div");
        if (block.type === "image") {
            inner.innerHTML = block.data.src
                ? `<img src="${escapeHtml(block.data.src)}" alt="${escapeHtml(block.data.alt || "image")}" />`
                : "<span class=\"text-muted\">Image block</span>";
        } else if (block.type === "button") {
            inner.innerHTML = `<button type="button" class="btn btn-primary btn-sm">${escapeHtml(block.data.text || "Button")}</button>`;
        } else {
            inner.textContent = block.data.text || "Text block";
        }

        const resizeHandle = document.createElement("div");
        resizeHandle.className = "pb-resize-handle";
        resizeHandle.addEventListener("mousedown", e => onResizeStart(e, block.id));

        blockEl.appendChild(inner);
        blockEl.appendChild(resizeHandle);
        return blockEl;
    }

    function onColumnDragOver(event) {
        event.preventDefault();
    }

    function onColumnDrop(event) {
        event.preventDefault();
        const dragging = document.querySelector(".pb-block.dragging");
        const targetColumn = event.currentTarget.dataset.columnId;
        if (!dragging || !targetColumn) {
            return;
        }

        const blockId = dragging.dataset.blockId;
        moveBlockToColumn(blockId, targetColumn);
    }

    function moveBlockToColumn(blockId, targetColumnId) {
        if (!blockId) {
            return;
        }

        let movedBlock = null;
        for (const section of state.sections) {
            for (const column of section.columns) {
                const idx = column.blocks.findIndex(x => x.id === blockId);
                if (idx >= 0) {
                    movedBlock = column.blocks.splice(idx, 1)[0];
                    break;
                }
            }
            if (movedBlock) {
                break;
            }
        }

        if (!movedBlock) {
            return;
        }

        const target = state.sections.flatMap(x => x.columns).find(x => x.id === targetColumnId);
        if (!target) {
            return;
        }

        target.blocks.push(movedBlock);
        selectedBlockId = movedBlock.id;
        renderCanvas();
    }

    function onResizeStart(event, blockId) {
        event.preventDefault();
        event.stopPropagation();
        const item = findBlock(blockId);
        if (!item) {
            return;
        }

        const startX = event.clientX;
        const startY = event.clientY;
        const initialWidth = item.block.style.width;
        const initialHeight = item.block.style.height;

        function onMove(moveEvent) {
            const width = Math.max(80, initialWidth + (moveEvent.clientX - startX));
            const height = Math.max(60, initialHeight + (moveEvent.clientY - startY));
            item.block.style.width = width;
            item.block.style.height = height;
            renderCanvas();
        }

        function onUp() {
            window.removeEventListener("mousemove", onMove);
            window.removeEventListener("mouseup", onUp);
        }

        window.addEventListener("mousemove", onMove);
        window.addEventListener("mouseup", onUp);
    }

    function renderProperties() {
        if (!selectedBlockId) {
            propertiesPanel.textContent = "Select a block to edit properties.";
            return;
        }

        const item = findBlock(selectedBlockId);
        if (!item) {
            propertiesPanel.textContent = "Select a block to edit properties.";
            return;
        }

        const block = item.block;
        propertiesPanel.innerHTML = `
            <label class="form-label mt-2">Font</label>
            <input class="form-control form-control-sm" id="pbFontFamily" value="${escapeAttr(block.style.fontFamily)}" />
            <label class="form-label mt-2">Font Size</label>
            <input type="number" class="form-control form-control-sm" id="pbFontSize" value="${block.style.fontSize}" min="10" max="96" />
            <label class="form-label mt-2">Align</label>
            <select class="form-select form-select-sm" id="pbTextAlign">
                ${["left", "center", "right"].map(x => `<option value="${x}" ${block.style.textAlign === x ? "selected" : ""}>${x}</option>`).join("")}
            </select>
            <div class="d-flex gap-2 mt-2">
                <button type="button" class="btn btn-sm btn-outline-secondary" id="pbBoldButton">Bold</button>
                <button type="button" class="btn btn-sm btn-outline-secondary" id="pbItalicButton">Italic</button>
            </div>
            <label class="form-label mt-2">Width</label>
            <input type="number" class="form-control form-control-sm" id="pbWidth" value="${block.style.width}" min="80" max="1200" />
            <label class="form-label mt-2">Height</label>
            <input type="number" class="form-control form-control-sm" id="pbHeight" value="${block.style.height}" min="60" max="1200" />
            ${renderTypeSpecificInputs(block)}
            <button type="button" class="btn btn-sm btn-outline-danger mt-3" id="pbDeleteBlock">Delete Block</button>
        `;

        bindPropertyEvents(block.id);
    }

    function renderTypeSpecificInputs(block) {
        if (block.type === "image") {
            return `
                <label class="form-label mt-2">Image</label>
                <input type="file" class="form-control form-control-sm" id="pbImageFile" accept=".jpg,.jpeg,.png,.webp,.gif" />
                <input type="text" class="form-control form-control-sm mt-2" id="pbImageAlt" placeholder="Alt text" value="${escapeAttr(block.data.alt || "image")}" />
            `;
        }

        if (block.type === "button") {
            return `
                <label class="form-label mt-2">Button Text</label>
                <input type="text" class="form-control form-control-sm" id="pbButtonText" value="${escapeAttr(block.data.text || "Button")}" />
                <label class="form-label mt-2">Button Link</label>
                <input type="text" class="form-control form-control-sm" id="pbButtonHref" value="${escapeAttr(block.data.href || "#")}" />
            `;
        }

        return `
            <label class="form-label mt-2">Text</label>
            <textarea class="form-control form-control-sm" id="pbTextValue" rows="4">${escapeHtml(block.data.text || "")}</textarea>
        `;
    }

    function bindPropertyEvents(blockId) {
        const item = findBlock(blockId);
        if (!item) {
            return;
        }

        const block = item.block;
        const byId = id => document.getElementById(id);

        byId("pbFontFamily")?.addEventListener("input", e => updateBlock(blockId, b => b.style.fontFamily = e.target.value));
        byId("pbFontSize")?.addEventListener("input", e => updateBlock(blockId, b => b.style.fontSize = clampNumber(e.target.value, 10, 96, 16)));
        byId("pbTextAlign")?.addEventListener("change", e => updateBlock(blockId, b => b.style.textAlign = e.target.value));
        byId("pbWidth")?.addEventListener("input", e => updateBlock(blockId, b => b.style.width = clampNumber(e.target.value, 80, 1200, 240)));
        byId("pbHeight")?.addEventListener("input", e => updateBlock(blockId, b => b.style.height = clampNumber(e.target.value, 60, 1200, 120)));
        byId("pbBoldButton")?.addEventListener("click", () => updateBlock(blockId, b => b.style.fontWeight = b.style.fontWeight === "bold" ? "normal" : "bold"));
        byId("pbItalicButton")?.addEventListener("click", () => updateBlock(blockId, b => b.style.fontStyle = b.style.fontStyle === "italic" ? "normal" : "italic"));
        byId("pbDeleteBlock")?.addEventListener("click", () => deleteBlock(blockId));

        if (block.type === "text") {
            byId("pbTextValue")?.addEventListener("input", e => updateBlock(blockId, b => b.data.text = e.target.value));
        }

        if (block.type === "button") {
            byId("pbButtonText")?.addEventListener("input", e => updateBlock(blockId, b => b.data.text = e.target.value));
            byId("pbButtonHref")?.addEventListener("input", e => updateBlock(blockId, b => b.data.href = e.target.value));
        }

        if (block.type === "image") {
            byId("pbImageAlt")?.addEventListener("input", e => updateBlock(blockId, b => b.data.alt = e.target.value));
            byId("pbImageFile")?.addEventListener("change", async e => {
                const file = e.target.files?.[0];
                if (!file) {
                    return;
                }

                const url = await uploadImage(file);
                if (url) {
                    updateBlock(blockId, b => b.data.src = url);
                }
            });
        }
    }

    function deleteBlock(blockId) {
        for (const section of state.sections) {
            for (const column of section.columns) {
                const idx = column.blocks.findIndex(x => x.id === blockId);
                if (idx >= 0) {
                    column.blocks.splice(idx, 1);
                    selectedBlockId = null;
                    renderCanvas();
                    return;
                }
            }
        }
    }

    function updateBlock(blockId, updater) {
        const item = findBlock(blockId);
        if (!item) {
            return;
        }

        updater(item.block);
        renderCanvas();
    }

    async function uploadImage(file) {
        const formData = new FormData();
        formData.append("File", file);

        const response = await fetch(cfg.uploadImageUrl, {
            method: "POST",
            headers: {
                "RequestVerificationToken": cfg.antiForgeryToken
            },
            body: formData
        });

        if (!response.ok) {
            alert("Image upload failed.");
            return null;
        }

        const result = await response.json();
        return result.url;
    }

    function persistState() {
        layoutJsonInput.value = JSON.stringify(state);
    }

    function escapeHtml(value) {
        return String(value)
            .replaceAll("&", "&amp;")
            .replaceAll("<", "&lt;")
            .replaceAll(">", "&gt;")
            .replaceAll("\"", "&quot;");
    }

    function escapeAttr(value) {
        return escapeHtml(value).replaceAll("'", "&#39;");
    }

    function clampNumber(value, min, max, fallback) {
        const num = Number.parseInt(value, 10);
        if (Number.isNaN(num)) {
            return fallback;
        }

        return Math.min(Math.max(num, min), max);
    }

    addSectionButton.addEventListener("click", () => {
        state.sections.push(createSection());
        renderCanvas();
    });

    addBlockButtons.forEach(button => {
        button.addEventListener("click", () => {
            const type = button.getAttribute("data-add-block");
            const firstColumn = state.sections[0]?.columns[0];
            if (!firstColumn || !type) {
                return;
            }

            const block = createBlock(type);
            firstColumn.blocks.push(block);
            selectedBlockId = block.id;
            renderCanvas();
        });
    });

    form.addEventListener("submit", () => persistState());
    renderCanvas();
})();
