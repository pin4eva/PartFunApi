using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using PartyFunApi.DTO;

namespace PartyFunApi.Services;

public class ImageService : IImageService
{
  private readonly Cloudinary cloudinary;



  public ImageService(IConfiguration _config)
  {
    var cloudinaryUrl = _config["CLOUDINARY_URL"];

    Console.WriteLine("cloudinaryUrl {0}", cloudinaryUrl);
    Cloudinary _cloudinary = new(cloudinaryUrl);
    cloudinary = _cloudinary;
  }

  public async Task<UploadImageResponseDTO> AddImageUploadAsync(IFormFile file, Transformation? inputDTO)
  {
    var uploadResult = new ImageUploadResult();


    if (file.Length > 0)
    {
      using var stream = file.OpenReadStream();
      ImageUploadParams uploadParams = new()
      {
        File = new FileDescription(file.FileName, stream),
        Folder = "PartyFun",
        Transformation = inputDTO
      };



      uploadResult = await cloudinary.UploadAsync(uploadParams);

    }

    var response = new UploadImageResponseDTO
    {
      PublicId = uploadResult.PublicId,
      Url = uploadResult.Url.AbsoluteUri,
      ErrorMessage = uploadResult?.Error?.Message
    };

    return response;
  }


  public async Task<DeletionResult> DeleteImageUploadAsync(string publicId)
  {
    DeletionParams deletionParams = new(publicId);

    return await cloudinary.DestroyAsync(deletionParams);
  }

  public async Task<List<UploadImageResponseDTO>> UploadBulkAsync(IFormFileCollection files)
  {
    List<UploadImageResponseDTO> results = [];
    foreach (IFormFile file in files)
    {
      var result = await AddImageUploadAsync(file, null);
      results.Add(result);
    }

    return results;
  }

  public async Task<UploadImageResponseDTO> UploadUserAvatar(IFormFile file)
  {
    var transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face");
    var data = await AddImageUploadAsync(file, transformation);

    return data;
  }
}
