using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Business;
using System.Collections.Generic;

namespace DataAcess
{
    public partial class StockInInvoicePara : DevExpress.XtraReports.UI.XtraReport
    {
        public StockInInvoicePara()
        {
            InitializeComponent();
        }
        public void InitData(string StockInCode, DateTime DateIn, string EmployeeCode, string Name, string Dept, string User, string Note, List<StockInReportModel> data)
        {
            pStockInCode.Value = StockInCode;
            pDateIn.Value = DateIn;
            pEmployeeCode.Value = EmployeeCode;
            pName.Value = Name;
            pDept.Value = Dept;
            pUser.Value = User;
            pNote.Value = Note;
            objectDataSource1.DataSource = data;
        }
    }
}
