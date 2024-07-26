using DataModal.CommanClass;
using DataModal.DataModal.ModelsMaster;
using DataModal.Models;
using DataModal.ModelsMaster;
using DataModal.ModelsMasterHelper;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.CommonClass;


namespace Website.Controllers
{
    [CheckLoginFilter]
    public class RecuritController : Controller
    {
        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        IRecuritHelper recur;
        public RecuritController()
        {
            getResponse = new GetResponse();
            long.TryParse(ClsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            IPAddress = ClsApplicationSetting.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
            recur = new RecuritModal();
        }

        public ActionResult RequestsList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _RequestsList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            ViewBag.Approved = Modal.Approved;
            return PartialView(recur.GetRequirement_MyRequest(Modal));
        }

        public ActionResult _AddRequest(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ReqID = GetQueryString[2];
            long ReqID = 0;
            long.TryParse(ViewBag.ReqID, out ReqID);
            getResponse.ID = ReqID;
            Requirement.AddRequest result = new Requirement.AddRequest();
            result = recur.GetRequirement_Request(getResponse);
            return PartialView(result);
        }


        [HttpPost]
        public ActionResult _AddRequest(string src, Requirement.AddRequest Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ReqID = GetQueryString[2];
            long ReqID = 0;
            long.TryParse(ViewBag.ReqID, out ReqID);
            Result.SuccessMessage = "Request Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.ReqID = ReqID;
                Result = recur.fnSetRequirement_Request(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }


        public ActionResult AllRequestsList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _AllRequestsList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            ViewBag.Approved = Modal.Approved;
            return PartialView(recur.GetRequirement_RequestList(Modal));
        }

        public ActionResult _RequestApplicationList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ReqID = GetQueryString[2];
            long ReqID = 0;
            long.TryParse(ViewBag.ReqID, out ReqID);
            getResponse.ID = ReqID;
            return PartialView(recur.GetRequirement_ApplicationList(getResponse));
        }

        public ActionResult AddApplication(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ReqID = GetQueryString[2];
            long ReqID = 0;
            long.TryParse(ViewBag.ReqID, out ReqID);
            Requirement.Application.Add modal = new Requirement.Application.Add();
            modal.ReqID = ReqID;
            return View(modal);
        }

        [HttpPost]
        public ActionResult AddApplication(string src, Requirement.Application.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ReqID = GetQueryString[2];
            long ReqID = 0;
            long.TryParse(ViewBag.ReqID, out ReqID);
            Result.SuccessMessage = "Can't Update";
            string PhysicalPath = ClsApplicationSetting.GetPhysicalPath("");
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.ReqID = ReqID;
                if (Modal.Upload != null)
                {
                    UploadAttachment attachModal = new UploadAttachment();
                    attachModal.File = Modal.Upload;
                    attachModal.LoginID = LoginID;
                    attachModal.IPAddress = IPAddress;
                    attachModal.AttachID = Modal.AttachID;
                    attachModal.Doctype = "recruit";
                    var Attach = ClsApplicationSetting.UploadAttachment(attachModal);
                    Modal.AttachID = Attach.ID;
                    if (!Attach.Status)
                    {
                        Result.SuccessMessage = Attach.SuccessMessage;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }
                Result = recur.fnSetRequirement_Application(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ViewCompleteRequirement(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ReqID = GetQueryString[2];
            long ReqID = 0;
            long.TryParse(ViewBag.ReqID, out ReqID);
            getResponse.ID = ReqID;
            return View(recur.GetRequirement_FullView(getResponse));
        }

        public ActionResult ApprovedApplication(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ReqID = GetQueryString[2];
            long ReqID = 0;
            long.TryParse(ViewBag.ReqID, out ReqID);
            getResponse.ID = ReqID;
            return View(recur.GetRequirement_FullView(getResponse));
        }

        [HttpPost]
        public ActionResult ApprovedApplication(string src, ApprovalAction Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (!string.IsNullOrEmpty(Modal.IDs))
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.IDs = Modal.IDs.TrimEnd(',');
                Result = recur.fnSetRequirementApplication_Approved(Modal);
                if (Result.Status)
                {
                    Result.RedirectURL = "/Recurit/RequestsList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Recurit/RequestsList");
                }
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult LevelApprovals(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _LevelApprovals(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            ViewBag.Approved = Modal.Approved;
            return PartialView(recur.GetRequirement_LevelApprovalList(Modal));
        }


        public ActionResult ApproveRequirementRequest(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ReqID = GetQueryString[2];
            long ReqID = 0;
            long.TryParse(ViewBag.ReqID, out ReqID);
            getResponse.ID = ReqID;
            return View(recur.GetRequirement_FullView(getResponse));
        }

        [HttpPost]
        public ActionResult ApproveRequirementRequest(string src, ApprovalAction Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ReqID = GetQueryString[2];
            long ReqID = 0;
            long.TryParse(ViewBag.ReqID, out ReqID);
            if (ReqID!=0)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.IDs = ReqID.ToString();
                Result = recur.fnSetRequirement_Approved(Modal);
                if (Result.Status)
                {
                    Result.RedirectURL = "/Recurit/RequestsList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Recurit/RequestsList");
                }
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
    }
}