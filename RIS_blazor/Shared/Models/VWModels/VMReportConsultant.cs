using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIS_blazor.Shared.Models.VWModels
{
    public class VMReportConsultant
    {
        public int RCId { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string RadNextCloudID { get; set; }
    }
}
