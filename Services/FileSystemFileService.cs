namespace PickEm.Api.Services;

public class FileSystemFileService : IFileService
{
    private readonly string _basePath;

    public FileSystemFileService(string basePath)
    {
        _basePath = basePath;
    }

    public async Task<string> UploadFileAsync(IFormFile file)
    {
        var filePath = Path.Combine(_basePath, file.FileName);
        try
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }
        catch (Exception ex)
        {
            // Log the exception (not shown here for brevity)
            return string.Empty;
        }

        return filePath;
    }
}