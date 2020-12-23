using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class DAL_PhanQuyen
    {
        DBQL_MatBangTTTMDataContext db = new DBQL_MatBangTTTMDataContext();
        public IQueryable<NhomNguoiDung> layDSNhomNguoiDung()
        {
            return db.NhomNguoiDungs.Select(t => t);
        }
        public List<PhanQuyenModel> layDSQuyen(string maNhom)
        {
            var phanquyen = from pq in db.PhanQuyens join mh in db.ManHinhs on pq.MaMH equals mh.MaManHinh where pq.MaNhom == maNhom select new { pq.MaMH, mh.TenMH, pq.CoQuyen };
            List<PhanQuyenModel> ds = new List<PhanQuyenModel>();
            foreach (var item in phanquyen)
            {
                PhanQuyenModel model = new PhanQuyenModel();
                model.MaMH = item.MaMH;
                model.TenMH = item.TenMH;
                model.Quyen = item.CoQuyen;
                ds.Add(model);
            }
            return ds;
        }
        public bool capNhatQuyen(UpdateQuyenModel updates)
        {
            try
            {
                PhanQuyen phanQuyen = db.PhanQuyens.FirstOrDefault(t => t.MaNhom.Equals(updates.MaNhom) && t.ManHinh.MaManHinh.Equals(updates.MaMH));
                phanQuyen.CoQuyen = updates.Quyen;
                db.SubmitChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }       
    }
}
