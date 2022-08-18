using Microsoft.Net.Http.Headers;

namespace FileServiceApi.Service.File
{
    public interface IFileStoreService
    {
        Task<(Stream largeFileStream, string fileName)> GetFileInfoFromRequest(HttpRequest request, MediaTypeHeaderValue mediaTypeHeader);
    }
}