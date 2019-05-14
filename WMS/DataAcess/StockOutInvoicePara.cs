using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace DataAcess
{
    public partial class StockOutInvoicePara : DevExpress.XtraReports.UI.XtraReport
    {
        public StockOutInvoicePara()
        {
            InitializeComponent();
        }

        public void InitData(string StockOutCode, DateTime DateOut, string RecipientName, string IDCard, string User, string Company, string Note, List<StockOutReportModel> data)
        {
            pStockOutCode.Value = StockOutCode;
            pDateOut.Value = DateOut;
            pRecipientName.Value = RecipientName;
            pIDCard.Value = IDCard;
            pUser.Value = User;
            pCompany.Value = Company;
            pNote.Value = Note;
            objectDataSource1.DataSource = data;
        }
    }
}
