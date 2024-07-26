using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDbWebApi.Models
{
    public class clsToken
    {
        public int TokenID { get; set; }
        public string TokenKey { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
        public int? CompanyID { get; set; }
        public int UserID { get; set; }
        public DateTime? CreateOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpldatedBy { get; set; }
     
    }
   
}