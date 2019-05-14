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
using DevExpress.XtraGrid.Columns;

namespace Presentation
{
    public partial class QLNhapKho : DevExpress.XtraEditors.XtraForm
    {
        public QLNhapKho()
        {
            InitializeComponent();
        }

        DataTable dt = new DataTable();
        DataRow dr;
        WMSDataContext dc = new WMSDataContext();

        private void gridViewQLNhapKho_DoubleClick(object sender, EventArgs e)
        {
            
            int RowIndex = gridViewQLNhapKho.FocusedRowHandle;
            string stockincode = gridViewQLNhapKho.GetRowCellValue(RowIndex, "StockInCode").ToString();
            StockIn si = dc.StockIns.Where(x => x.StockInCode == stockincode).SingleOrDefault();
            //goi su kien click form(show form) => biding du lieu ra form
            //binding du lieu phien
            NhapKho nhapkho = new NhapKho();
            nhapkho.Show();
            nhapkho.txtMaPN.Text = stockincode;
            nhapkho.datePN.EditValue = si.DateIn;
            nhapkho.txtMaNV.Text = si.EmployeeCode;
            nhapkho.txtUser.Text = si.UserID;
            nhapkho.txtGhiChuPhien.Text = si.Note;
            nhapkho.txtTongTrongLuong.Text = si.TotalWeight.ToString();
            nhapkho.txtSoLuong.Text = si.Quantity.ToString();
            //binding du lieu luoi
            var model = (from sid in dc.StockInDetails
                         join bar in dc.BarcodeDetails on sid.Barcode equals bar.Barcode
                         where (sid.StockInCode == stockincode)
                         select new
                         {
                             bar.Barcode,
                             bar.WasteCode,
                             bar.FactoryCode,
                             bar.StorageCode,
                             bar.Weigh,
                             bar.Note,
                         }).ToList();
            //tao moi datatable va datarow
            DataTable datatable = new DataTable();
            DataRow datarow;
            //addcolum
            datatable.Columns.Add("Barcode");
            datatable.Columns.Add("TrongLuong");
            datatable.Columns.Add("Xuong");
            datatable.Columns.Add("Kho");
            datatable.Columns.Add("TenRac");
            datatable.Columns.Add("GhiChu");
            datatable.Columns.Add("DonVi");
            foreach (var item1 in model)
            {
                datarow = datatable.NewRow();
                datarow["Barcode"] = item1.Barcode;
                datarow["TrongLuong"] = item1.Weigh;
                datarow["Xuong"] = item1.FactoryCode;
                datarow["Kho"] = item1.StorageCode;
                datarow["TenRac"] = item1.WasteCode;
                datarow["GhiChu"] = item1.Note;
                Waste var = (from c in dc.Wastes
                             where c.WasteCode == item1.WasteCode
                             select c).FirstOrDefault();
                datarow["DonVi"] = var.Unit;
                datatable.Rows.Add(datarow);
            }
            nhapkho.gridControlBarcode.DataSource = datatable;
        }

        private void QLNhapKho_Load(object sender, EventArgs e)
        {
            //Tao cac cot trong luoi
            dt.Columns.Add("StockInCode");
            dt.Columns.Add("User");
            dt.Columns.Add("EmployeeCode");
            dt.Columns.Add("Name");
            dt.Columns.Add("Dept");
            dt.Columns.Add("DateIn");
            dt.Columns.Add("TotalWeigh");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Note");

            DisplayData();
        }

        public void DisplayData()
        {
            var model = (from si in dc.StockIns
                         join e in dc.Employees
  on si.EmployeeCode equals e.EmployeeCode
                         select new
                         {
                             si.StockInCode,
                             si.UserID,
                             si.EmployeeCode,
                             e.Name,
                             e.Dept,
                             si.DateIn,
                             si.TotalWeight,
                             si.Quantity,
                             si.Note
                         }).OrderByDescending(x => x.DateIn).ToList();

            dt.Clear();
            foreach (var item in model)
            {
                dr = dt.NewRow();
                dr["StockInCode"] = item.StockInCode;
                dr["User"] = item.UserID;
                dr["EmployeeCode"] = item.EmployeeCode;
                dr["Name"] = item.Name;
                dr["Dept"] = item.Dept;
                dr["DateIn"] = item.DateIn;
                dr["TotalWeigh"] = item.TotalWeight;
                dr["Quantity"] = item.Quantity;
                dr["Note"] = item.Note;
                dt.Rows.Add(dr);
            }
            GridQLNhapKho.DataSource = dt;
            //Hien thi caption header
            gridViewQLNhapKho.Columns["StockInCode"].Caption = "Mã phiên nhập";
            gridViewQLNhapKho.Columns["User"].Caption = "Người nhập";
            gridViewQLNhapKho.Columns["EmployeeCode"].Caption = "Mã nhân viên";
            gridViewQLNhapKho.Columns["Name"].Caption = "Tên nhân viên";
            gridViewQLNhapKho.Columns["Dept"].Caption = "Phòng ban";
            gridViewQLNhapKho.Columns["DateIn"].Caption = "Ngày nhập";
            gridViewQLNhapKho.Columns["TotalWeigh"].Caption = "Tổng trọng lượng";
            gridViewQLNhapKho.Columns["Quantity"].Caption = "Số lượng";
            gridViewQLNhapKho.Columns["Note"].Caption = "Ghi chú";

            double tongtrongluong = 0;
            for (int y = 0; y < gridViewQLNhapKho.RowCount; y++)
            {
                tongtrongluong = tongtrongluong + double.Parse(gridViewQLNhapKho.GetRowCellValue(y, "TotalWeigh").ToString());
            }
            txtTongTrongLuong.Text = tongtrongluong.ToString();
            txtSoLuong.Text = gridViewQLNhapKho.RowCount.ToString();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dateDenNgay.Text = "";
            dateTuNgay.Text = "";
            txtTongTrongLuong.Text = "";
            txtSoLuong.Text = "";
            DisplayData();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            int compare = DateTime.Compare(Convert.ToDateTime(dateTuNgay.EditValue), Convert.ToDateTime(dateDenNgay.EditValue));
            if (compare < 0)
            {
                var model = (from si in dc.StockIns
                             join em in dc.Employees
                             on si.EmployeeCode equals em.EmployeeCode
                             where (Convert.ToDateTime(dateTuNgay.EditValue) < si.DateIn && si.DateIn < Convert.ToDateTime(dateDenNgay.EditValue))
                             select new
                             {
                                 si.StockInCode,
                                 si.UserID,
                                 si.EmployeeCode,
                                 em.Name,
                                 em.Dept,
                                 si.DateIn,
                                 si.TotalWeight,
                                 si.Quantity,
                                 si.Note
                             }).OrderByDescending(x => x.DateIn).ToList();

                dt.Clear();
                foreach (var item in model)
                {
                    dr = dt.NewRow();
                    dr["StockInCode"] = item.StockInCode;
                    dr["User"] = item.UserID;
                    dr["EmployeeCode"] = item.EmployeeCode;
                    dr["Name"] = item.Name;
                    dr["Dept"] = item.Dept;
                    dr["DateIn"] = item.DateIn;
                    dr["TotalWeigh"] = item.TotalWeight;
                    dr["Quantity"] = item.Quantity;
                    dr["Note"] = item.Note;
                    dt.Rows.Add(dr);
                }
                GridQLNhapKho.DataSource = dt;

                double tongtrongluong = 0;
                for (int y = 0; y < gridViewQLNhapKho.RowCount; y++)
                {
                    tongtrongluong = tongtrongluong + double.Parse(gridViewQLNhapKho.GetRowCellValue(y, "TotalWeigh").ToString());
                }
                txtTongTrongLuong.Text = tongtrongluong.ToString();
                txtSoLuong.Text = gridViewQLNhapKho.RowCount.ToString();
            }
            else
            {
                MessageBox.Show("Ngày kết thúc lớn hơn ngày bắt đầu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridViewQLNhapKho_ColumnFilterChanged(object sender, EventArgs e)
        {
            double tongtrongluong = 0;
            for (int y = 0; y < gridViewQLNhapKho.RowCount; y++)
            {
                tongtrongluong = tongtrongluong + double.Parse(gridViewQLNhapKho.GetRowCellValue(y, "TotalWeigh").ToString());
            }
            txtTongTrongLuong.Text = tongtrongluong.ToString();
            txtSoLuong.Text = gridViewQLNhapKho.RowCount.ToString();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            //Creating DataTable
            DataTable dt = new DataTable();

            //Adding the Columns with gridview devexpress
            foreach (GridColumn c in gridViewQLNhapKho.Columns)
            {
                dt.Columns.Add(c.FieldName, c.ColumnType);
            }

            //Adding the Rows with gridview devexpress
            for (int r = 0; r < gridViewQLNhapKho.RowCount; r++)
            {
                object[] rowValues = new object[dt.Columns.Count];
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    rowValues[c] = gridViewQLNhapKho.GetRowCellValue(r, dt.Columns[c].ColumnName);
                }
                dt.Rows.Add(rowValues);
            }

            //Exporting to Excel
            SaveFileDialog fsave = new SaveFileDialog();
            //kiem tra duoi 
            fsave.Filter = "Excel|*.xls|Excel 2010|*.xlsx";
            fsave.ShowDialog();

            if (fsave.FileName != null)
            {
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel._Worksheet sheet = null;
                try
                {
                    sheet = wb.ActiveSheet;
                    sheet.Name = "WMS_StockInManagement";
                    for (int i = 1; i <= dt.Columns.Count; i++)
                    {
                        sheet.Cells[1, i] = dt.Columns[i - 1].Caption;
                    }
                    for (int i = 1; i <= dt.Rows.Count; i++)
                    {
                        sheet.Cells[1 + i, 1] = dt.Rows[i - 1].ItemArray[0];
                        sheet.Cells[1 + i, 2] = dt.Rows[i - 1].ItemArray[1];
                        sheet.Cells[1 + i, 3] = dt.Rows[i - 1].ItemArray[2];
                        sheet.Cells[1 + i, 4] = dt.Rows[i - 1].ItemArray[3];
                        sheet.Cells[1 + i, 5] = dt.Rows[i - 1].ItemArray[4];
                        sheet.Cells[1 + i, 6] = dt.Rows[i - 1].ItemArray[5];
                        sheet.Cells[1 + i, 7] = dt.Rows[i - 1].ItemArray[6];
                        sheet.Cells[1 + i, 8] = dt.Rows[i - 1].ItemArray[7];
                        sheet.Cells[1 + i, 9] = dt.Rows[i - 1].ItemArray[8];
                        //sheet.Cells[1 + i, 10] = dt.Rows[i - 1].ItemArray[9];
                        //sheet.Cells[1 + i, 11] = dt.Rows[i - 1].ItemArray[10];

                    }
                    sheet.Columns.AutoFit();
                    sheet.Rows.AutoFit();
                    wb.SaveAs(fsave.FileName);
                    MessageBox.Show("Xuất dữ liệu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally
                {
                    app.Quit();
                    wb = null;
                }
            }
        }
    }
}