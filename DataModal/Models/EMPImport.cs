using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.Models
{
   public class EMPImport
    {
        public class List
        {
            public int RowNum { get; set; }
            public long DealerImportID { get; set; }
            public string EMPCode { get; set; }
            public string EMPName { get; set; }
            public string Phone { get; set; }
            public string EmailID { get; set; }
            public string FatherName { get; set; }
            public string DOB { get; set; }
            public string Gender { get; set; }
            public string Design { get; set; }
            public string Depart { get; set; }
            public string DOJ { get; set; }
            public string PAN { get; set; }
            public string UAN { get; set; }
            public string ESIC { get; set; }
            public string PaymentMode { get; set; }

            public string Country { get; set; }
            public string State { get; set; }
            public string City { get; set; }
            public string Location { get; set; }
            public string Address { get; set; }
            public string Pincode { get; set; }
            public string AccountNo { get; set; }
            public string BankName { get; set; }
            public string BranchName { get; set; }
            public string IFSCCode { get; set; }
            public string UserID { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
            public string Remarks { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string IPAddress { get; set; }
        }
    }
}
