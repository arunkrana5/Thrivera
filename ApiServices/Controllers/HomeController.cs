using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMaster;
using DataModal.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ApiServices.Controllers
{
    [Authorize]
    [Route("Home")]
    public class HomeController : ApiController
    {
        IToolHelper tools;
        public HomeController()
        {
            tools = new ToolsModal();
        }

        [HttpPost]
        [Route("GetAppMenuList")]
        public IHttpActionResult GetAppMenuList(GetMenuResponse Modal)
        {
            List<AdminMenu> result = new List<AdminMenu>();
            result = tools.GetAppMenuList(Modal);
            return Ok(result);
        }
       
      

    }
}
