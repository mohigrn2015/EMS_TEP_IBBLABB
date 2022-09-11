using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_TEP_AB.Models.RequestModel
{
    public class CommonReqModel
    {
        [Required]
        public string sessionToken { get; set; }
    }
}
