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
using DataAcess;

namespace Presentation
{
    public partial class DoiMatKhau : DevExpress.XtraEditors.XtraForm
    {
        private string taikhoan;
        public DoiMatKhau()
        {
            InitializeComponent();
        }
        public DoiMatKhau(string Taikhoan) : this()
        {
            taikhoan = Taikhoan;
        }

        private void btnDoiMK_Click(object sender, EventArgs e)
        {
            WMSDataContext dc = new WMSDataContext();
            User var = (from c in dc.Users
                        where c.UserName == taikhoan
                        select c).SingleOrDefault();
            if (var.Password != txtMKcu.Text)
            {
                MessageBox.Show("mật khẩu cũ không đúng, vui lòng kiểm tra lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMKcu.Text = "";
                txtMKcu.Focus();
                txtMKmoi.Text = "";
                txtMKmoixacnhan.Text = "";
            }
            else if (txtMKmoi.Text == "" || txtMKmoixacnhan.Text == "")
            {
                MessageBox.Show("Chưa nhập đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtMKmoi.Text != txtMKmoixacnhan.Text)
            {
                MessageBox.Show("xác nhận mật khẩu không đúng, vui lòng kiểm tra lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMKcu.Text = "";
                txtMKcu.Focus();
                txtMKmoi.Text = "";
                txtMKmoixacnhan.Text = "";
            }
            else
            {
                try
                {
                    var.Password = txtMKmoi.Text;
                    dc.SubmitChanges();
                    MessageBox.Show("Đổi mật khẩu thành công", "Thông báo", MessageBoxButtons.OK);
                    txtMKcu.Text = "";
                    txtMKmoi.Text = "";
                    txtMKmoixacnhan.Text = "";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}