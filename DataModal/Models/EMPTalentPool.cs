using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataModal.Models
{
    public class EMPTalentPool
    {
        public class List
        {
            public long TPID { get; set; }
            public string TPCode { get; set; }
            public string DOB { get; set; }
            public string Name { get; set; }
            public string Mobile { get; set; }
            public string Email { get; set; }
            public string WorkProfile { get; set; }
            public string Address { get; set; }
            
            public string Region { get; set; }

            public string BranchName { get; set; }
            public string BranchCode { get; set; }
            public string DealerName { get; set; }
            public string DealerCode { get; set; }
            public string DealerType { get; set; }
            public string Trade_Experience { get; set; }
            public decimal ExpectedSalary { get; set; }
            public string Qualification { get; set; }
            public string State { get; set; }
            public string City { get; set; }
            public string Pincode { get; set; }
            public string Experience { get; set; }
            public int AttachID { get; set; }
            public string AttachPath { get; set; }
            public string CW_Company { get; set; }
            public string CW_Address { get; set; }
            
            public string CW_State { get; set; }
            public string CW_City { get; set; }
            public string CW_Pincode { get; set; }
            public decimal CW_Salary { get; set; }
            public bool IsActive { get; set; }
            public int Priority { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
            public string Status { get; set; }
        }
        public class Add
        {
            
            public long? TPID { get; set; }
            public string TPCode { get; set; }
            [Required(ErrorMessage = "Name Can't be Blank")]
            public string Name { get; set; }
            

            [Required(ErrorMessage = "DOB Can't be Blank")]
            public string DOB { get; set; }

            [Required(ErrorMessage = "Mobile Can't be Blank")]
            public string Mobile { get; set; }

            [EmailAddress]
            [Required(ErrorMessage = "Email Can't be Blank")]
            public string Email { get; set; }
            [Required(ErrorMessage = "Work Profile Can't be Blank")]
            public string WorkProfile { get; set; }
            public string Address { get; set; }

            [Required(ErrorMessage = "Dealer Can't be Blank")]
            public long? DealerID { get; set; }

            [Required(ErrorMessage = "Trade Experience Can't be Blank")]
            public string Trade_Experience { get; set; }

            [Required(ErrorMessage = "Expected Salary Can't be Blank")]
            public decimal? ExpectedSalary { get; set; }
            [Required(ErrorMessage = "Qualification Can't be Blank")]
            public string Qualification { get; set; }

            [Required(ErrorMessage = "Branch Can't be Blank")]
            public long? BranchID { get; set; }
            public long? State { get; set; }
            public long? City { get; set; }
            [Required(ErrorMessage = "Pincode Can't be Blank")]
            public string Pincode { get; set; }
            [Required(ErrorMessage = "Experience Can't be Blank")]
            public string Experience { get; set; }
            public long? AttachID { get; set; }
            public string AttachPath { get; set; }
            public string CW_Company { get; set; }
            public string CW_Address { get; set; }
            
            public long? CW_State { get; set; }
            public long? CW_City { get; set; }
            public string CW_Pincode { get; set; }
            public decimal? CW_Salary { get; set; }
          
            public int IsActive { get; set; }
            public int Priority { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }

            [Required(ErrorMessage = "Location not Found please check GPS Permission")]
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string Error { get; set; }
            public List<DropDownlist> BranchList { get; set; }

            public List<DropDownlist> DealerList { get; set; }
            public List<DropDownlist> StateList { get; set; }
            public List<DropDownlist> CityList { get; set; }
            public List<DropDownlist> CW_CityList { get; set; }
            public HttpPostedFileBase Upload { get; set; }
        }
    }
}
