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
using System.Threading;
using DevExpress.XtraGrid.Views.Grid;
using Business;

namespace Presentation
{
    public partial class NhapKho : DevExpress.XtraEditors.XtraForm
    {
        private string user;
        private string StockInCode;
        DataTable dt = new DataTable();
        public DataRow dr;
        WMSDataContext dc = new WMSDataContext();
        public NhapKho()
        {
            InitializeComponent();
        }

        public NhapKho(string taikhoan) : this()
        {
            user = taikhoan;
        }

        public NhapKho(string stockincode,int index) : this()
        {
            //StockInCode = stockincode;
            ////gan thong tin lai cho form stockin
            ////gan thong tin phien
            //StockIn item = dc.StockIns.Where(x => x.StockInCode == StockInCode).SingleOrDefault();
            //txtMaPN.Text = StockInCode;
            //datePN.EditValue = item.DateIn;
            //txtMaNV.Text = item.EmployeeCode;
            //txtGhiChuPhien.Text = item.Note;
            //gan thong tin luoi barcode
            //var model = (from sid in dc.StockInDetails
            //             join bar in dc.BarcodeDetails on sid.Barcode equals bar.Barcode
            //             where (sid.StockInCode == StockInCode)
            //             select new
            //             {
            //                 bar.Barcode,
            //                 bar.WasteCode,
            //                 bar.FactoryCode,
            //                 bar.StorageCode,
            //                 bar.Weigh,
            //                 bar.Note,
            //             }).ToList();

            //dt.Clear();
            //foreach (var item1 in model)
            //{
            //    dr = dt.NewRow();
            //    dr["Barcode"] = item1.Barcode;
            //    dr["TrongLuong"] = item1.Weigh;
            //    dr["Xuong"] = item1.FactoryCode;
            //    dr["Kho"] = item1.StorageCode;
            //    dr["TenRac"] = item1.WasteCode;
            //    dr["GhiChu"] = item1.Note;
            //    Waste var = (from c in dc.Wastes
            //                 where c.WasteCode == item1.WasteCode
            //                 select c).FirstOrDefault();
            //    dr["DonVi"] = var.Unit;
            //    dt.Rows.Add(dr);
            //}
            //gridControlBarcode.DataSource = dt;
        }

        private void NhapKho_Load(object sender, EventArgs e)
        {
            //Tao cac cot trong luoi
            dt.Columns.Add("Barcode");
            dt.Columns.Add("TrongLuong");
            dt.Columns.Add("Xuong");
            dt.Columns.Add("Kho");
            dt.Columns.Add("TenRac");
            dt.Columns.Add("DonVi");
            dt.Columns.Add("GhiChu");
            gridControlBarcode.DataSource = dt;
            //Hien thi caption header
            gridViewBarcode.Columns["TrongLuong"].Caption = "Trọng lượng";
            gridViewBarcode.Columns["Xuong"].Caption = "Xưởng";
            gridViewBarcode.Columns["Kho"].Caption = "Kho";
            gridViewBarcode.Columns["GhiChu"].Caption = "Ghi chú";
            gridViewBarcode.Columns["TenRac"].Caption = "Tên rác";
            gridViewBarcode.Columns["DonVi"].Caption = "Đơn vị";
            //load combox xuong, kho, loai rac
            List<Factory> ListFactory = (from f in dc.Factories
                                         where f.Status == true
                                         select f).ToList();
            cbXuong.Properties.DataSource = ListFactory;
            cbXuong.Properties.DisplayMember = "FactoryName";
            cbXuong.Properties.ValueMember = "FactoryCode";

            List<Storage> ListStorage = (from s in dc.Storages
                                         where s.Status == true
                                         select s).ToList();
            cbKho.Properties.DataSource = ListStorage;
            cbKho.Properties.DisplayMember = "StorageName";
            cbKho.Properties.ValueMember = "StorageCode";

            List<Waste> ListWaste = (from w in dc.Wastes
                                     where w.Status == true
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
            btnThem.Name = "Thêm";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //Clear thong tin de tao phien nhap moi
            //clear thong tin phien nhap
            txtMaPN.Text = "";
            datePN.EditValue = DateTime.Now;
            txtMaNV.Text = "";
            txtGhiChuPhien.Text = "";
            //clear thong tin nhap barcode va enable button chuc nang
            dt.Clear();
            clearData();

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
                //luu thong tin phien vao stockin va tao barcode trang thai chua nhap kho va luu thong tin bang stockindetail              
                //luu thong tin phien vao stockin
                StockIn si = new StockIn();
                si.StockInCode = txtMaPN.Text;
                si.UserID = txtUser.Text;
                si.EmployeeCode = txtMaNV.Text;
                si.DateIn = DateTime.Parse(datePN.EditValue.ToString());
                si.Note = txtGhiChuPhien.Text;
                si.TotalWeight = double.Parse(txtTongTrongLuong.Text);
                si.Quantity = Int32.Parse(txtSoLuong.Text);
                //Kiem tra trung stockincode
                StockIn var = (from s in dc.StockIns
                               where s.StockInCode == si.StockInCode
                               select s).FirstOrDefault();
                if (var == null)
                {
                    dc.StockIns.InsertOnSubmit(si);
                    dc.SubmitChanges();
                }
                else
                {
                    MessageBox.Show("Mã phiên nhập kho bị trùng lặp", "Thông báo", MessageBoxButtons.OK);
                }
                //luu thong tin barcode

                for (int i = 0; i < gridViewBarcode.RowCount; i++)
                {
                    BarcodeDetail bar = new BarcodeDetail();
                    bar.Barcode = gridViewBarcode.GetRowCellValue(i, "Barcode").ToString();
                    bar.WasteCode = gridViewBarcode.GetRowCellValue(i, "TenRac").ToString();
                    bar.FactoryCode = gridViewBarcode.GetRowCellValue(i, "Xuong").ToString();
                    bar.StorageCode = gridViewBarcode.GetRowCellValue(i, "Kho").ToString();
                    bar.Weigh = double.Parse(gridViewBarcode.GetRowCellValue(i, "TrongLuong").ToString());
                    bar.Status = true;
                    bar.Note = gridViewBarcode.GetRowCellValue(i, "GhiChu").ToString();

                    //kiem tra trung barcode
                    BarcodeDetail barcode = (from s in dc.BarcodeDetails
                                             where s.Barcode == bar.Barcode
                                             select s).FirstOrDefault();
                    if (var == null)
                    {
                        dc.BarcodeDetails.InsertOnSubmit(bar);
                        dc.SubmitChanges();
                    }
                    else
                    {
                        MessageBox.Show("Mã barcode bị trùng lặp", "Thông báo", MessageBoxButtons.OK);
                    }
                }

                //Luu thong tin stockindetail

                for (int i = 0; i < gridViewBarcode.RowCount; i++)
                {
                    StockInDetail sid = new StockInDetail();
                    sid.StockInCode = txtMaPN.Text;
                    sid.Barcode = gridViewBarcode.GetRowCellValue(i, "Barcode").ToString();
                    sid.Note = txtGhiChuPhien.Text;
                    dc.StockInDetails.InsertOnSubmit(sid);
                    dc.SubmitChanges();
                }
                MessageBox.Show("Lưu phien nhập thành công", "Thông báo", MessageBoxButtons.OK);
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTrongLuong.Text == "" || cbXuong.EditValue == "" || cbKho.EditValue == "" || cbTenRac.EditValue == "")
            {
                MessageBox.Show("Vui lòng điền đủ thông tin để tạo barcode", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //Add du lieu xuong luoi tam
                dr = dt.NewRow();
                dr["Barcode"] = txtBarcode.Text;
                dr["TrongLuong"] = txtTrongLuong.Text;
                dr["Xuong"] = cbXuong.EditValue.ToString();
                dr["Kho"] = cbKho.EditValue.ToString();
                dr["TenRac"] = cbTenRac.EditValue.ToString();
                dr["GhiChu"] = txtGhiChuRAc.Text;
                //gridViewBarcode.AddNewRow();
                //gridViewBarcode.SetFocusedRowCellValue("Barcode", txtBarcode.Text);
                //gridViewBarcode.SetFocusedRowCellValue("TrongLuong", txtTrongLuong.Text);
                //gridViewBarcode.SetFocusedRowCellValue("Xuong", cbXuong.EditValue.ToString());
                //gridViewBarcode.SetFocusedRowCellValue("Kho", cbKho.EditValue.ToString());
                //gridViewBarcode.SetFocusedRowCellValue("TenRac", cbTenRac.EditValue.ToString());
                //gridViewBarcode.SetFocusedRowCellValue("GhiChu", txtGhiChuRAc.Text);
                dt.Rows.Add(dr);
                gridControlBarcode.DataSource = dt;

                //Truy van tu dong them cot don vi cho tung loai rac
                Waste var = (from c in dc.Wastes
                             where c.WasteCode == cbTenRac.EditValue.ToString()
                             select c).FirstOrDefault();
                //gridViewBarcode.SetFocusedRowCellValue("DonVi", var.Unit);
                dr["DonVi"] = var.Unit;

                //tinh tong trong luong va count so luong barcode
                double tongtrongluong = 0;
                for (int i = 0; i < gridViewBarcode.RowCount; i++)
                {
                    tongtrongluong = tongtrongluong + double.Parse(gridViewBarcode.GetRowCellValue(i, "TrongLuong").ToString());
                }
                txtTongTrongLuong.Text = tongtrongluong.ToString();
                txtSoLuong.Text = gridViewBarcode.RowCount.ToString();

                //Cleardata
                clearData();
            }
        }

        public void clearData()
        {
            txtTrongLuong.Text = "";
            txtGhiChuRAc.Text = "";
            cbXuong.EditValue = "";
            cbKho.EditValue = "";
            cbTenRac.EditValue = "";
            txtBarcode.EditValue = "";
        }

        //Su kien kiem tra nhap trong luong khong duoc nhap chu cai va so am
        private void txtTrongLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            string decimalString = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            char decimalChar = Convert.ToChar(decimalString);
            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
            {

            }
            else if (e.KeyChar == decimalChar && txtTrongLuong.Text.IndexOf(decimalString) == -1)
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            //cleardata
            clearData();
            //lay chi so dong khi click vao 1 dong
            int RowIndex = gridViewBarcode.FocusedRowHandle;
            //gridViewBarcode.DeleteRow(RowIndex);
            //remove row co index trong datatable
            if (RowIndex < 0)
            {
                MessageBox.Show("Không còn dòng để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                dt.Rows.RemoveAt(RowIndex);
                gridControlBarcode.DataSource = dt;
                //tinh tong tien va count so luong barcode

                double tongtrongluong = 0;
                for (int i = 0; i < gridViewBarcode.RowCount; i++)
                {
                    tongtrongluong = tongtrongluong + double.Parse(gridViewBarcode.GetRowCellValue(i, "TrongLuong").ToString());
                }
                txtTongTrongLuong.Text = tongtrongluong.ToString();
                txtSoLuong.Text = gridViewBarcode.RowCount.ToString();
            }

        }

        private void gridViewBarcode_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            btnThem.Enabled = false;
            txtTrongLuong.Text = gridViewBarcode.GetRowCellValue(e.RowHandle, "TrongLuong").ToString();
            txtGhiChuRAc.Text = gridViewBarcode.GetRowCellValue(e.RowHandle, "GhiChu").ToString();
            cbXuong.EditValue = gridViewBarcode.GetRowCellValue(e.RowHandle, "Xuong").ToString();
            cbKho.EditValue = gridViewBarcode.GetRowCellValue(e.RowHandle, "Kho").ToString();
            cbTenRac.EditValue = gridViewBarcode.GetRowCellValue(e.RowHandle, "TenRac").ToString();
            txtBarcode.EditValue = gridViewBarcode.GetRowCellValue(e.RowHandle, "Barcode").ToString();
            //Sau khi lưu csdl, click vào row tren gridview btn them sẽ chuyen thanh sua
            btnThem.Name = "Sửa";
            //Click sua => cap nhat lai du lieu
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            clearData();
        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            //dua du lieu vao model
            var model = (from si in dc.StockIns
                         join sid in dc.StockInDetails on si.StockInCode equals sid.StockInCode
                         join bar in dc.BarcodeDetails on sid.Barcode equals bar.Barcode
                         join f in dc.Factories on bar.FactoryCode equals f.FactoryCode
                         join s in dc.Storages on bar.StorageCode equals s.StorageCode
                         join w in dc.Wastes on bar.WasteCode equals w.WasteCode
                         where (sid.StockInCode == txtMaPN.Text)
                         select new
                         {
                             sid.Barcode,
                             f.FactoryName,
                             s.StorageName,
                             w.WasteName,
                             bar.Weigh,
                             w.Unit,
                             si.Quantity,
                             si.TotalWeight
                         }).ToList();
            List<StockInReportModel> listsirm = new List<StockInReportModel>();
            foreach (var item1 in model)
            {
                StockInReportModel sirm = new StockInReportModel();
                sirm.Barcode = item1.Barcode;
                sirm.TenXuong = item1.FactoryName;
                sirm.TenKho = item1.StorageName;
                sirm.TenRac = item1.WasteName;
                sirm.TrongLuong = item1.Weigh;
                sirm.DonVi = item1.Unit;
                sirm.SoLuong = item1.Quantity;
                sirm.TongTrongLuong = item1.TotalWeight;
                listsirm.Add(sirm);
            }
            PrintInvoiceStockIn print = new PrintInvoiceStockIn();
            print.PrintInvoice(txtMaPN.Text, listsirm);
            print.ShowDialog();
        }
    }
}