using DataModal.Models;
using System.Collections.Generic;
using System.Data;

namespace DataModal.ModelsMasterHelper
{
    public interface IReportHelper
    {
        DataSet GetSaleEntryReport(Tab.Approval Modal);
        DataSet GetMTD_Report(Tab.Approval Modal);
        
        DataSet GetAllSSR_Hierarchy_Report(GetResponse Modal);
        List<CounterDisplay.Report> GetCounterDisplayReport(Tab.Approval Modal);
        List<MOP.Report> GetMOPReportList(Tab.Approval Modal);
        
        DataSet GetTargetVsAchievement_Report(Tab.Approval Modal);

        DailySummary.Data GetDailyReportSummaryData(Tab.Approval Modal);
        DataSet GetPJPEntryReport(Tab.Approval Modal);
        DataSet GetSaleEntry_WithCustomer(Tab.Approval Modal);
        List<Attendance_Log.Daily> GetAttendance_Log_Daily(Tab.Approval Modal);
        DataSet GetAttendance_Log_Monthly(Tab.Approval Modal);
        List<Attendance_Log.Monthly> GetAttendance_Log_MonthlyList(Tab.Approval Modal);
        List<Attendance_Log.Monthly_INOUT> GetAttendance_Log_Monthly_InOutList(Tab.Approval Modal);
        
        DataSet GetAttendance_Final(Tab.Approval Modal);
        DataSet GetAttendance(Tab.Approval Modal);
        DataSet GetAttendance_EMPWise(Tab.Approval Modal);
        DataSet GetTLTracker_Report(Tab.Approval Modal);
        PostResponse fnSetUpdateAttendance(GetResponse modal, DataTable Table);
        DataSet GetTravel_Expenses_Report(Tab.Approval Modal);
        DataSet GetTravel_Visit_Report(Tab.Approval Modal);
        List<AbsentTracker> GetAbsentTracker(Tab.Approval Modal);
        DataSet GetEMP_Incentive(Tab.Approval Modal);



    }
}
