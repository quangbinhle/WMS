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
    public partial class TonKho : DevExpress.XtraEditors.XtraForm
    {
        DataTable dt = new DataTable();
        public DataRow dr;
        WMSDataContext dc = new WMSDataContext();

        public TonKho()
        {
            InitializeComponent();
        }

        private void TonKho_Load(object sender, EventArgs e)
        {
            //Tao cac cot trong luoi
            dt.Columns.Add("Barcode");
            dt.Columns.Add("StockInCode");
            dt.Columns.Add("WasteName");
            dt.Columns.Add("Type");
            dt.Columns.Add("StorageName");
            dt.Columns.Add("DateIn");
            dt.Columns.Add("Weigh");
            dt.Columns.Add("Unit");
            dt.Columns.Add("Note");

            GridTonKho.DataSource = dt;
            DisplayData();

        }

        public void DisplayData()
        {
            var model = (from sid in dc.StockInDetails
                         join bar in dc.BarcodeDetails on sid.Barcode equals bar.Barcode
                         join w in dc.Wastes on bar.WasteCode equals w.WasteCode
                         join s in dc.Storages on bar.StorageCode equals s.StorageCode
                         join si in dc.StockIns on sid.StockInCode equals si.StockInCode
                         where bar.Status == true
                         select new
                         {
                             sid.Barcode,
                             sid.StockInCode,
                             si.DateIn,
                             w.WasteName,
                             w.Type,
                             s.StorageName,
                             bar.Weigh,
                             w.Unit,
                             bar.Note,
                         }).OrderByDescending(x => x.DateIn).ToList();
            dt.Clear();
            foreach (var item in model)
            {
                dr = dt.NewRow();
                dr["Barcode"] = item.Barcode;
                dr["StockInCode"] = item.StockInCode;
                dr["WasteName"] = item.WasteName;
                dr["Type"] = item.Type;
                dr["StorageName"] = item.StorageName;
                dr["DateIn"] = item.DateIn;
                dr["Weigh"] = item.Weigh;
                dr["Unit"] = item.Unit;
                dr["Note"] = item.Note;
                dt.Rows.Add(dr);
            }
            GridTonKho.DataSource = dt;
            //Hien thi caption header
            gridViewTonKho.Columns["Barcode"].Caption = "Barcode";
            gridViewTonKho.Columns["StockInCode"].Caption = "Mã phiên nhập";
            gridViewTonKho.Columns["WasteName"].Caption = "Tên rác";
            gridViewTonKho.Columns["Type"].Caption = "Loại rác";
            gridViewTonKho.Columns["StorageName"].Caption = "Tên kho chứa";
            gridViewTonKho.Columns["DateIn"].Caption = "Ngày nhập kho";
            gridViewTonKho.Columns["Weigh"].Caption = "Trọng lượng";
            gridViewTonKho.Columns["Unit"].Caption = "Đơn vị";
            gridViewTonKho.Columns["Note"].Caption = "Ghi chú";

            double tongtrongluong = 0;
            for (int y = 0; y < gridViewTonKho.RowCount; y++)
            {
                tongtrongluong = tongtrongluong + double.Parse(gridViewTonKho.GetRowCellValue(y, "Weigh").ToString());
            }
            txtTongTrongLuong.Text = tongtrongluong.ToString();
            txtSoLuong.Text = gridViewTonKho.RowCount.ToString();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void gridViewTonKho_ColumnFilterChanged(object sender, EventArgs e)
        {
            double tongtrongluong = 0;
            for (int y = 0; y < gridViewTonKho.RowCount; y++)
            {
                tongtrongluong = tongtrongluong + double.Parse(gridViewTonKho.GetRowCellValue(y, "Weigh").ToString());
            }
            txtTongTrongLuong.Text = tongtrongluong.ToString();
            txtSoLuong.Text = gridViewTonKho.RowCount.ToString();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            //Creating DataTable
            DataTable dt = new DataTable();

            //Adding the Columns with gridview devexpress
            foreach (GridColumn c in gridViewTonKho.Columns)
            {
                dt.Columns.Add(c.FieldName, c.ColumnType);
            }

            //Adding the Rows with gridview devexpress
            for (int r = 0; r < gridViewTonKho.RowCount; r++)
            {
                object[] rowValues = new object[dt.Columns.Count];
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    rowValues[c] = gridViewTonKho.GetRowCellValue(r, dt.Columns[c].ColumnName);
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
                    sheet.Name = "WMS_Inventory";
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

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            //var model = (from sid in dc.StockInDetails
            //             join bar in dc.BarcodeDetails on sid.Barcode equals bar.Barcode
            //             join w in dc.Wastes on bar.WasteCode equals w.WasteCode
            //             join s in dc.Storages on bar.StorageCode equals s.StorageCode
            //             join si in dc.StockIns on sid.StockInCode equals si.StockInCode
            //             where bar.Status == true
            //             select new
            //             {
            //                 sid.Barcode,
            //                 sid.StockInCode,
            //                 si.DateIn,
            //                 w.WasteName,
            //                 w.Type,
            //                 s.StorageName,
            //                 bar.Weigh,
            //                 w.Unit,
            //                 bar.Note,
            //             }).OrderByDescending(x => x.DateIn).ToList();
            //List<InventoryModel> listim = new List<InventoryModel>();
            //foreach (var item1 in model)
            //{
            //    InventoryModel im = new InventoryModel();
            //    im.barcode = item1.Barcode;
            //    im.StockInCode = item1.StockInCode;
            //    im.DateIn = item1.DateIn;
            //    im.WasteName = item1.WasteName;
            //    im.Type = item1.Type;
            //    im.StorageName = item1.StorageName;
            //    im.Weigh = item1.Weigh;
            //    im.Unit = item1.Unit;
            //    im.Note = item1.Note;
            //    listim.Add(im);
            //}
            List<InventoryModel> listim = new List<InventoryModel>();
            for (int y = 0; y < gridViewTonKho.RowCount; y++)
            {
                InventoryModel im = new InventoryModel();
                im.barcode= gridViewTonKho.GetRowCellValue(y, "Barcode").ToString();
                im.StockInCode= gridViewTonKho.GetRowCellValue(y, "StockInCode").ToString();
                im.DateIn= DateTime.Parse(gridViewTonKho.GetRowCellValue(y, "DateIn").ToString());
                im.WasteName= gridViewTonKho.GetRowCellValue(y, "WasteName").ToString();
                im.Type = gridViewTonKho.GetRowCellValue(y, "Type").ToString();
                im.StorageName = gridViewTonKho.GetRowCellValue(y, "StorageName").ToString();
                im.Weigh = double.Parse(gridViewTonKho.GetRowCellValue(y, "Weigh").ToString());
                im.Unit = gridViewTonKho.GetRowCellValue(y, "Unit").ToString();
                im.Note = gridViewTonKho.GetRowCellValue(y, "Note").ToString();
                listim.Add(im);
            }
            PrintInventory print = new PrintInventory();
            print.Print(txtSoLuong.Text,listim);
            print.ShowDialog();
        }
    }
}