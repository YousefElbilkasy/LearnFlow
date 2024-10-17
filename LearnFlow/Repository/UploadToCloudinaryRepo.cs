using System;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace LearnFlow.Repository;

public class UploadToCloudinaryRepo
{
  private readonly Cloudinary cloudinary;

  public UploadToCloudinaryRepo(Cloudinary cloudinary)
  {
    this.cloudinary = cloudinary;
  }

  public async Task<string> UploadFileToCloudinary(IFormFile file)
  {
    if (file == null || file.Length == 0)
      return null;

    using (var stream = file.OpenReadStream())
    {
      var uploadParams = new RawUploadParams()
      {
        File = new FileDescription(file.FileName, stream)
      };

      var uploadResult = await cloudinary.UploadAsync(uploadParams);
      return uploadResult.SecureUrl.ToString();
    }
  }
}
