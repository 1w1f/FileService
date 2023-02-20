using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataModel.File
{
    public class OssFileInfo
    {
        public OssFileInfo(string fileName, long fileSize, DateTime lastModifiy)
        {
            this.FileName = fileName;
            this.FileSize = fileSize;
            this.LastModifiy = lastModifiy;
        }
        /// <summary>
        /// oss中的文件名
        /// </summary>
        /// <value></value>
        public string FileName { get; private set; }
        public long FileSize { get; private set; }
        public DateTime LastModifiy { get; private set; }
    }
}