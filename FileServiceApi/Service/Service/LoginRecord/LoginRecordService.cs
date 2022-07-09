using DataModel.User;
using FileService.Service.Service;
using FileServiceApi.Service.Service.LoginRecord.IService;
using FileServiceRepsitory.Repository.LoginRecord;
using FileServiceRepsitory.Repository.LoginRecord.IRepository;

namespace FileServiceApi.Service.Service.LoginRecord
{
    public class LoginRecordService : BaseService<LoginRecordDto, ILoginRecordRepository>, ILoginRecordService
    {
        public LoginRecordService(ILoginRecordRepository repository) : base(repository)
        {
        }
    }
}