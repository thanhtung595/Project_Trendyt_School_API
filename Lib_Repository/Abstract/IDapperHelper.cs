using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.Abstract_DapperHelper
{
    public interface IDapperHelper
    {
        /// <summary>
        /// ExcuteNotReturn: Đây là một phương thức để thực thi một truy vấn không trả về kết quả (như INSERT, UPDATE, DELETE) trên cơ sở dữ liệu.
        /// ExecuteReturn: Đây là một phương thức để thực thi một truy vấn trả về một giá trị duy nhất từ cơ sở dữ liệu.
        /// ExcuteSqlReturnList: Đây là một phương thức để thực thi một truy vấn SQL và trả về một danh sách các đối tượng có kiểu T.
        /// ExcuteStoreProcedureReturnList: Đây là một phương thức để thực thi một thủ tục lưu trữ và trả về một danh sách các đối tượng có kiểu T.
        /// </summary>
        /// <param name="query">Đây là câu truy vấn SQL hoặc tên của một thủ tục lưu trữ (stored procedure) cần thực thi.</param>
        /// <param name="parameters">Đây là các tham số được truyền vào câu truy vấn. Có thể là null nếu truy vấn không yêu cầu tham số.</param>
        /// <param name="dbTransaction">Đây là một giao dịch cơ sở dữ liệu được sử dụng để nhóm các thao tác cơ sở dữ liệu lại với nhau.</param>
        /// <returns></returns>
        Task ExcuteNotReturn(string query, DynamicParameters parameters = null!, IDbTransaction dbTransaction = null!);
        Task<T> ExecuteReturn<T>(string query, DynamicParameters parameters = null!, IDbTransaction dbTransaction = null!);
        Task<IEnumerable<T>> ExcuteSqlReturnList<T>(string query, DynamicParameters parameters = null!, IDbTransaction dbTransaction = null!);
        Task<IEnumerable<T>> ExcuteStoreProcedureReturnList<T>(string query, DynamicParameters parameters = null!, IDbTransaction dbTransaction = null!);
    }
}
