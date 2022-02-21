using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RIS_blazor.Shared.Models
{
    public partial class RadiologistOpinionTwo
    {
        [Key]
        public int OpId { get; set; }
        public int? ProcId { get; set; }
        [Column("RCId")]
        public int? Rcid { get; set; }
        [Column(TypeName = "date")]
        public DateTime? OpDate { get; set; }
        [StringLength(50)]
        public string OpTime { get; set; }
        public byte[] ReportContent { get; set; }
        [StringLength(250)]
        public string ReportPath { get; set; }
        [Column("isReportComplete")]
        public bool? IsReportComplete { get; set; }
    }
}
