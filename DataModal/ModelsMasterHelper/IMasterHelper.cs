using DataModal.Models;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace DataModal.ModelsMasterHelper
{
    public interface IMasterHelper
    {
        List<Masters.List> GetMastersList(GetResponse Modal);
        Masters.Add GetMasters(GetResponse Modal);
        PostResponse fnSetMasters(Masters.Add modal);

        List<Branch.List> GetBranchList(GetResponse Modal);
        Branch.Add GetBranch(GetResponse Modal);
        PostResponse fnSetBranch(Branch.Add modal);

        List<Department.List> GetDepartmentList(GetResponse Modal);
        Department.Add GetDepartment(GetResponse Modal);
        PostResponse fnSetDepartment(Department.Add modal);

        List<Designation.List> GetDesignationList(GetResponse Modal);
        Designation.Add GetDesignation(GetResponse Modal);
        PostResponse fnSetDesignation(Designation.Add modal);

        List<Brand.List> GetBrandList(GetResponse Modal);
        Brand.Add GetBrand(GetResponse Modal);

        PostResponse fnSetBrand(Brand.Add modal);



        List<Employee.List> GetEMPList(Tab.Approval Modal);
        Employee.Add GetEMP(GetResponse Modal);
        PostResponse fnSetEMP(Employee.Add modal);
        PostResponse fnSetEMP_DOL(Employee.UpdateDOL model);
        List<Dealer.List> GetDealerList(GetResponse Modal);
        List<DealerImport.List> GetDealerImportList(GetResponse Modal);
        Dealer.Add GetDealer(GetResponse Modal);
        PostResponse fnSetDealer(Dealer.Add model);
        PostResponse UploadDealerImportDataExcelFile(HttpPostedFileBase file, string SheetName, GetResponse getResponse);
        PostResponse SaveDealerImportTempDetails(DataSet TempDataset, GetResponse getResponse);
        PostResponse ClearDealerImportTemp(GetResponse getResponse);
        PostResponse UploadDealerImportDetailList(GetResponse getResponse);


        List<AttendenceStatus.List> GetAttendenceStatusList(GetResponse Modal);
        AttendenceStatus.Add GetAttendenceStatus(GetResponse Modal);
        PostResponse fnSetAttendenceStatus(AttendenceStatus.Add model);

        List<UserHierarchy.List> GetUserHierarchyList(GetResponse Modal);
        UserHierarchy.Add GetUserHierarchy(GetResponse Modal);
        PostResponse fnSetUserHierarchy(UserHierarchy.Add modal);
        PostResponse fnSetUserHierarchy_Mapping(long HierarchyID, string LinkID, GetResponse Modal);

        List<EMPImport.List> GetEMPImportList(GetResponse Modal);
        PostResponse SaveEMPImportTempDetails(DataSet TempDataset, GetResponse getResponse);
        PostResponse ClearEMPImportTemp(GetResponse getResponse);
        PostResponse SetEMPFromEMPImport(GetResponse getResponse);
        PostResponse UploadEMPImportDataExcelFile(HttpPostedFileBase file, string SheetName, GetResponse getResponse);


        List<MastersImport.List> GetMastersImportList(GetResponse Modal);
        PostResponse SaveMastersImportTempDetails(DataSet TempDataset, GetResponse getResponse);
        PostResponse ClearMastersImportTemp(GetResponse getResponse);
        PostResponse SetMastersFromMastersImport(GetResponse getResponse);
        PostResponse UploadMastersImportDataExcelFile(HttpPostedFileBase file, string SheetName, GetResponse getResponse);

        EMPTalentPool.Add GetEMPTalentPool(GetResponse Modal);
        
        List<EMPTalentPool.List> GetEMPTalentPoolList(Tab.Approval Modal);
        PostResponse fnSetEMP_TalentPool(EMPTalentPool.Add model);
        PostResponse fnSetBranch_Mapping(long BranchID, string LinkID, GetResponse Modal);
        List<ChallanDocuments.List> GetChallanDocumentsList(Tab.Date Modal);
        ChallanDocuments.Add GetChallanDocuments(GetResponse Modal);
        PostResponse fnSetChallanDocuments(ChallanDocuments.Add Modal);

        List<EMPTalentPoolImport.List> GetEMPTalentPoolImportList(GetResponse Modal);

        PostResponse UploadEMP_TalentPoolImportDataExcelFile(HttpPostedFileBase file, string SheetName, GetResponse getResponse);
        PostResponse SaveEMP_TalentPoolImportTempDetails(DataSet TempDataset, GetResponse getResponse);
        PostResponse ClearEMP_TalentPoolImportTemp(GetResponse getResponse);
        PostResponse SetEMP_TalentPoolFromImportTable(GetResponse getResponse);
        List<UserHierarchyImport.List> GetUserHierarchyImportList(GetResponse Modal);

        PostResponse UploadUserHierarchyImportDataExcelFile(HttpPostedFileBase file, string SheetName, GetResponse getResponse);
        PostResponse SaveUserHierarchyImportTempDetails(DataSet TempDataset, GetResponse getResponse);
        PostResponse ClearUserHierarchyImportTemp(GetResponse getResponse);
        PostResponse SetUserHierarchyFromUserHierarchyImport(GetResponse getResponse);
        List<LeaveBalance.List> GetLeaveBalanceList(Tab.Approval Modal);
        List<LeaveBalance.TranList> GetLeaveBalanceTran(GetResponse Modal);
        List<DealerCategory.List> GetDealerCategoryList(GetResponse Modal);
        DealerCategory.Add GetDealerCategory(GetResponse Modal);
        PostResponse fnSetDealerCategory(DealerCategory.Add modal);

        List<DealerType.List> GetDealerTypeList(GetResponse Modal);
        DealerType.Add GetDealerType(GetResponse Modal);
        PostResponse fnSetDealerType(DealerType.Add modal);

        List<MasterCatalogue.List> GetMasterCatalogueList(Tab.Approval Modal);
        MasterCatalogue.Add GetMasterCatalogue(GetResponse Modal);
        PostResponse fnSetMasterCatalogue(MasterCatalogue.Add Modal);
        
    }
}
