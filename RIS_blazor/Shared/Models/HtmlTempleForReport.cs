using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIS_blazor.Shared.Models
{
    public class HtmlTempleForReport
    {
        public int Id { get; set; }
        [Column("RCId")]
        public int RCId { get; set; }
        [StringLength(250)]
        public string TemplateName { get; set; }

        [Column(TypeName = "ntext")]
        public string TemplateContent { get; set; }
    }
}
