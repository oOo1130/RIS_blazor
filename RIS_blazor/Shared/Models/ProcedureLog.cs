using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RIS_blazor.Shared.Models
{
    public partial class ProcedureLog
    {
        [Key]
        public int LogId { get; set; }
        public int? ProcId { get; set; }
        [StringLength(500)]
        public string LogText { get; set; }
        [StringLength(50)]
        public string UserName { get; set; }
        [Column(TypeName = "date")]
        public DateTime? EventDate { get; set; }
        [StringLength(50)]
        public string EventTime { get; set; }
    }
}
