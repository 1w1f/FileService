namespace FileService.Service.IService;

public interface IBaseService<T> where T : class, new()
{

    Task<List<T>> FindAllAsync();
    Task<T> FindByIdAsync(int id);

    Task<T> Create(T t);

    Task<bool> Delete(int id);

    Task<bool> Edit(T t);

}
