using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDbWebApi.Models
{
    public class CommonAPIResponse
    {
        public List<List<HBResponse>> ResultSets { get; set; }
    }

    public class HBResponse
    {
        public bool Status { get; set; }
        public string ResponseMessage { get; set; }
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
    }

}