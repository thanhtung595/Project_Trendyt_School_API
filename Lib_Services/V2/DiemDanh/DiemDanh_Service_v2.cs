using Lib_Models.Models_Table_Entity;
using Lib_Models.Status_Model;
using Lib_Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V2.DiemDanh
{
    public class DiemDanh_Service_v2 : IDiemDanh_Service_v2
    {
        private readonly IRepository<tbDiemDanh> _repositoryDiemDanh;
        public DiemDanh_Service_v2(IRepository<tbDiemDanh> repositoryDiemDanh)
        {
            _repositoryDiemDanh = repositoryDiemDanh;
        }

        public async Task<Status_Application> InsertAsync(List<int> idLichHoc, List<int> idStudent)
        {
            try
            {
                List<tbDiemDanh> addDiemDanhs = new List<tbDiemDanh>();

                foreach (var lichHoc in idLichHoc)
                {
                    foreach (var sudent in idStudent)
                    {
                        tbDiemDanh addDiemDanh = new tbDiemDanh
                        {
                            id_LichHoc = lichHoc,
                            id_MonHocClass_Student = sudent,
                            _DauGio = false,
                            _CuoiGio = false,
                            _DiMuon = false,
                        };
                        addDiemDanhs.Add(addDiemDanh);
                    }
                }

                await _repositoryDiemDanh.Insert(addDiemDanhs);
                await _repositoryDiemDanh.Commit();

                return new Status_Application { StatusBool = true, StatusType = "success" };
            }
            catch (Exception ex)
            {
                return new Status_Application { StatusBool = false , StatusType = ex.Message};
            }
        }


    }
}
