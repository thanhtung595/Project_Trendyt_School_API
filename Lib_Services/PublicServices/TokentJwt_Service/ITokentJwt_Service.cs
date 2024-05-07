using Lib_Models.Models_Table_Class.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.PublicServices.TokentJwt_Service
{
    public interface ITokentJwt_Service
    {
        Task<TokenModel> CreateToken(int id_Account, string name_Role);
        Task LogoutToken();
        Task<TokenModel> RefeshToken();
    }
}
