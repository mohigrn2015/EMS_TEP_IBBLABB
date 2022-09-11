using EMS_TEP_AB.Models.RequestModel;
using EMS_TEP_AB.Models.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_TEP_AB.IServices
{
    public interface IAttendance
    {
        List<CheckinResponseModel> CheckIn(AttendanceReqModel attendance);
    }
}
