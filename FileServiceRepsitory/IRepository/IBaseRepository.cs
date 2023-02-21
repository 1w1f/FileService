using DataModel;

namespace FileServiceRepsitory.IRepository;

public interface IBaseRepository<T> where T : ModelId
{

    Task<T> CreateAsync(T t);
    Task<List<T>> FindAllAsync();

    Task<T> FindByIdAsync(Guid id);

    Task<bool> Delete(Guid id);

    Task<bool> Edit(T t);

}
