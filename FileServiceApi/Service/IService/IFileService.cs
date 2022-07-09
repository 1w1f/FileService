using DataModel.File;
using FileService.Service.IService;
using Microsoft.Net.Http.Headers;

namespace FileServiceApi.Service.IService
{
    public interface IFileService 
    {
        Task<(Stream largeFileStream, string fileName)> GetFileInfoFromRequest(HttpRequest request,MediaTypeHeaderValue mediaTypeHeader);
    }
}