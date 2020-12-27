﻿using System;
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
using Liz.DoAn;

namespace QL_MatBangTTTM
{
    public partial class FrmTaoHoaDon : DevExpress.XtraEditors.XtraForm
    {
        BLL_ThueMatBang thueMB = new BLL_ThueMatBang();
        string maHD = "";
        string maDK = "";
        int trangThai=0;
        public FrmTaoHoaDon(string maMB,string maDK)
        {
            InitializeComponent();
            txtMatBang.Text = maMB;
            this.maDK = maDK;
        }

        private void FrmTaoHoaDon_Load(object sender, EventArgs e)
        {
            txtMaHoaDon.Text = thueMB.LayMaHoaDonTuSinh();
            txtNgayLap.EditValue = DateTime.Now;
            DateTime ngayHetHanDong = DateTime.Today.AddDays(3);
            DateTime NgayHetHieuLuc = DateTime.Today.AddDays(15);
            txtNgayHetHanDong.EditValue = ngayHetHanDong;
            txtNgayHetHieuLuc.EditValue = NgayHetHieuLuc;
            txtTienCoc.Text = String.Format("{0:0,0 vnđ}", thueMB.TinhTienCoc(txtMatBang.Text));
            cboTrangThai.SelectedIndex = 1;
        }
        public string MaHoaDon()
        {
            return maHD;
        }

        private void btnNhapLai_Click(object sender, EventArgs e)
        {
            txtTienCoc.Text = "";

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmTaoHoaDon_FormClosing(object sender, FormClosingEventArgs e)
        {
           /* DialogResult r;
            r = MessageBox.Show("Bạn có chắc muốn thoát không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (r == DialogResult.No)
            {
                e.Cancel = true;
            }*/
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            HoaDonTienCoc hd = new HoaDonTienCoc();
            hd.MaHD = txtMaHoaDon.Text;          
            hd.NgayLap = (DateTime)txtNgayLap.EditValue;
            if(txtNgayDong.EditValue!=null)
            {
                hd.NgayDong = (DateTime)txtNgayDong.EditValue;
                trangThai = 1;
            }
            hd.SoTien = (int)thueMB.TinhTienCoc(txtMatBang.Text);
            hd.NgayHetHanDong = (DateTime)txtNgayHetHanDong.EditValue;
            hd.NgayHetHan = (DateTime)txtNgayHetHieuLuc.EditValue;
            hd.TrangThai = trangThai;
            hd.MaDK = maDK;
            if(thueMB.ThemHoaDon(hd))
            {
                MessageBox.Show("Thêm hóa đơn thành công");
                maHD = hd.MaHD;
                this.Close();
            }    
        }
        public int TrangThai() {
            return trangThai;
        } 

        private void txtNgayDong_EditValueChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtNgayDong.Text))
            {
                DateTime ngaydong = (DateTime)txtNgayDong.EditValue;
                TimeSpan ktNgay = ngaydong - DateTime.Now;
                if (ktNgay.Days < 0)
                {
                    MessageBox.Show("Ngày đóng phải lớn hơn hoặc bằng ngày lập hóa đơn");
                    txtNgayDong.Text = "";
                    txtNgayDong.Focus();
                    return;
                }
                if (ngaydong != null)
                {
                    cboTrangThai.SelectedIndex = 0;
                }
                else
                {
                    cboTrangThai.SelectedIndex = 1;
                }
            }    
            
        }
    }
}