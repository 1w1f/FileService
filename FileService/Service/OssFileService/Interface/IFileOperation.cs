using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileServiceApi.Service.OssFileService.Interface
{
    public interface IFileOperation
    {
        Task UploadFormFileAsync(IFormFile file);
        Task GetFileInfoAsync(string fileName);
        // void UploadStreamFile(Stream file);
        // void DelectFile();
    }

    public interface IBucketOperation
    {

        void CreateBucket(string bucketName);
        void BucketIsExist(string buckName);
        bool DelectBucket(string buckName);
    }
}