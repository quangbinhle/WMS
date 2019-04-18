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
    public partial class Kho : DevExpress.XtraEditors.XtraForm
    {
        public Kho()
        {
            InitializeComponent();
        }

        DataTable dt = new DataTable();
        DataRow dr;
        WMSDataContext dc = new WMSDataContext();

        private void Kho_Load(object sender, EventArgs e)
        {
            //load gridview
            dt.Columns.Add("StorageCode");
            dt.Columns.Add("StorageName");
            dt.Columns.Add("Type");
            dt.Columns.Add("Status");
            dt.Columns.Add("Note");
            DisplayData();
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            txtMaKho.Enabled = true;
        }

        public void DisplayData()
        {
            List<Storage> ListStorage = (from c in dc.Storages
                                         orderby c.StorageCode ascending
                                         select c).ToList();
            dt.Clear();
            foreach (Storage item in ListStorage)
            {
                dr = dt.NewRow();
                dr["StorageCode"] = item.StorageCode;
                dr["StorageName"] = item.StorageName;
                dr["Type"] = item.Type;
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
            gridControlKho.DataSource = dt;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dt.Columns.Remove("StorageCode");
            dt.Columns.Remove("StorageName");
            dt.Columns.Remove("Type");
            dt.Columns.Remove("Status");
            dt.Columns.Remove("Note");
            txtMaKho.Text = "";
            txtTenKho.Text = "";
            txtGhiChu.Text = "";
            cbLoaiKho.SelectedIndex = -1;
            rbtnKichHoat.Checked = true;
            Kho_Load(sender, e);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTenKho.Text == "" || txtMaKho.Text == ""||cbLoaiKho.SelectedItem.ToString()=="")
            {
                MessageBox.Show("Vui lòng điền đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Storage sto = new Storage();
                sto.StorageCode = txtMaKho.Text;
                sto.StorageName = txtTenKho.Text;
                sto.Type = cbLoaiKho.SelectedItem.ToString();
                if (rbtnKichHoat.Checked == true)
                {
                    sto.Status = true;
                }
                else
                {
                    sto.Status = false;
                }
                sto.Note = txtGhiChu.Text;

                Storage var = (from c in dc.Storages
                               where c.StorageCode == txtMaKho.Text && c.StorageName == txtTenKho.Text
                               select c).FirstOrDefault();
                if (var == null)
                {
                    try
                    {
                        dc.Storages.InsertOnSubmit(sto);
                        MessageBox.Show("Thêm mới kho thành công", "Thông báo");
                        dc.SubmitChanges();
                        txtMaKho.Text = "";
                        txtTenKho.Text = "";
                        cbLoaiKho.SelectedIndex = -1;
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
                    MessageBox.Show("Kho thêm đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void gridViewKho_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            
            txtMaKho.Text = gridViewKho.GetRowCellValue(e.RowHandle, "StorageCode").ToString();
            txtTenKho.Text = gridViewKho.GetRowCellValue(e.RowHandle, "StorageName").ToString();
            cbLoaiKho.SelectedItem= gridViewKho.GetRowCellValue(e.RowHandle, "Type").ToString();
            txtGhiChu.Text = gridViewKho.GetRowCellValue(e.RowHandle, "Note").ToString();
            if (gridViewKho.GetRowCellValue(e.RowHandle, "Status").ToString() == "Kích hoạt")
            {
                rbtnKichHoat.Checked = true;
            }
            else
            {
                rbtnKhoa.Checked = true;
            }
            txtMaKho.Enabled = false;
            btnSua.Enabled = true;
            btnThem.Enabled = false;
        }

        private void gridViewKho_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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
           
            if (txtTenKho.Text == "" || txtMaKho.Text == ""||cbLoaiKho.SelectedItem.ToString() == "")
            {
                MessageBox.Show("Vui lòng điền đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Storage var = (from c in dc.Storages
                               where c.StorageCode == txtMaKho.Text
                               select c).FirstOrDefault();

                if (var != null)
                {
                    var.StorageName = txtTenKho.Text;
                    var.Note = txtGhiChu.Text;
                    var.Type = cbLoaiKho.SelectedItem.ToString();
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
                    MessageBox.Show("Cập nhật thông tin kho thành công", "Thông báo");
                    txtMaKho.Text = "";
                    txtTenKho.Text = "";
                    cbLoaiKho.SelectedIndex = -1;
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