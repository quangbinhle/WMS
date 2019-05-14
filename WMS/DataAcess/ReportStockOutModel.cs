using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess
{
    public class ReportStockOutModel
    {
        public string StockOutCode { set; get; }
        public string User { set; get; }
        public DateTime DateOut { set; get; }
        public string RecipientName { set; get; }
        public string IDCard { set; get; }
        public string Company { set; get; }
        public double? TotalWeigh { set; get; }
        public int? Quantity { set; get; }
        public string Note { set; get; }
    }
}
