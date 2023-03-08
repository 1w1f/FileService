using DataModel;
using FileServiceRepsitory.Repository.Base;

namespace FileService.Service.BaseService;

public class BaseService<T, TRepostitory> : IBaseService<T, TRepostitory> where T : ModelId where TRepostitory : IBaseRepository<T>
{
    public TRepostitory Repository { get; protected set; }

    public BaseService(TRepostitory repository)
    {
        Repository = repository;
    }
    public virtual Task<T> CreateAsync(T t)
    {
        return Repository.CreateAsync(t);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        return Repository.Delete(id);
    }

    public virtual Task<bool> UpdateAsync(T t)
    {
        return Repository.Update(t);
    }

    public virtual Task<List<T>> FindAllAsync()
    {
        return Repository.FindAllAsync();
    }

    public Task<T> FindByIdAsync(Guid id)
    {
        return Repository.FindByIdAsync(id);
    }
}
