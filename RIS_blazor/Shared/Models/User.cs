using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIS_blazor.Shared.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public int? RoleId { get; set; }
        public string LoginName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string MobileNo { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public int Rcid { get; set; }
        public int TenantId { get; set; }
        public string Comments { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string CloudAccessLink { get; set; } = null!;
        public string CloudUserName { get; set; } = null!;
        public string CloudPassword { get; set; } = null!;
        public string ReportCreateLocation { get; set; } = null!;
        public bool IsAssignToRadAllow { get; set; }
        public bool IsMainViewerAlloted { get; set; }
        public bool IsReportWriteAllow { get; set; }
        public bool IsReportViewAllow { get; set; }

    }

}
