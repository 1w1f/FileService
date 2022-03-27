using System.IO;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.WebUtilities;
using DataModel.File;
using FileService.Service.Service;
using FileServiceApi.Service.IService;
using FileServiceRepsitory.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace FileServiceApi.Service.Service
{
    public class FileService : BaseService<FileModel>, IFileService
    {
        public FileService(IBaseRepository<FileModel> baseRepository) : base(baseRepository)
        {



        }
        public async Task<(Stream largeFileStream, string fileName)> GetFileInfoFromRequest(HttpRequest request,MediaTypeHeaderValue mediaTypeHeader)
        {
            var reader = new MultipartReader(mediaTypeHeader.Boundary.Value, request.Body);
            var section = await reader.ReadNextSectionAsync();
            // This sample try to get the first file from request and save it
            // Make changes according to your needs in actual use
            while (section != null)
            {
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition,
                    out var contentDisposition);
                if (hasContentDispositionHeader && contentDisposition.DispositionType.Equals("form-data"))
                {
                    if (!string.IsNullOrEmpty(contentDisposition.FileName.Value) && contentDisposition.FileName.Value == "File")
                    {

                        var fileName = Path.GetRandomFileName();
                        // var saveToPath = Path.Combine(Path.GetTempPath(), fileName);
                        var saveToPath = Path.Combine(@"C:\Users\82316\Desktop", fileName);


                        using (var targetStream = System.IO.File.Create(saveToPath))
                        {
                            await section.Body.CopyToAsync(targetStream);
                        }
                    }

                    if (!string.IsNullOrEmpty(contentDisposition.Name.Value) && contentDisposition.Name.Value == "FileName")
                    {
                        using (var stream = new MemoryStream())
                        {
                            section.Body.CopyTo(stream);
                            var array = new byte[stream.Length];
                            stream.Seek(0, SeekOrigin.Begin);
                            stream.Read(array, 0, (int)stream.Length);
                        }
                    }
                }

                section = await reader.ReadNextSectionAsync();
            }

            return (new MemoryStream(), "");
        }

    }
}