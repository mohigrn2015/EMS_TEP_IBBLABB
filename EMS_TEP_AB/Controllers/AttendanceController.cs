using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS_TEP_AB.Common;
using EMS_TEP_AB.DAL;
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
       
        private IAttendance _attendance;
        public AttendanceController(IAttendance attendance)
        {
            _attendance = attendance;
        }
        TokenValidation _token = new TokenValidation();
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPost]
        [Route("Checkin")]
        public async Task<IActionResult> Checkin([FromBody] AttendanceReqModel model)
        {
            List<CheckinResponseModel> responseModel = new List<CheckinResponseModel>();
            SequrityValue sequrityValue = new SequrityValue();
            try 
            {
                sequrityValue = _token.ValidateSessionToken(model.sessionToken);
                if (sequrityValue.isSuccess == 0)
                    throw new Exception("Invalid Session Token or Loged in another device!");

                DateTime checkinTime = Convert.ToDateTime(model.checkinTime);
                model.userId = sequrityValue.userId;
                if (checkinTime == null || checkinTime < DateTime.Now)
                {
                    model.checkinTime = DateTime.Now; 
                }
                responseModel =  _attendance.CheckIn(model);
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return Ok(responseModel);
        }
    }
}