using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIS_blazor.Shared.Models
{
    [Table("NextCloudUser")]
    public partial class NextCloudUser
    {
        [Key]
        public int CloudUserId { get; set; }
        [StringLength(64)]
        public string GroupName { get; set; }
        [StringLength(64)]
        public string UserName { get; set; }
        [StringLength(64)]
        public bool ShareStatus { get; set; }
        [StringLength(250)]
        public string FileName { get; set; }
        public int Share_id { get; set; }


    }
}
