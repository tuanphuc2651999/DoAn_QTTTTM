using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_KhachHang
    {
        DBQL_MatBangTTTMDataContext db = new DBQL_MatBangTTTMDataContext();
        public List<KhachHangModel> layDSKhachHang()
        {
            var khachHang = from kh in db.KhachHangs
                    select new KhachHangModel
                    {
                        MaKH = kh.MaKH,
                        HoTenKH = kh.HoTenKH,
                        CMND= kh.CMND,
                        DiaChi = kh.DiaChi,
                        GioiTinh = kh.GioiTinh,
                        NgaySinh = (DateTime)kh.NgaySinh,
                        SDT = kh.SDT,
                        Email= kh.Email,
                        DuongDanHinh = kh.DuongDanHinh,
                        TinhTrang = (int)kh.TinhTrang,                        
                    };                            
            return khachHang.ToList();
        }
        public bool themKhachHang(KhachHangModel khachHang)
        {
            try
            {
                KhachHang kh = new KhachHang();
                kh.MaKH = khachHang.MaKH;
                kh.HoTenKH = khachHang.HoTenKH;
                kh.NgaySinh = khachHang.NgaySinh;
                kh.GioiTinh = khachHang.GioiTinh;
                kh.DiaChi = khachHang.DiaChi;
                kh.SDT = khachHang.SDT;
                kh.Email = khachHang.Email;
                kh.DuongDanHinh = khachHang.DuongDanHinh;
                if (khachHang.TinhTrangAsString.Equals("Đang làm"))
                    kh.TinhTrang = 1;
                else
                    kh.TinhTrang = 0;
                db.KhachHangs.InsertOnSubmit(kh);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }
        public bool suaKhachHang(KhachHangModel khachHang)
        {
            try
            {
                KhachHang kh = db.KhachHangs.FirstOrDefault(t => t.MaKH.Equals(khachHang.MaKH));
                
                kh.MaKH = khachHang.MaKH;
                kh.HoTenKH = khachHang.HoTenKH;
                kh.NgaySinh = khachHang.NgaySinh;
                kh.GioiTinh = khachHang.GioiTinh;
                kh.DiaChi = khachHang.DiaChi;
                kh.SDT = khachHang.SDT;
                kh.Email = khachHang.Email;
                kh.TinhTrang = khachHang.TinhTrang;

                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public string layMaKHTuSinh()
        {
            string result = "";
            KhachHang khachHang = db.KhachHangs.Where(x => x.MaKH.Contains($"AEON_KH"))
                .OrderByDescending(x => x.MaKH).FirstOrDefault();
            if (khachHang != null && !string.IsNullOrWhiteSpace(khachHang.MaKH))
            {
                int so =Convert.ToInt32(khachHang.MaKH.Replace("AEON_KH", "")) + 1;
                 result = "AEON_KH"+ so.ToString().PadLeft(4,'0');
            }
            return result;
        }

        public List<TaiKhoanKHModel> layDSTKKH()
        {
            var taiKhoans = from tk in db.TaiKhoanKHs
                            join kh in db.KhachHangs on tk.TaiKhoan equals kh.SDT
                            select new TaiKhoanKHModel {
                TaiKhoan = tk.TaiKhoan,
                MatKhau = tk.MatKhau,
                Email = tk.Email,
                TinhTrang = tk.TinhTrang,
                TenKhachHang=kh.HoTenKH
                            };
            return taiKhoans.ToList();
        }
    }
}
