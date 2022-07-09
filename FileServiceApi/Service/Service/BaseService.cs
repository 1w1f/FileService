using FileService.Service.IService;
using FileServiceRepsitory.IRepository;
using FileServiceRepsitory.Repository;

namespace FileService.Service.Service;

public class BaseService<T,T1> : IBaseService<T,T1> where T : class, new() where T1:IBaseRepository<T>
{
    protected T1 Repository { get; set; }

    public BaseService(T1 repository)
    {
        Repository = repository;
    }
    public virtual Task<T> Create(T t)
    {
        return Repository.Create(t);
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Edit(T t)
    {
        throw new NotImplementedException();
    }

    public virtual Task<List<T>> FindAllAsync()
    {
        return  Repository.FindAllAsync();
    }

    public Task<T> FindByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
