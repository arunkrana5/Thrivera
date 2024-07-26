using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDbWebApi.Models
{
    public class Documents
    {
        public int DocumentID { get; set; }
        public string docBase64 { get; set; }
        public int AccesssionNumber { get; set; }
        public string Description { get; set; }
        public string FileType { get; set; }
        public string filepath { get; set; }
        public string Documenttype { get; set; }
        public bool IsArchiveable { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedUserID { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUserId { get; set; }
    }
    //public class User
    //{
    //    public int UserId { get; set; }
    //    public bool IsBlocked { get; set; }
    //    public bool IsLimitOver { get; set; }
    //    public string Username { get; set; }
    //    public string Password { get; set; }
    //    public string EmailId { get; set; }
    //    public bool Issuccess { get; set; }
    //    public string Role { get; set; }
    //    public string Status { get; set; }
    //    public string Access { get; set; }
    //    public string Name { get; set; }
    //    public int SCID { get; set; }
    //    public string CreatedDate { get; set; }
    //    public string CreatedBy { get; set; }
    //    public string ModifiedDate { get; set; }
    //    public string ModifiedBy { get; set; }
    //    public string LoginUrl { get; set; }
    //    public string Message { get; set; }
    //    public bool status { get; set; }
    //    public long LoginID { get; set; }
    //    public string UserID { get; set; }
    //    public long RoleID { get; set; }
    //    public long EMPID { get; set; }
    //    public string DesignName { get; set; }
    //    public string DeptName { get; set; }

    //    public decimal Work_Latitude { get; set; }
    //    public decimal Work_Longitude { get; set; }
    //    public string EMPName { get; set; }
    //    public string EMPCode { get; set; }
    //    public string Gender { get; set; }
    //    public string AttendenceStatus { get; set; }
    //    public string DealerName { get; set; }
    //    public string DealerCode { get; set; }
    //    public string Work_AreaName { get; set; }
    //    public string ImageURL { get; set; }

    //    public long TourPlanID { get; set; }
    //    public string UserType { get; set; }

    //    public string AllowLogin { get; set; }
    //    public string CompanyCode { get; set; }
    //}


    public class User
    {
        public bool IsBlocked { get; set; }
        public bool IsLimitOver { get; set; }
        public string Access { get; set; }
        public string status { get; set; }
        public long LoginID { get; set; }
        public string UserID { get; set; }
        public long RoleID { get; set; }
        public string SessionID { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string AllowLogin { get; set; }

        public string EMPName { get; set; }
        public long EMPID { get; set; }
        public string Gender { get; set; }
        public string DesignName { get; set; }
        public string EMPCode { get; set; }
        public string AttendenceStatus { get; set; }
        public long TourPlanID { get; set; }
        public string DealerName { get; set; }
        public string DealerCode { get; set; }
        public string Work_AreaName { get; set; }
        public string Work_Latitude { get; set; }
        public string Work_Longitude { get; set; }
        public string ImageURL { get; set; }
        public string UserType { get; set; }
        public string CompanyCode { get; set; }
        public string Message { get; set; }
    }

    public class ResultSet
    {
        Documents ResultSets { get; set; }
    }
    public class OutputParameters
    {
    }

    public class RootObject<T>
    {
        public List<List<T>> ResultSets { get; set; }

    }

    public class Response
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string PreSignedUrl { get; set; }
    }
}