using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using BLL;
using DAL;

namespace QL_MatBangTTTM
{
    public partial class FrmThueMatBang : DevExpress.XtraEditors.XtraForm
    {
        BLL_ThueMatBang thueMB = new BLL_ThueMatBang();
        BLL_KhachHang khachHang = new BLL_KhachHang();
        int tienCoc = 0;
        int thoiHanThue = 0;
        int phiDV = 0;
        int soNamDaThanhToan = 0;
        bool check = true;
        public FrmThueMatBang()
        {
            InitializeComponent();
        }

        private void btnThem_ItemClick(object sender, ItemClickEventArgs e)
        {
            TaoMoi();
            check = false;
            txtMaDK.Focus();
        }
        public void LoadDSThue()
        {
            gcThueMatBang.DataSource = thueMB.LayDSThueMatBang();
        }
        public void LoadDSDKThue()
        {
            txtMaDK.Properties.DataSource = thueMB.LayDSMatBangChuaThue();
        }
        public void LoadTienThue()
        {
           // txtLoaiDichVu.Properties.DataSource = thueMB.DSGia();
        }
        private void TaoMoi()
        {
            txtMaThue.EditValue = thueMB.LayMaThueTuSinh();
            txtNgayLap.EditValue = DateTime.Now;
            txtNhanVien.EditValue = "";
            txtNgayThue.EditValue = "";
            txtNgayHetHan.EditValue = "";
            txtThoiHanThue.EditValue = "";
            txtPhiDV.EditValue = "";
            txtMaKhachHang.EditValue = "";
            txtSDT.EditValue = "";
            txtEmail.EditValue = "";
            txtNamDaThanhToan.EditValue = 0;
            txtTienCoc.EditValue = 0;
            txtMaMB.EditValue = "";
            txtMaDK.EditValue = "";
        }
        private void LayThongTinDKThue(string ma)
        {
            var tt = thueMB.LayThongTinDKThue(ma);    
            if(tt!=null)
            {
                var mb = thueMB.LayThongTinMB(tt.MatBang);
                var kh = khachHang.LayTTKhachHang(tt.MaKhachHang);
                var hoaDon = thueMB.HoaDon(tt.MaHD);
                DateTime ngayHetHan;
                DateTime ngayThue = (DateTime)tt.NgayMoCua;
                ngayHetHan = ngayThue.AddYears(1);
                txtNgayThue.EditValue = tt.NgayMoCua;
                txtNgayHetHan.EditValue = ngayHetHan;
                txtThoiHanThue.EditValue =tt.ThoiHanThue+" năm";
                txtTienCoc.EditValue = String.Format("{0:0,0 vnđ}", hoaDon.SoTien);
                txtPhiDV.EditValue= String.Format("{0:0,0 vnđ/năm}", thueMB.PhiDichVu((int)mb.DienTich));
                txtMaMB.EditValue = tt.MatBang;
                txtMaKhachHang.EditValue = kh.MaKH;
                txtSDT.EditValue = kh.SDT;
                txtEmail.EditValue = kh.Email;
                tienCoc = (int)hoaDon.SoTien;
                thoiHanThue = (int)tt.ThoiHanThue;
                phiDV = (int)thueMB.PhiDichVu((int)mb.DienTich);
                txtNamDaThanhToan.EditValue = dgvDSThueMatBang.GetFocusedRowCellDisplayText(colDaThanhToan).ToString();
            }               
        }
        public void LoadLaiDS()
        {
            LoadDSThue();
            LoadDSDKThue();
            TaoMoi();
            LoadTienThue();
        }
        private void FrmThueMatBang_Load(object sender, EventArgs e)
        {
            LoadLaiDS();
        }

        private void txtMaDK_EditValueChanged(object sender, EventArgs e)
        {
            LayThongTinDKThue(txtMaDK.EditValue.ToString());
        }

        private void btnLuu_ItemClick(object sender, ItemClickEventArgs e)
        {
            if(string.IsNullOrEmpty(txtMaDK.EditValue.ToString()))
            {
                MessageBox.Show("Bạn chưa chọn mã thuê mặt bằng");
                txtMaDK.Focus();
                return;
            }    
            ThueMatBang themMB = new ThueMatBang();
            themMB.MaThueMB = txtMaThue.EditValue.ToString();
            themMB.NgayLap = (DateTime)txtNgayLap.EditValue;
            themMB.NgayThue = (DateTime)txtNgayThue.EditValue;
            themMB.NgayHetHanHopDong = (DateTime)txtNgayHetHan.EditValue;
            themMB.ThoiHanThue = thoiHanThue;
            themMB.SoNamDaThanhToan = int.Parse(txtNamDaThanhToan.EditValue.ToString());
            themMB.TienCoc = tienCoc;
            themMB.PhiDichVuMotNam = phiDV.ToString();
            themMB.TinhTrang = 1;
            themMB.MaNhanVien = null;
            themMB.MaDKThue = txtMaDK.EditValue.ToString();
            
            if(!thueMB.ThemThueMatBang(themMB))
            {
                MessageBox.Show("Không thể thêm thuê mặt bằng");
                return;
            }
            MessageBox.Show("Thêm thành công");
            LoadLaiDS();
        }

        private void dgvDSThueMatBang_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (check == false)
                return;
            string maDK = dgvDSThueMatBang.GetFocusedRowCellValue(colMaDKThue).ToString();
            LayThongTinDKThue(maDK);
        }
    }
}