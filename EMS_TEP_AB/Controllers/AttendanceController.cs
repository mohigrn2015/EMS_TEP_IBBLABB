using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS_TEP_AB.Common;
using EMS_TEP_AB.Models.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace EMS_TEP_AB.Controllers
{
    [Route("api/Attendance")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPost]
        [Route("Checkin")]
        public async Task<IActionResult> Checkin([FromBody] CheckInModel model)
        {
            try
            {
                DateTime checkinTime = Convert.ToDateTime(model.checkInTime);
                if (checkinTime == null || checkinTime < DateTime.Now)
                {
                    model.checkInTime = DateTime.Now;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(model);
        }
    }
}