using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RIS_blazor.Shared.Models
{
    public partial class Tenant
    {
        [Key]
        public int TenantId { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        [StringLength(50)]
        public string ShortName { get; set; }
        [StringLength(250)]
        public string Address { get; set; }
        [StringLength(50)]
        public string MobileNo { get; set; }
        [StringLength(50)]
        public string PhoneNo { get; set; }
        [StringLength(50)]
        public string Fax { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(50)]
        public string ContactPerson { get; set; }
        public bool? IsActive { get; set; }
        public bool? HasDefaultRadiologist { get; set; }
    }
}
