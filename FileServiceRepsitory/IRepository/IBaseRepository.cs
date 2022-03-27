namespace FileServiceRepsitory.IRepository;

public interface IBaseRepository<T> where T:class,new()
{
    
    Task<T> Create(T t);
    Task<List<T>> FindAllAsync(T t);
    // Task<T> FindByIdAsync(int id);


    // Task<bool> Delete(int id);

    // Task<bool> Edit(T t);

}
