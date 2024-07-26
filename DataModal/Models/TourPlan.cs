using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModal.Models
{
    public class TourPlan
    {
        public class List
        {
            public int RowNum { get; set; }
            public long TourPlanID { get; set; }
            public string RegionName { get; set; }
            public string StateName { get; set; }
            public string CityName { get; set; }
            public string AreaName { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string DealerName { get; set; }
            public string DealerCode { get; set; }
            public string StartDate { get; set; }
            public string BranchName { get; set; }
            public string BranchCode { get; set; }
            public string EndDate { get; set; }
            public bool IsActive { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
        }
        public class Add
        {
            public long? TourPlanID { get; set; }

            [Required(ErrorMessage = "Branch Can't be Blank")]
            public long? BranchID { get; set; }

            [Required(ErrorMessage = "Region Can't be Blank")]
            public long? RegionID { get; set; }
            [Required(ErrorMessage = "State Can't be Blank")]
            public long? StateID { get; set; }
            [Required(ErrorMessage = "City Can't be Blank")]
            public long? CityID { get; set; }
            [Required(ErrorMessage = "Area Can't be Blank")]
            public long? AreaID { get; set; }
            [Required(ErrorMessage = "Employee Can't be Blank")]
            public long? EMPID { get; set; }
            [Required(ErrorMessage = "Dealer Can't be Blank")]
            public long? DealerID { get; set; }
            [Required(ErrorMessage = "Start Date Can't be Blank")]
            public string StartDate { get; set; }
            [Required(ErrorMessage = "End Date Can't be Blank")]
            public string EndDate { get; set; }
            public int? Priority { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
            public int MaxEMPCount { get; set; }

            public List<DropDownlist> DealerList { get; set; }
            public List<DropDownlist> EMPList { get; set; }
            public List<DropDownlist> StateList { get; set; }
            public List<DropDownlist> CityList { get; set; }
            public List<DropDownlist> AreaList { get; set; }
            public List<DropDownlist> RegionList { get; set; }
            public List<DropDownlist> BranchList { get; set; }
        }
    }
}

