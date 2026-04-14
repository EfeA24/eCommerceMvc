using System.Net;
using System.Text;
using System.Text.Json;
using eCommerceMvc.Models.PageContentEntities;

namespace eCommerceMvc.Services
{
    public class PageRenderService
    {
        private static readonly HashSet<string> AllowedBlockTypes = new(StringComparer.OrdinalIgnoreCase)
        {
            "text",
            "image",
            "button"
        };

        public string RenderFromJson(string? layoutJson)
        {
            var layout = ParseLayout(layoutJson);
            var sb = new StringBuilder();
            sb.AppendLine("<div class=\"pb-public-page\">");

            foreach (var section in layout.Sections)
            {
                sb.AppendLine("<section class=\"pb-public-section\">");
                sb.AppendLine("<div class=\"row g-3\">");

                foreach (var column in section.Columns)
                {
                    var colWidth = Math.Clamp(column.Width, 1, 12);
                    sb.AppendLine($"<div class=\"col-md-{colWidth}\">");
                    sb.AppendLine("<div class=\"pb-public-column\">");

                    foreach (var block in column.Blocks.Where(x => AllowedBlockTypes.Contains(x.Type)))
                    {
                        sb.AppendLine(RenderBlock(block));
                    }

                    sb.AppendLine("</div>");
                    sb.AppendLine("</div>");
                }

                sb.AppendLine("</div>");
                sb.AppendLine("</section>");
            }

            sb.AppendLine("</div>");
            return sb.ToString();
        }

        private static PageLayoutDto ParseLayout(string? layoutJson)
        {
            if (string.IsNullOrWhiteSpace(layoutJson))
            {
                return new PageLayoutDto();
            }

            try
            {
                return JsonSerializer.Deserialize<PageLayoutDto>(layoutJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new PageLayoutDto();
            }
            catch
            {
                return new PageLayoutDto();
            }
        }

        private static string RenderBlock(BlockDto block)
        {
            var style = ToInlineStyle(block.Style);
            var content = block.Type.ToLowerInvariant() switch
            {
                "image" => RenderImage(block),
                "button" => RenderButton(block),
                _ => RenderText(block)
            };

            return $"<div class=\"pb-public-block pb-block-{WebUtility.HtmlEncode(block.Type)}\" style=\"{style}\">{content}</div>";
        }

        private static string RenderText(BlockDto block)
        {
            var text = block.Data.TryGetValue("text", out var value) ? value : string.Empty;
            return $"<div>{WebUtility.HtmlEncode(text)}</div>";
        }

        private static string RenderImage(BlockDto block)
        {
            var src = block.Data.TryGetValue("src", out var value) ? value : string.Empty;
            var alt = block.Data.TryGetValue("alt", out var altText) ? altText : "image";
            if (string.IsNullOrWhiteSpace(src))
            {
                return "<div>Image not set</div>";
            }

            return $"<img src=\"{WebUtility.HtmlEncode(src)}\" alt=\"{WebUtility.HtmlEncode(alt)}\" style=\"width:100%;height:100%;object-fit:cover;\" />";
        }

        private static string RenderButton(BlockDto block)
        {
            var text = block.Data.TryGetValue("text", out var value) ? value : "Button";
            var href = block.Data.TryGetValue("href", out var link) ? link : "#";
            return $"<a class=\"btn btn-primary\" href=\"{WebUtility.HtmlEncode(href)}\">{WebUtility.HtmlEncode(text)}</a>";
        }

        private static string ToInlineStyle(BlockStyleDto style)
        {
            var fontFamily = SanitizeFont(style.FontFamily);
            var fontSize = Math.Clamp(style.FontSize, 10, 96);
            var width = Math.Clamp(style.Width, 50, 1200);
            var height = Math.Clamp(style.Height, 40, 1200);
            var fontWeight = style.FontWeight.Equals("bold", StringComparison.OrdinalIgnoreCase) ? "bold" : "normal";
            var fontStyle = style.FontStyle.Equals("italic", StringComparison.OrdinalIgnoreCase) ? "italic" : "normal";
            var textAlign = style.TextAlign.ToLowerInvariant() switch
            {
                "center" => "center",
                "right" => "right",
                _ => "left"
            };

            return $"font-family:{fontFamily};font-size:{fontSize}px;font-weight:{fontWeight};font-style:{fontStyle};text-align:{textAlign};width:{width}px;height:{height}px;";
        }

        private static string SanitizeFont(string? fontFamily)
        {
            if (string.IsNullOrWhiteSpace(fontFamily))
            {
                return "Arial, sans-serif";
            }

            var safe = new string(fontFamily.Where(ch => char.IsLetterOrDigit(ch) || ch == ' ' || ch == ',' || ch == '-').ToArray());
            return string.IsNullOrWhiteSpace(safe) ? "Arial, sans-serif" : safe;
        }
    }
}
