using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AQShop.Data.Infrastruture
{
    public abstract class RepositoryBase<T>: IRepository<T> where T : class
    {
        #region Properties
        private AQShopDbContext dbContext;
        private readonly IDbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        public AQShopDbContext DbContext
        {
            get { return dbContext ?? (dbContext = DbFactory.Init()); }
        }

        #endregion

        public RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }

        #region Implement
        //Marks an entity as new
       public virtual  T Add(T entity)
        {
           return dbSet.Add(entity);
        }

        //Marks an entity as modified
        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
             dbContext.Entry(entity).State = EntityState.Modified;                
        }

                

        //Marks an entity to be removed
        public virtual T Delete(T entity)
        {
           return  dbSet.Remove(entity);
        }

        //Marks an entity to be removed
        public virtual T Delete(int id)
        {
            var entity = dbSet.Find(id);
            dbSet.Remove(entity);
            return entity;
        }

        //Delete multi record
        public virtual void DeleteMulti(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> Objects= dbSet.Where<T>(where).AsEnumerable();
            foreach(T obj in Objects)
            {
                dbSet.Remove(obj);
            }
        }

        //Get an entity by int id 
       public virtual T GetSingleById(int id)
        {
            return dbSet.Find(id);
        }

       public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where, string[] includes = null)
        {
            return dbSet.Where(where).ToList();
        }

        public virtual IEnumerable<T> GetAll(string[] includes = null)
        {
            //HANDE INCLUDES FOR ASSOICIATED OBJECT IF APPLICABLE
            if(includes != null && includes.Any())
            {
                var query = dbContext.Set<T>().Include(includes.First());
                 foreach(var include in includes.Skip(1))
                {
                    query = query.Include(include);
                }
                return query.AsEnumerable();
            }
            return dbContext.Set<T>().AsEnumerable();
            
        }

        public T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            if (includes != null && includes.Any())
            {
                var query = dbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                {
                    query = query.Include(include);
                }
                return query.FirstOrDefault(expression);
            }
            return dbContext.Set<T>().FirstOrDefault();
        }

        public virtual IEnumerable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            //HANDE INCLUDES FOR ASSOICIATED OBJECT IF APPLICABLE
            if (includes != null && includes.Any())
            {
                var query = dbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                {
                    query = query.Include(include);
                }
                return query.Where<T>(predicate).AsEnumerable();
            }
            return dbContext.Set<T>().Where(predicate).AsEnumerable<T>();
        }

        public virtual IEnumerable<T> GetMultiPaging(Expression<Func<T, bool>> predicate, out int total, int index = 0, int size = 50, string[] includes = null)
        {
            int skipCount = index * size;
            IEnumerable<T> _resetSet;
            if(includes != null && includes.Any())
            {
                var query = dbContext.Set<T>().Include(includes.First());
                foreach(var include in includes.Skip(1))
                {
                    query = query.Include(include);
                  
                }
                _resetSet = predicate != null ? query.Where<T>(predicate).AsEnumerable() : query.AsEnumerable();
            }
            else
            {
                _resetSet = predicate != null ? dbContext.Set<T>().Where<T>(predicate).AsEnumerable<T>(): dbContext.Set<T>().AsEnumerable();
            }

            _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsEnumerable();

        }

        public virtual int Count(Expression<Func<T, bool>> where)
        {
            return dbSet.Count(where);
        }

        public virtual bool CheckContains(Expression<Func<T, bool>> predicate)
        {
            return dbContext.Set<T>().Count<T>(predicate) > 0;
        }
        #endregion

    }
}
