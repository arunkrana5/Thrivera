using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.Models
{
    public class PJPPlan_Import
    {
        public class List
        {
            public int RowNum { get; set; }
            public string VisitDate { get; set; }
            public string DealerCode { get; set; }
            public string EMPCode { get; set; }
            public string Remarks { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string IPAddress { get; set; }
        }
    }
}
