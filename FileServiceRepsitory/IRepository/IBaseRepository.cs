using DataModel;

namespace FileServiceRepsitory.IRepository;

public interface IBaseRepository<T> where T : ModelId
{

    Task<T> CreateAsync(T t);
    Task<List<T>> FindAllAsync();

    Task<T> FindByIdAsync(int id);

    Task<bool> Delete(int id);

    Task<bool> Edit(T t);

}
