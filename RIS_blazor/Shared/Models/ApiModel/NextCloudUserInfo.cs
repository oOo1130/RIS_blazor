using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RIS_blazor.Shared.Models.ApiModel
{
    public class NextCloudUserInfo
    {
        public string userName { get; set; }
        public string? groupName { get; set; }
        public int? shareId { get; set; }
        public string fileName { get; set; }
    }

}
