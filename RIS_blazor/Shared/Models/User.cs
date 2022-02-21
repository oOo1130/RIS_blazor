using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RIS_blazor.Shared.Models
{
    public partial class User
    {
        [Key]
        public int UserId { get; set; }
        public int? RoleId { get; set; }
        [StringLength(50)]
        public string? LoginName { get; set; }
        [StringLength(250)]
        public string? FullName { get; set; }
        [StringLength(250)]
        public string? MobileNo { get; set; }
        [StringLength(250)]
        public string? Password { get; set; }
        [StringLength(250)]
        public string? Salt { get; set; }
        [Column("RCId")]
        public int Rcid { get; set; }
        public int TenantId { get; set; }
        [StringLength(250)]
        public string? Comments { get; set; }
        [StringLength(50)]
        public string? Status { get; set; }
        [StringLength(500)]
        public string? CloudAccessLink { get; set; }
        [StringLength(50)]
        public string? CloudUserName { get; set; }
        [StringLength(50)]
        public string? CloudPassword { get; set; }


        [StringLength(50)]
        public string? ReportCreateLocation { get; set; }
        public bool IsAssignToRadAllow { get; set; }
        public bool IsMainViewerAlloted { get; set; }
        public bool IsReportWriteAllow { get; set; }
        public bool IsReportViewAllow { get; set; }
    }

}
