using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace DataAcess
{
    public partial class PrintBarcodeReport : DevExpress.XtraReports.UI.XtraReport
    {
        public PrintBarcodeReport()
        {
            InitializeComponent();
        }

        public void InitData(List<BarcodeModel> data)
        {
            objectDataSource1.DataSource = data;
        }
    }
}
