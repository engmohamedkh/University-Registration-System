using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using University.Core.Helpers.Enums;


namespace University.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetById(Guid id);

        public  Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes);

        Task<T> Find(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<TResult>> FindAll<TResult>(Expression<Func<T, bool>> criteria, Expression<Func<T, TResult>> projection, string[] includes = null);
        Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> criteria, int skip, int take);

        Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> criteria, int? skip, int? take,
            Expression<Func<T, object>> orderBy = null, OrderBy order = OrderBy.Ascending);

        Task<T> Add(T entity);

        T Update(T entity);

        void Delete(T entity);

        Task<int> CountAsync();

        Task<int> CountAsync(Expression<Func<T, bool>> criteria);
    }
}
