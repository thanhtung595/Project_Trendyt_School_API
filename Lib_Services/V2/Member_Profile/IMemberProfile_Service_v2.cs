using Lib_Models.Model_Update.Member;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V2.Member_Profile
{
    public interface IMemberProfile_Service_v2
    {
        Task<Status_Application> UpdateAsync(UpdateMember_Profile request);
    }
}
