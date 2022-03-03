using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIS_blazor.Shared.Models.VWModels
{
    public class PermissionRequest
    {
        public List<int>? menuIds { get; set; }
        public int RoleId { get; set; }
    }
}
