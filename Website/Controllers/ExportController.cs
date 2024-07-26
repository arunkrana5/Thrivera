using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMaster;
using DataModal.ModelsMasterHelper;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Website.CommonClass;
using NPOI.HSSF.Util;
using System.IO;
using Rotativa;
using Rotativa.Options;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO.Compression;
using System.Net;


namespace Website.Controllers
{
    [CheckLoginFilter]
    public class ExportController : Controller
    {
        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        IExportHelper export;
        IReportHelper report;
        ReportDocument rd;
        public ExportController()
        {
            getResponse = new GetResponse();
            long.TryParse(ClsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            IPAddress = ClsApplicationSetting.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
            export = new ExportModal();
            report = new ReportModal();
        }


        public void TargetVsAchievement_Export(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Date = GetQueryString[2];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = ViewBag.Date;
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            Modal.Approved = -1;
            var workbook = export.GetTargetVsAchievement_Workbook(Modal);
            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);
                string docName = "TargetVsAchievement" + DateTime.Now.ToString("dd-MMM-yyyy");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", docName + ".xlsx"));
                Response.BinaryWrite(exportData.ToArray());
                Response.End();
            }

        }



        //public void AttendenceReport_Export(string src)
        //{
        //    ViewBag.src = src;
        //    string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
        //    ViewBag.GetQueryString = GetQueryString;
        //    ViewBag.MenuID = GetQueryString[0];
        //    ViewBag.Date = GetQueryString[2];
        //    ViewBag.Doctype = GetQueryString[3];
        //    Tab.Approval Modal = new Tab.Approval();
        //    Modal.Month = ViewBag.Date;
        //    Modal.LoginID = LoginID;
        //    Modal.IPAddress = IPAddress;
        //    Modal.Approved = -1;
        //    Modal.Doctype = ViewBag.Doctype;
        //    var workbook = export.GetFinalAttendence_Workbook(Modal);
        //    using (var exportData = new MemoryStream())
        //    {
        //        Response.Clear();
        //        workbook.Write(exportData);
        //        string docName = "FinalAttendence" + DateTime.Now.ToString("dd-MMM-yyyy");
        //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", docName + ".xlsx"));
        //        Response.BinaryWrite(exportData.ToArray());
        //        Response.End();
        //    }
        //}


        public void SaleEntryReport_Export(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.StartDate = GetQueryString[2];
            ViewBag.EndDate = GetQueryString[3];

            Tab.Approval Modal = new Tab.Approval();
            Modal.StartDate = ViewBag.StartDate;
            Modal.EndDate = ViewBag.EndDate;
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            Modal.Approved = -1;
            DateTime dt;
            DateTime.TryParse(Modal.StartDate, out dt);
            var workbook = export.GetSaleEntry_Workbook(Modal);
            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);
                string docName = "SaleEntry_" + dt.ToString("MMM-yyyy");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", docName + ".xlsx"));
                Response.BinaryWrite(exportData.ToArray());
                Response.End();
            }
        }

        public void MTDReport_Export(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Date = GetQueryString[2];

            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = ViewBag.Date;
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            Modal.Approved = -1;
            var workbook = export.GetMTDReport_Workbook(Modal);
            DateTime dt;
            DateTime.TryParse(Modal.Month, out dt);
            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);
                string docName = "MTD_" + dt.ToString("MMM-yyyy");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", docName + ".xlsx"));
                Response.BinaryWrite(exportData.ToArray());
                Response.End();
            }
        }

        public void PJPExpense_Export(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Date = GetQueryString[2];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = ViewBag.Date;
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            Modal.Approved = -1;
            var workbook = export.GetPJPExpense_Workbook(Modal);
            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);
                string docName = "PJPExpense" + DateTime.Now.ToString("dd-MMM-yyyy");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", docName + ".xlsx"));
                Response.BinaryWrite(exportData.ToArray());
                Response.End();
            }
        }

        //public ActionResult AttendanceLog_Day_PDF(string src)
        //{
        //    ViewBag.src = src;
        //    string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
        //    ViewBag.GetQueryString = GetQueryString;
        //    ViewBag.MenuID = GetQueryString[0];
        //    ViewBag.Date = GetQueryString[2];
        //    ViewBag.Usertype = GetQueryString[3];
        //    Tab.Approval Modal = new Tab.Approval();
        //    Modal.Month = ViewBag.Date;
        //    Modal.LoginID = LoginID;
        //    Modal.IPAddress = IPAddress;
        //    Modal.Usertype = ViewBag.Usertype;
        //    var Result = report.GetAttendance_Log_Daily(Modal);

        //    return new PartialViewAsPdf("_AttendanceLog_Day_PDF", Result)
        //    {
        //        PageOrientation = Orientation.Landscape,
        //        PageSize = Size.A4,
        //        PageMargins = { Left = 5, Bottom = 25, Right = 7, Top = 10 },
        //        CustomSwitches = "--print-media-type --header-center \"Daily Report\"",
        //        FileName = "TestPartialViewAsPdf.pdf"
        //    };
        //}

        public void AttendanceLog_Day_Export(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Date = GetQueryString[2];
            ViewBag.Usertype = GetQueryString[3];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = ViewBag.Date;
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            Modal.Usertype = ViewBag.Usertype;
            var workbook = export.GetAttendanceLog_Day_Workbook(Modal);
            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);
                string docName = "AttendanceLog_Day" + DateTime.Now.ToString("dd-MMM-yyyy");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", docName + ".xlsx"));
                Response.BinaryWrite(exportData.ToArray());
                Response.End();
            }
        }

        public void AttendanceLog_Monthly_Export(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Date = GetQueryString[2];
            ViewBag.Usertype = GetQueryString[3];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = ViewBag.Date;
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            Modal.Usertype = ViewBag.Usertype;
            Modal.Approved = -1;
            var workbook = export.GetAttendanceLog_Monthly_Workbook(Modal);
            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);
                string docName = "Attendance_Monthly" + DateTime.Now.ToString("dd-MMM-yyyy");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", docName + ".xlsx"));
                Response.BinaryWrite(exportData.ToArray());
                Response.End();
            }
        }

        public void AttendanceLog_Monthly_InOut_Export(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Date = GetQueryString[2];
            ViewBag.Usertype = GetQueryString[3];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = ViewBag.Date;
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            Modal.Usertype = ViewBag.Usertype;
            Modal.Approved = -1;
            var workbook = export.GetAttendanceLog_Monthly_InOut_Workbook(Modal);
            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);
                string docName = "Attendance_Monthly_InOut" + DateTime.Now.ToString("dd-MMM-yyyy");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", docName + ".xlsx"));
                Response.BinaryWrite(exportData.ToArray());
                Response.End();
            }
        }

        public void Attendance_Final_Export(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Date = GetQueryString[2];
            ViewBag.Usertype = GetQueryString[3];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = ViewBag.Date;
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            Modal.Usertype = ViewBag.Usertype;
            Modal.Approved = -1;
            var workbook = export.GetAttendance_Final_Workbook(Modal);
            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);
                string docName = "Attendance_Final" + DateTime.Now.ToString("dd-MMM-yyyy");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", docName + ".xlsx"));
                Response.BinaryWrite(exportData.ToArray());
                Response.End();
            }
        }

        public void Attendance_Export(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Date = GetQueryString[2];
            ViewBag.Usertype = GetQueryString[3];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = ViewBag.Date;
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            Modal.Usertype = ViewBag.Usertype;
            Modal.Approved = -1;
            DateTime dt;
            DateTime.TryParse(Modal.Month, out dt);
            var workbook = export.GetAttendance_Workbook(Modal);
            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);
                string docName = "Attendance_" + dt.ToString("MMM-yyyy");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", docName + ".xlsx"));
                Response.BinaryWrite(exportData.ToArray());
                Response.End();
            }
        }

        public void TLTracker_Export(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Date = GetQueryString[2];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = ViewBag.Date;
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            var workbook = export.GetTLTracker_Workbook(Modal);
            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);
                string docName = "TLTracker" + DateTime.Now.ToString("dd-MMM-yyyy");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", docName + ".xlsx"));
                Response.BinaryWrite(exportData.ToArray());
                Response.End();
            }
        }

        public void CompetitionSummary_Export(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Date = GetQueryString[2];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = ViewBag.Date;
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            var workbook = export.CompetitionSummary_Workbook(Modal);
            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);
                string docName = "CompetitionSummary_" + DateTime.Now.ToString("dd-MMM-yyyy");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", docName + ".xlsx"));
                Response.BinaryWrite(exportData.ToArray());
                Response.End();
            }
        }

        public void SaleEntryWithCustomerReport_Export(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.StartDate = GetQueryString[2];
            ViewBag.EndDate = GetQueryString[3];
            Tab.Approval Modal = new Tab.Approval();
            Modal.StartDate = ViewBag.StartDate;
            Modal.EndDate = ViewBag.EndDate;
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            var workbook = export.SaleEntryWithCustomerReport_Workbook(Modal);
            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);
                string docName = "SaleEntryWithCustomerReport_" + DateTime.Now.ToString("dd-MMM-yyyy");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", docName + ".xlsx"));
                Response.BinaryWrite(exportData.ToArray());
                Response.End();
            }
        }

        public void TravelExpenseList_Export(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Date = GetQueryString[2];

            Tab.Approval Modal = new Tab.Approval();
            DateTime dt;
            DateTime.TryParse(ViewBag.Date, out dt);
            Modal.Month = ViewBag.Date;
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            var workbook = export.GetTravel_Expenses_Workbook(Modal);
            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);
                string docName = "Travel_Expenses_" + dt.ToString("MMM-yyyy");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", docName + ".xlsx"));
                Response.BinaryWrite(exportData.ToArray());
                Response.End();
            }
        }

        public void TravelVisitReport_Export(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Date = GetQueryString[2];

            Tab.Approval Modal = new Tab.Approval();
            DateTime dt;
            DateTime.TryParse(ViewBag.Date, out dt);
            Modal.Month = ViewBag.Date;
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            var workbook = export.GetTravel_Visit_Report_Workbook(Modal);
            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);
                string docName = "TravelVisit_" + dt.ToString("MMM-yyyy");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", docName + ".xlsx"));
                Response.BinaryWrite(exportData.ToArray());
                Response.End();
            }
        }


        public ActionResult AttendanceLog_Day_PDF(string src)
        {
            try
            {
                ViewBag.src = src;
                string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
                ViewBag.GetQueryString = GetQueryString;
                ViewBag.MenuID = GetQueryString[0];
                ViewBag.Date = GetQueryString[2];
                ViewBag.Usertype = GetQueryString[3];
                Tab.Approval Modal = new Tab.Approval();
                Modal.Month = ViewBag.Date;
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.Usertype = ViewBag.Usertype;
                var Result = report.GetAttendance_Log_Daily(Modal);
                string PhysicalPath = ClsApplicationSetting.GetPhysicalPath("pdfexport");
                string FileName = "";
                if (Result != null)
                {
                    FileName = "AttendanceLog_Day" + DateTime.Now.ToString("dd-MMM-yyyy") + ".pdf";
                    rd = new ReportDocument();
                    string _reportPath = Server.MapPath(@"\CrystalReport\AttendanceLog_Daily.rpt");
                    rd.Load(_reportPath);
                    rd.SetDataSource(Result);
                    if (System.IO.File.Exists(Path.Combine(PhysicalPath, FileName)))
                    {
                        System.IO.File.Delete(Path.Combine(PhysicalPath, FileName));
                    }
                    string FilePath = Path.Combine(PhysicalPath, FileName);
                    rd.ExportToDisk(ExportFormatType.PortableDocFormat, FilePath);
                    rd.Close();
                    rd.Dispose();
                    byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(PhysicalPath, FileName));
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, FileName);
                }
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        public void CounterDisplayReport_Export(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.StartDate = GetQueryString[2];
            ViewBag.EndDate = GetQueryString[3];

            Tab.Approval Modal = new Tab.Approval();
            Modal.StartDate = ViewBag.StartDate;
            Modal.EndDate = ViewBag.EndDate;
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            Modal.Approved = -1;
            DateTime dt;
            DateTime.TryParse(Modal.StartDate, out dt);
            var workbook = export.CounterDisplayReport_Workbook(Modal);
            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);
                string docName = "CounterDisplay_" + dt.ToString("MMM-yyyy");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", docName + ".xlsx"));
                Response.BinaryWrite(exportData.ToArray());
                Response.End();
            }
        }

        public void CompetitionEntryReport_Export(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.StartDate = GetQueryString[2];
            ViewBag.EndDate = GetQueryString[3];

            Tab.Approval Modal = new Tab.Approval();
            Modal.StartDate = ViewBag.StartDate;
            Modal.EndDate = ViewBag.EndDate;
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            Modal.Approved = -1;
            DateTime dt;
            DateTime.TryParse(Modal.StartDate, out dt);
            var ds = Common_SPU.GetCompetitionEntry_Report(Modal);
            var workbook = export.GetDataTable_Workbook_Common(ds.Tables[0],"");
            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);
                string docName = "CompetitionEntry_" + dt.ToString("MMM-yyyy");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", docName + ".xlsx"));
                Response.BinaryWrite(exportData.ToArray());
                Response.End();
            }
        }

        public void MiscellaneousReports_Export(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            ViewBag.Name = GetQueryString[3];
            Tab.Approval Modal = new Tab.Approval();
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);
            Modal.ID = ID;
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            DataTable dt = Common_SPU.GetMiscellaneousReports(Modal).Tables[0];
            var workbook =  export.GetDataTable_Workbook(dt, ViewBag.Name);
            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);
                string docName = ViewBag.Name +"_"+ DateTime.Now.ToString("dd-MMM-yyyy");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", docName + ".xlsx"));
                Response.BinaryWrite(exportData.ToArray());
                Response.End();
            }

        }

        public void IncentiveCalculator_Export(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Date = GetQueryString[2];
            Tab.Approval Modal = new Tab.Approval();
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            DateTime dt;
            DateTime.TryParse(ViewBag.Date, out dt);
            Modal.Month = dt.ToString("dd-MMM-yyyy");
            var workbook = export.GetIncentiveCalculator_Workbook(Modal);
            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);
                string docName = "Incentive_" + DateTime.Now.ToString("dd-MMM-yyyy");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", docName + ".xlsx"));
                Response.BinaryWrite(exportData.ToArray());
                Response.End();
            }

        }

    }
}