using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using Lib_Models.Models_Select.Student;
using Lib_Repository.V1.Student_Repository;
using Lib_Services.Token_Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.Student_Service
{
    public class Student_Service_v1 : IStudent_Service_v1
    {
        private readonly IStudent_Repository_v1 _student_Repository_V1;
        private readonly IToken_Service_v1 _token_Service_v1;
        private readonly IToken_Service_v2 _token_Service_v2;
        private readonly Trendyt_DbContext _db;
        public Student_Service_v1(IStudent_Repository_v1 student_Repository_V1 , IToken_Service_v1 token_Service_V1,
            Trendyt_DbContext db, IToken_Service_v2 token_Service_v2)
        {
            _student_Repository_V1 = student_Repository_V1;
            _token_Service_v1 = token_Service_V1;
            _db = db;
            _token_Service_v2 = token_Service_v2;
        }

        # region SelectAllAsync
        public async Task<List<Student_Select_v1>> SelectAllAsync()
        {
            var menberManager = await _token_Service_v2.Get_Menber_Token();

            return await _student_Repository_V1.SelectAllAsync(menberManager!);
        }
        #endregion
    }
}
