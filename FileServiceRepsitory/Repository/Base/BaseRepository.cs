using DataModel;
using FileServiceRepsitory.Repository.DbContextModel;
using Microsoft.EntityFrameworkCore;

namespace FileServiceRepsitory.Repository.Base;

public class BaseRepository<T> : IBaseRepository<T>
    where T : ModelId
{
    protected FileServiceDbContext DbContext { get; set; }

    public BaseRepository(FileServiceDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<T> CreateAsync(T t)
    {
        var result = await DbContext.AddAsync(t);
        DbContext.SaveChanges();
        return result.Entity;
    }

    public async Task<bool> Delete(Guid id)
    {
        var result = await DbContext.Set<T>().FirstOrDefaultAsync(item => item.Id == id);
        if (result != default)
        {
            DbContext.Remove(result);
            if (DbContext.SaveChanges() > 0)
            {
                return true;
            }
        }
        return false;
    }

    public async Task<bool> Update(T t)
    {
        DbContext.Update(t);
        if (await DbContext.SaveChangesAsync() > 0)
        {
            return true;
        }
        return false;
    }

    public async virtual Task<List<T>> FindAllAsync()
    {
        return await DbContext.Set<T>().ToListAsync();
    }

    public async Task<T> FindByIdAsync(Guid id)
    {
        var result = await DbContext.Set<T>().Where(item => item.Id == id).ToListAsync();
        return result.FirstOrDefault();
    }
}
