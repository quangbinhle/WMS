using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class StockInReportModel
    {
        public string Barcode { get; set; }
        public string TenXuong { get; set; }
        public string TenKho { get; set; }
        public string TenRac { get; set; }
        public double TrongLuong {get;set;}

        public string DonVi {get;set;}
        public int?  SoLuong { get; set; }
        public double? TongTrongLuong { get; set; }

    }
}
