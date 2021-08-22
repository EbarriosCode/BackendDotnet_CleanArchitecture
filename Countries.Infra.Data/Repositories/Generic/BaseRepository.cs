using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Countries.Infra.Data.Repositories.Generic
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> Get(Expression<Func<T, bool>> whereCondition = null,
                           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                           string includeProperties = "");
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity, object key);
        Task<int> DeleteAsync(T entity);
    }
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> whereCondition = null,
                                  Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                  string includeProperties = "")
        {
            IQueryable<T> query = _unitOfWork.Context.Set<T>();

            if (whereCondition != null)
            {
                query = query.Where(whereCondition);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual async Task<T> CreateAsync(T model)
        {
            _unitOfWork.Context.Set<T>().Add(model);
            await _unitOfWork.Context.SaveChangesAsync();

            return model;
        }

        public virtual async Task<T> UpdateAsync(T model, object key)
        {
            if (model == null)
                return null;

            T exist = await _unitOfWork.Context.Set<T>().FindAsync(key);

            if (exist != null)
            {
                _unitOfWork.Context.Entry(exist).CurrentValues.SetValues(model);
                await _unitOfWork.Context.SaveChangesAsync();
            }

            return exist;
        }

        public virtual async Task<int> DeleteAsync(T model)
        {
            _unitOfWork.Context.Set<T>().Remove(model);
            return await _unitOfWork.Context.SaveChangesAsync();
        }
    }
}
