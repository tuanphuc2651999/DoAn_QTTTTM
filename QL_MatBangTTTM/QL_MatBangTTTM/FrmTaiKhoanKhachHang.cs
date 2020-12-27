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
using DevExpress.XtraGrid.Localization;
using Model;
using BLL;

namespace QL_MatBangTTTM
{
    public partial class FrmTaiKhoanKhachHang : DevExpress.XtraEditors.XtraForm
    {
        BLL_KhachHang khachHang = new BLL_KhachHang();
        public FrmTaiKhoanKhachHang()
        {
            InitializeComponent();
            GridLocalizer.Active = new MyGridLocalizer();
        }
        private void Databingding(BindingList<TaiKhoanKHModel> kh)
        {
            cboKhachHang.DataBindings.Add("text", kh, "MaKhachHang");
            txtTK.DataBindings.Add("text", kh, "TaiKhoan");
            txtMatKhau.DataBindings.Add("text", kh, "MatKhau");
            txtEmail.DataBindings.Add("text", kh, "Email");
            cboTinhTrang.DataBindings.Add("text", kh, "TinhTrang");         
        }
        private void FrmTaiKhoanKhachHang_Load(object sender, EventArgs e)
        {
            BindingList<TaiKhoanKHModel> bindingList = new BindingList<TaiKhoanKHModel>(khachHang.layDSTKKH());
            this.gCDSTaiKhoanKH.DataSource = bindingList;
            //dem = dgvDSKhachHang.RowCount;
            GridLocalizer.Active = new MyGridLocalizer();
            Databingding(bindingList);
        }
    }
}