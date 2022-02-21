using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RIS_blazor.Shared.Models
{
    [Table("HISModalityProcedureMappings")]
    public partial class HismodalityProcedureMapping
    {
        [Key]
        public int MapId { get; set; }
        public int? TenantId { get; set; }
        [Column("PId")]
        public int? Pid { get; set; }
        [StringLength(150)]
        public string ModalityProcDescription { get; set; }
    }
}
