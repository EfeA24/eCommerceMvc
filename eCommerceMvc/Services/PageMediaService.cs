using eCommerceMvc.Models.PageContentEntities;

namespace eCommerceMvc.Services
{
    public class PageMediaService : IPageMediaService
    {
        private readonly IWebHostEnvironment _environment;

        public PageMediaService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<ImageUploadResponse> UploadImageAsync(ImageUploadRequest request)
        {
            var file = request.File;
            if (file is null || file.Length == 0)
            {
                throw new InvalidOperationException("File is required.");
            }

            var allowedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".jpg", ".jpeg", ".png", ".webp", ".gif" };
            var extension = Path.GetExtension(file.FileName);
            if (!allowedExtensions.Contains(extension))
            {
                throw new InvalidOperationException("Unsupported file extension.");
            }

            if (file.Length > 5 * 1024 * 1024)
            {
                throw new InvalidOperationException("File size cannot exceed 5 MB.");
            }

            var uploadDirectory = Path.Combine(_environment.WebRootPath, "uploads", "pages");
            Directory.CreateDirectory(uploadDirectory);

            var generatedName = $"{Guid.NewGuid():N}{extension.ToLowerInvariant()}";
            var filePath = Path.Combine(uploadDirectory, generatedName);

            await using (var stream = File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }

            return new ImageUploadResponse
            {
                Url = $"/uploads/pages/{generatedName}"
            };
        }
    }
}
