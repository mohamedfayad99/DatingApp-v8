using System;
using CloudinaryDotNet.Actions;

namespace API.Interfaces;

public interface IPhotoServices
{
    Task<ImageUploadResult> AddphotoAsync(IFormFile file);
    Task<DeletionResult> DeletephototoAsync(string publicId);
}
