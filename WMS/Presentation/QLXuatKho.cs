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
    public partial class QLXuatKho : DevExpress.XtraEditors.XtraForm
    {
        public QLXuatKho()
        {
            InitializeComponent();
        }


        DataTable dt = new DataTable();
        DataRow dr;
        WMSDataContext dc = new WMSDataContext();

        private void QLXuatKho_Load(object sender, EventArgs e)
        {
            //Tao cac cot trong luoi
            dt.Columns.Add("StockOutCode");
            dt.Columns.Add("User");
            dt.Columns.Add("DateOut");
            dt.Columns.Add("RecipientName");
            dt.Columns.Add("IDCard");
            dt.Columns.Add("Company");
            dt.Columns.Add("TotalWeigh");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Note");

            DisplayData();
        }

        public void DisplayData()
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
            dt.Clear();
            foreach (var item in model)
            {
                dr = dt.NewRow();
                dr["StockOutCode"] = item.StockOutCode;
                dr["User"] = item.UserID;
                dr["DateOut"] = item.DateOut;
                dr["RecipientName"] = item.RecipientName;
                dr["IDCard"] = item.IDCard;
                dr["Company"] = item.Company;
                dr["TotalWeigh"] = item.TotalWeight;
                dr["Quantity"] = item.Quantity;
                dr["Note"] = item.Note;
                dt.Rows.Add(dr);
            }
            GridQLXuatKho.DataSource = dt;
            //Hien thi caption header
            gridViewQLXuatKho.Columns["StockOutCode"].Caption = "Mã phiên xuất";
            gridViewQLXuatKho.Columns["User"].Caption = "Người nhập";
            gridViewQLXuatKho.Columns["DateOut"].Caption = "Ngày xuất";
            gridViewQLXuatKho.Columns["RecipientName"].Caption = "Tên người nhận";
            gridViewQLXuatKho.Columns["IDCard"].Caption = "CMND";
            gridViewQLXuatKho.Columns["Company"].Caption = "Công ty xử lý";
            gridViewQLXuatKho.Columns["TotalWeigh"].Caption = "Tổng trọng lượng";
            gridViewQLXuatKho.Columns["Quantity"].Caption = "Số lượng";
            gridViewQLXuatKho.Columns["Note"].Caption = "Ghi chú";

            double tongtrongluong = 0;
            for (int y = 0; y < gridViewQLXuatKho.RowCount; y++)
            {
                tongtrongluong = tongtrongluong + double.Parse(gridViewQLXuatKho.GetRowCellValue(y, "TotalWeigh").ToString());
            }
            txtTongTrongLuong.Text = tongtrongluong.ToString();
            txtSoLuong.Text = gridViewQLXuatKho.RowCount.ToString();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dateDenNgay.Text = "";
            dateTuNgay.Text = "";
            DisplayData();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            int compare = DateTime.Compare(Convert.ToDateTime(dateTuNgay.EditValue), Convert.ToDateTime(dateDenNgay.EditValue));
            if (compare < 0)
            {
                var model = (from so in dc.StockOuts
                                 //                       join e in dc.Employees
                                 //on si.EmployeeCode equals e.EmployeeCode
                             where (Convert.ToDateTime(dateTuNgay.EditValue) < so.DateOut && so.DateOut < Convert.ToDateTime(dateDenNgay.EditValue))
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
            dt.Clear();
            foreach (var item in model)
            {
                dr = dt.NewRow();
                dr["StockOutCode"] = item.StockOutCode;
                dr["User"] = item.UserID;
                dr["DateOut"] = item.DateOut;
                dr["RecipientName"] = item.RecipientName;
                dr["IDCard"] = item.IDCard;
                dr["Company"] = item.Company;
                dr["TotalWeigh"] = item.TotalWeight;
                dr["Quantity"] = item.Quantity;
                dr["Note"] = item.Note;
                dt.Rows.Add(dr);
                }
                GridQLXuatKho.DataSource = dt;

                double tongtrongluong = 0;
                for (int y = 0; y < gridViewQLXuatKho.RowCount; y++)
                {
                    tongtrongluong = tongtrongluong + double.Parse(gridViewQLXuatKho.GetRowCellValue(y, "TotalWeigh").ToString());
                }
                txtTongTrongLuong.Text = tongtrongluong.ToString();
                txtSoLuong.Text = gridViewQLXuatKho.RowCount.ToString();
            }
            else
            {
                MessageBox.Show("Ngày kết thúc lớn hơn ngày bắt đầu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridViewQLXuatKho_DoubleClick(object sender, EventArgs e)
        {
            
            int RowIndex = gridViewQLXuatKho.FocusedRowHandle;
            string stockoutcode = gridViewQLXuatKho.GetRowCellValue(RowIndex, "StockOutCode").ToString();
            StockOut so = dc.StockOuts.Where(x => x.StockOutCode == stockoutcode).SingleOrDefault();
            int ma = 0;
            //goi su kien click form(show form) => biding du lieu ra form
            //binding du lieu phien
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
            DataTable datatable = new DataTable();
            DataRow datarow;
            //addcolum
            datatable.Columns.Add("Barcode");
            datatable.Columns.Add("WasteName");
            datatable.Columns.Add("Type");
            datatable.Columns.Add("Weigh");
            datatable.Columns.Add("Unit");
            datatable.Columns.Add("Note");
            foreach (var item1 in model)
            {
                datarow = datatable.NewRow();
                datarow["Barcode"] = item1.Barcode;
                datarow["WasteName"] = item1.WasteName;
                datarow["Type"] = item1.Type;
                datarow["Weigh"] = item1.Weigh;
                datarow["Unit"] = item1.Unit;
                datarow["Note"] = item1.Note;
                //Waste var = (from c in dc.Wastes
                //             where c.WasteCode == item1.WasteCode
                //             select c).FirstOrDefault();
                //datarow["DonVi"] = var.Unit;
                datatable.Rows.Add(datarow);
            }
            xuatkho.gridControlBarcode.DataSource = datatable;
        }

        private void gridViewQLXuatKho_ColumnFilterChanged(object sender, EventArgs e)
        {
            double tongtrongluong = 0;
            for (int y = 0; y < gridViewQLXuatKho.RowCount; y++)
            {
                tongtrongluong = tongtrongluong + double.Parse(gridViewQLXuatKho.GetRowCellValue(y, "TotalWeigh").ToString());
            }
            txtTongTrongLuong.Text = tongtrongluong.ToString();
            txtSoLuong.Text = gridViewQLXuatKho.RowCount.ToString();
        }
    }
}