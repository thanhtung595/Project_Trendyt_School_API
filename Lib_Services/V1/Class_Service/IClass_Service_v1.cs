﻿using Lib_Models.Model_Update.Class;
using Lib_Models.Models_Select.Class;
using Lib_Models.Models_Select.MonHoc;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.Class_Service
{
    public interface IClass_Service_v1
    {
        Task<List<Class_Select_v1>> SelectAll();
        Task<Class_Select_v1> SelectById(int id);
        Task<Status_Application> InsertAsync(string name_ClassSchool);
        Task<Status_Application> UpdateAsync(Class_Update_v1 request);
        Task<MonHoc_Member_Select> GetMember(int idMonHoc);
    }
}
