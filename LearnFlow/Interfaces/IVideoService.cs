using CloudinaryDotNet.Actions;
namespace LearnFlow.Interfaces
{
    public interface IVideoService
    {
        Task<VideoUploadResult> AddVideoAsync(IFormFile file);
        Task<DeletionResult> DeleteVideoAsync(string publicId);
        //Task<RawUploadResult> AddPdfAsync(IFormFile file);
    }
}
