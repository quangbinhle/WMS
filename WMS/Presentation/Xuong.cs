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
using DevExpress.XtraGrid.Views.Grid;

namespace Presentation
{
    public partial class Xuong : DevExpress.XtraEditors.XtraForm
    {
        public Xuong()
        {
            InitializeComponent();
        }

        DataTable dt = new DataTable();
        DataRow dr;
        WMSDataContext dc = new WMSDataContext();

        private void Xuong_Load(object sender, EventArgs e)
        {
            //load gridview
            dt.Columns.Add("FactoryCode");
            dt.Columns.Add("FactoryName");
            dt.Columns.Add("Status");
            dt.Columns.Add("Note");
            DisplayData();
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            txtMaXuong.Enabled = true;
        }
        public void DisplayData()
        {
            List<Factory> ListFactory = (from c in dc.Factories
                                         orderby c.FactoryCode ascending
                                         select c).ToList();
            dt.Clear();
            foreach (Factory item in ListFactory)
            {
                dr = dt.NewRow();
                dr["FactoryCode"] = item.FactoryCode;
                dr["FactoryName"] = item.FactoryName;
                if (item.Status == true)
                {
                    dr["Status"] = "Kích hoạt";
                }
                else
                {
                    dr["Status"] = "Khóa";
                }
                dr["Note"] = item.Note;
                dt.Rows.Add(dr);
            }
            gridControlXuong.DataSource = dt;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dt.Columns.Remove("FactoryCode");
            dt.Columns.Remove("FactoryName");
            dt.Columns.Remove("Status");
            dt.Columns.Remove("Note");
            txtMaXuong.Text = "";
            txtTenXuong.Text = "";
            txtGhiChu.Text = "";
            rbtnKichHoat.Checked = true;
            Xuong_Load(sender, e);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTenXuong.Text == "" || txtMaXuong.Text == "")
            {
                MessageBox.Show("Vui lòng điền đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Factory f = new Factory();
                f.FactoryCode = txtMaXuong.Text;
                f.FactoryName = txtTenXuong.Text;
                if (rbtnKichHoat.Checked == true)
                {
                    f.Status = true;
                }
                else
                {
                    f.Status = false;
                }
                f.Note = txtGhiChu.Text;

                Factory var = (from c in dc.Factories
                               where c.FactoryCode == txtMaXuong.Text && c.FactoryName == txtTenXuong.Text
                               select c).FirstOrDefault();
                if (var == null)
                {
                    try
                    {
                        dc.Factories.InsertOnSubmit(f);
                        MessageBox.Show("Thêm mới xưởng thành công", "Thông báo");
                        dc.SubmitChanges();
                        txtMaXuong.Text = "";
                        txtTenXuong.Text = "";
                        txtGhiChu.Text = "";
                        rbtnKichHoat.Checked = true;
                        DisplayData();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    MessageBox.Show("Xưởng thêm đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void gridViewXuong_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            txtMaXuong.Text = gridViewXuong.GetRowCellValue(e.RowHandle, "FactoryCode").ToString();
            txtTenXuong.Text = gridViewXuong.GetRowCellValue(e.RowHandle, "FactoryName").ToString();
            txtGhiChu.Text = gridViewXuong.GetRowCellValue(e.RowHandle, "Note").ToString();
            if (gridViewXuong.GetRowCellValue(e.RowHandle, "Status").ToString() == "Kích hoạt")
            {
                rbtnKichHoat.Checked = true;
            }
            else
            {
                rbtnKhoa.Checked = true;
            }
            txtMaXuong.Enabled = false;
            btnSua.Enabled = true;
            btnThem.Enabled = false;
        }

        private void gridViewXuong_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

            GridView currentView = sender as GridView;
            if (e.Column.FieldName == "Status")
            {
                if (currentView.GetRowCellValue(e.RowHandle, "Status").ToString() == "Kích hoạt")
                {
                    e.Appearance.ForeColor = Color.Green;
                }
                else
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            
            if (txtTenXuong.Text == "" || txtMaXuong.Text == "")
            {
                MessageBox.Show("Vui lòng điền đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Factory var = (from c in dc.Factories
                               where c.FactoryCode == txtMaXuong.Text
                               select c).FirstOrDefault();

                if (var != null)
                {
                    var.FactoryName = txtTenXuong.Text;
                    var.Note = txtGhiChu.Text;
                    if (rbtnKichHoat.Checked == true)
                    {
                        var.Status = true;
                    }
                    else
                    {
                        var.Status = false;
                    }
                }
                try
                {
                    dc.SubmitChanges();
                    MessageBox.Show("Cập nhật thông tin xưởng thành công", "Thông báo");
                    txtMaXuong.Text = "";
                    txtTenXuong.Text = "";
                    txtGhiChu.Text = "";
                    rbtnKichHoat.Checked = true;
                    DisplayData();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}