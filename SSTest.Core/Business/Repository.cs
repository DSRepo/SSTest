using SSTest.Core.Interface;
using SSTest.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SSTest.Core.Business
{
   public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private ProductsEntities db = null;
        private DbSet<TEntity> table = null;
        public Repository()
        {
            this.db = new ProductsEntities();
            this.db.Database.CommandTimeout = 216000;//Set command time out to 60 min.
            table = db.Set<TEntity>();
           
        }
        public Repository(ProductsEntities db)
        {
            this.db = db;
            table = db.Set<TEntity>();
        }

        public IList<TEntity> GetAll()
        {

            return table.ToList();
        }
        public TEntity Find(object id)
        {
            return table.Find(id);
        }

        public async Task<TEntity> FindAsync(object id)
        {
            return await table.FindAsync(id);
        }

        public void Add(TEntity obj)
        {
            table.Add(obj);
            db.SaveChanges();
        }

        public async Task AddAsync(TEntity obj)
        {
            table.Add(obj);
            await db.SaveChangesAsync();
        }
        public void Update(TEntity obj)
        {
                table.Attach(obj);
                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
           
        }

        public async Task UpdateAsync(TEntity obj)
        {
            table.Attach(obj);
            db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();

        }
        public void Delete(object id)
        {
            TEntity existing = table.Find(id);
            table.Remove(existing);
            db.SaveChanges();
        }

        public async Task DeleteAsync(object id)
        {
            TEntity existing = await table.FindAsync(id);
            table.Remove(existing);
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            db.Dispose();
        }

    }
}
