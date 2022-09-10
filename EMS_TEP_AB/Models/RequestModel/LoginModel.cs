using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_TEP_AB.Models.RequestModel
{
    public class LoginModel
    {
        [Required]
        public string user_name { get; set; }

        [Required]
        public string password { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string device_id { get; set; }
    }
}
