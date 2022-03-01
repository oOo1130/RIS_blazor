using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RIS_blazor.Shared.Models
{
    public partial class ReportConsultant
    {
        [Key]
        [Column("RCId")]
        public int Rcid { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        public int? Fsize1 { get; set; }
        [StringLength(250)]
        public string IdentityLine1 { get; set; }
        public int? Fsize2 { get; set; }
        [StringLength(250)]
        public string IdentityLine2 { get; set; }
        public int? Fsize3 { get; set; }
        [StringLength(250)]
        public string IdentityLine3 { get; set; }
        public int? Fsize4 { get; set; }
        [StringLength(250)]
        public string IdentityLine4 { get; set; }
        public int? Fsize5 { get; set; }
        [StringLength(250)]
        public string IdentityLine5 { get; set; }
        public int? Fsize6 { get; set; }
        [StringLength(250)]
        public string IdentityLine6 { get; set; }
        public int? Fsize7 { get; set; }
        [Column("ESignature")]
        public byte[] ESignature { get; set; }
        [Column("IsESignatureAllow")]
        public bool IsESignatureAllow { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        public string SignatureBase64HtmlEmbeded { get; set; }
        public bool IsViewerWithDefaultTemplate { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        
        public string RadNextCloudID { get; set; }
        [StringLength(50)]
        public string DicomImagePath { get; set; }

    }
}
