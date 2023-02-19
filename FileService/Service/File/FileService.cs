using DataModel.File;
using FileServiceApi.Service.File;
using FileServiceApi.Service.OssFileService.Interface;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace FileService.Service.Service.File;

public class FileStoreService : IFileStoreService
{
    private readonly IFileOperation fileOperation;
    public FileStoreService(IFileOperation fileOperation)
    {
        this.fileOperation = fileOperation;
    }
    public async Task<(Stream largeFileStream, string fileName)> GetFileInfoFromRequest(HttpRequest request, MediaTypeHeaderValue mediaTypeHeader)
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

    public async Task<OssFileInfo> SaveFileToOssServiceAsync(IFormFile file)
    {
        var ossFileName = await fileOperation.UploadFormFileAsync(file);
        if (ossFileName is not null)
        {
            // 获取上传后的文件
            var ossFileInfo = await fileOperation.GetFileInfoAsync(ossFileName);
            return ossFileInfo;
        }
        //没有获取到保存到Oss中文件的文件信息 一般可能是文件上传失败
        return null;
    }
}