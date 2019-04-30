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
    public partial class LoaiRac : DevExpress.XtraEditors.XtraForm
    {
        public LoaiRac()
        {
            InitializeComponent();
        }

        DataTable dt = new DataTable();
        DataRow dr;
        WMSDataContext dc = new WMSDataContext();
        
        private void LoaiRac_Load(object sender, EventArgs e)
        {
            //load gridview
            dt.Columns.Add("WasteCode");
            dt.Columns.Add("WasteName");
            dt.Columns.Add("Type");
            dt.Columns.Add("Unit");
            dt.Columns.Add("Status");
            dt.Columns.Add("Note");
            DisplayData();
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            txtMaRac.Enabled = true;
        }

        public void DisplayData()
        {
            List<Waste> ListWaste = (from c in dc.Wastes
                                     orderby c.WasteCode ascending
                                     select c).ToList();
            dt.Clear();
            foreach (Waste item in ListWaste)
            {
                dr = dt.NewRow();
                dr["WasteCode"] = item.WasteCode;
                dr["WasteName"] = item.WasteName;
                dr["Type"] = item.Type;
                dr["Unit"] = item.Unit;
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
            gridControlLoaiRac.DataSource = dt;
            //Hien thi caption header
            gridViewLoaiRac.Columns["WasteCode"].Caption = "Mã rác";
            gridViewLoaiRac.Columns["WasteName"].Caption = "Tên rác";
            gridViewLoaiRac.Columns["Type"].Caption = "Loại";
            gridViewLoaiRac.Columns["Status"].Caption = "Trạng thái";
            gridViewLoaiRac.Columns["Unit"].Caption = "Đơn vị";
            gridViewLoaiRac.Columns["Note"].Caption = "Ghi chú";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dt.Columns.Remove("WasteCode");
            dt.Columns.Remove("WasteName");
            dt.Columns.Remove("Type");
            dt.Columns.Remove("Status");
            dt.Columns.Remove("Unit");
            dt.Columns.Remove("Note");
            txtMaRac.Text = "";
            txtTenRac.Text = "";
            txtGhiChu.Text = "";
            cboLoaiRac.SelectedIndex = -1;
            txtDonVi.SelectedIndex = -1;
            rbtnKichHoat.Checked = true;
            LoaiRac_Load(sender, e);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTenRac.Text == "" || txtMaRac.Text == "" || cboLoaiRac.SelectedItem.ToString() == ""|| txtDonVi.SelectedItem.ToString() == "")
            {
                MessageBox.Show("Vui lòng điền đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Waste was = new Waste();
                was.WasteCode = txtMaRac.Text;
                was.WasteName = txtTenRac.Text;
                was.Type = cboLoaiRac.SelectedItem.ToString();
                was.Unit = txtDonVi.SelectedItem.ToString();
                if (rbtnKichHoat.Checked == true)
                {
                    was.Status = true;
                }
                else
                {
                    was.Status = false;
                }
                was.Note = txtGhiChu.Text;

                Waste var = (from c in dc.Wastes
                             where c.WasteCode == txtMaRac.Text && c.WasteName == txtTenRac.Text
                             select c).FirstOrDefault();
                if (var == null)
                {
                    try
                    {
                        dc.Wastes.InsertOnSubmit(was);
                        MessageBox.Show("Thêm mới loại rác thành công", "Thông báo");
                        dc.SubmitChanges();
                        txtMaRac.Text = "";
                        txtTenRac.Text = "";
                        cboLoaiRac.SelectedIndex = -1;
                        txtDonVi.SelectedIndex = -1;
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
                    MessageBox.Show("Loại rác thêm đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void gridViewLoaiRac_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            txtMaRac.Text = gridViewLoaiRac.GetRowCellValue(e.RowHandle, "WasteCode").ToString();
            txtTenRac.Text = gridViewLoaiRac.GetRowCellValue(e.RowHandle, "WasteName").ToString();
            cboLoaiRac.SelectedItem = gridViewLoaiRac.GetRowCellValue(e.RowHandle, "Type").ToString();
            txtDonVi.SelectedItem = gridViewLoaiRac.GetRowCellValue(e.RowHandle, "Unit").ToString();
            txtGhiChu.Text = gridViewLoaiRac.GetRowCellValue(e.RowHandle, "Note").ToString();
            if (gridViewLoaiRac.GetRowCellValue(e.RowHandle, "Status").ToString() == "Kích hoạt")
            {
                rbtnKichHoat.Checked = true;
            }
            else
            {
                rbtnKhoa.Checked = true;
            }
            txtMaRac.Enabled = false;
            btnSua.Enabled = true;
            btnThem.Enabled = false;
        }

        private void gridViewLoaiRac_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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
            if (txtTenRac.Text == "" || txtMaRac.Text == ""|| cboLoaiRac.SelectedItem.ToString() == ""|| txtDonVi.SelectedItem.ToString() == "")
            {
                MessageBox.Show("Vui lòng điền đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Waste was = (from c in dc.Wastes
                             where c.WasteCode == txtMaRac.Text
                               select c).FirstOrDefault();

                if (was != null)
                {
                    was.WasteName = txtTenRac.Text;
                    was.Note = txtGhiChu.Text;
                    was.Type = cboLoaiRac.SelectedItem.ToString();
                    was.Unit = txtDonVi.SelectedItem.ToString();
                    if (rbtnKichHoat.Checked == true)
                    {
                        was.Status = true;
                    }
                    else
                    {
                        was.Status = false;
                    }
                }
                try
                {
                    dc.SubmitChanges();
                    MessageBox.Show("Cập nhật thông tin loại rác thành công", "Thông báo");
                    txtMaRac.Text = "";
                    txtTenRac.Text = "";
                    cboLoaiRac.SelectedIndex = -1;
                    txtDonVi.SelectedIndex = -1;
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