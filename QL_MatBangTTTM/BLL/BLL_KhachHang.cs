using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_KhachHang
    {
        DAL_KhachHang khachHang = new DAL_KhachHang();
        public List<KhachHangModel> layDSKhachHang()
        {
            return khachHang.layDSKhachHang();
        }
        public bool suaKhachHang(KhachHangModel kh)
        {
            return khachHang.suaKhachHang(kh);
        }
        public bool themKhachHang(KhachHangModel kh)
        {
            return khachHang.themKhachHang(kh);
        }
        public string layMaKHTuSinh()
        {
            return khachHang.layMaKHTuSinh();
        }
        public List<TaiKhoanKHModel> layDSTKKH()
        {

            return khachHang.layDSTKKH();
        }
    }
}
