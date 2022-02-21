using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RIS_blazor.Shared.Models
{
    [Table("RISWorkLists")]
    public partial class RISWorkList
    {
        [Key]
        public int ProcId { get; set; }
        [StringLength(64)]
        public string PatientId { get; set; }
        [StringLength(64)]
        public string AccessionNumber { get; set; }
        [StringLength(64)]
        public string StudyInstanceUid { get; set; }
        [Required]
        [StringLength(250)]
        public string PatientName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PatientBirthdate { get; set; }
        [StringLength(16)]
        public string PatientSex { get; set; }
        [StringLength(64)]
        public string ProcedureName { get; set; }
        public int? NoOfImages { get; set; }
        [StringLength(10)]
        public string Modality { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ArrivalDateTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? OrderDateTime { get; set; }
        [StringLength(250)]
        public string ClinicalHistory { get; set; }
        [StringLength(150)]
        public string ReferralPhysician { get; set; }
        public int TenantId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AssignedDateTime { get; set; }
        [StringLength(50)]
        public string AssignedByUser { get; set; }
        public int ConsultantId { get; set; }
        public int? ImgSource { get; set; }
        public int? Status { get; set; }

        public int? Share_Id { get; set; }
    }
}
