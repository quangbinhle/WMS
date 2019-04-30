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
                         where bar.Status==true
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
    }
}