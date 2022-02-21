using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RIS_blazor.Shared.Models
{
    [Table("ProcedureStatus")]
    public partial class ProcedureStatus
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
    }
}
