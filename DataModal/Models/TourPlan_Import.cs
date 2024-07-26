using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.Models
{
    public class TourPlan_Import
    {
        public class List
        {
            public int RowNum { get; set; }
            public long ID { get; set; }
            public string RegionCode { get; set; }
            public string StateCode { get; set; }
            public string CityCode { get; set; }
            public string AreaCode { get; set; }
            public string Remarks { get; set; }
            public string EMPCode { get; set; }
            public string BranchCode { get; set; }
            public string DealerCode { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string IPAddress { get; set; }
        }
    }
}
