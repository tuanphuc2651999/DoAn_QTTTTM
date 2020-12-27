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
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraGrid.Views.Grid;
using Model;
using Liz.DoAn;
using System.IO;

namespace QL_MatBangTTTM
{
    public partial class FrmKhachHang : DevExpress.XtraEditors.XtraForm
    {
        BLL_KhachHang khachHang = new BLL_KhachHang();

        List<int> listViTriSua;
        List<int> listViTriThem;
        int dem = 0;
        string pathHinh;
        public FrmKhachHang()
        {
            InitializeComponent();
            listViTriSua = new List<int>();
            listViTriThem = new List<int>();
        }
        private void Databingding(BindingList<KhachHangModel> kh)
        {
            txtMaKH.DataBindings.Add("text", kh, "MaKH");
            txtTenKH.DataBindings.Add("text", kh, "HoTenKH");
            txtCMND.DataBindings.Add("text", kh, "CMND");
            txtDiaChi.DataBindings.Add("text", kh, "DiaChi");
            txtSDT.DataBindings.Add("text", kh, "SDT");
            cboGioiTinh.DataBindings.Add("text", kh, "GioiTinh");
            cboTrangThai.DataBindings.Add("text", kh, "TinhTrangAsString");
            dENgaySinh.DataBindings.Add("datetime", kh, "ngaysinh");
            txtEmail.DataBindings.Add("text", kh, "Email");
        }
        private void FrmKhachHang_Load(object sender, EventArgs e)
        {
            BindingList<KhachHangModel> bindingList =new BindingList<KhachHangModel>(khachHang.layDSKhachHang());
            this.gcDSKhachHang.DataSource = bindingList;
            dem = dgvDSKhachHang.RowCount;
            GridLocalizer.Active = new MyGridLocalizer();
            Databingding(bindingList);
        }       
        private void dgvDSKhachHang_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string maKH = dgvDSKhachHang.GetFocusedRowCellDisplayText(colMaKH).ToString();            
            if (dem==dgvDSKhachHang.RowCount)
            {
                int index = dgvDSKhachHang.FocusedRowHandle;
                int kt = listViTriSua.Where(t => t== index).Count();                
                if (kt==0)
                    listViTriSua.Add(index);                
            }
            else
            {
                int demRow = dgvDSKhachHang.RowCount;
                int kt = listViTriThem.Where(t => t == demRow).Count();
                if (kt == 0)
                    listViTriThem.Add(demRow);
            }    
          

        }

        private void btnThemKH_ItemClick(object sender, ItemClickEventArgs e)
        {
            dgvDSKhachHang.AddNewRow();
        }

        private void btnLuuKH_ItemClick(object sender, ItemClickEventArgs e)
        {            
            KhachHangModel kh = new KhachHangModel();
            string maKH;
            string tenKH;
            DateTime ngaySinh;
            string gioiTinh;
            string cMND;
            string SDT;
            string diaChi;
            string tinhTrangAsTring;
            string email;
            string duongDanHinh="";
            if (listViTriSua.Count>0)
            {
                foreach (var item in listViTriSua)
                {                   
                    maKH = dgvDSKhachHang.GetRowCellDisplayText(item, colMaKH).ToString();
                    tenKH = dgvDSKhachHang.GetRowCellDisplayText(item, colTenKH).ToString();
                    ngaySinh = (DateTime)dgvDSKhachHang.GetRowCellValue(item, colNgaySinh);
                    gioiTinh = dgvDSKhachHang.GetRowCellDisplayText(item, colGioiTinh).ToString();
                    cMND = dgvDSKhachHang.GetRowCellDisplayText(item, colCMND).ToString();                  
                    SDT = dgvDSKhachHang.GetRowCellDisplayText(item, colSDT).ToString();
                    diaChi = dgvDSKhachHang.GetRowCellDisplayText(item, colDiaChi).ToString();
                    duongDanHinh = dgvDSKhachHang.GetRowCellDisplayText(item, colHinh).ToString();
                    email = dgvDSKhachHang.GetRowCellDisplayText(item, colEmail).ToString();
                    tinhTrangAsTring = dgvDSKhachHang.GetRowCellDisplayText(item, colTinhTrang).ToString();
                    kh.MaKH = maKH;
                    kh.HoTenKH = tenKH;
                    kh.NgaySinh = ngaySinh;
                    kh.CMND = cMND;
                    kh.GioiTinh = gioiTinh;
                    kh.SDT = SDT;
                    kh.Email = email;
                    kh.DiaChi = diaChi;
                    kh.DuongDanHinh = duongDanHinh;
                    if (tinhTrangAsTring.Equals("Đang hoạt động"))
                    {
                        kh.TinhTrang = 1;
                    }
                    else
                    {
                        kh.TinhTrang = 0;
                    }
                    if (khachHang.suaKhachHang(kh) == false)
                    {
                        MessageBox.Show("Khi sửa khách hàng: " + maKH + " đã lỗi");
                        return;
                    }
                }
            }
            if (listViTriThem.Count > 0)
            {
                foreach (var item in listViTriThem)
                {
                    //GetRowCellDisplayText vì khi có giá trị rỗng sẽ không lỗi
                    int viTriRow = item - 1;//Vì item là đếm phần từ nên vị trí sẽ là trừ 1
                    maKH = dgvDSKhachHang.GetRowCellDisplayText(viTriRow, colMaKH).ToString();
                    tenKH = dgvDSKhachHang.GetRowCellDisplayText(viTriRow, colTenKH).ToString();                  
                    ngaySinh = Commons.ConvertStringToDate(dgvDSKhachHang.GetRowCellDisplayText(viTriRow, colNgaySinh).ToString());
                    gioiTinh = dgvDSKhachHang.GetRowCellDisplayText(viTriRow, colGioiTinh).ToString();
                    cMND = dgvDSKhachHang.GetRowCellDisplayText(viTriRow, colCMND).ToString();
                    SDT = dgvDSKhachHang.GetRowCellDisplayText(viTriRow, colSDT).ToString();
                    diaChi = dgvDSKhachHang.GetRowCellDisplayText(viTriRow, colDiaChi).ToString();
                    tinhTrangAsTring = dgvDSKhachHang.GetRowCellValue(viTriRow, colTinhTrang).ToString();
                    duongDanHinh = dgvDSKhachHang.GetRowCellDisplayText(item, colHinh).ToString();
                    kh.MaKH = maKH;
                    kh.HoTenKH = tenKH;
                    kh.NgaySinh = ngaySinh;
                    kh.CMND = cMND;
                    kh.GioiTinh = gioiTinh;
                    kh.SDT = SDT;
                    kh.DiaChi = diaChi;
                    kh.DuongDanHinh = duongDanHinh;
                    if (tinhTrangAsTring.Equals("Đang hoạt động"))
                    {
                        kh.TinhTrang = 1;
                    }
                    else
                    {
                        kh.TinhTrang = 0;
                    }
                    if (khachHang.themKhachHang(kh) == false)
                    {
                        MessageBox.Show("Khi thêm khách hàng: "+maKH +" đã lỗi");
                        return;
                    }
                }
            }
            listViTriSua.Clear();
            listViTriThem.Clear();
            if(!string.IsNullOrEmpty(duongDanHinh))
            FileUtils.SaveFile(pathHinh, duongDanHinh, pEHinh);
            MessageBox.Show("Lưu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void dgvDSKhachHang_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            string mahk = khachHang.layMaKHTuSinh(); 
           
            dgvDSKhachHang.SetRowCellValue(e.RowHandle, colMaKH, mahk);
           dgvDSKhachHang.SetRowCellValue(e.RowHandle, colNgaySinh, DateTime.Now);
            dgvDSKhachHang.SetRowCellValue(e.RowHandle, colGioiTinh, "Nam");
            /*dgvDSKhachHang.SetRowCellValue(e.RowHandle, colMaKH, mahk);
            dgvDSKhachHang.SetRowCellValue(e.RowHandle, colMaKH, mahk);
            dgvDSKhachHang.SetRowCellValue(e.RowHandle, colMaKH, mahk);
            dgvDSKhachHang.SetRowCellValue(e.RowHandle, colMaKH, mahk);
            dgvDSKhachHang.SetRowCellValue(e.RowHandle, colMaKH, mahk);
            dgvDSKhachHang.SetRowCellValue(e.RowHandle, colMaKH, mahk);*/
            dgvDSKhachHang.FocusedColumn = dgvDSKhachHang.VisibleColumns[1];
            dgvDSKhachHang.ShowEditor();
        }
 

        private void btnTaiLen_Click(object sender, EventArgs e)
        {
            try
            {
                openFile.Filter = "png files(*.png)|*.png|jpg files(*.jpg)|*.jpg|All files (*.*)|*.*";

                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    pathHinh = openFile.FileName;
                    Image img = Image.FromFile(pathHinh);
                    pEHinh.Image = img;
                    dgvDSKhachHang.SetRowCellValue(dgvDSKhachHang.FocusedRowHandle, colHinh, txtMaKH.Text+".PNG");                     
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hãy chọn hình ảnh");
            }
        }

        private void dgvDSKhachHang_FocusedRowChanged_1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            string DuongDanHinh = dgvDSKhachHang.GetFocusedRowCellDisplayText("DuongDanHinh");
            string pathHinh = FileUtils.FolderUploads + "\\" + DuongDanHinh;
            if (!string.IsNullOrEmpty(pathHinh))
            {
                Image img = null;
                FileInfo fileInfo = new FileInfo(pathHinh);
                if (fileInfo.Exists)
                {
                    img = Image.FromFile(pathHinh);

                }
                pEHinh.Image = img;
            }
        }
    }
}