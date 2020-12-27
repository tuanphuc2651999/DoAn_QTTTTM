using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BLL;
using DAL;
using Model;

namespace QL_MatBangTTTM
{
    public partial class FrmDangNhap : DevExpress.XtraEditors.XtraForm
    {
        BLL_PhanQuyen phanQuyen = new BLL_PhanQuyen();
        TaiKhoanNV tk = new TaiKhoanNV();
        BLL_NhanVien tknv = new BLL_NhanVien();
        public FrmDangNhap()
        {
            InitializeComponent();
                
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            var dsTaiKhoan = tknv.layDSTKNV();
            var ttTaiKhoan = dsTaiKhoan.Where(t => t.TaiKhoan.Equals(txtTenDangNhap.Text)).FirstOrDefault();
            if (string.IsNullOrEmpty(txtTenDangNhap.Text.Trim()))
            {
                MessageBox.Show("Không được bỏ trống tên đăng nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtTenDangNhap.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtMatKhau.Text))
            {
                MessageBox.Show("Không được bỏ trống mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtMatKhau.Focus();
                return;
            }
            
            if (!ttTaiKhoan.MatKhau.Equals(txtMatKhau.Text))
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu");
                this.txtTenDangNhap.Focus();
                return;
            } 
            if(ttTaiKhoan.TinhTrang==1)
            {
                MessageBox.Show("Vào From đăng nhập");
            }
            if(ttTaiKhoan.TinhTrang == 0)
            {
                FrmDoiMatKhau doiMatKhau = new FrmDoiMatKhau(txtTenDangNhap.Text);
                doiMatKhau.ShowDialog();
                tk = null;
                return;
            }    
           
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void FrmDangNhap_Load(object sender, EventArgs e)
        {
            var tk1 = phanQuyen.KiemTraDangNhap(txtTenDangNhap.Text);
        }
    }
}