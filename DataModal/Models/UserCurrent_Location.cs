using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.Models
{
    public class UserCurrent_Location
    {
        public long EMPID { get; set; }
        public string Location { get; set; }
        [Required(ErrorMessage = "Location not Found please check GPS Permission")]
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Error { get; set; }
        public string Notes { get; set; }
        public long LoginID { get; set; }
        public string IPAddress { get; set; }

    }
}
