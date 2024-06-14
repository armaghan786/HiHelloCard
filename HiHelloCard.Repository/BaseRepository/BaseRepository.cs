using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using HiHelloCard.Interfaces.Repository;
using HiHelloCard.Model.Domain;
using System.Threading.Tasks;

namespace HiHelloCard.Repository.BaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        #region Properties

        private readonly IHttpContextAccessor _httpContextAccessor;

        protected readonly HihelloContext _dbContext;

        #endregion

        #region Constructor

        public BaseRepository(HihelloContext dbContext)

        {
            _dbContext = dbContext;
        }

        public BaseRepository(HihelloContext dbContext, IHttpContextAccessor httpContextAccessor)

        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Interface Methods

        public virtual async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public virtual async Task<IEnumerable<T>> GetMany(Expression<Func<T, bool>> where)
        {
            return await _dbContext.Set<T>().Where(where).ToListAsync();
        }
        public virtual async Task<IEnumerable<T>> GetManyList(Expression<Func<T, bool>> where)
        {
            return await _dbContext.Set<T>().Where(where).ToListAsync();
        }
        public virtual T FirstOrDefault(Expression<Func<T, bool>> where)
        {
            return _dbContext.Set<T>().FirstOrDefault(where);
        }
        public virtual async Task<T> GetByGUID(string guid)
        {
            return await _dbContext.Set<T>().FindAsync(guid);
        }
        public virtual async Task<IEnumerable<T>> List()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }


        public virtual async Task<IEnumerable<T>> List(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            query = query.Where(predicate);
            return await includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> List(Expression<Func<T, bool>> predicate, string orderBy, int pageSize = 15, int pageNumber = 1, params Expression<Func<T, object>>[] includes)
        {

            IQueryable<T> query = _dbContext.Set<T>();
            query = query.Where(predicate).OrderBy(orderBy).Skip((pageNumber) * pageSize).Take(pageSize);
            return await includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).ToListAsync();
        }

        public virtual async Task<int> Count(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>()
                   .Where(predicate)
                   .CountAsync();
        }

        public virtual async Task<int> CountAsync()
        {
            return await _dbContext.Set<T>().CountAsync();
        }

        public async Task<int> Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return await _dbContext.SaveChangesAsync();
        }
        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public async Task<int> TempAdd(T entity)
        {
            _dbContext.Set<T>().Add(entity);

            var result = await _dbContext.SaveChangesAsync();
            var Id = entity.GetType().GetProperty("Id").GetValue(entity, null);
            var id = (int)Id;

            return id;
        }

        public async Task<int> Update(T entity)
        {
            _dbContext.ChangeTracker.Clear();
            _dbContext.Set<T>().Update(entity);
            //_dbContext.Entry(entity).State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<int> BulkUpdate(List<T> entities)
        {
            _dbContext.BulkUpdate(entities);
            return await _dbContext.SaveChangesAsync();
        }
        public void BulkRemove(List<T> entities)
        {
            _dbContext.RemoveRange(entities);
            _dbContext.SaveChangesAsync();
        }
        public void JustBulkRemove(List<T> entities)
        {
            _dbContext.RemoveRange(entities);
        }
        public void BulkInsert(List<T> entities)
        {
            _dbContext.BulkInsert(entities);
        }

        public async Task Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public virtual async Task Delete(object id)
        {
            T entity = await _dbContext.Set<T>().FindAsync(id);
            await Delete(entity);
        }
        #endregion

        #region Interface Properties

        public string LoggedInUserIdentity
        {
            get
            {
                return _httpContextAccessor.HttpContext.User.Identity.Name;
            }
        }

        public TimeSpan UserTimezoneOffSet
        {
            get
            {
                var userTimeZoneOffsetClaim = TimeSpan.FromMinutes(0);

                if (userTimeZoneOffsetClaim == null)
                {
                    return TimeSpan.FromMinutes(0);
                }

                TimeSpan userTimeZoneOffset;
                TimeSpan.TryParse(userTimeZoneOffsetClaim.ToString(), out userTimeZoneOffset);

                return userTimeZoneOffset;
            }
        }

        #endregion

        #region Helpers


        #endregion
    }
}
