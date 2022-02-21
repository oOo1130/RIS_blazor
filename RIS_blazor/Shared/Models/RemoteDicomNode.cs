using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RIS_blazor.Shared.Models
{
    public partial class RemoteDicomNode
    {
        [Key]
        public Guid NodeGuid { get; set; }
        [StringLength(250)]
        public string NodeName { get; set; }
        [StringLength(50)]
        public string NodeHost { get; set; }
        public int? NodePort { get; set; }
        [StringLength(50)]
        public string NodeAet { get; set; }
        public int? TenantId { get; set; }
        public bool? IsRouter { get; set; }
    }
}
