using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS_TEP_AB.Common;
using EMS_TEP_AB.IServices;
using EMS_TEP_AB.Models.RequestModel;
using EMS_TEP_AB.Models.ResponseModel;
using Microsoft.AspNetCore.Mvc;

namespace EMS_TEP_AB.Controllers
{
    [Route("api/Attendance")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private IAttendance attendance;
        public AttendanceController(IAttendance _attendance)
        {
            attendance = _attendance;
        }
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPost]
        [Route("Checkin")]
        public async Task<IActionResult> Checkin([FromBody] AttendanceReqModel model)
        {
            List<CheckinResponseModel> responseModel = new List<CheckinResponseModel>(); 
            try
            {

                DateTime checkinTime = Convert.ToDateTime(model.checkinTime);

                if (checkinTime == null || checkinTime < DateTime.Now)
                {
                    model.checkinTime = DateTime.Now; 
                }
                responseModel =  attendance.CheckIn(model);

            }
            catch (Exception)
            {

                throw;
            }
            return Ok(responseModel);
        }
    }
}