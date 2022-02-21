using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RIS_blazor.Shared.Models
{
    public partial class DatabaseDataset
    {
        [Key]
        public int DatasetId { get; set; }
        [Required]
        [StringLength(64)]
        public string PatientId { get; set; }
        [Required]
        [StringLength(64)]
        public string PatientName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PatientBirthdate { get; set; }
        [Required]
        [StringLength(16)]
        public string PatientSex { get; set; }
        [Required]
        [StringLength(64)]
        public string AccessionNumber { get; set; }
        [Required]
        [StringLength(64)]
        public string StudyDescription { get; set; }
        [Required]
        [StringLength(64)]
        public string SeriesDescription { get; set; }
        [Required]
        [StringLength(16)]
        public string StudyId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StudyDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SeriesDate { get; set; }
        [Required]
        [StringLength(16)]
        public string Modality { get; set; }
        [Required]
        [StringLength(16)]
        public string SeriesNumber { get; set; }
        [Required]
        [StringLength(64)]
        public string StudyInstanceUid { get; set; }
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
        [Column(TypeName = "datetime")]
        public DateTime ReceptionDateTime { get; set; }
        [Required]
        [StringLength(16)]
        public string ReceptionAet { get; set; }
        public int TenantId { get; set; }
        public int ImgCount { get; set; }
    }
}
