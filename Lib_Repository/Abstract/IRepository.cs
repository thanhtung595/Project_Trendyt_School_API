using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.Abstract
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> expression = null!);
        Task<IEnumerable<T>> GetAllIncluding(Expression<Func<T, bool>> expression = null!, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetById(object id);
        Task Insert(T entity);
        Task Insert(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> expression);
        Task Commit();
    }
}
