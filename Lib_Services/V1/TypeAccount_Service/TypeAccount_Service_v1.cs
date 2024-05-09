using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using AutoMapper;
using Lib_Models.Models_Insert.v1;
using Lib_Models.Models_Select.TypeAccount;
using Lib_Models.Status_Model;
using Lib_Repository.V1.TypeAccount_Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.TypeAccount_Service
{
    public class TypeAccount_Service_v1 : ITypeAccount_Service_v1
    {
        private readonly Trendyt_DbContext _db;
        private readonly ITypeAccount_Repository_v1 _typeAccount_Repository_V1;
        private readonly IMapper _mapper;
        public TypeAccount_Service_v1(Trendyt_DbContext db, ITypeAccount_Repository_v1 typeAccount_Repository_V1,
            IMapper mapper)
        {
            _db = db;
            _typeAccount_Repository_V1 = typeAccount_Repository_V1;
            _mapper = mapper;
        }
        public async Task<Status_Application> InsertAsync(TypeAccount_Insert_v1 request)
        {
            if (string.IsNullOrEmpty(request.name_TypeAccount))
            {
                return new Status_Application { StatusBool = false, StatusType = "Chưa nhập name type account." };
            }
            var checkIsTypeAccount = await _db.tbTypeAccount.AnyAsync(x =>
                x.name_TypeAccount!.ToLower() == request.name_TypeAccount.ToLower());
            if (checkIsTypeAccount)
            {
                return new Status_Application { StatusBool = false, StatusType = "Name type account đã tồn tại." };
            }

            tbTypeAccount typeAccount = new tbTypeAccount
            {
                id_TypeAccount = new Guid(),
                name_TypeAccount = request.name_TypeAccount
            };

            return await _typeAccount_Repository_V1.InsertAsync(typeAccount);
        }

        public async Task<List<TypeAccount_Select_v1>> SelectAll()
        {
            return _mapper.Map<List<TypeAccount_Select_v1>>(await _typeAccount_Repository_V1.SelectAll());
        }
    }
}
