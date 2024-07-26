using System.Net.Http;
using System.Web.Http;
using DataBooster.DbWebApi;
using System.Web.Routing;
using System.Net;
using MyDbWebApi.Models;
using System.Web;
using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf;
using System.Text;
using System;
using System.Security.Claims;
using static MyDbWebApi.AES256Encryption.EncryptionLibrary;
using MyDbWebApi.BCryptEncryption;
using MyDbWebApi.AES256Encryption;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Net.Mail;
using MyDbWebApi.AwsS3Utiltyy;
using MyDbWebApi.Classes;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Drawing;
using System.IO;
using System.Web.Script.Serialization;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace MyDbWebApi.Controllers
{

    public class DbWebApiController : ApiController
    {
        public static string PathURL= ConfigurationManager.AppSettings["Pathurl"].ToString();
        string cryptoIV = "RAISOFT";
        [DbWebApiAuthorize]  // Authorize filter which is used to check token is validate or not.
        [AcceptVerbs("GET", "POST", "PUT", "DELETE")]
        public HttpResponseMessage DynExecute(string sp, InputParameters allParameters)
        {
          
            // following method decypt parameters from request //
            allParameters = DecryptParams(allParameters, out bool isDataEncrypted);

            var isSP = (bool)allParameters.Parameters.ToList().Find(m => m.Key.Equals("isSP")).Value;

            if (isSP)
            {
                sp = "SPU_" + sp;
            }

            var base64String = allParameters.Parameters.ToList().Find(m => m.Key.Equals("base64String"));
            var fileName = allParameters.Parameters.ToList().Find(m => m.Key.Equals("fileName"));
            if (base64String.Key != null && fileName.Key != null)
            {
                //item.Value = EncryptText(item.Value.ToString()) as object;
                allParameters.Parameters.Remove(base64String.Key);
                //allParameters.Parameters.Add("Password", EncryptText(itemPassword.Value.ToString()));               


                bool IsFileUpload = S3Storage.UploadFile(base64String.Value.ToString(), fileName.Value.ToString());

                if (!IsFileUpload)
                {
                    var response1 = new Response[1][];
                    response1.SetValue(new Response[] { new Response { IsSuccess = false, Message = "Error occured while file uploaded on server", PreSignedUrl = "" } }, 0);
                    return Request.CreateResponse(HttpStatusCode.OK, new { ResultSets = response1, ReturnValue = 0 });

                }
            }

            SetUserName(allParameters);                         // Set the conventional User Name Parameter if configured.

            //return this.DynExecuteDbApi(sp, allParameters);     // The main entry point to call the DbWebApi.

            HttpResponseMessage response = this.DynExecuteDbApi(sp, allParameters);
            response = EncryptParams(response, isDataEncrypted);
            return response;

        }

        [DbWebApiAuthorize]  // Authorize filter which is used to check token is validate or not.
        [AcceptVerbs("GET", "POST", "PUT", "DELETE")]
        public HttpResponseMessage RegisterUser(string sp, string other, InputParameters allParameters)
        {
            try { 
            string pathFor = (string)allParameters.Parameters.ToList().Find(m => m.Key.Equals("pathFor")).Value;
            if (!string.IsNullOrEmpty(pathFor))
            {

                IDictionary<string, object> numberNames = new Dictionary<string, object>();
                numberNames.Add("ConfigKey", "ApplicationPhysicalPath"); //adding a key/value using the Add() method
                var result = this.ExecuteDbApi("spu_GetConfigSetting", numberNames);
                var RmpathFor = allParameters.Parameters.ToList().Find(m => m.Key.Equals("pathFor"));
                allParameters.Parameters.Remove(RmpathFor.Key);

                var userFetchDataResponses = result.Content.ReadAsAsync<RootObject<ResponsesFilePath>>().Result;
                var data = userFetchDataResponses.ResultSets[0][0];
                string PathURL = GetPhysicalPath(pathFor, data.ConfigValue);

                Guid obj = Guid.NewGuid();
                string FileName = obj + DateTime.Now.Ticks.ToString();
                string path = PathURL + "\\" + FileName + ".jpg";
                string FileImage = (string)allParameters.Parameters.ToList().Find(m => m.Key.Equals("FileImage")).Value;
                byte[] arrByte = Convert.FromBase64String(FileImage);
                allParameters = InputParameters.SupplementFromQueryString(allParameters, Request);  // Supplement input parameters from URI query string.
                byte[] Image = Common.CreateThumbnail(arrByte, 1000);
                System.IO.File.WriteAllBytes(path, Image);
                // following method decypt parameters from request //
                allParameters = DecryptParams(allParameters, out bool isDataEncrypted);

                var fileName = allParameters.Parameters.ToList().Find(m => m.Key.Equals("filename"));
                allParameters.Parameters.Remove(fileName.Key);
                allParameters.Parameters.Add("fileName", FileName);
                var isSP = (bool)allParameters.Parameters.ToList().Find(m => m.Key.Equals("isSP")).Value;

                if (isSP)
                {
                    sp = "SPU_" + sp;
                }

                var base64String = allParameters.Parameters.ToList().Find(m => m.Key.Equals("base64String"));
                var FileImageKey = allParameters.Parameters.ToList().Find(m => m.Key.Equals("FileImage"));

                if (base64String.Key != null && fileName.Key != null)
                {
                    //item.Value = EncryptText(item.Value.ToString()) as object;
                    allParameters.Parameters.Remove(base64String.Key);
                    allParameters.Parameters.Remove(FileImageKey.Key);
                    //allParameters.Parameters.Add("Password", EncryptText(itemPassword.Value.ToString()));               


                    bool IsFileUpload = S3Storage.UploadFile(base64String.Value.ToString(), fileName.Value.ToString());

                    if (!IsFileUpload)
                    {
                        var response1 = new Response[1][];
                        response1.SetValue(new Response[] { new Response { IsSuccess = false, Message = "Error occured while file uploaded on server", PreSignedUrl = "" } }, 0);
                        return Request.CreateResponse(HttpStatusCode.OK, new { ResultSets = response1, ReturnValue = 0 });

                    }
                }

                SetUserName(allParameters);                         // Set the conventional User Name Parameter if configured.

                //return this.DynExecuteDbApi(sp, allParameters);     // The main entry point to call the DbWebApi.

                HttpResponseMessage response = this.DynExecuteDbApi(sp, allParameters);
                response = EncryptParams(response, isDataEncrypted);
                return response;
            }
            else
                {
                    HttpResponseMessage responsenull = null;
                    responsenull.StatusCode = 0;
                    responsenull.ReasonPhrase = "Attachement File Path ";
                    return responsenull;
                }
        }
            catch(Exception ex )
            {
                IDictionary<string, object> numberNames = new Dictionary<string, object>();
        numberNames.Add("ErrDescription", ex.Message); //adding a key/value using the Add() method
                numberNames.Add("SystemException", ex.InnerException);
                numberNames.Add("ActiveFunction", "FilePathAttachment");
                numberNames.Add("ActiveForm", "FilePathAttachment");
                numberNames.Add("ActiveModule", "FilePathAttachment");
                numberNames.Add("createdby",0);
                numberNames.Add("IPAddress","192.168.1.1");
                var result = this.ExecuteDbApi("spu_SetErrorLog", numberNames);
                HttpResponseMessage responsenull = null;
                responsenull.StatusCode = 0;
                responsenull.ReasonPhrase = ex.Message.ToString();
                return responsenull;


            }

        }
        public  string GetPhysicalPath(string pathFor = "",string PhysicalPath="")
        {
            try
            {
                string InnerPath = "";
                string functionReturnValue = "";
                if (pathFor.ToLower() == "images")
                {
                    InnerPath = "/assets/design/images";
                    if (!Directory.Exists(PhysicalPath + InnerPath))
                    {
                        Directory.CreateDirectory(PhysicalPath + InnerPath);
                    }
                    functionReturnValue = PhysicalPath + InnerPath;
                }
                else if (pathFor.ToLower() == "json")
                {
                    InnerPath = "/Attachments/UserDetails/Jsondata";
                    if (!Directory.Exists(PhysicalPath + InnerPath))
                    {
                        Directory.CreateDirectory(PhysicalPath + InnerPath);
                    }
                    functionReturnValue = PhysicalPath + InnerPath;

                }
                else if (pathFor.ToLower() == "import")
                {
                    InnerPath = "/Attachments/UserDetails/Importdata";
                    if (!Directory.Exists(PhysicalPath + InnerPath))
                    {
                        Directory.CreateDirectory(PhysicalPath + InnerPath);
                    }
                    functionReturnValue = PhysicalPath + InnerPath;

                }
                else if (pathFor.ToLower() == "ssrentry")
                {
                    string Year = DateTime.Now.Year.ToString();
                    string Month = DateTime.Now.Month.ToString("d2");
                    InnerPath = "\\Attachments\\Images\\" + Year + "\\" + Month;
                    if (!Directory.Exists(PhysicalPath + InnerPath))
                    {
                        Directory.CreateDirectory(PhysicalPath + InnerPath);
                    }
                    functionReturnValue = PhysicalPath + InnerPath;
                }
                else if (pathFor.ToLower() == "tlentry")
                {
                    string Year = DateTime.Now.Year.ToString();
                    string Month = DateTime.Now.Month.ToString("d2");

                    InnerPath = "/Attachments/TLImages/" + Year + "/" + Month;
                    if (!Directory.Exists(PhysicalPath + InnerPath))
                    {
                        Directory.CreateDirectory(PhysicalPath + InnerPath);
                    }
                    functionReturnValue = PhysicalPath + InnerPath;
                }

                else if (pathFor.ToLower() == "autonsm")
                {
                    InnerPath = "/Attachments/AutoReport/NSM";
                    if (!Directory.Exists(PhysicalPath + InnerPath))
                    {
                        Directory.CreateDirectory(PhysicalPath + InnerPath);
                    }
                    functionReturnValue = PhysicalPath + InnerPath;
                }
                else if (pathFor.ToLower() == "autorsm")
                {
                    InnerPath = "/Attachments/AutoReport/RSM";
                    if (!Directory.Exists(PhysicalPath + InnerPath))
                    {
                        Directory.CreateDirectory(PhysicalPath + InnerPath);
                    }
                    functionReturnValue = PhysicalPath + InnerPath;

                }
                else if (pathFor.ToLower() == "autobsm")
                {
                    InnerPath = "/Attachments/AutoReport/BSM";
                    if (!Directory.Exists(PhysicalPath + InnerPath))
                    {
                        Directory.CreateDirectory(PhysicalPath + InnerPath);
                    }
                    functionReturnValue = PhysicalPath + InnerPath;
                }


                else
                {
                    InnerPath = "/Attachments";
                    if (!Directory.Exists(PhysicalPath + InnerPath))
                    {
                        Directory.CreateDirectory(PhysicalPath + InnerPath);
                    }
                    functionReturnValue = PhysicalPath + InnerPath;
                }
                return functionReturnValue;
            }
            catch(Exception ex )
            {
                IDictionary<string, object> numberNames = new Dictionary<string, object>();
                numberNames.Add("ErrDescription", ex.Message); //adding a key/value using the Add() method
                numberNames.Add("SystemException", ex.InnerException);
                numberNames.Add("ActiveFunction", "FilePathAttachment");
                numberNames.Add("ActiveForm", "FilePathAttachment");
                numberNames.Add("ActiveModule", "FilePathAttachment");
                numberNames.Add("createdby",0);
                numberNames.Add("IPAddress","192.168.1.1");
                var result = this.ExecuteDbApi("spu_SetErrorLog", numberNames);
                return ex.Message.ToString();

            }
        }

       
        [AcceptVerbs("GET", "POST", "PUT", "DELETE")]
        public HttpResponseMessage Login(string sp, string other, string auth, InputParameters allParameters)
        {

            try
            {
                    allParameters =
                    InputParameters.SupplementFromQueryString(allParameters,
                        Request); // Supplement input parameters from URI query string.

                // following method decypt parameters from request //
                allParameters = DecryptParams(allParameters, out bool isDataEncrypted);

                // Checking if SP_ required in procedure //
                var isSP = (bool)allParameters.Parameters.ToList().Find(m => m.Key.Equals("isSP")).Value;

                if (isSP)
                {
                    sp = "SPU_" + sp;
                }
                if (sp.ToLower().Equals(("spu_GetConfigSetting").ToLower()))
                {
                    var res=this.DynExecuteDbApi(sp, allParameters);
                    return res;
                }
                    if (sp.ToLower().Equals("spu_getlogin"))
                {
                    //allParameters.Parameters.Add("IsAuthenticated", false);
                    //allParameters.Parameters.Add("FetchUserData", true);

                    var ItemPassword = allParameters.Parameters.ToList().Find(m => m.Key.Equals("Password"));

                    if (ItemPassword.Key != null)
                    {
                      
                           allParameters.Parameters.Remove(ItemPassword.Key);
                        allParameters.Parameters.Add("Password",Common.Encrypt(ItemPassword.Value.ToString()));
                    }

                    // to Get IP Address from Parameter //
                    var ipAddress = allParameters.Parameters.ToList().Find(m => m.Key.Equals("LastLoginIP"));
                
                    var result = this.DynExecuteDbApi(sp, allParameters);
                    if (result.IsSuccessStatusCode)
                    {
                        var userFetchDataResponse = result.Content.ReadAsAsync<RootObject<User>>().Result;
                        if (userFetchDataResponse.ResultSets.Count > 0)
                        {
                            var userdata = userFetchDataResponse.ResultSets[0][0];
                            userdata.Access = "Authorized";
                            if (!string.IsNullOrEmpty(userdata.EMPName))
                            {
                                SetUserName(allParameters);
                                string str1 = new JavaScriptSerializer().Serialize(userdata);
                                var token = GenerateToken("", str1, DateTime.Now);

                                HttpResponseMessage response = new HttpResponseMessage();
                                response = Request.CreateResponse(HttpStatusCode.OK, userdata);
                                response.Headers.Add("Token", token + ':' + userdata.LoginID.ToString());
                                response.Headers.Add("TokenExpiry", "1");
                                response.Headers.Add("Access-Control-Expose-Headers", "Token,TokenExpiry");

                                response = EncryptParams(response, isDataEncrypted);
                                    return response;
                            }
                            else
                            {
                                // return Request.CreateResponse(HttpStatusCode.OK, "UnAuthorized");
                                userdata.Access = "UnAuthorized";
                                HttpResponseMessage response = new HttpResponseMessage();
                                response = Request.CreateResponse(HttpStatusCode.OK, userdata);
                                return response;
                            }

                        }

                    }

                    return Request.CreateResponse(HttpStatusCode.OK, "UnAuthorized");

                    // The main entry point to call the DbWebApi.
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "UnAuthorized");
                }
            }
            catch (Exception exception)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "UnAuthorized " + exception.Message);
            }
        }

      

       

        private void SetUserName(InputParameters dynParameters)
        {
            if (!string.IsNullOrEmpty(ConfigHelper.UserNameReservedParameter) && User != null && User.Identity != null)
            {
                string userName = User.Identity.Name;

                if (!string.IsNullOrEmpty(userName))
                    dynParameters.SetParameter(ConfigHelper.UserNameReservedParameter, userName);
            }
        }
        public string GenerateToken(string ClientKeys, string UserID, DateTime IssuedOn)
        {
            try
            {
                string randomnumber =
                   string.Join("^", new string[]
                   {   UserID,
                KeyGenerator.GetUniqueKey(),
                Convert.ToString(1),
                Convert.ToString(IssuedOn),
                "1001"
                   });

                return Common.Encrypt(randomnumber);
            }
            catch (Exception)
            {

                throw;
            }
        }

       
        // Decryption of parameter //

        private InputParameters DecryptParams(InputParameters allParameters, out bool isEncrypted)
        {
            isEncrypted = false;
            var encryptedParam = allParameters.Parameters.ToList().Find(m => m.Key.Equals("encryptedParam"));
            if (encryptedParam.Key != null && encryptedParam.Value != null)
            {
                var response = EncryptDecrypt.DecryptStringAES(encryptedParam.Value.ToString(), cryptoIV, cryptoIV);
                allParameters = (response != null && response != "null") ? JsonConvert.DeserializeObject<InputParameters>(response.ToString()) : allParameters;
                isEncrypted = true;
            }
            return allParameters;
        }

        // Encryption of parameter //
        private HttpResponseMessage EncryptParams(HttpResponseMessage response, bool isEncrypted)
        {
            if (isEncrypted == true && response != null && response.Content != null)
            {
                var encryptedResponse = EncryptDecrypt.EncryptStringAES(JsonConvert.SerializeObject(response.Content), cryptoIV, cryptoIV);
                response.Content = new StringContent(JsonConvert.SerializeObject(new { encryptedParam = encryptedResponse }));
            }
            return response;
        }
        public static bool ResizeImage(HttpPostedFileBase fileToUpload, string SavePath= "C:\\inetpub\\wwwroot\\Images")
        {
            bool isSave = false;
            long LoginID = 0;
            try
            {
                using (Image image = Image.FromStream(fileToUpload.InputStream, true, false))
                {
                    try
                    {
                        //Size can be change according to your requirement 
                        float thumbWidth = 1000F;
                        float thumbHeight = 1000F;
                        //calculate  image  size
                        if (image.Width > image.Height)
                        {
                            thumbHeight = ((float)image.Height / image.Width) * thumbWidth;
                        }
                        else
                        {
                            thumbWidth = ((float)image.Width / image.Height) * thumbHeight;
                        }

                        int actualthumbWidth = Convert.ToInt32(Math.Floor(thumbWidth));
                        int actualthumbHeight = Convert.ToInt32(Math.Floor(thumbHeight));
                        var thumbnailBitmap = new Bitmap(actualthumbWidth, actualthumbHeight);
                        var thumbnailGraph = Graphics.FromImage(thumbnailBitmap);
                        thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
                        thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
                        thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        var imageRectangle = new Rectangle(0, 0, actualthumbWidth, actualthumbHeight);
                        thumbnailGraph.DrawImage(image, imageRectangle);
                        var ms = new MemoryStream();
                        if (!Directory.Exists(SavePath))
                        {
                            Directory.CreateDirectory(SavePath);
                        }
                       
                        thumbnailBitmap.Save(SavePath, ImageFormat.Jpeg);
                        ms.Position = 0;
                        GC.Collect();
                        thumbnailGraph.Dispose();
                        thumbnailBitmap.Dispose();
                        image.Dispose();
                        isSave = true;
                    }
                    catch (Exception ex)
                    {
                        //Common_SPU.LogError("Error during ResizeImage. The query was executed :", ex.ToString(), SavePath, "ClsApplicationSetting", "ClsApplicationSetting", LoginID, IPAddress);

                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }



        [HttpGet]
        public HttpResponseMessage DownloadSalarySlip(string Month,string EMPCode)
        {
            HttpResponseMessage Getresult = null;
            try
            {
                IDictionary<string, object> numberNames = new Dictionary<string, object>();
                numberNames.Add("ConfigKey", "SalarySlipPhysicalPath");
                var result = this.ExecuteDbApi("spu_GetConfigSetting", numberNames);
                var userFetchDataResponses = result.Content.ReadAsAsync<RootObject<ResponsesFilePath>>().Result;
                var data = userFetchDataResponses.ResultSets[0][0];
                string PhysicalPath = data.ConfigValue;
                string DocName = Month +"/"+ EMPCode + "_SalarySlip.pdf";
                PhysicalPath = System.IO.Path.Combine("D:/Hostsite/ProHRMS/Application/THRV/PDFFiles/", DocName);

                if (System.IO.File.Exists(PhysicalPath))
                {
                    byte[] bytes = System.IO.File.ReadAllBytes(PhysicalPath);
                    Getresult = Request.CreateResponse(HttpStatusCode.OK);
                    Getresult.Content = new ByteArrayContent(bytes);
                    Getresult.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                    Getresult.Content.Headers.ContentDisposition.FileName = DocName + ".pdf";
                }
                else
                {
                    Getresult = Request.CreateResponse(HttpStatusCode.Gone);
                }
                return Getresult;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.Gone);
            }
        }

    }

    internal class ResponsesFilePath
    {
        public string ConfigValue { get; set; }
        
    }

   
}

