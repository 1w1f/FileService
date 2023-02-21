using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModel.File;
using FileServiceRepsitory.Repository.Base;

namespace FileServiceRepsitory.Repository.File
{
    public interface IFileRepository : IBaseRepository<FileEntity>
    {

    }
}