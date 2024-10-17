using System;
using CloudinaryDotNet.Actions;

namespace LearnFlow.Interfaces;

public interface IImageService
{
  Task<ImageUploadResult> AddImageAsync(IFormFile file);
  Task<DeletionResult> DeleteImageAsync(string publicId);
}
