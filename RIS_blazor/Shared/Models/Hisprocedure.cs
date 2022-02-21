using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RIS_blazor.Shared.Models
{
    [Table("HISProcedures")]
    public partial class Hisprocedure
    {
        [Key]
        [Column("PId")]
        public int Pid { get; set; }
        [StringLength(50)]
        public string Modality { get; set; }
        [StringLength(250)]
        public string ProcName { get; set; }
    }
}
