using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace eCommerceMvc.Models.PageContentEntities
{
    public class PageLayoutDto
    {
        public List<SectionDto> Sections { get; set; } = new();
    }

    public class SectionDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public List<ColumnDto> Columns { get; set; } = new();
    }

    public class ColumnDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public int Width { get; set; } = 12;
        public List<BlockDto> Blocks { get; set; } = new();
    }

    public class BlockDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string Type { get; set; } = "text";
        public BlockStyleDto Style { get; set; } = new();
        public Dictionary<string, string> Data { get; set; } = new();
    }

    public class BlockStyleDto
    {
        public string FontFamily { get; set; } = "Arial";
        public int FontSize { get; set; } = 16;
        public string FontWeight { get; set; } = "normal";
        public string FontStyle { get; set; } = "normal";
        public string TextAlign { get; set; } = "left";
        public int Width { get; set; } = 100;
        public int Height { get; set; } = 120;
    }

    public class PageBuilderFormViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string LayoutJson { get; set; } = "{\"sections\":[]}";
        public PageStatus Status { get; set; } = PageStatus.Draft;
    }

    public class PageBuilderListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public PageStatus Status { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class PageBuilderIndexViewModel
    {
        public List<PageBuilderListItemViewModel> Pages { get; set; } = new();
    }

    public class PageRenderViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string RenderedHtml { get; set; } = string.Empty;
        public bool IsPreview { get; set; }
    }

    public class ImageUploadResponse
    {
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }

    public class ImageUploadRequest
    {
        public IFormFile? File { get; set; }
    }
}
