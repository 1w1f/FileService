using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModel.File;

namespace FileServiceApi.Service.OssFileService.Interface
{
    public interface IFileOperation
    {
        Task<string> UploadFormFileAsync(IFormFile file);
        Task<OssFileInfo> GetFileInfoAsync(string fileName);
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