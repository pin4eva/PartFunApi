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

  public async Task<UploadImageResponseDTO> AddImageUploadAsync(IFormFile file)
  {
    var uploadResult = new ImageUploadResult();


    if (file.Length > 0)
    {
      using var stream = file.OpenReadStream();
      ImageUploadParams uploadParams = new()
      {
        File = new FileDescription(file.FileName, stream),
        Folder = "PartyFun"
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
      var result = await AddImageUploadAsync(file);
      results.Add(result);
    }

    return results;
  }
}
