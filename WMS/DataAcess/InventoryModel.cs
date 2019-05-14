using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess
{
    public class InventoryModel
    {
        public string barcode { set; get; }
        public string StockInCode { set; get; }
        public string WasteName { set; get; }
        public string Type { set; get; }
        public string StorageName { set; get; }
        public DateTime DateIn { set; get; }
        public double? Weigh { set; get; }
        public string Unit { set; get; }
        public string Note { set; get; }

    }
}
