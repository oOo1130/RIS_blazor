using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RIS_blazor.Shared.Models
{
    //[Keyless]
    public partial class Vwworklist
    {
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
        public int ConsultantId { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        [StringLength(250)]
        public string HospitalName { get; set; }
        [Required]
        [StringLength(250)]
        public string ConsultantName { get; set; }
        [Column("ProcedureHISNmame")]
        [StringLength(50)]
        public string ProcedureHisnmame { get; set; }
        public int? NoOfImage { get; set; }
        public int DatasetId { get; set; }
        [Required]
        [StringLength(64)]
        public string SeriesDescription { get; set; }
        [Required]
        [StringLength(16)]
        public string StudyId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StudyDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SeriesDate { get; set; }
        [Required]
        [StringLength(16)]
        public string SeriesNumber { get; set; }
        [Required]
        [StringLength(64)]
        public string SeriesInstanceUid { get; set; }
        [Required]
        [StringLength(64)]
        public string SopInstanceUid { get; set; }
        [Required]
        [StringLength(64)]
        public string SopClassUid { get; set; }
        [Required]
        [StringLength(16)]
        public string InstanceNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InstanceDateTime { get; set; }
        [Required]
        [StringLength(255)]
        public string DatasetPath { get; set; }
    }
}
