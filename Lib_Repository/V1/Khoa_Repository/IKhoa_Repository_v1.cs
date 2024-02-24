using App_Models.Models_Table_CSDL;
using Lib_Models.Model_Update.Khoa;
using Lib_Models.Models_Insert.v1;
using Lib_Models.Models_Select.Khoa;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.Khoa_Repository
{
    public interface IKhoa_Repository_v1
    {
        Task<List<KhoaSchool_Select_v1>> SelectAll(int id_School);
        Task<Status_Application> InsertAysnc(tbKhoaSchool khoaSchool);
        Task<Status_Application> UpdateAysnc(KhoaSchool_Update_v1 khoaSchool);
    }
}
