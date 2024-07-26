
using DataBooster.DbWebApi;
using Rotativa;
using System.Web.Mvc;
using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using MyDbWebApi.Models;
using ClosedXML.Excel;
using System.Data;
using System.Text;
using System.Web.Routing;
using System.Web;
using Rotativa.Options;
using System.Configuration;
using System.Xml;
using iTextSharp.text;
using iTextSharp.text.xml;
using iTextSharp.text.pdf;
using System.Net.Mail;

using System.Linq;
using System.Globalization;
using System.Net.Http.Headers;
using iTextSharp.text.pdf.parser;

namespace MyDbWebApi.Controllers
{
    [AllowAnonymous]
    public class MiscController : Controller
    {
        string url = ConfigurationManager.AppSettings["URL"].ToString();

        public byte[] GetPdfBytesFormView(ControllerContext controlerContext, int accessionId)
        {
            HttpClient client = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
            //client.DefaultRequestHeaders.co, "application/json; charset=utf-8");
            var model = client.GetAsync(url + "/dbo.SP_GetExcelReportDocumentWithBinaryDataByAccesssionNumber/json?AccesssionNumber=5").Result
                .Content.ReadAsAsync<RootObject<Documents>>().Result;
            string base64string = model.ResultSets[0][0].docBase64;
            int index = base64string.LastIndexOf(',');
            string texts = base64string.Replace(base64string.Substring(0, index + 1), "");
            var sheet = workBook(texts);
            getReportHeaderInfo(accessionId);
            reportObj.headerObj = headerObj;
            IXLWorksheet workSheet = sheet.Worksheet(1);




            //Loop through the Worksheet rows.
            bool firstRow = true;

            StringBuilder sb = new StringBuilder();
            sb = GetHtmlString(sb, workSheet, firstRow);
            ViewBag.ResultRows = sb;

            var actionPDF = new Rotativa.PartialViewAsPdf("ResultReport", reportObj)
            {
                PageSize = Size.A4,
                PageOrientation = Orientation.Portrait,
                PageMargins = { Left = 1, Right = 2 }
            };

            ControllerContext = this.ControllerContext;
            byte[] applicationPDFData = actionPDF.BuildPdf(controlerContext);
            return applicationPDFData;
        }


        public byte[] getPDFInBytes(string viewName, Object obj)
        {
            var actionPDF = new Rotativa.PartialViewAsPdf(viewName, obj)
            {
                PageSize = Size.A4,
                PageOrientation = Orientation.Portrait,
                PageMargins = { Left = 1, Right = 2 }
            };

            byte[] applicationPDFData = actionPDF.BuildPdf(this.ControllerContext);
            return applicationPDFData;
        }

        public byte[] getPDFInBytesWrapper(string viewName, int accessionId)
        {
            if (viewName == "ResultReport") { getCerviGENEReportInfo(accessionId); }
            else if (viewName == "BreastCancerReportTemplate") { getBreastCancerReportInfo(accessionId); }

            ViewBag.ShowSignature = true;
            return getPDFInBytes(viewName, reportObj);
        }


        //public Boolean savePDFToDatabase(int accessionId,string viewName,int userID,string ReportName)
        //{
        //    byte[] fileData = Convert.FromBase64String(Base64string);
        //    String filename = Guid.NewGuid().ToString().Substring(0, 8);
        //    string path = Server.MapPath("images/" + filename);
        //    using (System.IO.FileStream fs = new System.IO.FileStream("", System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite))
        //    {
        //        using (System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs))
        //        {
        //            bw.Write(fileData);
        //            bw.Close();
        //        }
        //    }
        //    return "";
        //}
        public Boolean savePDFToDatabase(int accessionId, string viewName, int userID, string ReportName)
        {
            using (var httpClient = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true }))
            {

                using (var content = new MultipartFormDataContent())
                {
                    var byteArrayContent = getPDFInBytesWrapper(viewName, accessionId);

                    var values = new[]
                    {
                 new KeyValuePair<string, string>("doc64string", Convert.ToBase64String(byteArrayContent)),
                 new KeyValuePair<string, string>("name", ReportName),
                 new KeyValuePair<string, string>("Documenttype", "application/pdf"),
                 new KeyValuePair<string, string>("CreatedUserID", userID.ToString()),
                 new KeyValuePair<string, string>("Description", "Signed " + ReportName),
                 new KeyValuePair<string, string>("AccesssionNumber", accessionId.ToString()),
                 };

                    foreach (var keyValuePair in values)
                    {
                        content.Add(new StringContent(keyValuePair.Value), keyValuePair.Key);
                    }


                    var requestUri = url + "/dbo.SP_InsertSignedDocument/json";
                    var result = httpClient.PostAsync(requestUri, content).Result;

                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        return true;
                    }
                }

            }
            return false;
        }

        public ActionResult DownloadCerviGENEReport(int accessionId)
        {
            getCerviGENEReportInfo(accessionId);
            ViewBag.ShowSignature = true;
            return new Rotativa.PartialViewAsPdf("ResultReport", reportObj) { FileName = "UrlTest.pdf" };

        }
        public ActionResult DownloadBreastCancerReport(int accessionId)
        {
            getBreastCancerReportInfo(accessionId);
            ViewBag.ShowSignature = true;
            return new Rotativa.PartialViewAsPdf("BreastCancerReportTemplate", reportObj) { FileName = "BreastCancerReport.pdf", PageMargins = new Margins(1, 1, 1, 1) };

        }

        public ActionResult DownloadSTLTestReport(int accessionId)
        {
            getReportHeaderInfo(accessionId);
            ViewBag.ShowSignature = true;
            return new Rotativa.PartialViewAsPdf("stltest", headerObj) { FileName = "stl.pdf", PageMargins = new Margins(1, 1, 1, 1) };

        }

        public ActionResult ResultReport()
        {
            ViewBag.PatientName = "Broke jak";
            return View();
        }
        public ActionResult GetHtml(string viewName)
        {
            var htmlcontent = RenderPartialViewToString(this, viewName, new object());


            return Content(htmlcontent, "text/html");
        }
        public ActionResult GetHtml(object obj, string viewName)
        {
            var htmlcontent = RenderPartialViewToString(this, viewName, obj);


            return Content(htmlcontent, "text/html");
        }
        public static string RenderPartialViewToString(Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;
            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.ToString();
            }
        }
        public XLWorkbook workBook(string File)
        {
            byte[] data = System.Convert.FromBase64String(File);
            MemoryStream ms = new MemoryStream(data);

            XLWorkbook wb = new XLWorkbook(ms);
            return wb;
        }
        //public string ReadExcelFile(int AccesssionNumber)
        //{
        //    HttpClient client = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
        //    //client.DefaultRequestHeaders.co, "application/json; charset=utf-8");
        //    var model = client.GetAsync(url+"/dbo.SP_GetExcelReportDocumentWithBinaryDataByAccesssionNumber/json?AccesssionNumber=" + AccesssionNumber).Result
        //        .Content.ReadAsAsync<RootObject<Documents>>().Result;
        //    if (model.ResultSets[0].Count > 0)
        //    {
        //        string base64string = model.ResultSets[0][0].docBase64;
        //        int index = base64string.LastIndexOf(',');
        //        string texts = base64string.Replace(base64string.Substring(0, index + 1), "");
        //        var sheet = workBook(texts);

        //        IXLWorksheet workSheet = sheet.Worksheet(1);

        //        //Loop through the Worksheet rows.
        //        bool firstRow = true;

        //        StringBuilder sb = new StringBuilder();
        //        sb = GetHtmlString(sb, workSheet, firstRow);


        //        return sb.ToString();
        //    }
        //    else return "<div style='font-size = 16px; color: #E91E63;margin:  0 auto;text-align: center;font-size: 28px;top: 27px;position: relative;'>Result Report Not Uploaded</div>";
        //}
        ReportFields reportObj = new ReportFields();
        clsHeaderTemplate headerObj = new clsHeaderTemplate();
        private void getReportHeaderInfo(int accessionId)
        {
            if (headerObj == null)
            {
                headerObj = new clsHeaderTemplate();
            }

            HttpClient client = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
            //client.DefaultRequestHeaders.co, "application/json; charset=utf-8");
            var model = client.GetAsync(url + "/dbo.SP_GetReportHeader/json?AccessionID=" + accessionId).Result;
            var data = model.Content.ReadAsAsync<RootObject<clsHeaderTemplate>>().Result;
            var Result = data.ResultSets[0][0];

            headerObj.PatientFirstName = Result.PatientFirstName;
            headerObj.PatientMiddleName = Result.PatientMiddleName;
            headerObj.PatientLastName = Result.PatientLastName;
            headerObj.PatientGender = Result.PatientGender; ;
            headerObj.PatientDateofbirth = Result.PatientDateofbirth;
        }
        private StringBuilder GetHtmlString(StringBuilder sb, IXLWorksheet workSheet, Boolean firstRow)
        {
            sb.Append("<table class='tbl table-bordered'>");
            foreach (IXLRow row in workSheet.Rows())
            {
                //Use the first row to add columns to DataTable.
                string col = "";
                if (firstRow)
                {
                    col = col + "<tr>";
                    col = col + " <th>Therapeutic</th><th>Genes Analyzed</th><th>Expression Results</th><th>Interpretation</th><th></th><th>Ref</th> </tr>";

                    //foreach (IXLCell cell in row.Cells())
                    //{

                    //    col = col + "<th>" + cell.Value.ToString() + "</th>";
                    //}
                    //col = col + "</tr> <tr>";
                    sb.Append(col);
                    firstRow = false;
                }
                else
                {
                    col = col + "<tr>";
                    //Add rows to DataTable.

                    int i = 0;
                    string result = "";
                    foreach (IXLCell cell in row.Cells())
                    {

                        //dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                        if (cell.Value.ToString().ToLower().Equals("outcomes"))
                        {
                            // col = col.Substring(0, col.Length - 4);

                            col = col + " <th>Prognostic /Recurrence</th><th>Genes Analyzed</th><th>Expression Results</th><th>Interpretation</th><th></th><th>Ref</th> </tr>";
                            col = col + "<tr> <td>" + cell.Value.ToString() + "</td>";

                        }

                        else
                        {
                            if (i == 2)
                            {
                                switch (cell.Value.ToString().Replace(" ", "").ToLower())
                                {
                                    case "high":
                                        result = "<td class='bgRed'></td>";
                                        break;
                                    case "low":
                                        result = "<td class='bgGreen'></td>";
                                        break;
                                    case "inrange":
                                        result = "<td class='bgwhite'></td>";
                                        break;
                                    case "notdetected":
                                        result = "<td></td>";
                                        break;
                                }
                            }
                            col = col + (i == 4 ? result : "") + " <td>" + cell.Value.ToString() + "</td>";
                        }
                        i++;
                    }
                    sb.Append(col + "</tr>");
                }


            }
            sb.Append("</tbody></table>");
            return sb;
        }
        public ActionResult CerviGENEReportTemplate(int accessionId)
        {
            getCerviGENEReportInfo(accessionId);
            return View("ResultReport", reportObj);
        }
        public String CerviGENEReportTemplateHTML(int accessionId)
        {
            getCerviGENEReportInfo(accessionId);
            return RenderPartialViewToString(this, "ResultReport", reportObj);
        }

        private void getCerviGENEReportInfo(int accessionId)
        {
            HttpClient client = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
            //client.DefaultRequestHeaders.co, "application/json; charset=utf-8");
            var model = client.GetAsync(url + "/dbo.SP_GetReportResultsInformation/json?AccessionID=" + accessionId).Result;
            var data = model.Content.ReadAsAsync<RootObject<clsReportResultsInformation>>().Result;
            var Result = data.ResultSets[0];

            getReportHeaderInfo(accessionId);
            reportObj.headerObj = headerObj;
            client = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
            //client.DefaultRequestHeaders.co, "application/json; charset=utf-8");
            var models = client.GetAsync(url + "/dbo.SP_GetExcelReportDocumentWithBinaryDataByAccesssionNumber/json?AccesssionNumber=" + accessionId).Result
                .Content.ReadAsAsync<RootObject<Documents>>().Result;
            if (models.ResultSets[0].Count > 0)
            {
                string base64string = models.ResultSets[0][0].docBase64;
                int index = base64string.LastIndexOf(',');
                string texts = base64string.Replace(base64string.Substring(0, index + 1), "");
                var sheet = workBook(texts);

                IXLWorksheet workSheet = sheet.Worksheet(1);




                //Loop through the Worksheet rows.
                bool firstRow = true;

                StringBuilder sb = new StringBuilder();
                sb = GetHtmlString(sb, workSheet, firstRow);
                ViewBag.ResultRows = sb;
            }
            else { ViewBag.ResultRows = "<div style='font-size = 16px; color: #E91E63;margin:  0 auto;text-align: center;font-size: 28px;top: 27px;position: relative;'>Result Report Not Uploaded</div>"; }
        }

        [AllowAnonymous]
        public ActionResult BreastCancerHeader(int accessionId)
        {
            getReportHeaderInfo(accessionId);
            return View(headerObj);

        }
        [AllowAnonymous]
        public String BreastCancerReportTemplateHTML(int accessionId)
        {
            getBreastCancerReportInfo(accessionId);
            ViewBag.ShowSignature = false;
            return RenderPartialViewToString(this, "BreastCancerReportTemplate", reportObj);
        }
        [AllowAnonymous]
        public ActionResult BreastCancerReportTemplate(int accessionId)
        {
            getBreastCancerReportInfo(accessionId);
            ViewBag.ShowSignature = false;
            return View(reportObj);
        }

        private void getBreastCancerReportInfo(int accessionId)
        {
            HttpClient client = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
            //client.DefaultRequestHeaders.co, "application/json; charset=utf-8");
            var model = client.GetAsync(url + "/dbo.SP_GetReportResultsInformation/json?AccessionID=" + accessionId).Result;
            var data = model.Content.ReadAsAsync<RootObject<clsReportResultsInformation>>().Result;
            var Result = data.ResultSets[0];


            foreach (var item in Result)
            {
                if (item.type.Equals("IntegrationResult"))
                {
                    reportObj.Results.Add(new clsResults(item.ColValue1, item.ColValue2));
                }
                else if (item.type.Equals("Interpre"))
                {
                    reportObj.Interpretation = item.ColValue2;
                }
                else if (item.type.Equals("Summary"))
                {
                    reportObj.Specimen = item.ColValue1;
                }
                else if (item.type.Equals("PhysicianComment"))
                {
                    reportObj.PhysicianComments = item.ColValue1;
                }
                else if (item.type.Equals("ResultImages"))
                {
                    reportObj.ResultImages.Add(item.ColValue1);
                }
                else if (item.type.Equals("ClinicalHistory"))
                {
                    reportObj.ClinicalHistory = item.ColValue1;
                }
            }

            getReportHeaderInfo(accessionId);
            reportObj.headerObj = headerObj;
        }

        public ActionResult UrlAsPDF()
        {
            // string cusomtSwitches = string.Format("--print-media-type --allow {0} --enable-local-file-access  --header-html {0}",
            //   Url.Action("BreastCancerHeader", "Misc", new { }, this.Request.Url.Scheme)
            //);
            // //initiateEmptyObject();
            // //return GetHtml(reportObj);
            // return new Rotativa.ViewAsPdf("BreastCancerReportTemplate",reportObj)
            // {
            //     CustomSwitches = cusomtSwitches
            // };
            string ht = RenderPartialViewToString(this, "BreastCancerHeader", headerObj);
            //initiateEmptyObject();
            //return GetHtml(reportObj);
            string header = Url.Action("Header", "Misc", new { Areas = "" }, Request.Url.Scheme);
            string footer = Url.Action("Footer", "Misc", new { Areas = "" }, Request.Url.Scheme);

            string customSwitches = string.Format("--header-html {0} --header-spacing 10 --footer-html {1} --header-spacing 10",
            header, footer);
            //string header = Server.MapPath("~/views/misc/BreastCancerHeader.cshtml");//Path of PrintHeader.html File
            //string footer = Server.MapPath("~/bin/PrintFooter.html");//Path of PrintFooter.html File

            //string customSwitches = string.Format("--header-html  \"{0}\" " +
            //                       "--header-spacing \"0\" " +

            //                       "--footer-spacing \"10\" " +
            //                       "--footer-font-size \"10\" " +
            //                       "--header-font-size \"10\" ", header );
            customSwitches = string.Format("--header-html  \"{0}\" " +
                              "--header-spacing \"0\" " +
                              "--footer-html \"{1}\" " +
                              "--footer-spacing \"10\" " +
                              "--footer-font-size \"10\" " +
                              "--header-font-size \"10\" ", header, footer);
            getBreastCancerReportInfo(1);
            return new Rotativa.PartialViewAsPdf("BreastCancerReportTemplate", reportObj)
            {
                CustomSwitches = customSwitches
            };
            //string customSwitches = string.Format("--header-html  \"{0}\" " +
            //         "--header-spacing \"0\" " +
            //         "--footer-html \"{1}\" " +
            //         "--footer-spacing \"10\" " +
            //         "--footer-font-size \"10\" " +
            //         "--header-font-size \"10\" , Url.Action('asdasdasd', 'asdasdasdasd', new { hid = xxxx.ID }, httType), Url.Action(Footer', 'xxxxx', new { hid = xxxxx.ID }, httType ));");


            //   string customSwitches = string.Format("--print-media-type --allow {0} --footer-html {0} --footer-spacing -10",
            //    Url.Action("Footer", "Misc", new { area = "" }, this.Request.Url.Scheme));
            // return Footer();
            Url.Action("Footer", "Misc", new { area = "" }, this.Request.Url.Scheme);
            return new Rotativa.PartialViewAsPdf("BreastCancerReportTemplate", reportObj)
            {
                CustomSwitches = customSwitches
            };
        }

        public void initiateEmptyObject()
        {
            reportObj = new ReportFields();
            reportObj.headerObj = new clsHeaderTemplate();

            reportObj.Results = new List<clsResults>();
            reportObj.ResultImages = new List<string>();
        }
        public ActionResult stltest(int accessionId)
        {
            getReportHeaderInfo(accessionId);
            return View(headerObj);
        }
        [AllowAnonymous]
        public ActionResult Footer()
        {
            return View();
        }
        public ActionResult Header()
        {
            return View("Footer");
        }
        [HttpPost]
        public bool SendMail(string EmailTo, string From, string CC, string Subject, string Body)
        {
            try
            {

                MailMessage mail = new MailMessage();
                mail.To.Add(EmailTo);
                mail.From = new MailAddress(From);
                mail.Subject = Subject;

                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["Host"].ToString();
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"].ToString());
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential
                (ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString());// Enter seders User name and password  
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private object GetTextFromPDF(string FilePath)
        {
            StringBuilder text = new StringBuilder();


            string[] words;
            string line;
            string textother = "";
            string afterfirststring = "", ndnamessa = "", casestring = "", GenderString = "", SpecimenString = "", InterpretationString = "",
                         CollectedString = "", ReceivedString = "", ReportedString = "";

            using (PdfReader reader = new PdfReader(FilePath))
            {
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }
                int intPageNum = reader.NumberOfPages;
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    textother = PdfTextExtractor.GetTextFromPage(reader, i, new LocationTextExtractionStrategy());
                    if (i == 1)
                    {
                        ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                        string currentText = PdfTextExtractor.GetTextFromPage(reader, i);
                        words = textother.Split('\n');
                        //First Name Extration
                        try
                        {
                            int firstNameIndex = currentText.IndexOf("Name") + 5;
                            if (currentText.Length > firstNameIndex)
                            {
                                afterfirststring = currentText.Substring(firstNameIndex, currentText.Length - (firstNameIndex));
                            }
                            // refering Provider  Extraction
                            int index2ndname = afterfirststring.IndexOf("Name:") + 6;
                            string firstname = afterfirststring.Substring(0, index2ndname);
                            if (afterfirststring.IndexOf("Case #: ") > index2ndname)
                            {
                                ndnamessa = afterfirststring.Substring(index2ndname, afterfirststring.IndexOf("Case #: ") - (index2ndname));
                            }
                            //CaseNo Extraction
                            int indexCase = afterfirststring.IndexOf("Case #: ") + 8;
                            int indexGender = afterfirststring.IndexOf("Sex:");
                            if (indexGender > indexCase)
                            {
                                casestring = afterfirststring.Substring(indexCase, indexGender - indexCase).Replace(" \n      ", "");
                            }
                            //Gender Extraction
                            int indexCC = afterfirststring.IndexOf("Fax: ");
                            indexGender = indexGender + 4;
                            if (indexCC > indexGender)
                            {
                                GenderString = afterfirststring.Substring(indexGender, indexCC - indexGender).Replace(" \n      ", "");
                            }
                            //SPECIMEN: Extraction

                            int indexSPECIMEN = afterfirststring.IndexOf("Specimen:") + 10;
                            int indexClinicalHistory = afterfirststring.IndexOf("Clinical Information:");
                            if (indexClinicalHistory > indexSPECIMEN)
                            {
                                SpecimenString = afterfirststring.Substring(indexSPECIMEN, indexClinicalHistory - indexSPECIMEN).Replace("\n", "");
                            }
                            int indexINTERPRETATION = afterfirststring.IndexOf("Interpretation: ");
                            int indexClinicalSigni = afterfirststring.IndexOf("Clinical significance:");
                            if (indexClinicalSigni > indexINTERPRETATION)
                            {
                                InterpretationString = afterfirststring.Substring(indexINTERPRETATION, indexClinicalSigni - indexINTERPRETATION).Replace("\n", "");

                            }

                            int indexCollected = afterfirststring.IndexOf("Collected:") + 10;
                            int indexDOB = afterfirststring.IndexOf("DOB: ");
                            if (indexDOB > indexCollected)
                            {
                                //int indexCOMMENTS = afterfirststring.IndexOf("COMMENTS: ");
                                CollectedString = afterfirststring.Substring(indexCollected, indexDOB - indexCollected);
                            }

                            int indexReceived = afterfirststring.IndexOf("Received:") + 10;
                            int indexID = afterfirststring.IndexOf("ID#:");
                            if (indexID > indexReceived)
                            {
                                //int indexCOMMENTS = afterfirststring.IndexOf("COMMENTS: ");
                                ReceivedString = afterfirststring.Substring(indexReceived, indexID - indexReceived);
                            }

                            int indexReported = afterfirststring.IndexOf("Reported:") + 10;
                            int indexReportedtext = afterfirststring.IndexOf("Reported:") + 20;
                            if (indexReportedtext > indexReported)
                            {
                                //int indexCOMMENTS = afterfirststring.IndexOf("COMMENTS: ");
                                ReportedString = afterfirststring.Substring(indexReported, indexReportedtext - indexReported);
                            }
                            //Response.Write(InterpretationString);
                        }
                        catch { }
                    }

                    //for (int j = 0, len = words.Length; j < len; j++)
                    //{
                    //    line = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(words[j]));
                    //}
                }
            }

            return new
            {
                Patientname = afterfirststring,
                ReferingProviderName = ndnamessa,
                CaseNo = casestring,
                Gender = GenderString,
                Specimen = SpecimenString,
                Interpretation = InterpretationString,
                Collected = CollectedString,
                Received = ReceivedString,
                Reported = ReportedString
            };
        }
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        public JsonResult saveFileDatabase(int accessionId, int userID, string Filetype)
        {
            byte[] byteArrayContent;
            var httpRequest = HttpContext.Request;
            var DataObj = new object();
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Server.MapPath("~/UploadFile/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    byteArrayContent = ReadFully(postedFile.InputStream);
                    DataObj = GetTextFromPDF(filePath);
                    using (var httpClient = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true }))
                    {

                        using (var content = new MultipartFormDataContent())
                        {


                            var values = new[]
                            {
                 new KeyValuePair<string, string>("doc64string", Convert.ToBase64String(byteArrayContent)),
                 new KeyValuePair<string, string>("name", postedFile.FileName),
                 new KeyValuePair<string, string>("filepath", "testpath"),
                 new KeyValuePair<string, string>("Documenttype", "application/pdf"),
                 new KeyValuePair<string, string>("CreatedUserID", userID.ToString()),
                 new KeyValuePair<string, string>("Description", postedFile.FileName),
                  new KeyValuePair<string, string>("FileType", Filetype ),
                 new KeyValuePair<string, string>("AccesssionNumber", accessionId.ToString()),
                            };

                            foreach (var keyValuePair in values)
                            {
                                content.Add(new StringContent(keyValuePair.Value), keyValuePair.Key);
                            }


                            var requestUri = url + "/dbo.SP_InsertDocument/json";
                            var result = httpClient.PostAsync(requestUri, content).Result;

                            if (result.StatusCode == HttpStatusCode.OK)
                            {
                                Response.StatusCode = 201;

                            }
                        }

                    }
                }
            }

            return Json(DataObj, JsonRequestBehavior.AllowGet);
        }
      


    }
}