using System.Text;
using DataModel.File;
using FileService.Option;
using FileServiceApi.Service.OssFileService.Interface;
using Microsoft.Extensions.Options;
using Minio;


namespace FileServiceApi.Service.OssFileService.Operaction
{
    public class MinioOperation : IFileOperation//, IBucketOperation
    {

        private readonly MinioClient _minioClient;
        private readonly IOptions<MinioSetUp> _minioOption;
        private readonly ILogger<MinioOperation> _logger;

        public MinioOperation(MinioClient minioClient, IOptions<MinioSetUp> option, ILogger<MinioOperation> logger)
        {
            _minioClient = minioClient;
            _minioOption = option;
            _logger = logger;
        }

        public async Task<OssFileInfo> GetFileInfoAsync(string fileName)
        {
            var args = new StatObjectArgs().WithBucket(_minioOption.Value.Bucket).WithObject(fileName);
            var result = await _minioClient.StatObjectAsync(args);
            return new OssFileInfo(_minioOption.Value.Bucket, result.ObjectName, result.Size, result.LastModified);
        }

        public async Task<string> UploadFormFileAsync(IFormFile file)
        {
            var fileNameWithSuffix = file.FileName;
            using var Stream = file.OpenReadStream();
            var newName = GenerateFileName(fileNameWithSuffix);
            var putArgs = new PutObjectArgs().WithBucket(_minioOption.Value.Bucket).WithObject(newName).WithStreamData(Stream).WithObjectSize(Stream.Length);
            await _minioClient.PutObjectAsync(putArgs);
            return newName;
        }



        private static string GenerateFileName(string fileNameWithSuffix)
        {
            var fileName = Path.GetFileNameWithoutExtension(fileNameWithSuffix);
            return new StringBuilder(Guid.NewGuid().ToString()).Append('|').Append(fileName).ToString();
        }
    }
}