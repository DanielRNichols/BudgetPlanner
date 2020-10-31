using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IList<T>> GetAll();
        Task<T> GetById(int id);
        Task<bool> Exists(int id);
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);

        Task<bool> Save();

    }
}
