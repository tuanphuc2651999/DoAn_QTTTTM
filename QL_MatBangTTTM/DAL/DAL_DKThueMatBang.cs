using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
  public class DAL_DKThueMatBang
    {
        DBQL_MatBangTTTMDataContext db = new DBQL_MatBangTTTMDataContext();
        public List<DangKyThueModel> LayDSDKThueMatBang()
        {
            var dsThue = from ds in db.DangKyThues
                         join kh in db.KhachHangs on ds.MaKhachHang equals kh.MaKH
                         join mb in db.MatBangs on ds.MatBang equals mb.MaMB
                         select new DangKyThueModel {
                         MaDK= ds.MaDK,
                         NgayLap=ds.NgayLap,
                         NgayMoCua= ds.NgayMoCua,
                         ThoiHanThue= ds.ThoiHanThue,
                         LichHen= ds.LichHen,
                         MaKhachHang=kh.MaKH,
                         TinhTrang = ds.TinhTrang,
                         MaMB = mb.MaMB,
                         MaHD= ds.MaHD
                         };
            return dsThue.ToList();
        }
        public List<MatBangModel> DSMatBang()
        {
            var matBang = from mb in db.MatBangs
                          select new MatBangModel
                          {
                              MaMB = mb.MaMB,
                              TinhTrang = mb.TinhTrang,
                              DienTich = mb.DienTich
                          };

            return matBang.ToList();
        }
        public bool ThemDangKyMatBang(DangKyThue input)
        {
            try
            {
                DangKyThue dk = new DangKyThue();
                dk.MaDK = input.MaDK;
                dk.NgayLap = input.NgayLap;
                dk.NgayMoCua = input.NgayMoCua;
                dk.ThoiHanThue = input.ThoiHanThue;
                dk.TinhTrang = input.TinhTrang;
                dk.LichHen = input.LichHen;
                dk.MatBang = input.MatBang;
                dk.MaKhachHang = input.MaKhachHang;
                dk.MaHD = input.MaHD;
                db.DangKyThues.InsertOnSubmit(dk);
                db.SubmitChanges();
                return true;//Đang ký thành công
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SuaDKThueMatBang(DangKyThue input)
        {
            try
            {
                DangKyThue dk = db.DangKyThues.FirstOrDefault(t => t.MaDK.Equals(input.MaDK));
                dk.LichHen = input.LichHen;
                dk.MaHD = input.MaHD;             
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
        public string LayMaDKTuSinh(int count)
        {
            string result = "AEON_PDK" + count.ToString().PadLeft(4, '0');
            DangKyThue dk = db.DangKyThues.Where(x => x.MaDK.Contains($"AEON_PDK"))
                .OrderByDescending(x => x.MaDK).FirstOrDefault();
            if (dk != null && !string.IsNullOrWhiteSpace(dk.MaDK))
            {               
                int so = Convert.ToInt32(dk.MaDK.Replace("AEON_PDK", "")) + 1;
                if(so==count)
                    result = "AEON_PDK" + so.ToString().PadLeft(4, '0');
                else
                    result = "AEON_PDK" + count.ToString().PadLeft(4, '0');
            }
            return result;
        }

        public HoaDonTienCoc HoaDon(string ma)
        {
            var hD =  db.HoaDonTienCocs.FirstOrDefault(t => t.MaHD.Equals(ma));
            return hD;
        }
        public LichHen LayThongTinLichHen(string ma)
        {
            var hD = db.LichHens.FirstOrDefault(t => t.MaLichHen.Equals(ma));
            return hD;
        }
        public bool UpdateTrangThaiMatBang(string maMB, int TrangThai)
        {
            try
            {
                MatBang dk = db.MatBangs.FirstOrDefault(t => t.MaMB.Equals(maMB));
                dk.TinhTrang = TrangThai;
                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            
        }

        public bool ThemHoaDon(HoaDonTienCoc hd)
        {
            try
            {
                HoaDonTienCoc hoaDon = new HoaDonTienCoc();
                hoaDon = hd;
                db.HoaDonTienCocs.InsertOnSubmit(hoaDon);
                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public string LayMaHoaDonTuSinh()
        {
            string result = "AEON_HHDC" + 1.ToString().PadLeft(4, '0');
            HoaDonTienCoc hd = db.HoaDonTienCocs.Where(x => x.MaHD.Contains($"AEON_HHDC"))
                .OrderByDescending(x => x.MaHD).FirstOrDefault();
            if (hd != null && !string.IsNullOrWhiteSpace(hd.MaHD))
            {
                int so = Convert.ToInt32(hd.MaHD.Replace("AEON_HHDC", "")) + 1;
                result = "AEON_HHDC" + so.ToString().PadLeft(4, '0');
            }
            return result;
        }

        public double TinhTienCoc(string maMB) {
            var matBang = db.MatBangs.FirstOrDefault(t => t.MaMB.Equals(maMB));
            var giaThueMB = db.GiaThues.FirstOrDefault(t => t.MaGiaThue.Equals(matBang.Gia));
            return (double)matBang.DienTich * (double)giaThueMB.Gia;
        }

        public bool XoaHoaDon(string maHD)
        {
            try
            {
                HoaDonTienCoc hd = db.HoaDonTienCocs.Where(t => t.MaHD.Equals(maHD)).FirstOrDefault();
                db.HoaDonTienCocs.DeleteOnSubmit(hd);
                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }       
        }

        public string LayMaLichHenTuSinh()
        {
            string result = "AEON_LH" + 1.ToString().PadLeft(4, '0');
            LichHen lh = db.LichHens.Where(x => x.MaLichHen.Contains($"AEON_LH"))
                .OrderByDescending(x => x.MaLichHen).FirstOrDefault();
            if (lh != null && !string.IsNullOrWhiteSpace(lh.MaLichHen))
            {
                int so = Convert.ToInt32(lh.MaLichHen.Replace("AEON_LH", "")) + 1;
                result = "AEON_LH" + so.ToString().PadLeft(4, '0');
            }
            return result;
        }
        public bool ThemLichHen(LichHen lh)
        {
            try
            {
                LichHen lichHen = new LichHen();
                lichHen = lh;
                db.LichHens.InsertOnSubmit(lichHen);
                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public bool XoaLichHen(string maLH)
        {
            try
            {
                LichHen lh = db.LichHens.Where(t => t.MaLichHen.Equals(maLH)).FirstOrDefault();
                db.LichHens.DeleteOnSubmit(lh);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public bool KiemTraLichHen(LichHen lh)
        {
            try
            {
                var lichHen = db.LichHens.Where(t => t.MaNhanVien == lh.MaNhanVien && lh.GioBatDau < t.GioKetThuc && t.GioBatDau <= lh.GioBatDau && lh.NgayHen == t.NgayHen).FirstOrDefault();
                if(lichHen==null)
                    return true;
                else
                {
                    return false;
                }    
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public MatBang LayThongTinMB(string mb)
        {
            try
            {
                MatBang matBang = db.MatBangs.Where(t => t.MaMB.Equals(mb)).FirstOrDefault();
                return matBang;
            }
            catch (Exception)
            {      
                throw;
            }
        }
        public List<ThueMatBang> LayDSThueMatBang()
        {
            var thueMB = db.ThueMatBangs.Select(t=>t);
            return thueMB.ToList();
        }
        public bool ThemThueMatBang(ThueMatBang thue)
        {
            try
            {
                db.ThueMatBangs.InsertOnSubmit(thue);
                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool SuaThueMatBang(ThueMatBang thue)
        {
            try
            {
                ThueMatBang dk = db.ThueMatBangs.FirstOrDefault(t => t.MaThueMB.Equals(thue.MaThueMB));
                dk = thue;
                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }           
        }
        public string LayMaThueTuSinh()
        {
            string result = "";
            ThueMatBang lh = db.ThueMatBangs.Where(x => x.MaThueMB.Contains($"AEON_TMB"))
                .OrderByDescending(x => x.MaThueMB).FirstOrDefault();
            if (lh != null && !string.IsNullOrWhiteSpace(lh.MaThueMB))
            {
                int so = Convert.ToInt32(lh.MaThueMB.Replace("AEON_TMB", "")) + 1;
                result = "AEON_TMB" + so.ToString().PadLeft(4, '0');
            }
            return result;
        }

        public List<DangKyThueModel> LayDSMatBangChuaThue()
        {
            var dsChuaThue = from mb in db.DangKyThues
                             where mb.TinhTrang == 1
                             select new DangKyThueModel
                             {
                                 TinhTrang = mb.TinhTrang,
                                 MaDK = mb.MaDK
                             };       
                return dsChuaThue.ToList();
            }

        public DangKyThue LayThongTinDKThue(string ma)
        {
            DangKyThue thue = db.DangKyThues.Where(t => t.MaDK == ma).FirstOrDefault();
            return thue;
        }
        public List<GiaThue> DSGia()
        {
            var gia = db.GiaThues.Select(t => t);
            return gia.ToList();
        }
        public double PhiDichVu(int dientich)
        {
            GiaThue gia = db.GiaThues.Where(t=>t.MaGiaThue== "AEON_MGT0004").Select(t => t).FirstOrDefault();
            return (double)(gia.Gia*dientich);
        }
    }
}
