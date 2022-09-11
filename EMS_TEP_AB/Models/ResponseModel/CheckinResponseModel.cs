using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_TEP_AB.Models.ResponseModel
{
    public class CheckinResponseModel
    {
        public int userId { get; set; }
        public DateTime checkinDate { get; set; }
        public DateTime checkinTime { get; set; } 
        public DateTime checkoutTime { get; set; }
        public string overTime { get; set; } 
        public int leaveDaysCount { get; set; }
        public int absentDaysCount { get; set; }
        public int holidaysCount { get; set; }
        public int weekendDaysCount { get; set; }
        public int lateDaysCount { get; set; } 

    }
}
