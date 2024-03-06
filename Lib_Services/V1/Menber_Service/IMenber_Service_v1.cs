using Lib_Models.Model_Update.School;
using Lib_Models.Models_Insert.v1;
using Lib_Models.Models_Select.Menber;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.Menber_Service
{
    public interface IMenber_Service_v1
    {
        Task<List<Menber_SclectAll_v1>> SelectAllAsync();
        Task<Status_Application> InsertAsync(MenberSchool_Insert_v1 request);
        Task<Status_Application> SchoolMenberUpdateAsync(School_Menber_Update_v1 request);
        Task<Member_Profile_v1> Profile();
        Task<Status_Application> Delete(int id_member);

    }
}
