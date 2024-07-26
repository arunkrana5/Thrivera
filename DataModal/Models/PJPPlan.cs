using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModal.Models
{
    public class PJPPlan
    {
        public class List
        {
            public int RowNum { get; set; }
            public long PJPID { get; set; }
            public string DocNo{ get; set; }
            public string DocDate { get; set; }
            public string DealerName { get; set; }
            public string DealerCode { get; set; }
            public string DealerArea { get; set; }
            
            public string DealerAddress { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string VisitDate { get; set; }
            public bool IsActive { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
        }
        public class Add
        {
            public int? OnDemand { get; set; }
            public long? PJPID { get; set; }

            [Required(ErrorMessage = "Dealer Can't be Blank")]
            public long? DealerID { get; set; }

            [Required(ErrorMessage = "EMP Can't be Blank")]
            public long? EMPID { get; set; }

            [Required(ErrorMessage = "Visit Date Can't be Blank")]
            public string VisitDate { get; set; }
            public int? Priority { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }

            public string DocNo { get; set; }
            public string DocDate { get; set; }
            public List<DropDownlist> DealerList { get; set; }
            public List<DropDownlist> EMPList { get; set; }
        }
    }
}
