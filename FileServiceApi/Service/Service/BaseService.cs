using FileService.Service.IService;
using FileServiceRepsitory.IRepository;
using FileServiceRepsitory.Repository;

namespace FileService.Service.Service;

public class BaseService<T> : IBaseService<T> where T:class,new()
{
    protected IBaseRepository<T> BaseRepository{get;set;}
    
    public BaseService(IBaseRepository<T> baseRepository)
    {
        BaseRepository=baseRepository;
    }
    public Task<T> Create(T t)
    {
       return BaseRepository.Create(t);
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
        throw new NotImplementedException();
    }

    public Task<T> FindByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
