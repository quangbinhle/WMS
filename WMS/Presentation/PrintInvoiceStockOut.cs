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
    public partial class PrintInvoiceStockOut : DevExpress.XtraEditors.XtraForm
    {
        WMSDataContext dc = new WMSDataContext();
        public PrintInvoiceStockOut()
        {
            InitializeComponent();
        }

        public void PrintInvoice(StockOut so, List<StockOutReportModel> data)
        {
            StockOutInvoicePara report = new StockOutInvoicePara();
            foreach (DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            //
            //var model = (from sid in dc.StockIns
            //             join e in dc.Employees on sid.EmployeeCode equals e.EmployeeCode
            //             where (sid.StockInCode == code)
            //             select new
            //             {
            //                 sid.StockInCode,
            //                 sid.DateIn,
            //                 sid.EmployeeCode,
            //                 e.Name,
            //                 e.Dept,
            //                 sid.UserID,
            //                 sid.Note
            //             });
            //StockInModel sim = new StockInModel();
            //foreach (var item1 in model)
            //{
            //    sim.StockInCode = item1.StockInCode;
            //    sim.DateIn = item1.DateIn;
            //    sim.EmployeeCode = item1.EmployeeCode;
            //    sim.Name = item1.Name;
            //    sim.Dept = item1.Dept;
            //    sim.User = item1.UserID;
            //    sim.Note = item1.Note;
            //}
            report.InitData(so.StockOutCode, so.DateOut, so.RecipientName, so.IDCard, so.UserID, so.Company, so.Note, data);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}