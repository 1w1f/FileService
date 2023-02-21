using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataModel.File
{
    public class OssFileInfo
    {
        public OssFileInfo(string bucket, string fileName, long fileSize, DateTime lastModifiy)
        {
            FileName = fileName;
            FileSize = fileSize;
            LastModifiy = lastModifiy;
            Bucket = bucket;
        }
        /// <summary>
        /// 存储桶名称
        /// </summary>
        /// <value></value>
        public string Bucket { get; private set; }

        /// <summary>
        /// oss中的文件名
        /// </summary>
        /// <value></value>
        public string FileName { get; private set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        /// <value></value>
        public long FileSize { get; private set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        /// <value></value>
        public DateTime LastModifiy { get; private set; }
    }
}