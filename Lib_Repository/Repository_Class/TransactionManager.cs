using Microsoft.EntityFrameworkCore.Storage;
using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using System;

namespace Lib_Repository.Repository_Class
{
    public class TransactionManager : IDisposable
    {
        private readonly Trendyt_DbContext _dbContext;
        private bool disposed = false;
        private IDbContextTransaction _dbContextTransaction;

        public TransactionManager(Trendyt_DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContextTransaction = _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _dbContextTransaction.Commit();
        }

        public void Rollback()
        {
            _dbContextTransaction.Rollback();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    
                    _dbContext.Dispose();
                }
                disposed = true;
            }
        }
    }
}
