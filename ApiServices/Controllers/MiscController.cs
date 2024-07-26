using DataModal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using DataModal;
using DataModal.ModelsMasterHelper;
using DataModal.ModelsMaster;
using DataModal.DataModal.ModelsMaster;
using DataModal.CommanClass;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace ApiServices.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    public class MiscController : ApiController
    {
        IAccountsHelper Account;
        IMasterHelper Master;
        IProductHelper Product;
        ITransactionHelper transaction;
        IReportHelper report;
        public MiscController()
        {
            Account = new AccountsModel();
            Master=new MasterModal();
            Product = new ProductModal();
            transaction = new TransactionModal();
            report = new ReportModal();
        }
        [HttpGet]
        public JsonResult<string> Test()
        {
            return Json("dasdasd");
        }

        [HttpPost]
        public IHttpActionResult GetUserDetails(AdminUser.Login modal)
        {
            AdminUser.Details Results = new AdminUser.Details();
            Results = Account.GetLogin(modal);
            return Ok(Results);
        }
        [HttpPost]
        public IHttpActionResult GetMasterList(GetResponse Modal)
        {
            List<Masters.List> result = new List<Masters.List>();
            result = Master.GetMastersList(Modal);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult GetMaster(GetResponse Modal)
        {
            Masters.Add result = new Masters.Add();
            result = Master.GetMasters(Modal);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult SetMaster(Masters.Add Modal)
        {
            PostResponse Result = new PostResponse();
            Result = Master.fnSetMasters(Modal);
            return Ok(Result);
        }


        [HttpPost]
        public IHttpActionResult GetBranchList(GetResponse Modal)
        {
            List<Branch.List> result = new List<Branch.List>();
            result = Master.GetBranchList(Modal);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult GetBranch(GetResponse Modal)
        {
            Branch.Add result = new Branch.Add();
            result = Master.GetBranch(Modal);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult SetBranch(Branch.Add Modal)
        {
            PostResponse Result = new PostResponse();
            Result = Master.fnSetBranch(Modal);
            return Ok(Result);
        }





        [HttpPost]
        public IHttpActionResult GetDepartmentList(GetResponse Modal)
        {
            List<Department.List> result = new List<Department.List>();
            result = Master.GetDepartmentList(Modal);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult GetDepartment(GetResponse Modal)
        {
            Department.Add result = new Department.Add();
            result = Master.GetDepartment(Modal);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult SetDepartment(Department.Add Modal)
        {
            PostResponse Result = new PostResponse();
            Result = Master.fnSetDepartment(Modal);
            return Ok(Result);
        }



        [HttpPost]
        public IHttpActionResult GetDesignationList(GetResponse Modal)
        {
            List<Designation.List> result = new List<Designation.List>();
            result = Master.GetDesignationList(Modal);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult GetDesignation(GetResponse Modal)
        {
            Designation.Add result = new Designation.Add();
            result = Master.GetDesignation(Modal);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult SetDesignation(Designation.Add Modal)
        {
            PostResponse Result = new PostResponse();
            Result = Master.fnSetDesignation(Modal);
            return Ok(Result);
        }



        //[HttpPost]
        //[Authorize]
        //public IHttpActionResult GetBrandList(GetResponse Modal)
        //{
        //    ResponseResult<List<Brand.List>> result = new ResponseResult<List<Brand.List>>();
        //    result = Master.GetBrandList(Modal);
        //    return Ok(result);
        //}

        [HttpPost]
        [Authorize]
        public IHttpActionResult GetBrand(GetResponse Modal)
        {
            Brand.Add result = new Brand.Add();
            result = Master.GetBrand(Modal);
            return Ok(result);
        }


        [HttpPost]
        public IHttpActionResult SetBrand(Brand.Add Modal)
        {
            PostResponse Result = new PostResponse();
            Result = Master.fnSetBrand(Modal);
            return Ok(Result);
        }


        [HttpPost]
        public IHttpActionResult GetProductTypeList(GetResponse Modal)
        {
            List<ProductType.List> result = new List<ProductType.List>();
            result = Product.GetProductTypeList(Modal);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult GetProductType(GetResponse Modal)
        {
            ProductType.Add result = new ProductType.Add();
            result = Product.GetProductType(Modal);
            return Ok(result);
        }


        [HttpPost]
        public IHttpActionResult SetProductType(ProductType.Add Modal)
        {
            PostResponse Result = new PostResponse();
            Result = Product.fnSetProductType(Modal);
            return Ok(Result);
        }


        [HttpPost]
        public IHttpActionResult GetProductList(GetResponse Modal)
        {
            List<Product.List> result = new List<Product.List>();
            result = Product.GetProductList(Modal);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult GetProduct(GetResponse Modal)
        {
            Product.Add result = new Product.Add();
            result = Product.GetProduct(Modal);
            return Ok(result);
        }  
        
        [HttpPost]
        public IHttpActionResult SetProduct(Product.Add Modal)
        {
            PostResponse Result = new PostResponse();
            Result = Product.fnSetProduct(Modal);
            return Ok(Result);
        }


        [HttpPost]
        public IHttpActionResult GetProductTranList(GetResponse Modal)
        {
            List<ProductTran.List> result = new List<ProductTran.List>();
            result = Product.GetProduct_TranList(Modal);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult GetProductTran(GetResponse Modal)
        {
            ProductTran.Add result = new ProductTran.Add();
            result = Product.GetProduct_Tran(Modal);
            return Ok(result);
        }
        [HttpPost]
        public IHttpActionResult SetProductTran(ProductTran.Add Modal)
        {
            PostResponse Result = new PostResponse();
            Result = Product.fnSetProductTran(Modal);
            return Ok(Result);
        }

        [HttpPost]
        public IHttpActionResult GetDropDownList(GetDropDownResponse Modal)
        {
            List<DropDownlist> result = new List<DropDownlist>();
            result = Common_SPU.GetDropDownList(Modal);
            return Ok(result);
        }
        [HttpPost]
        public IHttpActionResult SetMasterAttachment(FileResponse Modal)
        {
            PostResponse Result = new PostResponse();
            Result = Common_SPU.fnSetMasterAttachment(Modal);
            return Ok(Result);
        }

        [HttpPost]
        public IHttpActionResult SetMasterAttachment(GetResponse Modal)
        {
            PostResponse Result = new PostResponse();
            Result = Common_SPU.fnDelRecord(Modal);
            return Ok(Result);
        }
        [HttpPost]
        public IHttpActionResult UpdateColumnResponse(GetUpdateColumnResponse Modal)
        {
            PostResponse Result = new PostResponse();
            Result = Common_SPU.fnGetUpdateColumnResponse(Modal);
            return Ok(Result);
        }

        [HttpPost]
        public IHttpActionResult GetEMPList(GetResponse Modal)
        {
            List<Employee.List> Result = new List<Employee.List>();
            Result = Master.GetEMPList(Modal);
            return Ok(Result);
        }
        [HttpPost]
        public IHttpActionResult GetEMP(GetResponse Modal)
        {
            Employee.Add Result = new Employee.Add();
            Result = Master.GetEMP(Modal);
            return Ok(Result);
        }


        [HttpPost]
        public IHttpActionResult GetDealerList(GetResponse Modal)
        {
            List<Dealer.List> Result = new List<Dealer.List>();
            Result = Master.GetDealerList(Modal);
            return Ok(Result);
        }
        [HttpPost]
        public IHttpActionResult GetDealer(GetResponse Modal)
        {
            Dealer.Add Result = new Dealer.Add();
            Result = Master.GetDealer(Modal);
            return Ok(Result);
        }

        [HttpPost]
        public IHttpActionResult GetItemList(GetResponse Modal)
        {
            List<Items.List> result = new List<Items.List>();
            result = Product.GetItemList(Modal);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult GetItem(GetResponse Modal)
        {
            Items.Add result = new Items.Add();
            result = Product.GetItem(Modal);
            return Ok(result);
        }


        [HttpPost]
        public IHttpActionResult SetItems(Items.Add Modal)
        {
            PostResponse Result = new PostResponse();
            Result = Product.fnSetItems(Modal);
            return Ok(Result);
        }


        [HttpPost]
        public IHttpActionResult GetSaleEntryList(GetResponse Modal)
        {
            List<SaleEntry.List> Result = new List<SaleEntry.List>();
            Result = transaction.GetSaleEntryList(Modal);
            return Ok(Result);
        }

        [HttpPost]
        public IHttpActionResult GetSaleEntry(GetResponse Modal)
        {
            SaleEntry.Add Result = new SaleEntry.Add();
            Result = transaction.GetSaleEntry(Modal);
            return Ok(Result);
        }


        [HttpPost]
        public IHttpActionResult SetSaleEntry(SaleEntry.Add Modal)
        {
            PostResponse Result = new PostResponse();
            Result = transaction.fnSetSaleEntry(Modal);
            return Ok(Result);
        }

        [HttpPost]
        public IHttpActionResult SetMarkAttendence(MarkAttendence Modal)
        {
            PostResponse Result = new PostResponse();
            Result = Common_SPU.fnSetAttendenceLog(Modal);
            return Ok(Result);
        }

        [HttpPost]
        public IHttpActionResult GetSalesEntryReport(GetResponse Modal)
        {
            List<SaleEntry.List> result = new List<SaleEntry.List>();
            Modal.Doctype = "Report";
            result = transaction.GetSaleEntryList(Modal);
            return Ok(result);
        }


        [HttpPost]
        public IHttpActionResult GetCounterDisplayReport(GetResponse Modal)
        {
            List<CounterDisplay.List> result = new List<CounterDisplay.List>();
            Modal.Doctype = "Report";
            result = transaction.GetCounterDisplayList(Modal);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult GetMOPReport(GetResponse Modal)
        {
            List<MOP.List> result = new List<MOP.List>();
            Modal.Doctype = "Report";
            result = transaction.GetMOPList(Modal);
            return Ok(result);
        }
        [HttpPost]
        public IHttpActionResult GetCompetitionEntryReport(GetResponse Modal)
        {
            List<CompetitionEntry.List> result = new List<CompetitionEntry.List>();
            Modal.Doctype = "Report";
            result = transaction.GetCompetitionEntryList(Modal);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult GetRFCRequestReport(GetResponse Modal)
        {
            List<RFCRequest.List> result = new List<RFCRequest.List>();
            Modal.Doctype = "Report";
            result = transaction.GetRFCRequestList(Modal);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult GetProfile(GetResponse modal)
        {
            Profile Results = new Profile();
            Results = Common_SPU.GetProfile(modal);
            return Ok(Results);

        }        
        [HttpPost]
        public async System.Threading.Tasks.Task<bool> SaveTestPaperOfflineDocument()
        {
            var re = Request;

            string FileType = "";
            string FileSize = "";
            string FileName = "";
            string FileExtension = "";
            string RoutePath = "";


            FileType = System.Web.HttpContext.Current.Request.Form.Get("ContentType");
            RoutePath = "E:\\WVS2019\\Thrivera\\Thrivera\\Website\\UserDetails\\TEMP\\" + "\\UploadedDocs";

            var file = System.Web.HttpContext.Current.Request.Files.Count > 0 ?
        System.Web.HttpContext.Current.Request.Files[0] : null;

            if (file != null && file.ContentLength > 0)
            {
                var fileName = System.IO.Path.GetFileName(file.FileName);

                FileType = file.ContentType; FileSize = (file.ContentLength / 1024).ToString(); FileExtension = System.IO.Path.GetExtension(file.FileName);
                FileName = System.IO.Path.GetFileNameWithoutExtension(file.FileName);
                if (!System.IO.Directory.Exists(RoutePath))
                {
                    System.IO.DirectoryInfo dirInfo = System.IO.Directory.CreateDirectory(RoutePath);
                }

                if (!System.IO.File.Exists(RoutePath))
                {
                    file.SaveAs(RoutePath);
                }
                else
                {
                    System.IO.File.Delete(RoutePath);
                    file.SaveAs(RoutePath);
                }
            }

            return true;
        }

    }
}
