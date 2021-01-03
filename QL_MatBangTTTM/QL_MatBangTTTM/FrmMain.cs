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
using DevExpress.XtraSplashScreen;

namespace QL_MatBangTTTM
{
    public partial class FrmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string maNVDN;
        public FrmMain( string maNV)
        {
            InitializeComponent();
            maNVDN = maNV;
        }
        private bool CheckExitsForm(string name)
        {
            bool check = false;
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.Name == name)
                {
                    check = true;
                    break;
                }
            }
            return check;
        }
        private void ActiveChildForm(string name)
        {
            foreach (Form frm in MdiChildren)
            {
                if (frm.Name == name)
                {
                    frm.Activate();
                    break;
                }
            }
        }
        private void LoadFormDialog(Form form)
        {
            form.ShowDialog();
        }
        private void loadFrm(Form frm)
        {

            if (!CheckExitsForm(frm.Name))
            {
                SplashScreenManager.ShowForm(this,typeof(WaitLoadFrm));
                frm.MdiParent = this;
                frm.Show();
                SplashScreenManager.CloseDefaultSplashScreen();
            }
            else
            {
                ActiveChildForm(frm.Name);
            }
        }

        private void btnGiaThueMatBang_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadFrm(new FrmTrangChu());
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            loadFrm(new FrmTrangChu());
        }

        private void btnPhanQuyen_ItemClick(object sender, ItemClickEventArgs e)
        {         
            loadFrm(new FrmPhanQuyen());

        }

        private void btnNhanVien_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadFrm(new FrmNhanVien(maNVDN));
        }

        private void btnKhachHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadFrm(new FrmKhachHang(maNVDN));
        }

        private void btnLichHen_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadFrm(new FrmLichHen(maNVDN));
        }

        private void btnDangKyThue_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadFrm(new FrmDangKy(maNVDN));
        }

        private void btnThueMatBang_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadFrm(new FrmThueMatBang(maNVDN));
        }

        private void btnTraMatBang_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadFrm(new FrmTraMatBang(maNVDN));
        }

        private void btnXyLyViPham_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadFrm(new FrmXuLyViPham(maNVDN));
        }

        private void btnHoaDonDV_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadFrm(new FrmHoaDonDichVu(maNVDN));
        }

        private void btnHoaDonTienThue_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadFrm(new FrmHoaDonTienThue(maNVDN));
        }

        private void btnTaiKhoanNhanVien_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadFrm(new FrmTaiKhoanNV(maNVDN));
        }

        private void btnTaiKhoanKhachHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadFrm(new FrmTaiKhoanKhachHang());
        }

        private void btnMatBang_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadFrm(new FrmMatBang());
        }

        private void btnDoiMatKhau_ItemClick(object sender, ItemClickEventArgs e)
        {
            //LoadFormDialog(new FrmDoiMatKhau());
        }
    }
}