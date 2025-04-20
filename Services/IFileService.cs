namespace PickEm.Api.Services;

public interface IFileService
{
    Task<string> UploadFileAsync(IFormFile file);
}