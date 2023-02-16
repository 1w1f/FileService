using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileServiceApi.Service.OssFileService.Interface;
using Microsoft.Extensions.Options;
using Minio;

namespace FileServiceApi.Service.OssFileService.Operaction
{
    public class MinioOperation : IFileOperation//, IBucketOperation
    {

        private readonly MinioClient _minioClient;
        private readonly IConfiguration _configuration;

        public MinioOperation(MinioClient minioClient, IConfiguration configuration)
        {
            _minioClient = minioClient;
            _configuration = configuration;
        }

        // public Task<object> GetFileInfo(string fileName)
        // {

        // }

        public async Task UploadFormFileAsync(IFormFile file)
        {
            var fileNameWithSuffix = file.FileName;
            using var Stream = file.OpenReadStream();
            var putArgs = new PutObjectArgs().WithBucket(_configuration["Minio:Bucket"]).WithObject(GenerateFileName(fileNameWithSuffix)).WithStreamData(Stream).WithObjectSize(Stream.Length);
            await _minioClient.PutObjectAsync(putArgs);
        }



        private static string GenerateFileName(string fileNameWithSuffix)
        {
            var fileName = Path.GetFileNameWithoutExtension(fileNameWithSuffix);
            return new StringBuilder(fileName).Append('|').Append(Guid.NewGuid()).ToString();
        }
    }
}