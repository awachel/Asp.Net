using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IPersonRepository
    {
        Task<bool> DeleteAsync(int id);
        void Dispose();
        IQueryable<person> FindBy(Expression<Func<person, bool>> predicate);
        Task<person> GetAsync(int id);
        Task<List<person>> GetListAsync();
        Task<bool> UpdateAsync(person item);
    }
}