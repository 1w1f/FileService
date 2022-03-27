using FileServiceRepsitory.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FileServiceRepsitory.Repository
{
    public class BaseRepository<T,TDbContext> :IBaseRepository<T> where T : class, new() where TDbContext: DbContext
    {
        protected  TDbContext DbContext{get;set;}

        public BaseRepository(TDbContext dbContext)
        {
            this.DbContext=dbContext;
        }

        public async Task<T> Create(T t)
        {
            var result=await DbContext.AddAsync(t);
            DbContext.SaveChanges();
            return result.Entity;
        }


        

        // public Task<bool> Delete(int id)
        // {
        //     throw new NotImplementedException();
        // }

        // public Task<bool> Edit(T t)
        // {
        //     throw new NotImplementedException();
        // }

        public async virtual Task<List<T>> FindAllAsync(T t)
        {
            return await DbContext.Set<T>().ToListAsync();
        }

        // public Task<T> FindByIdAsync(int id)
        // {
        //     throw new NotImplementedException();
        // }
    }
}