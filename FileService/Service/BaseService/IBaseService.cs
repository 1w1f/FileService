using DataModel;
using FileServiceRepsitory.Repository.Base;

namespace FileService.Service.BaseService;

public interface IBaseService<T, TRepository> where T : ModelId where TRepository : IBaseRepository<T>
{
    Task<List<T>> FindAllAsync();
    Task<T> FindByIdAsync(Guid id);

    Task<T> CreateAsync(T t);

    Task<bool> DeleteAsync(Guid id);

    Task<bool> UpdateAsync(T t);

}