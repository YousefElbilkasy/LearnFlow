using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using LearnFlow.Helpers;
using LearnFlow.Interfaces;
using Microsoft.Extensions.Options;

namespace LearnFlow.Service
{
  public class VideoService : IVideoService
  {
    private readonly Cloudinary _cloud;

    public VideoService(Cloudinary cloudinary)
    {
      _cloud = cloudinary;
    }

    public async Task<VideoUploadResult> AddVideoAsync(IFormFile file)
    {
      var uploadResult = new VideoUploadResult();
      if (file.Length > 0)
      {
        using var stream = file.OpenReadStream();
        var uploadParams = new VideoUploadParams
        {
          File = new FileDescription(file.FileName, stream),
          Transformation = new Transformation().Height(500).Width(500).Gravity("face")
        };
        uploadResult = await _cloud.UploadAsync(uploadParams);
      }
      return uploadResult;
    }

    //// New method to upload PDF files
    //public async Task<RawUploadResult> AddPdfAsync(IFormFile file)
    //{
    //    var uploadResult = new RawUploadResult();
    //    if (file.Length > 0)
    //    {
    //        using var stream = file.OpenReadStream();
    //        var uploadParams = new RawUploadParams
    //        {
    //            File = new FileDescription(file.FileName, stream)
    //        };
    //        uploadResult = await _cloud.UploadAsync(uploadParams);
    //    }
    //    return uploadResult;
    //}

    public async Task<DeletionResult> DeleteVideoAsync(string publicId)
    {
      var deleteParms = new DeletionParams(publicId);
      var result = await _cloud.DestroyAsync(deleteParms);
      return result;
    }
  }
}
