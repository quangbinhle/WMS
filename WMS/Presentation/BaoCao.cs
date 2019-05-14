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
    public partial class BaoCao : DevExpress.XtraEditors.XtraForm
    {
        public BaoCao()
        {
            InitializeComponent();
        }

        DataTable dt = new DataTable();
        DataRow dr = null;

        DataTable datatable = new DataTable();
        DataRow datarow = null;
        //datatable cho bang stockin        
        WMSDataContext dc = new WMSDataContext();

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (rbtnNhapKho.Checked == true && rbtnXuatKho.Checked == false)
            {
                GridBaoCao.DataSource = null;
                gridViewBaoCao.Columns.Clear();

                if (dt.Columns.Count > 0)
                {
                    //remove cot datatable
                    dt.Columns.Remove("StockInCode");
                    dt.Columns.Remove("User");
                    dt.Columns.Remove("EmployeeCode");
                    dt.Columns.Remove("Name");
                    dt.Columns.Remove("Dept");
                    dt.Columns.Remove("DateIn");
                    dt.Columns.Remove("TotalWeigh");
                    dt.Columns.Remove("Quantity");
                    dt.Columns.Remove("Note");
                }

                //load nhung phien nhap kho

                //tao cot
                dt.Columns.Add("StockInCode");
                dt.Columns.Add("User");
                dt.Columns.Add("EmployeeCode");
                dt.Columns.Add("Name");
                dt.Columns.Add("Dept");
                dt.Columns.Add("DateIn");
                dt.Columns.Add("TotalWeigh");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("Note");
                GridBaoCao.DataSource = dt;

                //DisplayData
                DisplayDataStockIn(dt, dr);

                //tao caption
                gridViewBaoCao.Columns["StockInCode"].Caption = "Mã phiên nhập";
                gridViewBaoCao.Columns["User"].Caption = "Người nhập";
                gridViewBaoCao.Columns["EmployeeCode"].Caption = "Mã nhân viên";
                gridViewBaoCao.Columns["Name"].Caption = "Tên nhân viên";
                gridViewBaoCao.Columns["Dept"].Caption = "Phòng ban";
                gridViewBaoCao.Columns["DateIn"].Caption = "Ngày nhập";
                gridViewBaoCao.Columns["TotalWeigh"].Caption = "Tổng trọng lượng";
                gridViewBaoCao.Columns["Quantity"].Caption = "Số lượng";
                gridViewBaoCao.Columns["Note"].Caption = "Ghi chú";

            }
            if (rbtnNhapKho.Checked == false && rbtnXuatKho.Checked == true)
            {
                GridBaoCao.DataSource = null;
                gridViewBaoCao.Columns.Clear();
                //load nhung phien xuat kho     
                if (datatable.Columns.Count > 0)
                {
                    //remove cot
                    datatable.Columns.Remove("StockOutCode");
                    datatable.Columns.Remove("User");
                    datatable.Columns.Remove("DateOut");
                    datatable.Columns.Remove("RecipientName");
                    datatable.Columns.Remove("IDCard");
                    datatable.Columns.Remove("Company");
                    datatable.Columns.Remove("TotalWeigh");
                    datatable.Columns.Remove("Quantity");
                    datatable.Columns.Remove("Note");
                }

                //tao cot
                datatable.Columns.Add("StockOutCode");
                datatable.Columns.Add("User");
                datatable.Columns.Add("DateOut");
                datatable.Columns.Add("RecipientName");
                datatable.Columns.Add("IDCard");
                datatable.Columns.Add("Company");
                datatable.Columns.Add("TotalWeigh");
                datatable.Columns.Add("Quantity");
                datatable.Columns.Add("Note");

                GridBaoCao.DataSource = datatable;

                //DisplayData
                DisplayDataStockOut(datatable, datarow);

                //tao caption
                //Hien thi caption header
                gridViewBaoCao.Columns["StockOutCode"].Caption = "Mã phiên xuất";
                gridViewBaoCao.Columns["User"].Caption = "Người nhập";
                gridViewBaoCao.Columns["DateOut"].Caption = "Ngày xuất";
                gridViewBaoCao.Columns["RecipientName"].Caption = "Tên người nhận";
                gridViewBaoCao.Columns["IDCard"].Caption = "CMND";
                gridViewBaoCao.Columns["Company"].Caption = "Công ty xử lý";
                gridViewBaoCao.Columns["TotalWeigh"].Caption = "Tổng trọng lượng";
                gridViewBaoCao.Columns["Quantity"].Caption = "Số lượng";
                gridViewBaoCao.Columns["Note"].Caption = "Ghi chú";

            }
        }

        public void DisplayDataStockIn(DataTable dt, DataRow dr)
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
            GridBaoCao.DataSource = dt;

            double tongtrongluong = 0;
            for (int y = 0; y < gridViewBaoCao.RowCount; y++)
            {
                tongtrongluong = tongtrongluong + double.Parse(gridViewBaoCao.GetRowCellValue(y, "TotalWeigh").ToString());
            }
            txtTongTrongLuong.Text = tongtrongluong.ToString();
            txtSoLuong.Text = gridViewBaoCao.RowCount.ToString();
        }

        public void DisplayDataStockOut(DataTable datatable, DataRow datarow)
        {
            var model = (from so in dc.StockOuts
                             //                       join e in dc.Employees
                             //on si.EmployeeCode equals e.EmployeeCode
                         select new
                         {
                             so.StockOutCode,
                             so.UserID,
                             so.DateOut,
                             so.RecipientName,
                             so.IDCard,
                             so.Company,
                             so.TotalWeight,
                             so.Quantity,
                             so.Note
                         }).OrderByDescending(x => x.DateOut).ToList();
            datatable.Clear();
            foreach (var item in model)
            {
                datarow = datatable.NewRow();
                datarow["StockOutCode"] = item.StockOutCode;
                datarow["User"] = item.UserID;
                datarow["DateOut"] = item.DateOut;
                datarow["RecipientName"] = item.RecipientName;
                datarow["IDCard"] = item.IDCard;
                datarow["Company"] = item.Company;
                datarow["TotalWeigh"] = item.TotalWeight;
                datarow["Quantity"] = item.Quantity;
                datarow["Note"] = item.Note;
                datatable.Rows.Add(datarow);
            }
            GridBaoCao.DataSource = datatable;

            double tongtrongluong = 0;
            for (int y = 0; y < gridViewBaoCao.RowCount; y++)
            {
                tongtrongluong = tongtrongluong + double.Parse(gridViewBaoCao.GetRowCellValue(y, "TotalWeigh").ToString());
            }
            txtTongTrongLuong.Text = tongtrongluong.ToString();
            txtSoLuong.Text = gridViewBaoCao.RowCount.ToString();
        }      

        private void gridViewBaoCao_ColumnFilterChanged(object sender, EventArgs e)
        {
            double tongtrongluong = 0;
            for (int y = 0; y < gridViewBaoCao.RowCount; y++)
            {
                tongtrongluong = tongtrongluong + double.Parse(gridViewBaoCao.GetRowCellValue(y, "TotalWeigh").ToString());
            }
            txtTongTrongLuong.Text = tongtrongluong.ToString();
            txtSoLuong.Text = gridViewBaoCao.RowCount.ToString();
        }

        private void gridViewBaoCao_DoubleClick(object sender, EventArgs e)
        {
            //lay dc stockincode hoac stockout code
            int RowIndex = gridViewBaoCao.FocusedRowHandle;
            string stockoutcode;
            string stockincode;
            try
            {
                stockincode = gridViewBaoCao.GetRowCellValue(RowIndex, "StockInCode").ToString();
            }
            catch (Exception ex)
            {
                stockincode = "";
            }

            try
            {
                stockoutcode = gridViewBaoCao.GetRowCellValue(RowIndex, "StockOutCode").ToString();
            }
            catch (Exception ex)
            {
                stockoutcode = "";
            }        
            //tim ma lay duoc trong hai bang stockin va stock out
            if (stockincode != "" && stockoutcode == "")
            {
                //truong hop lay duoc ma nhap kho
                StockIn si = dc.StockIns.Where(x => x.StockInCode == stockincode).SingleOrDefault();
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
                DataTable datat = new DataTable();
                DataRow datar;
                //addcolum
                datat.Columns.Add("Barcode");
                datat.Columns.Add("TrongLuong");
                datat.Columns.Add("Xuong");
                datat.Columns.Add("Kho");
                datat.Columns.Add("TenRac");
                datat.Columns.Add("GhiChu");
                datat.Columns.Add("DonVi");
                foreach (var item1 in model)
                {
                    datar = datat.NewRow();
                    datar["Barcode"] = item1.Barcode;
                    datar["TrongLuong"] = item1.Weigh;
                    datar["Xuong"] = item1.FactoryCode;
                    datar["Kho"] = item1.StorageCode;
                    datar["TenRac"] = item1.WasteCode;
                    datar["GhiChu"] = item1.Note;
                    Waste var = (from c in dc.Wastes
                                 where c.WasteCode == item1.WasteCode
                                 select c).FirstOrDefault();
                    datar["DonVi"] = var.Unit;
                    datat.Rows.Add(datar);
                }
                nhapkho.gridControlBarcode.DataSource = datat;
            }
            if (stockincode == "" && stockoutcode != "")
            {
                StockOut so = dc.StockOuts.Where(x => x.StockOutCode == stockoutcode).SingleOrDefault();
                XuatKho xuatkho = new XuatKho();
                xuatkho.Show();
                xuatkho.txtMaPX.Text = stockoutcode;
                xuatkho.dateNgayPX.EditValue = so.DateOut;
                xuatkho.txtUser.Text = so.UserID;
                xuatkho.txtTenNguoiNhan.Text = so.RecipientName;
                xuatkho.txtCMND.Text = so.IDCard;
                xuatkho.txtCongTy.Text = so.Company;
                xuatkho.txtTongTrongLuong.Text = so.TotalWeight.ToString();
                xuatkho.txtSoLuong.Text = so.Quantity.ToString();
                //binding du lieu luoi
                var model = (from sod in dc.StockOutDetails
                             join bar in dc.BarcodeDetails on sod.Barcode equals bar.Barcode
                             join w in dc.Wastes on bar.WasteCode equals w.WasteCode
                             where (sod.StockOutCode == stockoutcode)
                             select new
                             {
                                 sod.Barcode,
                                 w.WasteName,
                                 w.Type,
                                 bar.Weigh,
                                 w.Unit,
                                 bar.Note
                             }).ToList();
                //tao moi datatable va datarow
                DataTable datat1 = new DataTable();
                DataRow datar1;
                //addcolum
                datat1.Columns.Add("Barcode");
                datat1.Columns.Add("WasteName");
                datat1.Columns.Add("Type");
                datat1.Columns.Add("Weigh");
                datat1.Columns.Add("Unit");
                datat1.Columns.Add("Note");
                foreach (var item1 in model)
                {
                    datar1 = datat1.NewRow();
                    datar1["Barcode"] = item1.Barcode;
                    datar1["WasteName"] = item1.WasteName;
                    datar1["Type"] = item1.Type;
                    datar1["Weigh"] = item1.Weigh;
                    datar1["Unit"] = item1.Unit;
                    datar1["Note"] = item1.Note;
                    //Waste var = (from c in dc.Wastes
                    //             where c.WasteCode == item1.WasteCode
                    //             select c).FirstOrDefault();
                    //datarow["DonVi"] = var.Unit;
                    datat1.Rows.Add(datar1);
                }
                xuatkho.gridControlBarcode.DataSource = datat1;
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            //Creating DataTable
            DataTable dt = new DataTable();

            //Adding the Columns with gridview devexpress
            foreach (GridColumn c in gridViewBaoCao.Columns)
            {
                dt.Columns.Add(c.FieldName, c.ColumnType);
            }

            //Adding the Rows with gridview devexpress
            for (int r = 0; r < gridViewBaoCao.RowCount; r++)
            {
                object[] rowValues = new object[dt.Columns.Count];
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    rowValues[c] = gridViewBaoCao.GetRowCellValue(r, dt.Columns[c].ColumnName);
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
                    sheet.Name = "WMS_Report";
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

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            if (rbtnNhapKho.Checked == true && rbtnXuatKho.Checked == false)
            {
                List<ReportStockInModel> listrsim = new List<ReportStockInModel>();
                for (int y = 0; y < gridViewBaoCao.RowCount; y++)
                {
                    ReportStockInModel rsim = new ReportStockInModel();
                    rsim.StockInCode = gridViewBaoCao.GetRowCellValue(y, "StockInCode").ToString();
                    rsim.User = gridViewBaoCao.GetRowCellValue(y, "User").ToString();
                    rsim.EmployeeCode = gridViewBaoCao.GetRowCellValue(y, "EmployeeCode").ToString();
                    rsim.Name = gridViewBaoCao.GetRowCellValue(y, "Name").ToString();
                    rsim.Dept = gridViewBaoCao.GetRowCellValue(y, "Dept").ToString();
                    rsim.DateIn = DateTime.Parse(gridViewBaoCao.GetRowCellValue(y, "DateIn").ToString());
                    rsim.TotalWeigh = double.Parse(gridViewBaoCao.GetRowCellValue(y, "TotalWeigh").ToString());
                    rsim.Quantity = Int32.Parse(gridViewBaoCao.GetRowCellValue(y, "Quantity").ToString());
                    rsim.Note = gridViewBaoCao.GetRowCellValue(y, "Note").ToString();
                    listrsim.Add(rsim);
                }
                PrintReportStockIn print = new PrintReportStockIn();
                print.Print(txtSoLuong.Text, listrsim);
                print.ShowDialog();
            }
            if (rbtnNhapKho.Checked == false && rbtnXuatKho.Checked == true)
            {
                List<ReportStockOutModel> listrsom = new List<ReportStockOutModel>();
                for (int y = 0; y < gridViewBaoCao.RowCount; y++)
                {
                    ReportStockOutModel rsom = new ReportStockOutModel();
                    rsom.StockOutCode = gridViewBaoCao.GetRowCellValue(y, "StockOutCode").ToString();
                    rsom.User = gridViewBaoCao.GetRowCellValue(y, "User").ToString();
                    rsom.DateOut = DateTime.Parse(gridViewBaoCao.GetRowCellValue(y, "DateOut").ToString());
                    rsom.RecipientName = gridViewBaoCao.GetRowCellValue(y, "RecipientName").ToString();
                    rsom.IDCard = gridViewBaoCao.GetRowCellValue(y, "IDCard").ToString();
                    rsom.Company = gridViewBaoCao.GetRowCellValue(y, "Company").ToString();
                    rsom.TotalWeigh = double.Parse(gridViewBaoCao.GetRowCellValue(y, "TotalWeigh").ToString());
                    rsom.Quantity = Int32.Parse(gridViewBaoCao.GetRowCellValue(y, "Quantity").ToString());
                    rsom.Note = gridViewBaoCao.GetRowCellValue(y, "Note").ToString();
                    listrsom.Add(rsom);
                }
                PrintReportStockOut print = new PrintReportStockOut();
                print.Print(txtSoLuong.Text, listrsom);
                print.ShowDialog();
            }
        }
    }
}