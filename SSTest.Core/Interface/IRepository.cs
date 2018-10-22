using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTest.Core.Interface
{
   public interface IRepository<TEntity> where TEntity : class
    {

        IList<TEntity> GetAll();
        TEntity Find(object id);
        Task<TEntity> FindAsync(object id);
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void Delete(object id);
        Task DeleteAsync(object id);
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
        void Dispose();

    }
}
