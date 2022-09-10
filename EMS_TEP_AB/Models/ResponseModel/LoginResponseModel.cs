using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_TEP_AB.Models.ResponseModel
{
    public class LoginResponseModel
    {
        public bool is_Authenticated { get; set; }
        public string auth_essage { get; set; }
        public bool hasUpdate { get; set; }
        public string session_token { get; set; }
        public int right_id { get; set; }
        public string accessed_role { get; set; }
    } 
}
 