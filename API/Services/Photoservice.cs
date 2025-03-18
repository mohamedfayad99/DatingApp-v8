using System;
using API.Helpers;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace API.Services;

public class Photoservice : IPhotoServices
{   private readonly Cloudinary _cloudinary;
    public Photoservice(IOptions<CloudinarySetting> confiq)
    {
        var acc= new Account(confiq.Value.CloudName,confiq.Value.ApiKey,confiq.Value.ApiSecret);
        _cloudinary = new Cloudinary(acc);

    }
    public  async Task<ImageUploadResult> AddphotoAsync(IFormFile file)

    {
        var uploadresult=new ImageUploadResult();
        if(file.Length>0){
            using var stream= file.OpenReadStream();
            var uploadparams=new ImageUploadParams{
                File=new FileDescription(file.FileName,stream),
                Transformation =new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                Folder="da-net8"
            };
            uploadresult=await _cloudinary.UploadAsync(uploadparams);
        }
        return uploadresult;
    }

    public async Task<DeletionResult> DeletephototoAsync(string publicId)
    {
        var deletephoto=new DeletionParams(publicId);
        return await  _cloudinary.DestroyAsync(deletephoto);
    }
}
