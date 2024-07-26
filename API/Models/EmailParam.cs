using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDbWebApi.Models
{
    public class EmailParam
    {
        public string Email { get; set; }
        public string Body { get; set; }
        public UserCredentialEmailParam Params { get; set; }
        public string Subject { get; set; }
        public EmailType EmailType { get; set; }
    }


    public enum EmailType
    {
        NewUserCrendential = 1,
        UpdateUserCrendential = 2
    }

    public class UserCredentialEmailParam
    {
        public string CustomerName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string LoginUrl { get; set; }

    }
}