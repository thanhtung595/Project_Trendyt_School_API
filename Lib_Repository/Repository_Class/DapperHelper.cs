using Dapper;
using Lib_Repository.Abstract_DapperHelper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.Repository_Class
{
    public class DapperHelper : IDapperHelper
    {
        private readonly string connectionString = string.Empty;
        private IDbConnection _dbConnection;
        public DapperHelper(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DataBase_Trendyt_School")!;
            _dbConnection = new SqlConnection(connectionString);
        }
        public async Task ExcuteNotReturn(string query, DynamicParameters parameters = null!, IDbTransaction dbTransaction = null!)
        {
            using (var dbConnection = _dbConnection)
            {
                await dbConnection.ExecuteAsync(query, parameters, dbTransaction, commandType: CommandType.Text);
            }
        }
        public async Task<T> ExecuteReturn<T>(string query, DynamicParameters parameters = null!, IDbTransaction dbTransaction = null!)
        {
            using (var dbConnection = _dbConnection)
            {
                var result = await dbConnection.ExecuteScalarAsync<T>(query, parameters, dbTransaction, commandType: System.Data.CommandType.Text);
                return (T)Convert.ChangeType(result, typeof(T))!;
            }
        }

        public async Task<IEnumerable<T>> ExcuteSqlReturnList<T>(string query, DynamicParameters parameters = null!, IDbTransaction dbTransaction = null!)
        {
            using (var dbConnection = _dbConnection)
            {
                return await dbConnection.QueryAsync<T>(query, parameters, dbTransaction, commandType: System.Data.CommandType.Text);
            }
        }

        public async Task<IEnumerable<T>> ExcuteStoreProcedureReturnList<T>(string query, DynamicParameters parameters = null!, IDbTransaction dbTransaction = null!)
        {
            using (var dbConnection = _dbConnection)
            {
                return await dbConnection.QueryAsync<T>(query, parameters, dbTransaction, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
