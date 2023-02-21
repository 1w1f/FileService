using DataModel;
using FileServiceRepsitory.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FileServiceRepsitory.Repository
{
    public class BaseRepository<T, TDbContext> : IBaseRepository<T>
        where T : ModelId
        where TDbContext : DbContext
    {
        protected TDbContext DbContext { get; set; }

        public BaseRepository(TDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public async Task<T> CreateAsync(T t)
        {
            var result = await DbContext.AddAsync(t);
            DbContext.SaveChanges();
            return result.Entity;
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Edit(T t)
        {
            throw new NotImplementedException();
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
}
