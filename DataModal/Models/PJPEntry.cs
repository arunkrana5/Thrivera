using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataModal.Models
{
    public class PJPEntry
    {
        public class List
        {
            public int RowNum { get; set; }
            public string VisitDate { get; set; }
            public long PJPEntryID { get; set; }
            public long PJPPlanID { get; set; }
            public string DealerType { get; set; }
            public string DealerName { get; set; }
            public string DealerCode { get; set; }
            public string DealerArea { get; set; }
            public long SSRTourPlanID { get; set; }
            public string SSRName { get; set; }
            public string SSRCode { get; set; }
            public string SSRPhone { get; set; }
            public string SSRGender { get; set; }
            public string SSRDesignName { get; set; }
            public string SSRDeptName { get; set; }
            public string SSREmailID { get; set; }
            public string SSRRole { get; set; }
            public string SSRUserID { get; set; }
            public string SSRDOJ { get; set; }
            public string SSRDOB { get; set; }
            public string SSRBranchCode { get; set; }
            public string SSRBranchName { get; set; }
            public string SSRRegionName { get; set; }
            public string SSRStateName { get; set; }
            public string SSRCityName { get; set; }
            public string SSRAreaName { get; set; }
     
            public long AttachID { get; set; }
            public string AttachPath { get; set; }
            public int ProductRating { get; set; }
            public int CustomerRating { get; set; }
            public string ProductKnw { get; set; }
            public string CustomerKnw { get; set; }
            public bool IsActive { get; set; }
            public int Priority { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }

            public string ExpenseAmt { get; set; }
            public string AttachmentPath { get; set; }
            public string ExpenseAttachmentPath { get; set; }
        }

        public class Add
        {
            public string ContactPerson_Name { get; set; }

            public string ContactPerson_Phone { get; set; }
            public long? PJPEntryID { get; set; }
            public long? PJPPlanID { get; set; }
            [Required(ErrorMessage = "SSR Can't be Blank")]
            public long? SSRTourPlanID { get; set; }
            public string SSRName { get; set; }
            public string SSRCode { get; set; }
            public string AttPunchedBySSR { get; set; }
            public long? SSR_AttendenceID { get; set; }
            [Required(ErrorMessage = "Availability Can't be Blank")]
            public string SSRAvailability { get; set; }

            [Required(ErrorMessage = "Image Can't be Blank")]
            public string ImageBase64String { get; set; }
            public long? AttachmentID { get; set; }
            public string AttachmentPath { get; set; }
            [Required(ErrorMessage = "Product Rating Can't be Blank")]
            public int? ProductRating { get; set; }
            [Required(ErrorMessage = "Customer Rating Can't be Blank")]
            public int? CustomerRating { get; set; }
            public string ProductKnw { get; set; }
            public string CustomerKnw { get; set; }
            public string Location { get; set; }
            [Required(ErrorMessage = "Location not Found please check GPS Permission")]
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string Error { get; set; }
            public string Notes { get; set; }

            [Required(ErrorMessage = "Expense Amount Can't be Blank")]
            public decimal? ExpenseAmt { get; set; }

            public HttpPostedFileBase ExpenseUpload { get; set; }

            public long? ExpenseAttachmentID { get; set; }
            public string ExpenseRemarks { get; set; }
            public string ExpenseAttachmentPath { get; set; }

            public List<PJPEntry_Brand.List> BrandEntryList { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
            

            public List<DropDownlist> SSRList { get; set; }
            public List<DropDownlist> BrandList { get; set; }


        }

        public class AddWithNoSSR
        {
            [Required(ErrorMessage = "Contact Person Name Can't be Blank")]
            public string ContactPerson_Name { get; set; }
            [Required(ErrorMessage = "Contact Person Phone Can't be Blank")]
            public string ContactPerson_Phone { get; set; }
            public long? PJPEntryID { get; set; }
            public long? PJPPlanID { get; set; }
          
            [Required(ErrorMessage = "Image Can't be Blank")]
            public string ImageBase64String { get; set; }
            public long? AttachmentID { get; set; }
            public string AttachmentPath { get; set; }
            public string Location { get; set; }
            [Required(ErrorMessage = "Location not Found please check GPS Permission")]
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string Error { get; set; }
            public string Notes { get; set; }

            [Required(ErrorMessage = "Expense Amount Can't be Blank")]
            public decimal? ExpenseAmt { get; set; }
            public HttpPostedFileBase ExpenseUpload { get; set; }
            public long? ExpenseAttachmentID { get; set; }
            public string ExpenseRemarks { get; set; }
            public string ExpenseAttachmentPath { get; set; }
            public List<PJPEntry_Brand.List> BrandEntryList { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
            public List<DropDownlist> BrandList { get; set; }

            public long? SSRTourPlanID { get; set; }
            public string SSRAvailability { get; set; }
            public long? SSR_AttendenceID { get; set; }


        }

    }
    public class PJPEntry_Brand
    {
        public class List
        {
            public int? RowNum { get; set; }
            public long? PJPBrandID { get; set; }
            public long? PJPEntryID { get; set; }
            public long? BrandID { get; set; }
            public string BrandName { get; set; }
            public long? Qty { get; set; }
            public long? AttachID { get; set; }
            public string AttachPath { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
            public long LoginID { get; set; }
            public string ImageBase64String { get; set; }
        }

        
    }


    public class PJPExpense
    {
        public class List
        {
            public long PJPEntryID { get; set; }
            public long PJPPlanID { get; set; }
            public string Image { get; set; }
            public string VisitDate { get; set; }
            public string BranchCode { get; set; }
            public string BranchName { get; set; }
            public string BTName { get; set; }
            public string BTCode { get; set; }
            public string DealerType { get; set; }
            public string DealerName { get; set; }
            public string DealerCode { get; set; }
            public string RegionName { get; set; }
            public string StateName { get; set; }
            public string CityName { get; set; }
            public string AreaName { get; set; }
            public string PurposeVisit { get; set; }
            public string Exp_Amount { get; set; }
            public string Exp_Remarks { get; set; }
            public string SSRName { get; set; }
            public string SSRCode { get; set; }
            public string ApprovedStatus { get; set; }
            public string ApprovedRemarks { get; set; }
            public string Approvedby { get; set; }
            public string ApprovedDate { get; set; }
            public int Approved { get; set; }
            public string ContactPerson_Name { get; set; }
            public string ContactPerson_Phone { get; set; }

        }
    }
}
