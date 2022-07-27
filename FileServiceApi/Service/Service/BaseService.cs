using DataModel;
using FileService.Service.IService;
using FileServiceRepsitory.IRepository;
using FileServiceRepsitory.Repository;

namespace FileService.Service.Service;

public class BaseService<T, T1> : IBaseService<T, T1> where T : ModelId where T1 : IBaseRepository<T>
{
    protected T1 Repository { get; set; }

    public BaseService(T1 repository)
    {
        Repository = repository;
    }
    public virtual Task<T> CreateAsync(T t)
    {
        return Repository.CreateAsync(t);
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public virtual Task<bool> EditAsync(T t)
    {
        throw new NotImplementedException();
    }

    public virtual Task<List<T>> FindAllAsync()
    {
        return Repository.FindAllAsync();
    }

    public Task<T> FindByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
