using DataModel.User;
using FileService.Service.BaseService;
using FileService.Service.IService;
using FileService.Service.Service;
using FileServiceRepsitory.Repository.LoginRecord;
using FileServiceRepsitory.Repository.LoginRecord.IRepository;

namespace FileServiceApi.Service.Service.LoginRecord.IService
{
    public interface ILoginRecordService : IBaseService<LoginRecordDto, ILoginRecordRepository>
    {

    }
}