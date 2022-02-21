using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIS_blazor.Shared.Models
{
    [Table("MenuItems")]
    public partial class MenuItem
    {
        public int id { get; set; }
        public string menuTitle { get; set; }
        public string menuTo { get; set; }
    }
}
