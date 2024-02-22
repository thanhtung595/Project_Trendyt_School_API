using App_Models.Models_Table_CSDL;
using Azure.Core;
using Lib_Models.Models_Table_Class.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.Token_Service
{
    public interface IToken_Service_v1
    {
        public Task CreateToken(int id_Account);
        public Task<TokenModel> RefeshToken(int id_Account);
        public Task<Token_Refesh_Model> RefeshToken(string access_Token);
        public Task DeleteToken();
        public Task<string> GetAccessTokenAccount();
        public Task<string> GetRefeshTokenAccount();
        public Task<int> GetAccessTokenIdAccount();
        public Task<tbMenberSchool> Get_Menber_Token();
        public Task<int> GetRefeshTokenIdAccount();
    }
}
