﻿using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMaster;
using DataModal.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Website.CommonClass;

namespace Website.Controllers
{
    [CheckLoginFilter]
    public class LeaveController : Controller
    {
        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        ILeaveHelper Leave;
        public LeaveController()
        {
            getResponse = new GetResponse();
            long.TryParse(ClsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            IPAddress = ClsApplicationSetting.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
            Leave = new LeaveModal();

        }
        public ActionResult LeaveTypeList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<LeaveType.List> result = new List<LeaveType.List>();
            result = Leave.GetMaster_LeaveTypeList(getResponse);
            return View(result);
        }
        public ActionResult _LeaveTypeAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.LeaveTypeID = GetQueryString[2];
            long LeaveTypeID = 0;
            long.TryParse(ViewBag.LeaveTypeID, out LeaveTypeID);
            getResponse.ID = LeaveTypeID;
            LeaveType.Add result = new LeaveType.Add();
            result = Leave.GetMaster_LeaveType(getResponse);
            return PartialView(result);
        }

        [HttpPost]
        public ActionResult _LeaveTypeAdd(string src, LeaveType.Add  Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.LeaveTypeID = GetQueryString[2];
            long LeaveTypeID = 0;
            long.TryParse(ViewBag.LeaveTypeID, out LeaveTypeID);
            Result.SuccessMessage = "Status Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.LeaveTypeID = LeaveTypeID;
                Result = Leave.fnSetMaster_LeaveType(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult AppliedLeaveList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _AppliedLeaveList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<LeaveLog.List> result = new List<LeaveLog.List>();
            ViewBag.Approved= Modal.Approved;
            getResponse.Approved = Modal.Approved;
            result = Leave.GetLeave_LogList(getResponse);
            return PartialView(result);
        }

        public ActionResult ApproverLeaveList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }
        public ActionResult _ApproverLeaveList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            List<LeaveLog.ApproverList> result = new List<LeaveLog.ApproverList>();
            result = Leave.GetLeaveLogApprovalList(Modal);
            return PartialView(result);
        }

        public ActionResult _LeaveLog_View(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.LogID = GetQueryString[2];
            long LogID = 0;
            long.TryParse(ViewBag.LogID, out LogID);
            LeaveLog.View result = new LeaveLog.View();
            getResponse.ID = LogID;
            result = Leave.GetLeaveLogTran(getResponse);
            return PartialView(result);
        }


        public ActionResult _ApplyLeaveTran(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            AddLeave Modal = new AddLeave();
            Modal = Leave.GetLeave_Apply(getResponse);

            DateTime StartDate, EndDate;
            long LeaveTypeID = 0;
            long.TryParse(GetQueryString[2], out LeaveTypeID);
            DateTime.TryParse(GetQueryString[3], out StartDate);
            DateTime.TryParse(GetQueryString[4], out EndDate);
            List<AddLeaveTran> TranList = new List<AddLeaveTran>();

            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
            getResponse.Param1 = StartDate.ToString("dd-MMM-yyyy");
            getResponse.Param2 = EndDate.ToString("dd-MMM-yyyy");
            getResponse.ID = LeaveTypeID;
            TranList = Leave.GetApplyLeaveTran(getResponse);
            Modal.LeaveTranList = TranList;
            return PartialView(Modal);
        }
        public ActionResult ApplyLeave(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            AddLeave Modal = new AddLeave();
            Modal = Leave.GetLeave_Apply(getResponse);
            return View(Modal);
        }

        [HttpPost]
        public ActionResult ApplyLeave(string src, AddLeave Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long EMPID = 0;
            long.TryParse(ClsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            if (EMPID == 0)
            {
                Result.SuccessMessage = "only Employee can apply for leave";
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            else if (Modal.LeaveTranList == null || Modal.LeaveTranList.Count == 0)
            {
                Result.SuccessMessage = "request for atleast one days";
                return Json(Result, JsonRequestBehavior.AllowGet);
            }

            Result.SuccessMessage = "Can't Update";
            if (ModelState.IsValid)
            {
                Modal.EMPID = EMPID;
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Result = Leave.fnSetLeaveLog(Modal);
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Leave/AppliedLeaveList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Leave/AppliedLeaveList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult ApproveLeave(string src, FormCollection form, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.LogID = GetQueryString[2];
            long LogID = 0;
            long.TryParse(ViewBag.LogID, out LogID);

            int Approved = 0;
            int.TryParse(Command, out Approved);

            LeaveApproval Modal = new LeaveApproval();
            Modal.LogID = LogID;
            Modal.Approved = Approved;
            Modal.ApprovedRemarks = form["ApproveRemarks"].ToString();
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            Result = Leave.fnSetLeave_Log_Approve(Modal);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
    }
}