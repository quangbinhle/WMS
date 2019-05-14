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
    public partial class PrintReportStockOut : DevExpress.XtraEditors.XtraForm
    {
        public PrintReportStockOut()
        {
            InitializeComponent();
        }

        public void Print(string count,List<ReportStockOutModel> data)
        {
            ReportStockOut pb = new ReportStockOut();
            foreach (DevExpress.XtraReports.Parameters.Parameter p in pb.Parameters)
            {
                p.Visible = false;
            }
            pb.InitData(count,data);
            documentViewer1.DocumentSource = pb;
            pb.CreateDocument();
        }
    }
}