using System;
using System.Net;
using System.Net.Http;
using System.Web;

namespace Website.Repository
{
    public class ServiceRepository
    {

        public HttpClient Client { get; set; }
        public ServiceRepository()
        {
            var request = HttpContext.Current.Request;
            var address = string.Format("{0}://{1}", request.Url.Scheme, request.Url.Authority);

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            Client = new HttpClient();
            Client.BaseAddress = new Uri(address);
        }
        public HttpResponseMessage GetResponse(string url)
        {

            return Client.GetAsync(url).Result;
        }
        public HttpResponseMessage PutResponse(string url, object model)
        {
            return Client.PutAsJsonAsync(url, model).Result;
        }
        public HttpResponseMessage PostResponse(string url, object model)
        {
            return Client.PostAsJsonAsync(url, model).Result;
        }
        public HttpResponseMessage DeleteResponse(string url)
        {
            return Client.DeleteAsync(url).Result;
        }
    }
}