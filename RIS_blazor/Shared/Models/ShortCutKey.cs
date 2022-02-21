using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RIS_blazor.Shared.Models
{
    public partial class ShortCutKey
    {
        [Key]
        public int Id { get; set; }
        [Column("RCId")]
        public int? Rcid { get; set; }
        [Column("textmacro")]
        [StringLength(50)]
        public string Textmacro { get; set; }
        [Column("expandedtext", TypeName = "ntext")]
        public string Expandedtext { get; set; }
    }
}
