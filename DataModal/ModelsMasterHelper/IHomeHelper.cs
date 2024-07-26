using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModal.Models;

namespace DataModal.ModelsMasterHelper
{
    public interface IHomeHelper
    {
        Attendance_Log.PunchStatus GetPunchTime_DateWise(GetResponse Modal);
        List<TrgVsAch> GetTargetAchieved_MonthWise(GetResponse Modal);
        List<Announcement.My> GetMyAnnouncement(GetResponse modal);
        List<BirthdayList> GetBirthdayList(GetResponse modal);
    }
}
