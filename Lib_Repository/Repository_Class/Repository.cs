using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using Lib_Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.Repository_Class
{
    /// <summary>
    /// _db: Database connection
    /// Table: Trả về 1 đối tượng T
    /// GetAll: Lấy tất cả dữ liệu
    /// GetAllIncluding: Lấy dữ liệu có đầu vào 1 biểu thức lambda
    /// GetById: Lấy dữ liệu với điều kiện có id
    /// Insert(T entity): Thêm 1 dữ liệu
    /// Insert(IEnumerable<T> entities): Thêm 1 list dữ liệu
    /// Update: Cập nhật lại dữ liệu
    /// Delete(T entity): Xóa 1 dữ liệu
    /// Delete(Expression<Func<T, bool>> expression): Cập nhật dữ liệu có điều kiện
    /// Commit: Lưu các thay đổi vào database
    /// </summary>
    /// <typeparam name="T">Đối tượng đầu vào</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly Trendyt_DbContext _db;

        public Repository(Trendyt_DbContext db)
        {
            _db = db;
        }

        public virtual IQueryable<T> Table => _db.Set<T>();

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> expression = null!)
        {
            if (expression == null)
            {
                return await _db.Set<T>().ToListAsync();
            }
            return await _db.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllIncluding(Expression<Func<T, bool>> expression = null!, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _db.Set<T>();

            if (expression != null)
            {
                query = query.Where(expression);
            }

            // Bổ sung các bảng liên quan vào truy vấn
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetById(object id)
        {
            return await _db.Set<T>().FindAsync(id) ?? null!;
        }

        public async Task Insert(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
        }

        public async Task Insert(IEnumerable<T> entities)
        {
            await _db.Set<T>().AddRangeAsync(entities);
        }
        
        public void Update(T entity)
        {
            EntityEntry entityEntry = _db.Entry<T>(entity);
            entityEntry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void Delete(T entity)
        {
            EntityEntry entityEntry = _db.Entry<T>(entity);
        }

        public void Delete(Expression<Func<T, bool>> expression)
        {
            var entities = _db.Set<T>().Where(expression).ToList();
            if (entities.Count > 0)
            {
                _db.Set<T>().RemoveRange(entities);
            }
        }

        public async Task Commit()
        {
            await _db.SaveChangesAsync();
        }
    }
}
