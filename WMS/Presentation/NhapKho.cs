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
    public partial class NhapKho : DevExpress.XtraEditors.XtraForm
    {
        private string user;
        public NhapKho()
        {
            InitializeComponent();
        }

        public NhapKho(string taikhoan):this()
        {
            user = taikhoan;
        }

        DataTable dt = new DataTable();
        DataRow dr;
        WMSDataContext dc = new WMSDataContext();

        private void NhapKho_Load(object sender, EventArgs e)
        {
            //load combox xuong, kho, loai rac
            List<Factory> ListFactory = (from f in dc.Factories where f.Status==true
                                     select f).ToList();
            cbXuong.Properties.DataSource = ListFactory;
            cbXuong.Properties.DisplayMember = "FactoryName";
            cbXuong.Properties.ValueMember = "FactoryCode";

            List<Storage> ListStorage = (from s in dc.Storages where s.Status == true
                                       select s).ToList();
            cbKho.Properties.DataSource = ListStorage;
            cbKho.Properties.DisplayMember = "StorageName";
            cbKho.Properties.ValueMember = "StorageCode";

            List<Waste> ListWaste = (from w in dc.Wastes where w.Status == true
                                     select w).ToList();
            cbTenRac.Properties.DataSource = ListWaste;
            cbTenRac.Properties.DisplayMember = "WasteName";
            cbTenRac.Properties.ValueMember = "WasteCode";

            //chi lay mot truong name trong lookupedit
            //List<Factory> listfactory = new List<Factory>();
            //List<Storage> liststorage = new List<Storage>();
            //List<Waste> listwaste = new List<Waste>();
            //foreach (Factory row in ListFactory)
            //{
            //    Factory f = new Factory();
            //    f.FactoryName = row.FactoryName;
            //    listfactory.Add(f);
            //}
            //cbXuong.Properties.DataSource = listfactory;
            //cbXuong.Properties.DisplayMember = "FactoryName";
            //cbXuong.Properties.ValueMember = "FactoryCode";

            datePN.EditValue = DateTime.Now;
            //lay ten user
            txtUser.Text = user;
            //enable cac truong bind
            txtUser.Enabled = false;
            txtTenNV.Enabled = false;
            txtPhongBan.Enabled = false;
            //xu ly text change ma nhan vien de bind ten nhan vien va phong ban
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //Clear thong tin de tao phien nhap moi
        }

        private void btnLưu_Click(object sender, EventArgs e)
        {
            // luu thong tin phien, trang thai barcode chuyen sang chua nhap kho
            if (txtMaPN.Text == "" || txtMaNV.Text == "")
            {
                MessageBox.Show("Vui lòng điền đủ thông tin phiên nhập kho", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

            }
        }

        private void txtMaNV_TextChanged(object sender, EventArgs e)
        {
            txtPhongBan.Text = "";
            txtTenNV.Text = "";
            WMSDataContext dc = new WMSDataContext();
            Employee var = (from c in dc.Employees
                            where c.EmployeeCode == txtMaNV.Text
                        select c).SingleOrDefault();
            if (var != null)
            {
                txtTenNV.Text = var.Name;
                txtPhongBan.Text = var.Dept;
            }
        }
    }
}