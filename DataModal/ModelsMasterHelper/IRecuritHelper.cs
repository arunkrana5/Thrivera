using DataModal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.ModelsMasterHelper
{
    public interface IRecuritHelper
    {
        List<Requirement.RequestList> GetRequirement_MyRequest(Tab.Approval Modal);
        PostResponse fnSetRequirement_Request(Requirement.AddRequest modal);
        Requirement.AddRequest GetRequirement_Request(GetResponse Modal);
        List<Requirement.RequestList> GetRequirement_RequestList(Tab.Approval Modal);
        PostResponse fnSetRequirement_Application(Requirement.Application.Add modal);
        List<Requirement.Application.List> GetRequirement_ApplicationList(GetResponse Modal);
        Requirement.FullView GetRequirement_FullView(GetResponse Modal);
        PostResponse fnSetRequirementApplication_Approved(ApprovalAction modal);

        List<Requirement.RequestList> GetRequirement_LevelApprovalList(Tab.Approval Modal);
        PostResponse fnSetRequirement_Approved(ApprovalAction modal);
    }
}
