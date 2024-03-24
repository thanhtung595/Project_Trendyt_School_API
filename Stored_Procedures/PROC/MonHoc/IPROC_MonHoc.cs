using Lib_Models.Models_Select.MonHoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stored_Procedures.PROC.MonHoc
{
    public interface IPROC_MonHoc
    {
        public bool CreateProc_GetAllMonHocPROC();
        Task<List<MonHoc_SelectAll_v1>> GetAllMonHocPROC(int idSchool);
    }
}
