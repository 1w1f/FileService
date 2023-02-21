using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModel.File;
using FileServiceRepsitory.IRepository;
using FileServiceRepsitory.Repository.Base;
using FileServiceRepsitory.Repository.DbContextModel;

namespace FileServiceRepsitory.Repository.File
{
    public class FileRepository : BaseRepository<FileEntity, FileServiceDbContext>, IFileRepository
    {
        public FileRepository(FileServiceDbContext dbContext) : base(dbContext)
        {
        }
    }
}