using DataModal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.ModelsMasterHelper
{
    public interface IApprovalsHelper
    {
        List<RFCEntry.List> GetRFCApprovalList(Tab.Approval Modal);
        PostResponse fnSetRFCApproved(RFCEntry.Action modal);
        List<SaleEntry.ApprovalList> GetSaleEntryApprovalList(Tab.Approval Modal);
        PostResponse fnSetSaleEntry_Approved(SaleEntry.ApprovalAction modal);
        PostResponse fnSetAttendenceApproved(ApprovalAction modal);
        List<PJPExpense.List> GetPJPEntry_Expense_ApprovalList(Tab.Approval Modal);
        PostResponse fnSetPJPEntry_Approved(ApprovalAction modal);



    }
}
