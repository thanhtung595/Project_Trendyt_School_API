using App_Models.Models_Table_CSDL;
using AutoMapper;
using Lib_Models.Models_Select.Role;
using Lib_Models.Models_Select.TypeAccount;
using Lib_Models.Models_Table_Class.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Helpers.AutoMapper
{
    public class AutoMapperV1 : Profile
    {
        public AutoMapperV1()
        {
            CreateMap<tbRole, Role_Select_v1>().ReverseMap();
            CreateMap<tbRoleSchool, Role_Select_v1>().ReverseMap();
            CreateMap<tbTypeAccount, TypeAccount_Select_v1>().ReverseMap();
            CreateMap<tbToken, TokenModel>().ReverseMap();
        }
    }
}
