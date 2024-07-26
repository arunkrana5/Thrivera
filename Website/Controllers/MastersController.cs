using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMaster;
using DataModal.ModelsMasterHelper;
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
    public class MastersController : Controller
    {

        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        IMasterHelper Master;
        IToolHelper Tools;
        public MastersController()
        {
            getResponse = new GetResponse();
            long.TryParse(ClsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            IPAddress = ClsApplicationSetting.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
            Master = new MasterModal();
            Tools = new ToolsModal();
        }

        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        public ActionResult PaymentModeList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TableName = "PaymentMode";
            ViewBag.Import = "True";
            getResponse.Doctype = ViewBag.TableName;

            List<Masters.List> result = new List<Masters.List>();
            result = Master.GetMastersList(getResponse);
            return View(result);


        }

        public ActionResult TravelModeList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TableName = "TravelMode";
            ViewBag.Import = "True";
            getResponse.Doctype = ViewBag.TableName;
            List<Masters.List> result = new List<Masters.List>();
            result = Master.GetMastersList(getResponse);
            return View(result);


        }
        public ActionResult CountryList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TableName = "Country";
            ViewBag.Import = "True";
            getResponse.Doctype = ViewBag.TableName;
            List<Masters.List> result = new List<Masters.List>();
            result = Master.GetMastersList(getResponse);
            return View(result);
        }

        public ActionResult StateList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TableName = "State";
            getResponse.Doctype = ViewBag.TableName;
            List<Masters.List> result = new List<Masters.List>();
            result = Master.GetMastersList(getResponse);
            return View(result);


        }

        public ActionResult CityList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TableName = "City";
            ViewBag.Import = "True";
            getResponse.Doctype = ViewBag.TableName;
            getResponse.Doctype = ViewBag.TableName;
            List<Masters.List> result = new List<Masters.List>();
            result = Master.GetMastersList(getResponse);
            return View(result);


        }


        public ActionResult AreaList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TableName = "Area";
            ViewBag.Import = "True";
            getResponse.Doctype = ViewBag.TableName;
            getResponse.Doctype = ViewBag.TableName;
            List<Masters.List> result = new List<Masters.List>();
            result = Master.GetMastersList(getResponse);
            return View(result);


        }

        public ActionResult RegionList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TableName = "Region";
            ViewBag.Import = "True";
            getResponse.Doctype = ViewBag.TableName;

            getResponse.Doctype = ViewBag.TableName;
            List<Masters.List> result = new List<Masters.List>();
            result = Master.GetMastersList(getResponse);
            return View(result);


        }

        public ActionResult _PaymentModeAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MasterID = GetQueryString[2];
            long MasterID = 0;
            long.TryParse(ViewBag.MasterID, out MasterID);
            ViewBag.TableName = "PaymentMode";
            ViewBag.Import = "True";
            getResponse.Doctype = ViewBag.TableName;
            Masters.Add result = new Masters.Add();
            result.TableName = ViewBag.TableName;
            getResponse.ID = MasterID;
            if (MasterID > 0)
            {
                result = Master.GetMasters(getResponse);
            }
            return PartialView(result);
        }

        public ActionResult _TravelModeAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MasterID = GetQueryString[2];
            long MasterID = 0;
            long.TryParse(ViewBag.MasterID, out MasterID);
            ViewBag.TableName = "TravelMode";
            ViewBag.Import = "True";
            getResponse.Doctype = ViewBag.TableName;
            Masters.Add result = new Masters.Add();
            result.TableName = ViewBag.TableName;
            getResponse.ID = MasterID;
            if (MasterID > 0)
            {
                result = Master.GetMasters(getResponse);
            }
            return PartialView(result);
        }

        public ActionResult _CountryAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MasterID = GetQueryString[2];
            long MasterID = 0;
            long.TryParse(ViewBag.MasterID, out MasterID);
            ViewBag.TableName = "Country";
            getResponse.Doctype = ViewBag.TableName;
            Masters.Add result = new Masters.Add();
            result.TableName = ViewBag.TableName;
            getResponse.ID = MasterID;
            if (MasterID > 0)
            {
                result = Master.GetMasters(getResponse);
            }
            return PartialView(result);
        }
        public ActionResult _StateAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MasterID = GetQueryString[2];
            long MasterID = 0;
            long.TryParse(ViewBag.MasterID, out MasterID);
            ViewBag.TableName = "State";
            getResponse.Doctype = ViewBag.TableName;
            getResponse.ID = MasterID;
            Masters.Add result = new Masters.Add();
            result.TableName = ViewBag.TableName;
            if (MasterID > 0)
            {
                result = Master.GetMasters(getResponse);
            }

            GetDropDownResponse getDropDownResponse = new GetDropDownResponse();
            getDropDownResponse.Doctype = "AllRegion";
            ViewBag.ddList = Common_SPU.GetDropDownList(getDropDownResponse);

            return PartialView(result);

        }
        public ActionResult _CityAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MasterID = GetQueryString[2];
            long MasterID = 0;
            long.TryParse(ViewBag.MasterID, out MasterID);
            ViewBag.TableName = "City";
            getResponse.Doctype = ViewBag.TableName;
            Masters.Add result = new Masters.Add();
            result.TableName = ViewBag.TableName;
            getResponse.ID = MasterID;
            if (MasterID > 0)
            {
                result = Master.GetMasters(getResponse);
            }


            GetDropDownResponse getDropDownResponse = new GetDropDownResponse();
            getDropDownResponse.Doctype = "AllState";
            ViewBag.ddList = Common_SPU.GetDropDownList(getDropDownResponse);


            return PartialView(result);

        }



        public ActionResult _AreaAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MasterID = GetQueryString[2];
            long MasterID = 0;
            long.TryParse(ViewBag.MasterID, out MasterID);
            ViewBag.TableName = "Area";
            getResponse.Doctype = ViewBag.TableName;
            getResponse.ID = MasterID;
            Masters.Add result = new Masters.Add();
            result.TableName = ViewBag.TableName;
            if (MasterID > 0)
            {
                result = Master.GetMasters(getResponse);
            }

            GetDropDownResponse getDropDownResponse = new GetDropDownResponse();
            getDropDownResponse.Doctype = "AllCity";
            ViewBag.ddList = Common_SPU.GetDropDownList(getDropDownResponse);
            return PartialView(result);

        }

        public ActionResult _RegionAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MasterID = GetQueryString[2];
            long MasterID = 0;
            long.TryParse(ViewBag.MasterID, out MasterID);
            ViewBag.TableName = "Region";
            getResponse.Doctype = ViewBag.TableName;
            getResponse.ID = MasterID;
            Masters.Add result = new Masters.Add();
            result.TableName = ViewBag.TableName;
            if (MasterID > 0)
            {
                result = Master.GetMasters(getResponse);
            }
            GetDropDownResponse getDropDownResponse = new GetDropDownResponse();
            getDropDownResponse.Doctype = "AllCountry";
            ViewBag.ddList = Common_SPU.GetDropDownList(getDropDownResponse);
            return PartialView(result);

        }

        [HttpPost]
        public ActionResult SaveMasterAll(string src, Masters.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MasterID = GetQueryString[2];
            long MasterID = 0;
            long.TryParse(ViewBag.MasterID, out MasterID);
            Result.SuccessMessage = "Masters Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.MasterID = MasterID;
                Result = Master.fnSetMasters(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }


        public ActionResult BranchList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Branch.List> result = new List<Branch.List>();
            result = Master.GetBranchList(getResponse);
            return View(result);
        }
        public ActionResult _BranchAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.BranchID = GetQueryString[2];
            long BranchID = 0;
            long.TryParse(ViewBag.BranchID, out BranchID);
            getResponse.ID = BranchID;
            Branch.Add result = new Branch.Add();
            result = Master.GetBranch(getResponse);
            return PartialView(result);
        }

        [HttpPost]
        public ActionResult _BranchAdd(string src, Branch.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.BranchID = GetQueryString[2];
            long BranchID = 0;
            long.TryParse(ViewBag.BranchID, out BranchID);
            Result.SuccessMessage = "Branch Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.BranchID = BranchID;
                Result = Master.fnSetBranch(Modal);

                if (Result.Status)
                {
                    // Delete Existing Record
                    getResponse.Doctype = "Branch_Mapping";
                    getResponse.ID = Result.ID;
                    Common_SPU.fnDelRecord(getResponse);
                    if (!string.IsNullOrEmpty(Modal.CityIDs))
                    {
                        if (Modal.CityIDs.Contains(','))
                        {
                            foreach (var item in Modal.CityIDs.Split(','))
                            {
                                Master.fnSetBranch_Mapping(Result.ID, item, getResponse);
                            }
                        }
                        else
                        {
                            Master.fnSetBranch_Mapping(Result.ID, Modal.CityIDs, getResponse);
                        }
                    }
                }
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Masters/BranchList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Masters/BranchList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }





        public ActionResult DepartmentList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Department.List> result = new List<Department.List>();
            result = Master.GetDepartmentList(getResponse);
            return View(result);
        }
        public ActionResult _DepartmentAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.DeptID = GetQueryString[2];
            long DeptID = 0;
            long.TryParse(ViewBag.DeptID, out DeptID);
            getResponse.ID = DeptID;
            Department.Add result = new Department.Add();
            if (DeptID > 0)
            {
                result = Master.GetDepartment(getResponse);
            }
            return PartialView(result);
        }

        [HttpPost]
        public ActionResult _DepartmentAdd(string src, Department.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.DeptID = GetQueryString[2];
            long DeptID = 0;
            long.TryParse(ViewBag.DeptID, out DeptID);
            Result.SuccessMessage = "Department Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.DeptID = DeptID;
                Result = Master.fnSetDepartment(Modal);

            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Masters/DepartmentList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Masters/DepartmentList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }



        public ActionResult DesignationList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            List<Designation.List> result = new List<Designation.List>();
            result = Master.GetDesignationList(getResponse);
            return View(result);
        }
        public ActionResult _DesignationAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.DesignID = GetQueryString[2];
            long DesignID = 0;
            long.TryParse(ViewBag.DesignID, out DesignID);
            getResponse.ID = DesignID;
            Designation.Add result = new Designation.Add();
            if (DesignID > 0)
            {
                result = Master.GetDesignation(getResponse);
            }
            return PartialView(result);
        }

        [HttpPost]
        public ActionResult _DesignationAdd(string src, Designation.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.DesignID = GetQueryString[2];
            long DesignID = 0;
            long.TryParse(ViewBag.DesignID, out DesignID);
            Result.SuccessMessage = "Designation Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.DesignID = DesignID;
                Result = Master.fnSetDesignation(Modal);
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Masters/DesignationList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Masters/DesignationList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }



        public ActionResult BrandList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            List<Brand.List> result = new List<Brand.List>();
            result = Master.GetBrandList(getResponse);
            return View(result);
        }
        public ActionResult _BrandAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.BrandID = GetQueryString[2];
            long BrandID = 0;
            long.TryParse(ViewBag.BrandID, out BrandID);
            getResponse.ID = BrandID;
            Brand.Add result = new Brand.Add();
            if (BrandID > 0)
            {
                result = Master.GetBrand(getResponse);
            }
            return PartialView(result);
        }

        [HttpPost]
        public ActionResult _BrandAdd(string src, Brand.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.BrandID = GetQueryString[2];
            long BrandID = 0;
            long.TryParse(ViewBag.BrandID, out BrandID);
            Result.SuccessMessage = "Brand Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.BrandID = BrandID;
                Result = Master.fnSetBrand(Modal);
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Masters/BrandList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Masters/BrandList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }


        public ActionResult EMPImport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<EMPImport.List> Result = new List<EMPImport.List>();
            Result = Master.GetEMPImportList(getResponse);
            ViewBag.ListCount = Result.Count;
            return View(Result);
        }

        [HttpPost]
        public ActionResult EMPImport(FormCollection Form, string Command, string src)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Command == "ImportData")
            {
                string SheetName = Form.Get("txtSheet");
                if (Request.Files == null)
                {
                    Result.SuccessMessage = "please select file";
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
                foreach (string upload in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[upload];
                    Result = Master.UploadEMPImportDataExcelFile(file, SheetName, getResponse);
                }
            }
            else if (Command == "ClearData")
            {
                Result = Master.ClearEMPImportTemp(getResponse);
            }
            else if (Command == "UploadData")
            {
                Result = Master.SetEMPFromEMPImport(getResponse);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EmployeeList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval result = new Tab.Approval();
            return View(result);

        }

        public ActionResult _EmployeeList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approved = Modal.Approved;
            List<Employee.List> Result = new List<Employee.List>();
            Result = Master.GetEMPList(Modal);
            return PartialView(Result);

        }
        public ActionResult EmployeeAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            long EMPID = 0;
            long.TryParse(ViewBag.EMPID, out EMPID);
            getResponse.ID = EMPID;
            Employee.Add result = new Employee.Add();
            result = Master.GetEMP(getResponse);
            return View(result);

        }

        [HttpPost]
        public ActionResult EmployeeAdd(string src, Employee.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            long EMPID = 0;
            long.TryParse(ViewBag.EMPID, out EMPID);

            string PhysicalPath = ClsApplicationSetting.GetPhysicalPath("");
            Result.SuccessMessage = "Employee Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.EMPID = EMPID;

                if (Modal.UserDetails != null)
                {
                    Modal.UserID = Modal.UserDetails.ID;
                    Modal.UserDetails.LoginID = LoginID;
                    Modal.UserDetails.IPAddress = IPAddress;
                    Modal.UserDetails.Phone = Modal.Phone;
                    Modal.UserDetails.email = Modal.EmailID;
                    var User = Tools.fnSetUsers(Modal.UserDetails);
                    if (User.Status)
                    {
                        Modal.UserID = User.ID;
                        Result.Status = true;
                    }
                    else
                    {
                        Result = User;
                    }
                }
                if (Result.Status)
                {
                    if (Modal.Upload != null)
                    {
                        UploadAttachment attachModal = new UploadAttachment();
                        attachModal.File = Modal.Upload;
                        attachModal.LoginID = LoginID;
                        attachModal.IPAddress = IPAddress;
                        attachModal.AttachID = Modal.AttachID;
                        attachModal.Doctype = "";
                        var Attach = ClsApplicationSetting.UploadAttachment(attachModal);
                        Modal.AttachID = Attach.ID;
                        if (!Attach.Status)
                        {
                            Result.SuccessMessage = Attach.SuccessMessage;
                            return Json(Result, JsonRequestBehavior.AllowGet);
                        }
                    }
                    Result = Master.fnSetEMP(Modal);
                    if (Result.Status)
                    {
                        if (Modal.BankDetails != null)
                        {
                            Modal.BankDetails.LoginID = LoginID;
                            Modal.BankDetails.IPAddress = IPAddress;
                            Modal.BankDetails.EMPID = Result.ID;
                            Modal.BankDetails.Doctype = "Saving";
                            Common_SPU.fnSetBank(Modal.BankDetails);
                        }
                        if (Modal.AddressDetails != null)
                        {
                            Modal.AddressDetails.LoginID = LoginID;
                            Modal.AddressDetails.IPAddress = IPAddress;
                            Modal.AddressDetails.TableID = Result.ID;
                            Modal.AddressDetails.TableName = "EMP";
                            Modal.AddressDetails.Doctype = "Local";
                            Common_SPU.fnSetAddress(Modal.AddressDetails);
                        }
                    }
                }
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Masters/EmployeeList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Masters/EmployeeList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult UpdateEMPDOL(string src, FormCollection form, string Command)
        {
            PostResponse Result = new PostResponse();
            Result.SuccessMessage = "Action not taken";
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            string EMPID = form["hdnEMPID"].ToString();
            string DOLDate = form["DOLDate"].ToString();
            string Reason= form["Reason"].ToString();
            DateTime dt; long _EMPID = 0;
            DateTime.TryParse(DOLDate, out dt);
            long.TryParse(EMPID, out _EMPID);
            Employee.UpdateDOL Modal = new Employee.UpdateDOL();

            if (string.IsNullOrEmpty( DOLDate))
            {
                Result.SuccessMessage = "DOL can't be blank";
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            if (_EMPID > 0)
            {
                
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.EMPID = _EMPID;
                Modal.DOL = dt.ToString("dd-MMM-yyyy");
                Modal.Reason = Reason;
                Result = Master.fnSetEMP_DOL(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }



        public ActionResult DealerList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Import = "True";
            List<Dealer.List> Result = new List<Dealer.List>();
            Result = Master.GetDealerList(getResponse);
            return View(Result);

        }
        public ActionResult DealerImport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<DealerImport.List> Result = new List<DealerImport.List>();
            Result = Master.GetDealerImportList(getResponse);
            ViewBag.ListCount = Result.Count;
            return View(Result);
        }

        [HttpPost]
        public ActionResult DealerImport(FormCollection Form, string Command, string src)
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
                    Result = Master.UploadDealerImportDataExcelFile(file, SheetName, getResponse);
                }
            }
            else if (Command == "ClearData")
            {
                Result = Master.ClearDealerImportTemp(getResponse);
            }
            else if (Command == "UploadData")
            {
                Result = Master.UploadDealerImportDetailList(getResponse);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DealerAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.DealerID = GetQueryString[2];
            long DealerID = 0;
            long.TryParse(ViewBag.DealerID, out DealerID);
            getResponse.ID = DealerID;

            Dealer.Add Result = new Dealer.Add();
            Result = Master.GetDealer(getResponse);
            return View(Result);

        }

        [HttpPost]
        public ActionResult DealerAdd(string src, Dealer.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.DealerID = GetQueryString[2];
            long DealerID = 0;
            long.TryParse(ViewBag.DealerID, out DealerID);
            Result.SuccessMessage = "Dealer Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.DealerID = DealerID;
                Result = Master.fnSetDealer(Modal);

            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Masters/DealerList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Masters/DealerList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }




        public ActionResult AttendenceStatusList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<AttendenceStatus.List> result = new List<AttendenceStatus.List>();
            result = Master.GetAttendenceStatusList(getResponse);
            return View(result);
        }
        public ActionResult _AttendenceStatusAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);
            getResponse.ID = ID;
            AttendenceStatus.Add result = new AttendenceStatus.Add();
            result = Master.GetAttendenceStatus(getResponse);
            return PartialView(result);
        }

        [HttpPost]
        public ActionResult _AttendenceStatusAdd(string src, AttendenceStatus.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);
            Result.SuccessMessage = "Status Can't Update";
            if (!Modal.UseFor.Contains("Leave") && (Modal.MonthlyAccrued ?? 0) > 0)
            {
                Result.SuccessMessage = "Monthly Accrued must be zero, only availiable for leave";
                ModelState.AddModelError("UseFor", Result.SuccessMessage);

            }
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.ID = ID;
                Result = Master.fnSetAttendenceStatus(Modal);
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Masters/AttendenceStatusList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Masters/AttendenceStatusList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }



        public ActionResult NSMList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<UserHierarchy.List> result = new List<UserHierarchy.List>();
            getResponse.Doctype = "NSM";
            result = Master.GetUserHierarchyList(getResponse);
            return View(result);
        }

        public ActionResult _NSMAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.HierarchyID = GetQueryString[2];
            long HierarchyID = 0;
            long.TryParse(ViewBag.HierarchyID, out HierarchyID);
            getResponse.Doctype = "NSM";
            getResponse.ID = HierarchyID;
            ViewBag.Doctype = getResponse.Doctype;
            UserHierarchy.Add result = new UserHierarchy.Add();
            result = Master.GetUserHierarchy(getResponse);
            return PartialView(result);

        }

        public ActionResult RSMList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<UserHierarchy.List> result = new List<UserHierarchy.List>();
            getResponse.Doctype = "RSM";
            ViewBag.Import = "True";
            result = Master.GetUserHierarchyList(getResponse);
            return View(result);
        }

        public ActionResult _RSMAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.HierarchyID = GetQueryString[2];
            long HierarchyID = 0;
            long.TryParse(ViewBag.HierarchyID, out HierarchyID);
            getResponse.Doctype = "RSM";
            getResponse.ID = HierarchyID;
            ViewBag.Doctype = getResponse.Doctype;
            UserHierarchy.Add result = new UserHierarchy.Add();
            result = Master.GetUserHierarchy(getResponse);
            return PartialView(result);

        }


        public ActionResult RMMList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<UserHierarchy.List> result = new List<UserHierarchy.List>();
            getResponse.Doctype = "RMM";
            ViewBag.Import = "True";
            result = Master.GetUserHierarchyList(getResponse);
            return View(result);
        }

        public ActionResult _RMMAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.HierarchyID = GetQueryString[2];
            long HierarchyID = 0;
            long.TryParse(ViewBag.HierarchyID, out HierarchyID);
            getResponse.Doctype = "RMM";
            getResponse.ID = HierarchyID;
            ViewBag.Doctype = getResponse.Doctype;
            UserHierarchy.Add result = new UserHierarchy.Add();
            result = Master.GetUserHierarchy(getResponse);
            return PartialView(result);

        }

        public ActionResult BSMList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<UserHierarchy.List> result = new List<UserHierarchy.List>();
            getResponse.Doctype = "BSM";
            ViewBag.Import = "True";
            result = Master.GetUserHierarchyList(getResponse);
            return View(result);
        }

        public ActionResult _BSMAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.HierarchyID = GetQueryString[2];
            long HierarchyID = 0;
            long.TryParse(ViewBag.HierarchyID, out HierarchyID);
            getResponse.Doctype = "BSM";
            getResponse.ID = HierarchyID;
            ViewBag.Doctype = getResponse.Doctype;
            UserHierarchy.Add result = new UserHierarchy.Add();
            result = Master.GetUserHierarchy(getResponse);
            return PartialView(result);

        }


        public ActionResult BMMList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<UserHierarchy.List> result = new List<UserHierarchy.List>();
            getResponse.Doctype = "BMM";
            ViewBag.Import = "True";
            result = Master.GetUserHierarchyList(getResponse);
            return View(result);
        }

        public ActionResult _BMMAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.HierarchyID = GetQueryString[2];
            long HierarchyID = 0;
            long.TryParse(ViewBag.HierarchyID, out HierarchyID);
            getResponse.Doctype = "BMM";
            getResponse.ID = HierarchyID;
            ViewBag.Doctype = getResponse.Doctype;
            UserHierarchy.Add result = new UserHierarchy.Add();
            result = Master.GetUserHierarchy(getResponse);
            return PartialView(result);

        }
        public ActionResult ASMList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<UserHierarchy.List> result = new List<UserHierarchy.List>();
            getResponse.Doctype = "ASM";
            ViewBag.Import = "True";
            result = Master.GetUserHierarchyList(getResponse);
            return View(result);
        }

        public ActionResult _ASMAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.HierarchyID = GetQueryString[2];
            long HierarchyID = 0;
            long.TryParse(ViewBag.HierarchyID, out HierarchyID);
            getResponse.Doctype = "ASM";
            getResponse.ID = HierarchyID;
            ViewBag.Doctype = getResponse.Doctype;
            UserHierarchy.Add result = new UserHierarchy.Add();
            result = Master.GetUserHierarchy(getResponse);
            return PartialView(result);

        }

        public ActionResult TLList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<UserHierarchy.List> result = new List<UserHierarchy.List>();
            getResponse.Doctype = "TL";
            ViewBag.Import = "True";
            result = Master.GetUserHierarchyList(getResponse);
            return View(result);
        }

        public ActionResult _TLAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.HierarchyID = GetQueryString[2];
            long HierarchyID = 0;
            long.TryParse(ViewBag.HierarchyID, out HierarchyID);
            getResponse.Doctype = "TL";
            getResponse.ID = HierarchyID;
            ViewBag.Doctype = getResponse.Doctype;
            UserHierarchy.Add result = new UserHierarchy.Add();
            result = Master.GetUserHierarchy(getResponse);
            return PartialView(result);

        }
        public ActionResult SSRList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<UserHierarchy.List> result = new List<UserHierarchy.List>();
            getResponse.Doctype = "SSR";
            ViewBag.Import = "True";
            result = Master.GetUserHierarchyList(getResponse);
            return View(result);
        }

        public ActionResult _SSRAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.HierarchyID = GetQueryString[2];
            long HierarchyID = 0;
            long.TryParse(ViewBag.HierarchyID, out HierarchyID);
            getResponse.Doctype = "SSR";
            getResponse.ID = HierarchyID;
            ViewBag.Doctype = getResponse.Doctype;
            UserHierarchy.Add result = new UserHierarchy.Add();
            result = Master.GetUserHierarchy(getResponse);
            return PartialView(result);

        }

        [HttpPost]
        public ActionResult SaveUserHierarchy(string src, UserHierarchy.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.HierarchyID = GetQueryString[2];
            ViewBag.Doctype = GetQueryString[3];
            long HierarchyID = 0;
            long.TryParse(ViewBag.HierarchyID, out HierarchyID);
            Result.SuccessMessage = "Hierarchy Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.HierarchyID = HierarchyID;
                Modal.UserType = ViewBag.Doctype;

                Modal.IsActive = true;
                Modal.TableIDs = Modal.TableIDs.TrimEnd(',');
                Result = Master.fnSetUserHierarchy(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }


        public ActionResult MastersImport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<MastersImport.List> Result = new List<MastersImport.List>();
            Result = Master.GetMastersImportList(getResponse);
            ViewBag.ListCount = Result.Count;
            return View(Result);
        }

        [HttpPost]
        public ActionResult MastersImport(FormCollection Form, string Command, string src)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Command == "ImportData")
            {
                string SheetName = Form.Get("txtSheet");
                if (Request.Files == null)
                {
                    Result.SuccessMessage = "please select file";
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
                foreach (string upload in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[upload];
                    Result = Master.UploadMastersImportDataExcelFile(file, SheetName, getResponse);
                }
            }
            else if (Command == "ClearData")
            {
                Result = Master.ClearMastersImportTemp(getResponse);
            }
            else if (Command == "UploadData")
            {
                Result = Master.SetMastersFromMastersImport(getResponse);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult EMPTalentPoolList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Import = "True";
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM-dd");
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _EMPTalentPoolList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            return PartialView(Master.GetEMPTalentPoolList(Modal));
        }

        public ActionResult AddEMPTalentPool(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TPID = GetQueryString[2];
            long TPID = 0;
            long.TryParse(ViewBag.TPID, out TPID);
            EMPTalentPool.Add result = new EMPTalentPool.Add();
            getResponse.ID = TPID;
            result = Master.GetEMPTalentPool(getResponse);
            return View(result);
        }
        [HttpPost]
        public ActionResult AddEMPTalentPool(string src, EMPTalentPool.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TPID = GetQueryString[2];
            long TPID = 0;
            long.TryParse(ViewBag.TPID, out TPID);
            Result.SuccessMessage = "Can't Update";
            string PhysicalPath = ClsApplicationSetting.GetPhysicalPath("");
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.TPID = TPID;
                Modal.IsActive = 1;
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

                Result = Master.fnSetEMP_TalentPool(Modal);
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Masters/EMPTalentPoolList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Masters/EMPTalentPoolList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult EMPTalentPoolImport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<EMPTalentPoolImport.List> Result = new List<EMPTalentPoolImport.List>();
            Result = Master.GetEMPTalentPoolImportList(getResponse);
            ViewBag.ListCount = Result.Count;
            return View(Result);
        }

        [HttpPost]
        public ActionResult EMPTalentPoolImport(FormCollection Form, string Command, string src)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Command == "ImportData")
            {
                string SheetName = Form.Get("txtSheet");
                if (Request.Files == null)
                {
                    Result.SuccessMessage = "please select file";
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
                foreach (string upload in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[upload];
                    Result = Master.UploadEMP_TalentPoolImportDataExcelFile(file, SheetName, getResponse);
                }
            }
            else if (Command == "ClearData")
            {
                Result = Master.ClearEMP_TalentPoolImportTemp(getResponse);
            }
            else if (Command == "UploadData")
            {
                Result = Master.SetEMP_TalentPoolFromImportTable(getResponse);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChallanDocumentsList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Date Modal = new Tab.Date();
            return View(Modal);
        }
        public ActionResult _ChallanDocumentsList(string src, Tab.Date Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<ChallanDocuments.List> Result = new List<ChallanDocuments.List>();
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            Result = Master.GetChallanDocumentsList(Modal);
            return PartialView(Result);
        }

        public ActionResult _ChallanDocumentsAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ChallanID = GetQueryString[2];
            long ChallanID = 0;
            long.TryParse(ViewBag.ChallanID, out ChallanID);
            ChallanDocuments.Add result = new ChallanDocuments.Add();
            getResponse.ID = ChallanID;
            result = Master.GetChallanDocuments(getResponse);
            return PartialView(result);
        }
        [HttpPost]
        public ActionResult _ChallanDocumentsAdd(ChallanDocuments.Add Modal, string Command, string src)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ChallanID = GetQueryString[2];
            long ChallanID = 0;
            long.TryParse(ViewBag.ChallanID, out ChallanID);
            string PhysicalPath = ClsApplicationSetting.GetPhysicalPath("");
            Result.SuccessMessage = "Can't Update";
            if (Modal.Upload == null && Modal.AttachmentID == 0)
            {
                Result.SuccessMessage = "please upload Attachment also";
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                if (Modal.Upload != null)
                {
                    UploadAttachment attachModal = new UploadAttachment();
                    attachModal.File = Modal.Upload;
                    attachModal.LoginID = LoginID;
                    attachModal.IPAddress = IPAddress;
                    attachModal.AttachID = Modal.AttachmentID;
                    attachModal.Doctype = "";
                    var Attach = ClsApplicationSetting.UploadAttachment(attachModal);
                    Modal.AttachmentID = Attach.ID;
                    if (!Attach.Status)
                    {
                        Result.SuccessMessage = Attach.SuccessMessage;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }
                Result = Master.fnSetChallanDocuments(Modal);
            }

            return Json(Result, JsonRequestBehavior.AllowGet);
        }




        public ActionResult UserHierarchyImport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<UserHierarchyImport.List> Result = new List<UserHierarchyImport.List>();
            Result = Master.GetUserHierarchyImportList(getResponse);
            ViewBag.ListCount = Result.Count;
            return View(Result);
        }

        [HttpPost]
        public ActionResult UserHierarchyImport(FormCollection Form, string Command, string src)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Command == "ImportData")
            {
                string SheetName = Form.Get("txtSheet");
                if (Request.Files == null)
                {
                    Result.SuccessMessage = "please select file";
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
                foreach (string upload in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[upload];
                    Result = Master.UploadUserHierarchyImportDataExcelFile(file, SheetName, getResponse);
                }
            }
            else if (Command == "ClearData")
            {
                Result = Master.ClearUserHierarchyImportTemp(getResponse);
            }
            else if (Command == "UploadData")
            {
                Result = Master.SetUserHierarchyFromUserHierarchyImport(getResponse);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult InhouseList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<UserHierarchy.List> result = new List<UserHierarchy.List>();
            getResponse.Doctype = "Inhouse";
            ViewBag.Import = "True";
            result = Master.GetUserHierarchyList(getResponse);
            return View(result);
        }

        public ActionResult _InhouseAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.HierarchyID = GetQueryString[2];
            long HierarchyID = 0;
            long.TryParse(ViewBag.HierarchyID, out HierarchyID);
            getResponse.Doctype = "Inhouse";
            getResponse.ID = HierarchyID;
            ViewBag.Doctype = getResponse.Doctype;
            UserHierarchy.Add result = new UserHierarchy.Add();
            result = Master.GetUserHierarchy(getResponse);
            return PartialView(result);

        }

        public ActionResult LeaveBalanceList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            GetDropDownResponse respdrop = new GetDropDownResponse();
            respdrop.Doctype = "FinancialYearList";
            respdrop.LoginID = LoginID;
            respdrop.IPAddress = IPAddress;
            Modal.List = Common_SPU.GetDropDownList(respdrop);

            return View(Modal);

        }
        public ActionResult _LeaveBalanceList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<LeaveBalance.List> result = new List<LeaveBalance.List>();
            Modal.LoginID = LoginID;
            result = Master.GetLeaveBalanceList(Modal);
            return PartialView(result);
        }
        public ActionResult _LeaveBalanceTranList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.LBID = GetQueryString[2];
            long LBID = 0;
            long.TryParse(ViewBag.LBID, out LBID);
            getResponse.ID = LBID;
            List<LeaveBalance.TranList> result = new List<LeaveBalance.TranList>();
            result = Master.GetLeaveBalanceTran(getResponse);
            return PartialView(result);
        }

        public ActionResult DealerCategoryList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            return View(Master.GetDealerCategoryList(getResponse));
        }

        public ActionResult _DealerCategoryAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.DealerCategoryID = GetQueryString[2];
            long DealerCategoryID = 0;
            long.TryParse(ViewBag.DealerCategoryID, out DealerCategoryID);
            DealerCategory.Add result = new DealerCategory.Add();
            getResponse.ID = DealerCategoryID;
            if (DealerCategoryID > 0)
            {
                result = Master.GetDealerCategory(getResponse);
            }
            return PartialView(result);
        }

        [HttpPost]
        public ActionResult _DealerCategoryAdd(string src, DealerCategory.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.DealerCategoryID = GetQueryString[2];
            long DealerCategoryID = 0;
            long.TryParse(ViewBag.DealerCategoryID, out DealerCategoryID);
            Result.SuccessMessage = "Category Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.DealerCategoryID = DealerCategoryID;
                Result = Master.fnSetDealerCategory(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }



        public ActionResult DealerTypeList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            return View(Master.GetDealerTypeList(getResponse));
        }

        public ActionResult _DealerTypeAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.DealerTypeID = GetQueryString[2];
            long DealerTypeID = 0;
            long.TryParse(ViewBag.DealerTypeID, out DealerTypeID);
            DealerType.Add result = new DealerType.Add();
            getResponse.ID = DealerTypeID;
            if (DealerTypeID > 0)
            {
                result = Master.GetDealerType(getResponse);
            }
            return PartialView(result);
        }

        [HttpPost]
        public ActionResult _DealerTypeAdd(string src, DealerType.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.DealerTypeID = GetQueryString[2];
            long DealerTypeID = 0;
            long.TryParse(ViewBag.DealerTypeID, out DealerTypeID);
            Result.SuccessMessage = "Type Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.DealerTypeID = DealerTypeID;
                Result = Master.fnSetDealerType(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }





        public ActionResult MasterCatalogueList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            return View(Modal);
        }
        public ActionResult _MasterCatalogueList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<MasterCatalogue.List> Result = new List<MasterCatalogue.List>();
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            Result = Master.GetMasterCatalogueList(Modal);
            return PartialView(Result);
        }

        public ActionResult _MasterCatalogueAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CatID = GetQueryString[2];
            long CatID = 0;
            long.TryParse(ViewBag.CatID, out CatID);
            MasterCatalogue.Add result = new MasterCatalogue.Add();
            if (CatID > 0)
            {
                getResponse.ID = CatID;
                result = Master.GetMasterCatalogue(getResponse);
            }
            return PartialView(result);
        }
        [HttpPost]
        public ActionResult _MasterCatalogueAdd(MasterCatalogue.Add Modal, string Command, string src)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CatID = GetQueryString[2];
            long CatID = 0;
            long.TryParse(ViewBag.CatID, out CatID);
            string PhysicalPath = ClsApplicationSetting.GetPhysicalPath("");
            Result.SuccessMessage = "Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                if (Modal.Upload != null)
                {
                    UploadAttachment attachModal = new UploadAttachment();
                    attachModal.File = Modal.Upload;
                    attachModal.LoginID = LoginID;
                    attachModal.IPAddress = IPAddress;
                    attachModal.AttachID = Modal.AttachmentID;
                    attachModal.Doctype = "";
                    var Attach = ClsApplicationSetting.UploadAttachment(attachModal);
                    Modal.AttachmentID = Attach.ID;
                    if (!Attach.Status)
                    {
                        Result.SuccessMessage = Attach.SuccessMessage;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }
                Result = Master.fnSetMasterCatalogue(Modal);
            }

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

    }
}