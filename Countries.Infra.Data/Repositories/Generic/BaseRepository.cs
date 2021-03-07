using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Countries.Infra.Data.Repositories.Generic
{
    public interface IBaseRepository<T> where T : class
    {
        #region Métodos Get 
        IEnumerable<T> Get(Expression<Func<T, bool>> whereCondition = null,
                           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                           string includeProperties = "");

        //T GetById(int id);
        //Task<T> GetByIdAsync(int id);
        #endregion Métodos Get

        //#region Métodos Create
        //T Create(T entity);
        //Task<T> CreateAsync(T entity);
        //#endregion Métodos Create

        //#region Métodos Update
        //T Update(T entity, object key);
        //Task<T> UpdateAsync(T entity, object key);
        //#endregion Métodos Update

        //#region Métodos Delete
        //void Delete(T entity);
        //Task<int> DeleteAsync(T entity);
        //#endregion Métodos Delete

        //// Métodos para buscar objetos
        //T Find(Expression<Func<T, bool>> match);
        //ICollection<T> FindAll(Expression<Func<T, bool>> match);
        //Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);
        //Task<T> FindAsync(Expression<Func<T, bool>> match);
        //IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        //Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate);

        // Métodos para hacer Count de listas de objetos
        //int Count();
        //Task<int> CountAsync();
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
    }
}
