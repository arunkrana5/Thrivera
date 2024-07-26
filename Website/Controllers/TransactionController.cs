using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMaster;
using DataModal.ModelsMasterHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.CommonClass;

namespace Website.Controllers
{
    [CheckLoginFilter]
    public class TransactionController : Controller
    {
        long LoginID = 0, EMPID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        ITransactionHelper Transaction;
        public TransactionController()
        {
            getResponse = new GetResponse();
            long.TryParse(ClsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            long.TryParse(ClsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            IPAddress = ClsApplicationSetting.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
            Transaction = new TransactionModal();

        }

        public ActionResult TourPlanList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval result = new Tab.Approval();
            return View(result);
        }
        [HttpPost]
        public ActionResult _TourPlanList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approved = Modal.Approved;
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            return PartialView(Transaction.GetTourPlanList(Modal));
        }

        public ActionResult _AddTourPlan(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TourPlanID = GetQueryString[2];
            TourPlan.Add Modal = new TourPlan.Add();
            long ID = 0;
            long.TryParse(ViewBag.TourPlanID, out ID);
            getResponse.ID = ID;
            Modal = Transaction.GetTourPlan(getResponse);
            
            return PartialView(Modal);

        }
        [HttpPost]
        public ActionResult _AddTourPlan(string src, TourPlan.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TourPlanID = GetQueryString[2];
           
            long TourPlanID = 0;
            long.TryParse(ViewBag.TourPlanID, out TourPlanID);
            Result.SuccessMessage = "Tour Plan Can't Update";
            
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.TourPlanID = TourPlanID;
                Result = Transaction.fnSetTourPlan(Modal);

            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Transaction/TourPlanList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Transaction/TourPlanList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult PJPPlanList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            ViewBag.Import = "True";
            Modal.Month = DateTime.Now.ToString("yyyy-MM-dd");
            return View(Modal);
        }

        [HttpPost]
        public ActionResult _PJPPlanList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<PJPPlan.List> Result = new List<PJPPlan.List>();
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            Result = Transaction.GetPJPPlanList(Modal);
            return PartialView(Result);
        }

        public ActionResult _AddPJPPlan(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.PJPID = GetQueryString[2];
            ViewBag.OnDemand = GetQueryString[3];
            int OnDemand = 0;
            int.TryParse(ViewBag.OnDemand, out OnDemand);
            PJPPlan.Add Modal = new PJPPlan.Add();
            long ID = 0;
            long.TryParse(ViewBag.PJPID, out ID);

            getResponse.ID = ID;
            Modal = Transaction.GetPJPPlan(getResponse);


            return PartialView(Modal);

        }
        [HttpPost]
        public ActionResult _AddPJPPlan(string src, PJPPlan.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.PJPID = GetQueryString[2];
            ViewBag.OnDemand = GetQueryString[3];
            int OnDemand = 0;
            long PJPID = 0;
            long.TryParse(ViewBag.PJPID, out PJPID);
            int.TryParse(ViewBag.OnDemand, out OnDemand);
            Result.SuccessMessage = "PJP Can't Update";
            
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.PJPID = PJPID;
                Modal.OnDemand = OnDemand;
                Result = Transaction.fnSetPJPPlan(Modal);

            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Transaction/PJPPlanList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Transaction/PJPPlanList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SaleEntryList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            ViewBag.Import = "True";
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }

        public ActionResult _SaleEntryList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approved = Modal.Approved;
            List<SaleEntry.List> Result = new List<SaleEntry.List>();
            Modal.LoginID = LoginID;
            Result = Transaction.GetSaleEntryList(Modal);
            return PartialView(Result);
        }
        public ActionResult SaleEntryAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.SaleEntryID = GetQueryString[2];
            long SaleEntryID = 0;
            long.TryParse(ViewBag.SaleEntryID, out SaleEntryID);
            getResponse.ID = SaleEntryID;
            SaleEntry.Add Modal = new SaleEntry.Add();
            Modal = Transaction.GetSaleEntry(getResponse);
            return View(Modal);
        }

        [HttpPost]
        public ActionResult SaleEntryAdd(string src, SaleEntry.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.SaleEntryID = GetQueryString[2];
            long SaleEntryID = 0, EMPID = 0;
            long.TryParse(ViewBag.SaleEntryID, out SaleEntryID);
            long.TryParse(ClsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            Result.SuccessMessage = "Sale Entry Can't Update";
           
            if (Modal.AttachmentID == 0 && Modal.Upload==null)
            {
                Result.SuccessMessage = "Image is mandiatory";
                ModelState.AddModelError("Upload", Result.SuccessMessage);
            }
            //if (Company.ToLower() == "daikin")
            //{
            //    DateTime invoiceDate;
            //    DateTime.TryParse(Modal.InvoiceDate, out invoiceDate);
            //    if (Modal.Price < 20000 || Modal.Price > 150000)
            //    {
            //        Result.SuccessMessage = "Price must between 20,000 to 1,50,000";
            //        return Json(Result, JsonRequestBehavior.AllowGet);
            //    }
            //    else if(invoiceDate.Month!= DateTime.Now)
            //    {

            //    }
            //}
            
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.SaleEntryID = SaleEntryID;
                Modal.EMPID = EMPID;
                if (Modal.Upload!=null)
                {
                    UploadAttachment attachModal = new UploadAttachment();
                    attachModal.File = Modal.Upload;
                    attachModal.LoginID = LoginID;
                    attachModal.IPAddress = IPAddress;
                    attachModal.AttachID = Modal.AttachmentID;
                    attachModal.Doctype = "SSR";
                    var Attach = ClsApplicationSetting.UploadAttachment(attachModal);
                    Modal.AttachmentID = Attach.ID;
                    if (!Attach.Status)
                    {
                        Result.SuccessMessage = Attach.SuccessMessage;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }
                Result = Transaction.fnSetSaleEntry(Modal);

            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Transaction/SaleEntryList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Transaction/SaleEntryList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }



        public ActionResult MOPList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Date Modal = new Tab.Date();
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }
        public ActionResult _MOPList(string src, Tab.Date Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<MOP.List> Result = new List<MOP.List>();
            Modal.LoginID = LoginID;
            Result = Transaction.GetMOPList(Modal);
            return PartialView(Result);
        }

        public ActionResult _AddMOP(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MOPID = GetQueryString[2];
            long MOPID = 0;
            long.TryParse(ViewBag.MOPID, out MOPID);

            MOP.Add Modal = new MOP.Add();
            getResponse.ID = MOPID;
            Modal = Transaction.GetMOP(getResponse);
            return PartialView(Modal);
        }

        [HttpPost]
        public ActionResult _AddMOP(string src, MOP.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MOPID = GetQueryString[2];
            long MOPID = 0;
            long.TryParse(ViewBag.MOPID, out MOPID);
            Result.SuccessMessage = "MOP Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.MOPID = MOPID;
                Modal.EMPID = EMPID;
                Result = Transaction.fnSetMOP(Modal);

            }

            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult CounterDisplayList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Date Modal = new Tab.Date();
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }
        public ActionResult _CounterDisplayList(string src, Tab.Date Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<CounterDisplay.List> Result = new List<CounterDisplay.List>();
            Modal.LoginID = LoginID;
            Result = Transaction.GetCounterDisplayList(Modal);
            return PartialView(Result);
        }
        public ActionResult _AddCounterDisplay(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MOPID = GetQueryString[2];
            long MOPID = 0;
            long.TryParse(ViewBag.MOPID, out MOPID);

            CounterDisplay.Add Modal = new CounterDisplay.Add();
            getResponse.ID = MOPID;
            Modal = Transaction.GetCounterDisplay(getResponse);
            return PartialView(Modal);
        }
        [HttpPost]
        public ActionResult _AddCounterDisplay(string src, CounterDisplay.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CounterID = GetQueryString[2];
            long CounterID = 0;
            long.TryParse(ViewBag.CounterID, out CounterID);
            Result.SuccessMessage = "Can't Update";
            string PhysicalPath = ClsApplicationSetting.GetPhysicalPath("SSREntry");
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.CounterID = CounterID;
                Modal.EMPID = EMPID;

                if (!string.IsNullOrEmpty(Modal.ImageBase64String))
                {
                    FileResponse attachModal = new FileResponse();
                    attachModal.ImageBase64String = Modal.ImageBase64String;
                    attachModal.LoginID = LoginID;
                    attachModal.IPAddress = IPAddress;
                    attachModal.ID = Modal.AttachmentID;
                    attachModal.Doctype = "SSR";
                    var Attach = ClsApplicationSetting.UploadCameraImage(attachModal);
                    Modal.AttachmentID = Attach.ID;
                    if (!Attach.Status)
                    {
                        Result.SuccessMessage = Attach.SuccessMessage;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }

                Result = Transaction.fnSetCounterDisplay(Modal);

            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult RFCRequestsList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _RFCRequestsList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approved = Modal.Approved;
            List<RFCEntry.List> result = new List<RFCEntry.List>();
            Modal.LoginID = LoginID;
            result = Transaction.GetRFCEntryList(Modal);
            return PartialView(result);

        }

        public ActionResult _AddRFCRequests(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.RFCID = GetQueryString[2];
            RFCEntry.Add Modal = new RFCEntry.Add();
            Modal.DocDate = DateTime.Now.ToString("dd-MMM-yyyy");
            GetDropDownResponse getRes = new GetDropDownResponse();
            getRes.Doctype = "FinalAttendenceStatusList";
            Modal.NewStatusList = Common_SPU.GetDropDownList(getRes);
            Modal.OldList = new List<DropDownlist>();
            return PartialView(Modal);
        }

        [HttpPost]
        public ActionResult _AddRFCRequests(string src, RFCEntry.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long EMPID = 0;
            long.TryParse(ClsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            Result.SuccessMessage = "Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.EMPID = EMPID;

                Result = Transaction.fnSetRFCEntry(Modal);

            }

            return Json(Result, JsonRequestBehavior.AllowGet);

        }


       
        public ActionResult CompetitionEntryList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Date Modal = new Tab.Date();
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }
        public ActionResult _CompetitionEntryList(string src, Tab.Date Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<CompetitionEntry.List> Result = new List<CompetitionEntry.List>();
            Modal.LoginID = LoginID;
            Result = Transaction.GetCompetitionEntryList(Modal);
            return PartialView(Result);
        }

        public ActionResult _AddCompetitionEntry(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CompetitionID = GetQueryString[2];
            long CompetitionID = 0;
            long.TryParse(ViewBag.CompetitionID, out CompetitionID);
            CompetitionEntry.Add Modal = new CompetitionEntry.Add();
            getResponse.ID = CompetitionID;
            Modal = Transaction.GetCompetitionEntry(getResponse);
            return PartialView(Modal);
        }

        [HttpPost]
        public ActionResult _AddCompetitionEntry(string src, CompetitionEntry.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CompetitionID = GetQueryString[2];
            long CompetitionID = 0;
            long.TryParse(ViewBag.CompetitionID, out CompetitionID);
            string CompanyCode = ClsApplicationSetting.GetSessionValue("CompanyCode");
            Result.SuccessMessage = "Can't Update";

            if(CompanyCode== "Ogeneral")
            {
                if ((Modal.BrandID ?? 0) == 0)
                {
                    Result.SuccessMessage = "Brand is mandiatory";
                    ModelState.AddModelError("BrandID", Result.SuccessMessage);
                }
                else if (string.IsNullOrEmpty(Modal.Category))
                {
                    Result.SuccessMessage = "Category is mandiatory";
                    ModelState.AddModelError("Category", Result.SuccessMessage);
                }
                else if (string.IsNullOrEmpty(Modal.SubCategory))
                {
                    Result.SuccessMessage = "Tonnage is mandiatory";
                    ModelState.AddModelError("SubCategory", Result.SuccessMessage);
                }
                else if (string.IsNullOrEmpty(Modal.ModalNo))
                {
                    Result.SuccessMessage = "Modal No is mandiatory";
                    ModelState.AddModelError("ModalNo", Result.SuccessMessage);
                }
                else if ((Modal.StarRating ?? 0) == 0)
                {
                    Result.SuccessMessage = "Star Rating is mandiatory";
                    ModelState.AddModelError("StarRating", Result.SuccessMessage);
                }
                else if ((Modal.Qty ?? 0) == 0)
                {
                    Result.SuccessMessage = "Qty is mandiatory";
                    ModelState.AddModelError("Qty", Result.SuccessMessage);
                }
                else if ((Modal.Price ?? 0) == 0)
                {
                    Result.SuccessMessage = "Price is mandiatory";
                    ModelState.AddModelError("Price", Result.SuccessMessage);
                }
            }
            if (ModelState.IsValid)
            {
                Modal.CompetitionID = CompetitionID;
                Modal.IPAddress = IPAddress;
                Modal.LoginID = LoginID;
                Modal.EMPID = EMPID;
                Result = Transaction.fnSetCompetitionEntry(Modal);
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Transaction/CompetitionEntryList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Transaction/CompetitionEntryList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult TourPlanImport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<TourPlan_Import.List> Result = new List<TourPlan_Import.List>();
            Result = Transaction.GetTourPlanImportList(getResponse);
            ViewBag.ListCount = Result.Count;
            return View(Result);
        }

        [HttpPost]
        public ActionResult TourPlanImport(FormCollection Form, string Command, string src)
        {
            PostResponse Result = new PostResponse();
            Result.SuccessMessage = "Not completed";
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Command == "ImportData")
            {
                string SheetName = Form.Get("txtSheet");
                foreach (string upload in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[upload];
                    Result = Transaction.UploadTourPlanImportExcelFile(file, SheetName, getResponse);
                }
            }
            else if (Command == "ClearData")
            {
                Result = Transaction.ClearTourPlanImportTemp(getResponse);
            }
            else if (Command == "UploadData")
            {
                Result = Transaction.SetTourPlanFromTouPlanImport(getResponse);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult MyPJPPlanList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM-dd");
            return View(Modal);
        }
        public ActionResult _MyPJPPlanList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<PJPPlan.List> result = new List<PJPPlan.List>();
            Modal.LoginID = LoginID;
            result = Transaction.GetMyPJPPlanList(Modal);
            return PartialView(result);
        }

        public ActionResult PlanWisePJPEntryList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.PJPPlanID = GetQueryString[2];
            List<PJPEntry.List> result = new List<PJPEntry.List>();
            long PJPPlanID = 0;
            long.TryParse(ViewBag.PJPPlanID, out PJPPlanID);
            getResponse.ID = PJPPlanID;
            result = Transaction.GetPlanWisePJPEntryList(getResponse);
            return View(result);
        }
       

        public ActionResult _PJPEntryView(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long PJPPlanID = 0, PJPEntryID = 0, SSRTourPlanID = 0;
            ViewBag.PJPPlanID = GetQueryString[2];
            ViewBag.PJPEntryID = GetQueryString[3];
            ViewBag.SSRTourPlanID = GetQueryString[4];
            long.TryParse(ViewBag.PJPPlanID, out PJPPlanID);
            long.TryParse(ViewBag.PJPEntryID, out PJPEntryID);
            long.TryParse(ViewBag.SSRTourPlanID, out SSRTourPlanID);
            PJPEntry.Add result = new PJPEntry.Add();
            result = Transaction.GetPJPEntryAdd(getResponse, PJPPlanID, PJPEntryID, SSRTourPlanID);
            return PartialView(result);
        }

        public ActionResult PJPEntryAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long PJPPlanID = 0, PJPEntryID = 0, SSRTourPlanID = 0;

            ViewBag.PJPPlanID = GetQueryString[2];
            ViewBag.PJPEntryID = GetQueryString[3];
            ViewBag.SSRTourPlanID = GetQueryString[4];
            
            long.TryParse(ViewBag.PJPPlanID, out PJPPlanID);
            long.TryParse(ViewBag.PJPEntryID, out PJPEntryID);
            long.TryParse(ViewBag.SSRTourPlanID, out SSRTourPlanID);
            PJPEntry.Add result = new PJPEntry.Add();
            result = Transaction.GetPJPEntryAdd(getResponse, PJPPlanID, PJPEntryID, SSRTourPlanID);
            if (result.SSRList.Count == 0)
            {
                PJPEntry.AddWithNoSSR SSRresult = new PJPEntry.AddWithNoSSR();
                string output = JsonConvert.SerializeObject(result);
                SSRresult = JsonConvert.DeserializeObject<PJPEntry.AddWithNoSSR>(output);
                return View("PJPEntryAdd_NOSSR", SSRresult);
            }


            return View(result);
        }

        [HttpPost]
        public ActionResult PJPEntryAdd(string src, PJPEntry.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long PJPPlanID = 0, PJPEntryID = 0, SSRTourPlanID = 0;
            ViewBag.PJPPlanID = GetQueryString[2];
            ViewBag.PJPEntryID = GetQueryString[3];
            ViewBag.SSRTourPlanID = GetQueryString[4];
            long.TryParse(ViewBag.PJPPlanID, out PJPPlanID);
            long.TryParse(ViewBag.PJPEntryID, out PJPEntryID);
            long.TryParse(ViewBag.SSRTourPlanID, out SSRTourPlanID);
            Result.SuccessMessage = "Can't Update";
            string Company = ClsApplicationSetting.GetConfigValue("Company");
            if (Company.ToLower() == "daikin" && Modal.BrandEntryList.Any(x => string.IsNullOrEmpty(x.ImageBase64String)))
            {
                Result.SuccessMessage = "Counter Display can't be blank";
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.PJPPlanID = PJPPlanID;
                Modal.PJPEntryID = PJPEntryID;
                Modal.IPAddress = IPAddress;

                if (!string.IsNullOrEmpty(Modal.ImageBase64String))
                {
                    FileResponse attachModal = new FileResponse();
                    attachModal.ImageBase64String = Modal.ImageBase64String;
                    attachModal.LoginID = LoginID;
                    attachModal.IPAddress = IPAddress;
                    attachModal.ID = Modal.AttachmentID;
                    attachModal.Doctype = "TL";
                    var Attach = ClsApplicationSetting.UploadCameraImage(attachModal);
                    Modal.AttachmentID = Attach.ID;
                    if (!Attach.Status)
                    {
                        Result.SuccessMessage = Attach.SuccessMessage;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }
                if (Modal.ExpenseUpload != null)
                {
                    UploadAttachment attachModal = new UploadAttachment();
                    attachModal.File = Modal.ExpenseUpload;
                    attachModal.LoginID = LoginID;
                    attachModal.IPAddress = IPAddress;
                    attachModal.AttachID = Modal.ExpenseAttachmentID;
                    attachModal.Doctype = "TL";
                    var Attach = ClsApplicationSetting.UploadAttachment(attachModal);
                    Modal.ExpenseAttachmentID = Attach.ID;
                    if (!Attach.Status)
                    {
                        Result.SuccessMessage = Attach.SuccessMessage;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }
                Result = Transaction.fnSetPJPEntry(Modal);
                if (Result.Status && Modal.BrandEntryList != null)
                {
                    foreach (var item in Modal.BrandEntryList)
                    {
                        item.LoginID = LoginID;
                        item.PJPEntryID = Result.ID;
                        item.IPAddress = IPAddress;

                        if (!string.IsNullOrEmpty(item.ImageBase64String))
                        {
                            FileResponse attachModal = new FileResponse();
                            attachModal.ImageBase64String = item.ImageBase64String;
                            attachModal.LoginID = LoginID;
                            attachModal.IPAddress = IPAddress;
                            attachModal.ID = item.AttachID;
                            attachModal.Doctype = "TL";
                            var Attach = ClsApplicationSetting.UploadCameraImage(attachModal);
                            item.AttachID = Attach.ID;
                            if (!Attach.Status)
                            {
                                Result.SuccessMessage = Attach.SuccessMessage;
                                return Json(Result, JsonRequestBehavior.AllowGet);
                            }
                        }


                        var a = Transaction.fnSetPJPEntry_Brand(item);
                    }
                }
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Transaction/PlanWisePJPEntryList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Transaction/PlanWisePJPEntryList*" + PJPPlanID + "*0*0");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult PJPEntryAdd_NOSSR(string src, PJPEntry.AddWithNoSSR Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long PJPPlanID = 0, PJPEntryID = 0, SSRTourPlanID = 0;
            ViewBag.PJPPlanID = GetQueryString[2];
            ViewBag.PJPEntryID = GetQueryString[3];
            ViewBag.SSRTourPlanID = GetQueryString[4];
            long.TryParse(ViewBag.PJPPlanID, out PJPPlanID);
            long.TryParse(ViewBag.PJPEntryID, out PJPEntryID);
            long.TryParse(ViewBag.SSRTourPlanID, out SSRTourPlanID);

            Result.SuccessMessage = "Can't Update";
            string Company = ClsApplicationSetting.GetConfigValue("Company");
            if (Company.ToLower() == "daikin" && Modal.BrandEntryList.Any(x => string.IsNullOrEmpty(x.ImageBase64String)))
            {
                Result.SuccessMessage = "Counter Display can't be blank";
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.PJPPlanID = PJPPlanID;
                Modal.PJPEntryID = PJPEntryID;
                Modal.IPAddress = IPAddress;

                if (!string.IsNullOrEmpty(Modal.ImageBase64String))
                {
                    FileResponse attachModal = new FileResponse();
                    attachModal.ImageBase64String = Modal.ImageBase64String;
                    attachModal.LoginID = LoginID;
                    attachModal.IPAddress = IPAddress;
                    attachModal.ID = Modal.AttachmentID;
                    attachModal.Doctype = "TL";
                    var Attach = ClsApplicationSetting.UploadCameraImage(attachModal);
                    Modal.AttachmentID = Attach.ID;
                    if (!Attach.Status)
                    {
                        Result.SuccessMessage = Attach.SuccessMessage;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }
                if (Modal.ExpenseUpload != null)
                {
                    UploadAttachment attachModal = new UploadAttachment();
                    attachModal.File = Modal.ExpenseUpload;
                    attachModal.LoginID = LoginID;
                    attachModal.IPAddress = IPAddress;
                    attachModal.AttachID = Modal.ExpenseAttachmentID;
                    attachModal.Doctype = "TL";
                    var Attach = ClsApplicationSetting.UploadAttachment(attachModal);
                    Modal.ExpenseAttachmentID = Attach.ID;
                    if (!Attach.Status)
                    {
                        Result.SuccessMessage = Attach.SuccessMessage;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }


                Result = Transaction.fnSetPJPEntryWithNoSSR(Modal);
                if (Result.Status && Modal.BrandEntryList != null)
                {
                    foreach (var item in Modal.BrandEntryList)
                    {
                        item.LoginID = LoginID;
                        item.PJPEntryID = Result.ID;
                        item.IPAddress = IPAddress;
                        if (!string.IsNullOrEmpty(item.ImageBase64String))
                        {
                            FileResponse attachModal = new FileResponse();
                            attachModal.ImageBase64String = item.ImageBase64String;
                            attachModal.LoginID = LoginID;
                            attachModal.IPAddress = IPAddress;
                            attachModal.ID = item.AttachID;
                            attachModal.Doctype = "TL";
                            var Attach = ClsApplicationSetting.UploadCameraImage(attachModal);
                            item.AttachID = Attach.ID;
                            if (!Attach.Status)
                            {
                                Result.SuccessMessage = Attach.SuccessMessage;
                                return Json(Result, JsonRequestBehavior.AllowGet);
                            }
                        }


                        var a = Transaction.fnSetPJPEntry_Brand(item);
                    }
                }
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Transaction/PlanWisePJPEntryList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Transaction/PlanWisePJPEntryList*" + PJPPlanID + "*0*0");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }




        public ActionResult SaleEntry_Import(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<SaleEntry_Import.List> Result = new List<SaleEntry_Import.List>();
            Result = Transaction.GetSaleEntry_ImportList(getResponse);
            ViewBag.ListCount = Result.Count;
            return View(Result);
        }
        [HttpPost]
        public ActionResult SaleEntry_Import(FormCollection Form, string Command, string src)
        {
            PostResponse Result = new PostResponse();
            Result.SuccessMessage = "Not completed";
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Command == "ImportData")
            {
                string SheetName = Form.Get("txtSheet");
                foreach (string upload in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[upload];
                    Result = Transaction.UploadSaleEntryImportExcelFile(file, SheetName, getResponse);
                }
            }
            else if (Command == "ClearData")
            {
                Result = Transaction.ClearSaleEntryImportTemp(getResponse);
            }
            else if (Command == "UploadData")
            {
                Result = Transaction.SetSaleEntryFromSaleEntryImport(getResponse);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }



        public ActionResult PJPPlan_Import(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<PJPPlan_Import.List> Result = new List<PJPPlan_Import.List>();
            Result = Transaction.GetPJPPlan_ImportList(getResponse);
            ViewBag.ListCount = Result.Count;
            return View(Result);
        }
        [HttpPost]
        public ActionResult PJPPlan_Import(FormCollection Form, string Command, string src)
        {
            PostResponse Result = new PostResponse();
            Result.SuccessMessage = "Not completed";
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Command == "ImportData")
            {
                string SheetName = Form.Get("txtSheet");
                foreach (string upload in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[upload];
                    Result = Transaction.UploadPJPPlanImportExcelFile(file, SheetName, getResponse);
                }
            }
            else if (Command == "ClearData")
            {
                Result = Transaction.ClearPJPPlanImportTemp(getResponse);
            }
            else if (Command == "UploadData")
            {
                Result = Transaction.SetPJPPlanFromPJPPlanImport(getResponse);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }



        public ActionResult MyPJPEntryList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM-dd");
            return View(Modal);
        }
        public ActionResult _MyPJPEntryList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            return PartialView(Transaction.GetMyPJPEntryList(Modal));
        }


        public ActionResult TargetsList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Targets.List> Modal = new List<Targets.List>();
            ViewBag.Import = "True";
            Tab.Approval result = new Tab.Approval();
            result.Month = DateTime.Now.ToString("yyyy-MM");
            return View(result);
        }
        [HttpPost]
        public ActionResult _TargetsList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
           
            return PartialView(Transaction.GetTargets_List(Modal));
        }
        public ActionResult _AddTarget(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TID = GetQueryString[2];
            long TID = 0;
            long.TryParse(ViewBag.TID, out TID);
            getResponse.ID = TID;
            return PartialView(Transaction.GetTargets_Add(getResponse) );
        }
        [HttpPost]
        public ActionResult _AddTarget(string src, Targets.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TID = GetQueryString[2];
            
            Result.SuccessMessage = "Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Result = Transaction.fnSetTarget(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult TargetTranList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long TID = 0;
            ViewBag.TID = GetQueryString[2];
            long.TryParse(ViewBag.TID, out TID);
            Targets.TranList result = new Targets.TranList();
            getResponse.ID = TID;
            result = Transaction.GetTargetsTran_List(getResponse);
            return View(result);
        }

        public ActionResult TargetImport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<TargetImport.List> Result = new List<TargetImport.List>();
            Result = Transaction.GetTargets_ImportList(getResponse);
            ViewBag.ListCount = Result.Count;
            return View(Result);
        }
        [HttpPost]
        public ActionResult TargetImport(FormCollection Form, string Command, string src)
        {
            PostResponse Result = new PostResponse();
            Result.SuccessMessage = "Not completed";
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Command == "ImportData")
            {
                string SheetName = Form.Get("txtSheet");
                foreach (string upload in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[upload];
                    Result = Transaction.UploadEMPTargetImportExcelFile(file, SheetName, getResponse);
                }
            }
            else if (Command == "ClearData")
            {
                Result = Transaction.ClearEMPTargetImportTemp(getResponse);
            }
            else if (Command == "UploadData")
            {
                Result = Transaction.SetTarget_FromTargetImport(getResponse);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _TourPlanHistory(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long TourPlanID = 0;
            ViewBag.TourPlanID = GetQueryString[2];
            long.TryParse(ViewBag.TourPlanID, out TourPlanID);            
            getResponse.ID = TourPlanID;
            return PartialView(Transaction.GetTourPlan_History(getResponse));
        }
    }
}