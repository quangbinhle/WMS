using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Presentation
{
    public partial class Home : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private string ten;
        public Home()
        {
            InitializeComponent();
        }

        public Home(string tennguoidung) : this()
        {
            ten = tennguoidung;
            lbl_ten.Text = ten;
        }

        public void ViewChildForm(Form _form)
        {
            if (!IsFormAcived(_form))
            {
                _form.MdiParent = this;
                _form.Show();
            }
        }

        private bool IsFormAcived(Form form)
        {
            bool IsOpenend = false;
            if (MdiChildren.Count() > 0)
            {
                foreach (var item in MdiChildren)
                {
                    if (form.Name == item.Name)
                    {
                        xtraTabbedMdiManager1.Pages[item].MdiChild.Activate();
                        IsOpenend = true;
                    }
                }
            }
            return IsOpenend;
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoiMatKhau frmDoiMK = new DoiMatKhau(ten);
            frmDoiMK.Name = "DoiMatKhau";
            ViewChildForm(frmDoiMK);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
            var flogin = new DangNhap();
            flogin.Closed += (s, args) => this.Close();
            flogin.Show();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Xuong frmXuong = new Xuong();
            frmXuong.Name = "Xuong";
            ViewChildForm(frmXuong);
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Kho frmKho = new Kho();
            frmKho.Name = "Kho";
            ViewChildForm(frmKho);
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoaiRac frmLoaiRac = new LoaiRac();
            frmLoaiRac.Name = "LoaiRac";
            ViewChildForm(frmLoaiRac);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NhapKho frmNhapKho = new NhapKho(ten);
            frmNhapKho.Name = "NhapKho";
            ViewChildForm(frmNhapKho);
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            QLNhapKho frmQLNhapKho = new QLNhapKho();
            frmQLNhapKho.Name = "QLNhapKho";
            ViewChildForm(frmQLNhapKho);
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XuatKho frmXuatKho = new XuatKho();
            frmXuatKho.Name = "XuatKho";
            ViewChildForm(frmXuatKho);
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            QLXuatKho frmQLXuatKho = new QLXuatKho();
            frmQLXuatKho.Name = "QLXuatKho";
            ViewChildForm(frmQLXuatKho);
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TonKho frmTonKho = new TonKho();
            frmTonKho.Name = "TonKho";
            ViewChildForm(frmTonKho);
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaoCaoThang frmBaoCaoThang = new BaoCaoThang();
            frmBaoCaoThang.Name = "BaoCaoThang";
            ViewChildForm(frmBaoCaoThang);
        }
    }
}
