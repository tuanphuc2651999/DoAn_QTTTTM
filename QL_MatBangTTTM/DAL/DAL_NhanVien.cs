using Liz.DoAn;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class DAL_NhanVien
    {
        DBQL_MatBangTTTMDataContext db = new DBQL_MatBangTTTMDataContext();
        public List<NhanVienModel> layDSNhanVien()
        {
            try
            {
                var nhanViens = (from nv in db.NhanViens
                                 join cv in db.ChucVus on nv.ChucVu equals cv.MaChucVu
                                 select new NhanVienModel
                                 {
                                     MaNV = nv.MaNhanVien,
                                     HoTenNV = nv.HoTenNV,
                                     NgayVL = (DateTime)nv.NgayVL,
                                     DiaChi = nv.DiaChi,
                                     GioiTinh = nv.GioiTinh,
                                     NgaySinh = (DateTime)nv.NgaySinh,
                                     SDT = nv.SDT,
                                     DuongDanHinh =nv.DuongDanHinh,
                                     TinhTrang = (int)nv.TinhTrang,
                                     ChucVu = cv.TenChucVu,
                                     CMND = nv.CMND,
                                     Email= nv.Email
                                 }) ;

                return nhanViens.ToList();
            }
            catch (Exception exe)
            {
                throw;
            }

        }

        public List<TaiKhoanNVModel> layDSTKNhanVien()
        {
            var taiKhoans = from tk in db.TaiKhoanNVs
                            join nv in db.NhanViens on tk.TaiKhoan equals nv.MaNhanVien
                            select new TaiKhoanNVModel {
                TaiKhoan = tk.TaiKhoan,
                MatKhau = tk.TaiKhoan,
                Email = tk.Email,
                TinhTrang= tk.TinhTrang,
                TenNhanVien=nv.HoTenNV
                            };
            return taiKhoans.ToList();
        }
    }
}
