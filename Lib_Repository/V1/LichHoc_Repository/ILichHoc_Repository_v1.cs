using App_Models.Models_Table_CSDL;
using Lib_Models.Models_Insert.v1.MonHoc;
using Lib_Models.Models_Select.LichHoc;
using Lib_Models.Models_Table_Entity;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.LichHoc_Repository
{
    public interface ILichHoc_Repository_v1
    {
        Task<List<LichHoc_Select_All_v1>> SelectAll(tbMenberSchool menberSchool);
        Task<Status_Application> Insert(LichHoc_Insert_v1 lichHoc);
    }
}
