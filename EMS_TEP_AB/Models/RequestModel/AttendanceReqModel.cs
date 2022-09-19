using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_TEP_AB.Models.RequestModel
{
    public class AttendanceReqModel : CommonReqModel
    {
        public DateTime checkinTime { get; set; }
        public DateTime checkoutTime { get; set; }
        public int userId { get; set; } 
        public string loginProvider { get; set; }

    }

}
