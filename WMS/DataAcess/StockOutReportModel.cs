using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess
{
    public class StockOutReportModel
    {
        public string Barcode { get; set; }
        public string WasteName { get; set; }
        public string Type { get; set; }
        public double Weigh { get; set; }
        public string Unit { get; set; }
        public string Note { get; set; }
        public int? Quantity { get; set; }
        public double? TotalWeigh { get; set; }
    }
}
