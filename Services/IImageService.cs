using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using PartyFunApi.DTO;

namespace PartyFunApi.Services;

public interface IImageService
{
  Task<UploadImageResponseDTO> AddImageUploadAsync(IFormFile file, Transformation? transformation);
  Task<UploadImageResponseDTO> UploadUserAvatar(IFormFile file);
  Task<DeletionResult> DeleteImageUploadAsync(string publicId);
  Task<List<UploadImageResponseDTO>> UploadBulkAsync(IFormFileCollection files);
}


