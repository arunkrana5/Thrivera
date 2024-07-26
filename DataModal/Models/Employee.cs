using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using static DataModal.Models.AllEnum;

namespace DataModal.Models
{
    public class Employee
    {
        public class List
        {
            public int RowNum { get; set; }
            public long EMPID { get; set; }
            public string EMPCode { get; set; }
            public string EMPName { get; set; }
            public string Phone { get; set; }
            public string EmailID { get; set; }
            public string FatherName { get; set; }
            public string DOB { get; set; }
            public string DOJ { get; set; }
            public string DOL { get; set; }
            public string DOLReason { get; set; }
            public string Gender { get; set; }
            public string DeptName { get; set; }
            public string DesignName { get; set; }
            public bool IsActive { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
        }
        public class Add
        {
            public long? EMPID { get; set; }
            public string EMPCode { get; set; }
            [Required(ErrorMessage = "Name Can't be Blank")]
            public string EMPName { get; set; }
            [Required(ErrorMessage = "Phone Can't be Blank")]
            public string Phone { get; set; }
            public string PAN { get; set; }
            [Required(ErrorMessage = "Email Can't be Blank")]
            public string EmailID { get; set; }

            [Required(ErrorMessage = "Father Can't be Blank")]
            public string FatherName { get; set; }
            [Required(ErrorMessage = "Date of Birth Can't be Blank")]
            public string DOB { get; set; }

            [Required(ErrorMessage = "Date of Birth Can't be Blank")]
            public string DOJ { get; set; }
            [Required(ErrorMessage = "Gender Can't be Blank")]
            public Gender? Gender { get; set; }
            public long? DesignID { get; set; }
            public long? DepartID { get; set; }
            public long? UserID { get; set; }
            public long? HODID { get; set; }

            public long AttachID { get; set; }
            public int Priority { get; set; }

            public string UAN { get; set; }
            public string ESIC { get; set; }
            public string PaymentMode { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
            public string DOL { get; set; }

            public HttpPostedFileBase Upload { get; set; }
            public List<DropDownlist> DepartmentList { get; set; }
            public List<DropDownlist> DesignationList { get; set; }
            public List<DropDownlist> RoleList { get; set; }

            public List<DropDownlist> EMPList { get; set; }

            public List<DropDownlist> StateList { get; set; }
            public List<DropDownlist> CityList { get; set; }
            public Users.Add UserDetails { get; set; }

            public Address AddressDetails { get; set; }
            public Bank BankDetails { get; set; }
        }

        public class UpdateDOL
        {
            public long EMPID { get; set; }
            public string DOL { get; set; }
            public string Reason { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }

        }


    }

    public class LeaveBalance
    {
        public class List
        {
            public int RowNum { get; set; }
            public long LBID { get; set; }
            public long EMPID { get; set; }
            public string EMPCode { get; set; }
            public string EMPName { get; set; }
            public string ImageURL { get; set; }
            public float Opening_Hrs { get; set; }
            public float Accrued_Hrs { get; set; }
            public float Total_Hrs { get; set; }
            public float Availed_Hrs { get; set; }
            public float Balance_Hrs { get; set; }
            public float Arrears_Hrs { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
        }

        public class TranList
        {
            public string Month { get; set; }
            public string MonthName { get; set; }
            public string LeaveTypeName { get; set; }
            public string FinYear { get; set; }
            public float Opening_Hrs { get; set; }
            public float Accrued_Hrs { get; set; }
            public float Total_Hrs { get; set; }
            public float Availed_Hrs { get; set; }
            public float Balance_Hrs { get; set; }
            public float Arrears_Hrs { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
        }
    }


    public class BirthdayList
    {
        public string EMPName { get; set; }
        public string EMPCode { get; set; }
        public string ImageURL { get; set; }
        public string EmailID { get; set; }
    }
}
