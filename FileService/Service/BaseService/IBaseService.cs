using DataModel;
using FileServiceRepsitory.Repository.Base;

namespace FileService.Service.BaseService;

public interface IBaseService<T, T1> where T : ModelId where T1 : IBaseRepository<T>
{

    Task<List<T>> FindAllAsync();
    Task<T> FindByIdAsync(int id);

    Task<T> CreateAsync(T t);

    Task<bool> DeleteAsync(int id);

    Task<bool> EditAsync(T t);

}
