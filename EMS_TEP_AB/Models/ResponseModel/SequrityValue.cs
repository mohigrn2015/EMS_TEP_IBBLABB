using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_TEP_AB.Models.ResponseModel
{
    public class SequrityValue
    {
        public string loginProvider { get; set; }
        public int userId { get; set; }
        public int rightId { get; set; }
        public string userName { get; set; }
        public string role { get; set; }
        public int isSuccess { get; set; } 

    }
}
