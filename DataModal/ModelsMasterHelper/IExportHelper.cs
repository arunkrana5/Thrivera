using DataModal.Models;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.ModelsMasterHelper
{
    public interface IExportHelper
    {
        ISheet GetWorkbookSheet(IWorkbook workbook, DataTable result, string Doctype);
        IWorkbook GetDataTable_Workbook_Common(DataTable result, string Doctype);
        IWorkbook GetDataTable_Workbook(DataTable result, string Doctype);
        IWorkbook GetSaleEntry_Workbook(Tab.Approval modal);
        IWorkbook GetTargetVsAchievement_Workbook(Tab.Approval modal);
        IWorkbook GetMTDReport_Workbook(Tab.Approval modal);
        IWorkbook GetPJPExpense_Workbook(Tab.Approval modal);
        IWorkbook GetAttendanceLog_Day_Workbook(Tab.Approval modal);
        IWorkbook GetAttendanceLog_Monthly_Workbook(Tab.Approval modal);
        IWorkbook GetAttendanceLog_Monthly_InOut_Workbook(Tab.Approval modal);
        IWorkbook GetAttendance_Final_Workbook(Tab.Approval modal);
        IWorkbook GetAttendance_Workbook(Tab.Approval modal);
        IWorkbook GetTLTracker_Workbook(Tab.Approval modal);
        IWorkbook CompetitionSummary_Workbook(Tab.Approval modal);
        IWorkbook SaleEntryWithCustomerReport_Workbook(Tab.Approval modal);
        IWorkbook GetTravel_Expenses_Workbook(Tab.Approval modal);
        IWorkbook GetTravel_Visit_Report_Workbook(Tab.Approval modal);
        IWorkbook CounterDisplayReport_Workbook(Tab.Approval modal);
        IWorkbook GetIncentiveCalculator_Workbook(Tab.Approval modal);
    }
}
