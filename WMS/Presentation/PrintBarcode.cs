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
    public partial class PrintBarcode : DevExpress.XtraEditors.XtraForm
    {
        public PrintBarcode()
        {
            InitializeComponent();
        }

        public void Print(List<BarcodeModel> data)
        {
            PrintBarcodeReport pb = new PrintBarcodeReport();
            pb.InitData(data);
            documentViewer1.DocumentSource = pb;
            pb.CreateDocument();
        }
    }
}