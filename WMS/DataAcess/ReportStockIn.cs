using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace DataAcess
{
    public partial class ReportStockIn : DevExpress.XtraReports.UI.XtraReport
    {
        public ReportStockIn()
        {
            InitializeComponent();
        }

        public void InitData(string count,List<ReportStockInModel> data)
        {
            pCount.Value = count;
            objectDataSource1.DataSource = data;
        }
    }
}
