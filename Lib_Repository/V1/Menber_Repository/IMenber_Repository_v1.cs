using App_Models.Models_Table_CSDL;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.Menber_Repository
{
    public interface IMenber_Repository_v1
    {
        Task<Status_Application> InsertAsync(tbMenberSchool menberSchool);
    }
}
