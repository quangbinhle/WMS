using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace DataAcess
{
    public partial class InventoryReport : DevExpress.XtraReports.UI.XtraReport
    {
        public InventoryReport()
        {
            InitializeComponent();
        }

        public void InitData(string count, List<InventoryModel> data)
        {
            pCount.Value = count;           
            objectDataSource1.DataSource = data;
        }
    }
}
