﻿using Lib_Models.Model_Update.DiemDanh;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V2.DiemDanh
{
    public interface IDiemDanh_Service_v2
    {
        Task<Status_Application> InsertAsync(List<int> idLichHoc, List<int> idStudent);
        Task<Status_Application> UpdateAsync(LopDiemDanh_Update_v1 request);
    }
}