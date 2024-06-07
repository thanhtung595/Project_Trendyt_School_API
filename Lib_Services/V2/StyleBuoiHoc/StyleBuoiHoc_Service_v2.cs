using Lib_Models.Models_Table_Entity;
using Lib_Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lib_Services.V2.StyleBuoiHoc
{
    public class StyleBuoiHoc_Service_v2 : IStyleBuoiHoc_Service_v2
    {
        private readonly IRepository<tbStyleBuoiHoc> _repositoryStyleBuoiHoc;
        public StyleBuoiHoc_Service_v2(IRepository<tbStyleBuoiHoc> repositoryStyleBuoiHoc)
        {
            _repositoryStyleBuoiHoc = repositoryStyleBuoiHoc;
        }
        public async Task<List<tbStyleBuoiHoc>> GetAllAsync()
        {
            var data = await _repositoryStyleBuoiHoc.GetAll();
            if (!data.Any())
            {
                return null!; // Trả về danh sách rỗng nếu không có dữ liệu
            }
            return data.ToList();
        }
    }
}