using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.Models
{
    public class UserHierarchyImport
    {
        public class List
        {
            public int RowNum { get; set; }

            public string UserID { get; set; }
            public string UserType { get; set; }
            public string LinkDoctype { get; set; }
            public string LinkCode { get; set; }
            public string Remarks { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string IPAddress { get; set; }
        }
    }
}
