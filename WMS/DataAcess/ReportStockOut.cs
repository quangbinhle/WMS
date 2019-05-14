using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace DataAcess
{
    public partial class ReportStockOut : DevExpress.XtraReports.UI.XtraReport
    {
        public ReportStockOut()
        {
            InitializeComponent();
        }

        public void InitData(string count,List<ReportStockOutModel> data)
        {
            pCount.Value = count;
            objectDataSource1.DataSource = data;
        }
    }
}
