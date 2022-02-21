using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RIS_blazor.Shared.Models
{
    public partial class TenantDefaultConsultantMapping
    {
        [Key]
        public int Id { get; set; }
        public int? TenantId { get; set; }
        [Column("RCId")]
        public int? Rcid { get; set; }
        [StringLength(50)]
        public string Modality { get; set; }
    }
}
