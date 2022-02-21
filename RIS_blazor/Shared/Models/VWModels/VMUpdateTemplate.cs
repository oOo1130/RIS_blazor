using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIS_blazor.Shared.Models.VWModels
{
    public class VMUpdateTemplate
    {
        public int TemplateId { get; set; }

        [Column(TypeName = "ntext")]
        public string TemplateContent { get; set; }
    }
}
