using MyDbWebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace MyDbWebApi.Classes
{
    public class ServicesHeartbeatService
    {
        public async Task<bool> ServicesHeartbeatLog(string spName)
        {
            bool returnValue = false;
            int serviceRelationID;
            switch (spName)
            {
                case Common.GetUnApprovedFacilityPhysicianList:
                    serviceRelationID = int.Parse(ConfigurationManager.AppSettings["HeartBeatSvcID_CPR2RH"].ToString());
                    returnValue = await MarkServiceStatus(serviceRelationID).ConfigureAwait(false);
                    break;
               
                    // create other cases also
            }
            return returnValue;
        }

        private async Task<bool> MarkServiceStatus(int serviceRelationID)
        {
            bool returnValue = false;
            HttpClient client = new HttpClient();
            try
            {
                string heartbeatUrl = ConfigurationManager.AppSettings["HeartBeatAPIURL"].ToString();
                heartbeatUrl += "Open/Execute/CreateServiceLog";

                var request = new ServicesHeartbeatRequest()
                {
                    ServiceEnvironmentRelationID = serviceRelationID,
                };

                StringContent content = new StringContent(JsonConvert.SerializeObject(request), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = client.PostAsync(heartbeatUrl, content).Result;
                if (responseMessage.IsSuccessStatusCode == true && responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string stringContent = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var response = JsonConvert.DeserializeObject<CommonAPIResponse>(stringContent);
                    if (response.ResultSets[0][0].Status == true)
                    {
                        returnValue = true;
                    }
                }
            }
            catch (Exception e)
            {
                returnValue = false;
            }
            finally
            {
                client.Dispose();
            }
            return returnValue;
        }
    }
}