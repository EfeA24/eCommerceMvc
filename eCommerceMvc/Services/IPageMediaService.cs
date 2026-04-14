using eCommerceMvc.Models.PageContentEntities;

namespace eCommerceMvc.Services
{
    public interface IPageMediaService
    {
        Task<ImageUploadResponse> UploadImageAsync(ImageUploadRequest request);
    }
}
