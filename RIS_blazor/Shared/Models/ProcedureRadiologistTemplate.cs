using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RIS_blazor.Shared.Models
{
    public partial class ProcedureRadiologistTemplate
    {
        [Key]
        public int Id { get; set; }
        [Column("PId")]
        public int? Pid { get; set; }
        [Column("RCId")]
        public int? Rcid { get; set; }
        [StringLength(250)]
        public string FileName { get; set; }
        [StringLength(250)]
        public string TemplateName { get; set; }
        public byte[] TemplateContent { get; set; }
        public bool? IsDefaultTemplate { get; set; }
    }
}
