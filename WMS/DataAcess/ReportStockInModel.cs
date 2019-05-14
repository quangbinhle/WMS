using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess
{
    public class ReportStockInModel
    {
        public string StockInCode { set; get; }
        public string User { set; get; }
        public string EmployeeCode { set; get; }
        public string Name { set; get; }
        public string Dept { set; get; }
        public DateTime DateIn { set; get; }
        public double? TotalWeigh { set; get; }
        public int? Quantity { set; get; }
        public string Note { set; get; }
    }
}
