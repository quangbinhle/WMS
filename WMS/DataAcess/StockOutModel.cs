using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess
{
    public class StockOutModel
    {
        public string StockOutCode { get; set; }
        public DateTime DateOut { get; set; }
        public string RecipientName { get; set; }
        public string IDCard { get; set; }
        public string User { get; set; }
        public string Company { get; set; }
        public string Note { get; set; }

    }
}
