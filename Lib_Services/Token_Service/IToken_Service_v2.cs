using App_Models.Models_Table_CSDL;
using Lib_Models.Models_Table_Class.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.Token_Service
{
    public interface IToken_Service_v2
    {
        public Task<TokenModel> CreateToken(int id_Account, string name_Role, string hostName);
        public Task<Token_Refesh_Model> RefeshToken();
        public Task LogoutToken();
        public Task<int> Get_Id_Account_Token();
        public Task<tbMenberSchool> Get_Menber_Token();
    }
}
