using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModal.Models
{
    public class UserHierarchy
    {
        public class List
        {
            public int RowNum { get; set; }
            public long HierarchyID { get; set; }
            public long LoginID { get; set; }
            public string UserName { get; set; }
            public string UserID { get; set; }
            public string UserType { get; set; }
            public string EmailID { get; set; }
            public string Doctype { get; set; }
            public string LinkName { get; set; }
           public string DealerType { get; set; }
            public bool IsActive { get; set; }
            public int? Priority { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
        }
        public class Add
        {
            public long? HierarchyID { get; set; }
            [Required(ErrorMessage = "User Can't be Blank")]
            public long UserLoginID { get; set; }
            [Required(ErrorMessage = "Can't be Blank")]
            public string TableIDs { get; set; }
            public string UserType { get; set; }
            public bool IsActive { get; set; }
            public int? Priority { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }

            
            public string DealerType { get; set; }
            public List<DropDownlist> TableList { get; set; }
            public List<DropDownlist> UserList { get; set; }
            public List<DropDownlist> DealerTypeList { get; set; }

        }
    }

   
}
