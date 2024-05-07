using App_Models.Models_Table_CSDL;
using Lib_Models.Models_Select.MonHoc;
using Lib_Models.Models_Table_Entity;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.MonHoc_Repository
{
    public interface IMonHoc_Repository_v1
    {
        Task<List<MonHoc_SelectAll_v1>> GetAll(tbMenberSchool menberManager);
        Task<Status_Application> Insert(tbMonHoc monHoc);
        Task<MonHocSelectById_v1> GetById(int idSchool, int id_MonHoc);
    }
}
