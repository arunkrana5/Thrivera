﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.Models
{
     public class ManageActivities
    {
        public class Sale
        {
            [Required(ErrorMessage = "Sale No Can't be Blank")]
            [StringLength(3999)]
            public string SaleNo { get; set; }
            [Required(ErrorMessage = "Status Can't be Blank")]
            public int? Approved { get; set; }
            [Required(ErrorMessage = "Remarks Can't be Blank")]
            public string ApprovedRemarks { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
        }

        public class Attendance
        {
            [Required(ErrorMessage = "EMP Code Can't be Blank")]
            public string EMPCode { get; set; }

            [Required(ErrorMessage = "Date Can't be Blank")]
            public DateTime Date { get; set; }
            [Required(ErrorMessage = "Status Can't be Blank")]
            public long? StatusID { get; set; }
            public string Notes { get; set; }
            public List<DropDownlist> AttendenceStatusList { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
        }
    }
}
