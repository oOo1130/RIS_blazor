using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIS_blazor.Shared.Models.VWModels
{
    public class VMRISWorklist
    {
        public VMRISWorklist()
        {
            VMReportObjs = new HashSet<VMReportObj>();
        }

        public ICollection<VMReportObj> VMReportObjs { get; set; }
        public int ProcId { get; set; }
        public string? PatientId { get; set; }
        public string? AccessionNumber { get; set; }
        public string? StudyInstanceUid { get; set; }
        public string? PatientName { get; set; }
        public DateTime? PatientBirthdate { get; set; }
        public string? PatientSex { get; set; }
        public string? ProcedureName { get; set; }  // Name within Modality
        public string? ProcedureHISName { get; set; } // Name know to general and HIS system
        public int? NoOfImages { get; set; }
        public string? Modality { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public DateTime? OrderDateTime { get; set; }
        public string? ClinicalHistory { get; set; }
        public string? ReferralPhysician { get; set; }
        public int TenantId { get; set; }
        public int ConsultantId { get; set; }
        public int Status { get; set; }
        public string? StatusString { get; set; }
        public string? HospitalName { get; set; }
        public string? ConsultantName { get; set; }
      
        public int? NoOfImage { get; set; }
        public int DatasetId { get; set; }
        public string? SeriesDescription { get; set; }
        public string? StudyId { get; set; }
        public DateTime? StudyDate { get; set; }
        public DateTime? SeriesDate { get; set; }
        public string? SeriesNumber { get; set; }
        public string? SeriesInstanceUid { get; set; }
        public string? SopInstanceUid { get; set; }
        public string? SopClassUid { get; set; }
        public string? InstanceNumber { get; set; }
        public DateTime? InstanceDateTime { get; set; }
        public string? DatasetPath { get; set; }

        public int? Share_Id { get; set; }

    }
}
