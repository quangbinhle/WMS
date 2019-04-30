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
    public partial class XuatKho : DevExpress.XtraEditors.XtraForm
    {
        private string user;
        DataTable dt = new DataTable();
        public DataRow dr;
        WMSDataContext dc = new WMSDataContext();
        public XuatKho()
        {
            InitializeComponent();
        }

        public XuatKho(string taikhoan) : this()
        {
            user = taikhoan;
        }

        private void XuatKho_Load(object sender, EventArgs e)
        {
            //Tao cac cot trong luoi
            dt.Columns.Add("Barcode");
            dt.Columns.Add("WasteName");
            dt.Columns.Add("Type");
            dt.Columns.Add("Weigh");
            dt.Columns.Add("Unit");
            dt.Columns.Add("Note");
            gridControlBarcode.DataSource = dt;
            //Hien thi caption header
            gridViewBarcode.Columns["Barcode"].Caption = "Barcode";
            gridViewBarcode.Columns["WasteName"].Caption = "Tên rác";
            gridViewBarcode.Columns["Type"].Caption = "Loại rác";
            gridViewBarcode.Columns["Weigh"].Caption = "Trọng lượng";
            gridViewBarcode.Columns["Unit"].Caption = "Đơn vị";
            gridViewBarcode.Columns["Note"].Caption = "Ghi chú";

            dateNgayPX.EditValue = DateTime.Now;
            txtUser.Text = user;
            txtUser.Enabled = false;
        }

        public void ScanBarcode()
        {
            //add vao luoi
            //truy van thong tin barcode
            var query = (from b in dc.BarcodeDetails
                         join w in dc.Wastes on b.WasteCode equals w.WasteCode
                         where b.Barcode == txtBarcode.Text
                         select new
                         {
                             b.Barcode,
                             b.Weigh,
                             b.Note,
                             w.WasteName,
                             w.Type,
                             w.Unit
                         });
            //add vao datatable
            foreach (var item1 in query)
            {
                dr = dt.NewRow();
                dr["Barcode"] = item1.Barcode;
                dr["WasteName"] = item1.WasteName;
                dr["Type"] = item1.Type;
                dr["Weigh"] = item1.Weigh;
                dr["Unit"] = item1.Unit;
                dr["Note"] = item1.Note;
                dt.Rows.Add(dr);
            }
            gridControlBarcode.DataSource = dt;
            //tinh tong trong luong va count so luong barcode
            double tongtrongluong = 0;
            for (int y = 0; y < gridViewBarcode.RowCount; y++)
            {
                tongtrongluong = tongtrongluong + double.Parse(gridViewBarcode.GetRowCellValue(y, "Weigh").ToString());
            }
            txtTongTrongLuong.Text = tongtrongluong.ToString();
            txtSoLuong.Text = gridViewBarcode.RowCount.ToString();

            txtBarcode.Text = "";
            txtBarcode.Focus();
            txtBarcode.BackColor = Color.Green;
        }

        public void CancelScan(string barcode)
        {
            DataRow[] delrow = dt.Select("Barcode='" + barcode + "'");
            foreach (DataRow dr in delrow)
            {
                //dt.Rows.Remove(delrow);
                dr.Delete();
            }
            //tinh tong tien va count so luong barcode

            double tongtrongluong = 0;
            for (int i = 0; i < gridViewBarcode.RowCount; i++)
            {
                tongtrongluong = tongtrongluong + double.Parse(gridViewBarcode.GetRowCellValue(i, "Weigh").ToString());
            }
            txtTongTrongLuong.Text = tongtrongluong.ToString();
            txtSoLuong.Text = gridViewBarcode.RowCount.ToString();

            txtBarcode.Text = "";
            txtBarcode.Focus();
            txtBarcode.BackColor = Color.Green;

        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //neu nut an la enter
            if (e.KeyChar == (char)13)
            {
                lblMessageBarcode.Text = "";
                //neu dang o trang thai scan
                if (rbtnScan.Checked == true && rbtnHuy.Checked == false)
                {
                    //kiem tra luoi rong
                    if (gridViewBarcode.RowCount == 0)
                    {
                        //kiem tra xem barcode da co trong bang barcode detail chua va trang thai la true(da nhap kho)
                        BarcodeDetail item1 = (from c in dc.BarcodeDetails where c.Barcode == txtBarcode.Text && c.Status == true select c).SingleOrDefault();
                        if (item1 == null)
                        {
                            //neu co thong bao la barcode chua ton tai va o scan hien do(that bai)
                            lblMessageBarcode.Text = "Barcode " + txtBarcode.Text + " không tồn tại";
                            txtBarcode.Text = "";
                            txtBarcode.Focus();
                            txtBarcode.BackColor = Color.Red;
                        }
                        else
                        {
                            //neu luoi rong
                            ScanBarcode();
                        }
                    }
                    else
                    {
                        //kiem tra xem barcode da co trong bang barcode detail chua va trang thai la true(da nhap kho)
                        BarcodeDetail item = (from c in dc.BarcodeDetails where c.Barcode == txtBarcode.Text && c.Status == true select c).SingleOrDefault();
                        if (item == null)
                        {
                            //neu co thong bao la barcode chua ton tai va o scan hien do(that bai)
                            lblMessageBarcode.Text = "Barcode " + txtBarcode.Text + " không tồn tại";
                            txtBarcode.Text = "";
                            txtBarcode.Focus();
                            txtBarcode.BackColor = Color.Red;
                        }
                        else
                        {
                            //neu barcode da co trong bang barcode detail va trang thai la true(da nhap kho)
                            //kiem tra xem barcode vua scan da xuat hien trong luoi chua
                            int count = 0;
                            for (int i = 0; i < gridViewBarcode.RowCount; i++)
                            {
                                if (gridViewBarcode.GetRowCellValue(i, "Barcode").ToString() == txtBarcode.Text)
                                {
                                    count = count + 1; break;
                                }
                            }
                            if (count > 0)
                            {
                                //neu co thong bao la barcode da duoc xuat hien trong luoi va o scan hien vang(canh bao)
                                lblMessageBarcode.Text = "Barcode " + txtBarcode.Text + " đã được scan";
                                txtBarcode.Text = "";
                                txtBarcode.Focus();
                                txtBarcode.BackColor = Color.Yellow;
                            }
                            else
                            {
                                //add barcode xuong luoi
                                ScanBarcode();
                            }
                        }
                    }
                }
                //new dang o trang thai scan huy
                else if (rbtnScan.Checked == false && rbtnHuy.Checked == true)
                {
                    //kiem tra so dong trong luoi
                    if (gridViewBarcode.RowCount == 0)
                    {
                        MessageBox.Show("Không còn dòng để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtBarcode.Text = "";
                        txtBarcode.Focus();
                    }
                    else
                    {
                        //kiem tra xem barcode da ton tai trong luoi chua
                        int dem = 0;
                        for (int i = 0; i < gridViewBarcode.RowCount; i++)
                        {
                            if (txtBarcode.Text == gridViewBarcode.GetRowCellValue(i, "Barcode").ToString())
                            {
                                dem = 0;
                            }
                            else
                            {
                                dem = dem + 1;
                            }
                        }
                        if (dem == gridViewBarcode.RowCount)
                        {
                            lblMessageBarcode.Text = "Barcode " + txtBarcode.Text + " không tồn tại trong lưới";
                            txtBarcode.Text = "";
                            txtBarcode.Focus();
                            txtBarcode.BackColor = Color.Red;
                        }
                        else
                        {
                            CancelScan(txtBarcode.Text);
                        }
                    }
                }
            }
            else
            {
                txtBarcode.Focus();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtMaPX.Text = "";
            dateNgayPX.EditValue = "";
            txtTenNguoiNhan.Text = "";
            txtCMND.Text = "";
            txtCMND.Text = "";
            txtCongTy.Text = "";
            txtGhiChu.Text = "";
            txtBarcode.BackColor = Color.White;
            dt.Clear();
        }

        private void btnLưu_Click(object sender, EventArgs e)
        {
            if (txtMaPX.Text == "" || txtTenNguoiNhan.Text == "" || txtCMND.Text == "")
            {
                MessageBox.Show("Vui lòng điền đủ thông tin phiên xuất kho", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //luu thong tin phien vao stockout, cap nhat trang thai barcode thanh false(da xuat kho), luu thong tin bang stockoutdetail
                //luu thong tin phien vao stockout
                StockOut so = new StockOut();
                so.StockOutCode = txtMaPX.Text;
                so.UserID = txtUser.Text;
                so.DateOut = DateTime.Parse(dateNgayPX.EditValue.ToString());
                so.RecipientName = txtTenNguoiNhan.Text;
                so.IDCard = txtCMND.Text;
                so.Company = txtCongTy.Text;
                so.TotalWeight = double.Parse(txtTongTrongLuong.Text);
                so.Quantity = Int32.Parse(txtSoLuong.Text);
                so.Note = txtGhiChu.Text;
                //Kiem tra trung stockoutcode
                StockOut var = (from s in dc.StockOuts
                                where s.StockOutCode == so.StockOutCode
                                select s).FirstOrDefault();
                if (var == null)
                {
                    dc.StockOuts.InsertOnSubmit(so);
                    dc.SubmitChanges();
                }
                else
                {
                    MessageBox.Show("Mã phiên xuất kho bị trùng lặp", "Thông báo", MessageBoxButtons.OK);
                }

                //cập nhật trang thai barcode thanh false (da xuat kho)
                for (int i = 0; i < gridViewBarcode.RowCount; i++)
                {
                    BarcodeDetail bd = (from c in dc.BarcodeDetails
                                        where c.Barcode == gridViewBarcode.GetRowCellValue(i, "Barcode").ToString()
                                        select c).FirstOrDefault();
                    if (bd != null)
                    {
                        bd.Status = false;
                        dc.SubmitChanges();
                    }
                }
                //luu thong tin stockout detail
                for (int i = 0; i < gridViewBarcode.RowCount; i++)
                {
                    StockOutDetail sod = new StockOutDetail();
                    sod.StockOutCode = txtMaPX.Text;
                    sod.Barcode = gridViewBarcode.GetRowCellValue(i, "Barcode").ToString();
                    dc.StockOutDetails.InsertOnSubmit(sod);
                    dc.SubmitChanges();
                }
                MessageBox.Show("Lưu phiên xuất kho thành công", "Thông báo", MessageBoxButtons.OK);
            }
        }
    }
}