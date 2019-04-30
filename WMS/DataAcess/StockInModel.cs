using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess
{
    public class StockInModel
    {
        public string StockInCode { set; get; }
        public DateTime DateIn { set; get; }
        public string EmployeeCode { set; get; }
        public string Name { set; get; }
        public string Dept { set; get; }
        public string User { set; get; }
        public string Note { set; get; }

    }
}
