using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_TEP_AB.Models.RequestModel
{
    public class UserLoginAttempts
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string loginProvider { get; set; }
        public string deviceId { get; set; }
        public string ip_address { get; set; }
        public string version { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }


    }
}
