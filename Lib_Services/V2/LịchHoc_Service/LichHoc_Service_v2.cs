using Lib_Models.Models_Insert.v2.MonHoc;
using Lib_Models.Models_Table_Entity;
using Lib_Models.Status_Model;
using Lib_Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V2.LịchHoc
{
    public class LichHoc_Service_v2 : ILichHoc_Service_v2
    {
        private readonly IRepository<tbLichHoc> _repositoryLichHoc;
        private readonly IRepository<tbStyleBuoiHoc> _repositoryStyleBuoiHoc;
        public LichHoc_Service_v2(IRepository<tbLichHoc> repositoryLichHoc, IRepository<tbStyleBuoiHoc> repositoryStyleBuoiHoc)
        {
            _repositoryLichHoc = repositoryLichHoc;
            _repositoryStyleBuoiHoc = repositoryStyleBuoiHoc;
        }

        public async Task<Status_Application> InsertAsync(List<LichHoc_MonHoc_Insert_v2>? lichHocs, int soBuoiHoc, int idMonHoc)
        {
            try
            {
                if (lichHocs!.Count() < 0)
                {
                    return new Status_Application { StatusBool = false, StatusType = "Lịch học chưa thêm"};
                }
                if (soBuoiHoc != lichHocs!.Count())
                {
                    return new Status_Application { StatusBool = false, StatusType = "Lịch học phải bằng số buổi học" };
                }

                List<tbLichHoc> addLichHocs = new List<tbLichHoc>();
                foreach (var item in lichHocs!)
                {
                    item.tinhTrangBuoiHoc = item.tinhTrangBuoiHoc!.Trim();
                    var tag = await _repositoryStyleBuoiHoc.GetAll(x => x.name!.ToLower() == item.tinhTrangBuoiHoc.ToLower());
                    if (tag.Any() && tag == null)
                    {
                        return new Status_Application { StatusBool = false, StatusType = $"Tình trang buổi học {item.tinhTrangBuoiHoc} không hợp lệ" };
                    }
                    int idTag = tag.First().id_StyleBuoiHoc;

                    tbLichHoc addLichHoc = new tbLichHoc
                    {
                        id_MonHoc = idMonHoc,
                        thoiGianBatDau = item.thoiGianBatDau,
                        thoiGianKetThuc = item.thoiGianKetThuc,
                        phonghoc = item.phonghoc,
                        style = item.phuongPhapHoc,
                        id_StyleBuoiHoc = idTag
                    };
                    addLichHocs.Add(addLichHoc);
                }

                await _repositoryLichHoc.Insert(addLichHocs);
                await _repositoryLichHoc.Commit();

                List<int> idLichHocs = new List<int>();
                foreach (var item in addLichHocs)
                {
                    idLichHocs.Add(item.id_LichHoc);
                }
                return new Status_Application { StatusBool = true, StatusType = "success" , List_Id_Int = idLichHocs };
            }
            catch (Exception ex)
            {
                return new Status_Application { StatusBool = false , StatusType = ex.Message};
            }
        }
    }
}
