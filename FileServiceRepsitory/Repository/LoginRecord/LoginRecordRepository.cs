using DataModel.User;
using FileServiceRepsitory.Repository.DbContextModel;
using FileServiceRepsitory.Repository.LoginRecord.IRepository;

namespace FileServiceRepsitory.Repository.LoginRecord
{
    public class LoginRecordRepository : BaseRepository<LoginRecordDto, FileServiceDbContext>,ILoginRecordRepository
    {
        public LoginRecordRepository(FileServiceDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}