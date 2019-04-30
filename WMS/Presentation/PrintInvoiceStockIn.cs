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
using Business;
using DataAcess;

namespace Presentation
{
    public partial class PrintInvoiceStockIn : DevExpress.XtraEditors.XtraForm
    {
        WMSDataContext dc = new WMSDataContext();
        //string code;
        public PrintInvoiceStockIn()
        {
            InitializeComponent();
        }


        //public PrintInvoiceStockIn(string stockincode):this()
        //{
        //    code = stockincode;
        //}

        

        public void PrintInvoice( string code ,List<StockInReportModel> data )
        {
            StockInInvoicePara report = new StockInInvoicePara();
            foreach (DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            //
            var model = (from sid in dc.StockIns
                        join e in dc.Employees on sid.EmployeeCode equals e.EmployeeCode
                        where (sid.StockInCode==code)
                        select new
                        {
                            sid.StockInCode,
                            sid.DateIn,
                            sid.EmployeeCode,
                            e.Name,
                            e.Dept,
                            sid.UserID,
                            sid.Note
                        });
            StockInModel sim = new StockInModel();
            foreach (var item1 in model)
            {
                sim.StockInCode = item1.StockInCode;
                sim.DateIn = item1.DateIn;
                sim.EmployeeCode = item1.EmployeeCode;
                sim.Name = item1.Name;
                sim.Dept = item1.Dept;
                sim.User = item1.UserID;
                sim.Note = item1.Note;
            }
            report.InitData(sim.StockInCode,sim.DateIn,sim.EmployeeCode,sim.Name,sim.Dept,sim.User,sim.Note,data);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }

    }
}