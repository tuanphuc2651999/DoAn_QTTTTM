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
using Model;
using DevExpress.XtraGrid.Localization;
using System.IO;
using Liz.DoAn;
using System.Drawing.Imaging;
using DevExpress.XtraEditors;

namespace QL_MatBangTTTM
{
    public partial class FrmNhanVien : DevExpress.XtraEditors.XtraForm
    {
        BLL_NhanVien nhanVien = new BLL_NhanVien();
        string tenHinh;
        public FrmNhanVien()
        {
            InitializeComponent();
        }
        private void Databingding(BindingList<NhanVienModel> nv)
        {
            txtMaNhanVien.DataBindings.Add("text", nv, "MaNV");
            txtTenNV.DataBindings.Add("text", nv, "HoTenNV");
            txtCMND.DataBindings.Add("text", nv, "CMND");
            txtDiaChi.DataBindings.Add("text", nv, "DiaChi");
            txtSDT.DataBindings.Add("text", nv, "SDT");
            cboGioiTinh.DataBindings.Add("text", nv, "GioiTinh");
            cboTrangThai.DataBindings.Add("text", nv, "TinhTrangAsString");
            dENgaySinh.DataBindings.Add("datetime", nv, "NgaySinh");
            dENgayVL.DataBindings.Add("datetime", nv, "NgayVL");
            txtEmail.DataBindings.Add("text", nv,"Email");
        }
        private void FrmNhanVien_Load(object sender, EventArgs e)
        {
            BindingList<NhanVienModel> bindingList = new BindingList<NhanVienModel>(nhanVien.layDSNhanVien());
            this.gcDSNhanVien.DataSource = bindingList;       
            GridLocalizer.Active = new MyGridLocalizer();
            Databingding(bindingList);       
        }

        private void btnTaiLen_Click(object sender, EventArgs e)
        {
            try
            {
                openFile.Filter = "png files(*.png)|*.png|jpg files(*.jpg)|*.jpg|All files (*.*)|*.*";

                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    tenHinh = openFile.FileName;
                    Image img = Image.FromFile(tenHinh);
                    pENhanVien.Image = img;               
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hãy chọn hình ảnh");
            }

        }
        private void dgvDSNhanVien_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            string DuongDanHinh = dgvDSNhanVien.GetFocusedRowCellDisplayText("DuongDanHinh");
            string pathHinh = FileUtils.FolderUploads + "\\" + DuongDanHinh;
            if (!string.IsNullOrEmpty(pathHinh))
            {
                Image img=null;
                FileInfo fileInfo = new FileInfo(pathHinh);
                if(fileInfo.Exists)
                {
                     img = Image.FromFile(pathHinh);
                   
                }   
                else
                {
                     //img = Image.FromFile(pathImages);
                }    
                    
                pENhanVien.Image = img;
            }    
            
        }

        private void btnLuu_ItemClick(object sender, ItemClickEventArgs e)
        {                
            FileInfo file = new FileInfo(tenHinh);
            FileUtils.SaveFile(tenHinh, file.Name, pENhanVien);
        }
    }
}