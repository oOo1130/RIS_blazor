using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RIS_blazor.Shared.Models
{
    public partial class Role
    {
        [Key]
        public int RoleId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
    }
}
