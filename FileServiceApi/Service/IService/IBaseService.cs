using FileServiceRepsitory.IRepository;

namespace FileService.Service.IService;

public interface IBaseService<T,T1> where T : class, new() where T1:IBaseRepository<T>
{

    Task<List<T>> FindAllAsync();
    Task<T> FindByIdAsync(int id);

    Task<T> Create(T t);

    Task<bool> Delete(int id);

    Task<bool> Edit(T t);

}
