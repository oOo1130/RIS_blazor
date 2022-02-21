using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIS_blazor.Shared.Models.VWModels
{
    public class VMIncompleteWorkList
    {

        public VMIncompleteWorkList(string _PatientId, string _StatusString, string _PatientName, 
            string _ProcedureHISName, string _ClinicalHistory, int? _NoOfImages, DateTime _ArrivalDateTime, 
            string _HospitalName, string _Modality, string _ConsultantName, string _ReferralPhysician, DateTime? _OrderDateTime) 
        {
            PatientId = _PatientId;
            StatusString = _StatusString;
            PatientName = _PatientName;
            ProcedureHISName = _ProcedureHISName;
            ClinicalHistory = _ClinicalHistory;
            NoOfImages = _NoOfImages;
            ArrivalDateTime = _ArrivalDateTime;
            HospitalName = _HospitalName;
            Modality = _Modality;
            ConsultantName = _ConsultantName;
            ReferralPhysician = _ReferralPhysician;
            OrderDateTime = _OrderDateTime;

        }
        public string PatientId { get; set; }
        public string StatusString { get; set; }
        public string PatientName { get; set; }
        public string ProcedureHISName { get; set; }
        public string ClinicalHistory { get; set; }
        public int? NoOfImages { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public string HospitalName { get; set; }
        public string Modality { get; set; }
        public string ConsultantName { get; set; }
        public string ReferralPhysician { get; set; }
        public DateTime? OrderDateTime { get; set; }

        
    }
}
