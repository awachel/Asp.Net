using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repos
{
    public class PersonRepository : IDisposable, IPersonRepository
    {

        private readonly DbContext context;
        private readonly DbSet<person> dbSet = null;

        public PersonRepository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<person>();
        }

        public async Task<person> GetAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<List<person>> GetListAsync()
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<int> AddAsync(person item)
        {
            try
            {
                await dbSet.AddAsync(item);
                return item.Id;
            }
            catch (Exception ex)
            {

                return -1;
            }
        }

        public async Task<bool> UpdateAsync(person item)
        {
            try
            {
                await Task.Run(() => dbSet.Update(item));
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await GetAsync(id);
            if (item != null)
            {
                dbSet.Remove(item);
                return true;
            }
            return false;
        }

        public IQueryable<person> FindBy(Expression<Func<person, bool>> predicate)
        {
            return dbSet.Where(predicate).AsQueryable();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }

}
