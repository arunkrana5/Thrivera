using System.Web.Http;
using System.Web.Http.Controllers;
using DataBooster.DbWebApi;
using MyDbWebApi.Controllers;
using System.Web;
using System.Configuration;
using System;
using MyDbWebApi.AES256Encryption;
using System.Linq;
using DataBooster.DbWebApi.DataAccess;
using System.Collections.Generic;
using Newtonsoft.Json;
using MyDbWebApi.Models;
using MyDbWebApi.Classes;
using System.Web.Script.Serialization;

namespace MyDbWebApi
{
    public class DbWebApiAuthorizeAttribute : AuthorizeAttribute
    {
        private static IDbWebApiAuthorization _DbWebApiAuthorization;

        public static void RegisterWebApiAuthorization<T>() where T : IDbWebApiAuthorization, new()
        {
            _DbWebApiAuthorization = new T();
        }

        /// <param name="actionContext">The context.</param>
        /// <returns>true if the control is authorized; otherwise, false.</returns>
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {

            var userIdentity = (actionContext.ControllerContext.Controller as DbWebApiController).User.Identity;

            if (userIdentity == null || userIdentity.IsAuthenticated == false || string.IsNullOrEmpty(userIdentity.Name))
                return true;
            string password = "";
            string user = userIdentity.Name;
            string sp = actionContext.ControllerContext.RouteData.Values["sp"] as string;

            return _DbWebApiAuthorization.IsAuthorized(user, password, sp);
        }
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(actionContext);
            }
            else
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            }

        }

        //    private DbParallel.DataAccess db =  new DbParallel.DataAccess.DbAccess(ConfigurationManager.ConnectionStrings["DataBooster.DbWebApi.MainConnection"].ConnectionString);
        public override void OnAuthorization(HttpActionContext filterContext)
        {

            if (Authorize(filterContext))
            {
                return;
            }
            HandleUnauthorizedRequest(filterContext);
        }


        private bool Authorize(HttpActionContext actionContext)
        {
            try
            {
                var encodedString = actionContext.Request.Headers.GetValues("Token").First();

                bool validFlag = false;

                if (!string.IsNullOrEmpty(encodedString))
                {
                    string[] parts = encodedString.Split(new char[] { ':' });
                   var Splitparts = Common.Decrypt(parts[0]);
                    string[] key= Splitparts.Split(new char[] { '^' });

                   // if(Convert.ToDateTime(key[3]).AddDays(Convert.ToInt32))
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    // UserID
                    User blogObject = JsonConvert.DeserializeObject<User>(key[0]);
                    

                    #region Extracting Value from Token In future we will use


                    //var RandomKey = parts[1];                     // Random Key
                    //var CompanyID = Convert.ToInt32(parts[2]);    // CompanyID
                    //long ticks = long.Parse(parts[3]);            // Ticks
                    //DateTime IssuedOn = new DateTime(ticks);
                    //var ClientID = parts[4];                      // ClientID 
                    #endregion

                    //Newtonsoft.Json.Linq.JObject jprm = Newtonsoft.Json.Linq.JObject.Parse("{ 'TokenKey' : '" + encodedString + "','UserID': '" + UserID + "'}");

                    //InputParameters allParameters = new InputParameters(jprm);
                    DbWebApiController d = new DbWebApiController();
                    d.ControllerContext = actionContext.ControllerContext;


                    return true;
                }
                return validFlag;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}