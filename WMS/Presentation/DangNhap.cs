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
    public partial class DangNhap : DevExpress.XtraEditors.XtraForm
    {

        private string TaiKhoan;
        public DangNhap()
        {
            InitializeComponent();
        }

        private void btn_dangnhap_Click(object sender, EventArgs e)
        {
            if (txt_taikhoan.Text == "" || txt_matkhau.Text == "")
            {
                MessageBox.Show("Chưa nhập đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_taikhoan.Focus();
                txt_matkhau.Clear();
            }
            else
            {
                try
                {
                    WMSDataContext dc = new WMSDataContext();
                    var item = (from c in dc.Users
                                where c.UserName == txt_taikhoan.Text
                                && c.Password == txt_matkhau.Text
                                select c).SingleOrDefault();
                    if (item != null)
                    {
                        TaiKhoan = txt_taikhoan.Text;
                        this.Hide();
                        var formmain = new Home(TaiKhoan);
                        formmain.Closed += (s, args) => this.Close();
                        formmain.Show();
                    }
                    else
                    {
                        MessageBox.Show("Tài khoản hoặc mật khẩu không đúng, vui lòng kiểm tra lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt_taikhoan.Focus();
                        txt_matkhau.Clear();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}