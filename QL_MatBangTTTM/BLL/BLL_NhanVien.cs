using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_NhanVien
    {
        DAL_NhanVien nhanVien = new DAL_NhanVien();
        public List<NhanVienModel> layDSNhanVien()
        {
            return nhanVien.layDSNhanVien();
        }
        public List<TaiKhoanNVModel> layDSTKNV()
        {
            
            return nhanVien.layDSTKNhanVien();
        }
    }
}
