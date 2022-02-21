using RIS_blazor.Shared.Models;
using RIS_blazor.Server.Models;
using RIS_blazor.Shared.Models.VWModels;
using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIS_blazor.Server.Repositories
{
    public class ReportRepository
    {
        internal List<VMRadProcTemplate> GetTemplates(int rCId)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    return context.VMRadProcTemplates.FromSqlRaw<VMRadProcTemplate>(@"SELECT        dbo.ProcedureRadiologistTemplates.Id, dbo.ProcedureRadiologistTemplates.PId, dbo.ProcedureRadiologistTemplates.RCId, dbo.ProcedureRadiologistTemplates.FileName, dbo.ProcedureRadiologistTemplates.TemplateName, 
                         dbo.ProcedureRadiologistTemplates.TemplateContent, dbo.HISProcedures.Modality, dbo.HISProcedures.ProcName AS ProcedureName
FROM            dbo.ProcedureRadiologistTemplates INNER JOIN
                         dbo.HISProcedures ON dbo.ProcedureRadiologistTemplates.PId = dbo.HISProcedures.PId Where dbo.ProcedureRadiologistTemplates.RCId=@RCId", new SqlParameter("@RCId", rCId)).ToList();
                }catch(Exception ex)
                {
                    Console.Write(ex.Message);
                    return null;
                }
            }
        }

        internal ReportConsultant GetReportConsultant(int rCId)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                var item = context.ReportConsultants.Where(x => x.Rcid == rCId).FirstOrDefault();

                return item;
            }
        }

        internal bool SaveDxDrCrReport(RadiologistOpinionOne newReport, bool isReportCompleted)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    context.RadiologistOpinionOnes.Add(newReport);
                    context.SaveChanges();

                    if (isReportCompleted)
                    {
                        RISWorkList _workList = context.RISWorkLists.Where(x => x.ProcId == newReport.ProcId).FirstOrDefault();

                        _workList.Status = 6;
                        context.Entry(_workList).State = EntityState.Modified;
                        context.SaveChanges();
                    }

                    return true;

                }catch(Exception ex)
                {
                    Console.Write(ex.Message);
                    return false;
                }
            }
        }

        internal bool UpdateDxDrCrReport(RadiologistOpinionOne existingReport, bool isReportCompleted)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    context.Entry(existingReport).State = EntityState.Modified;
                    context.SaveChanges();

                    if (isReportCompleted)
                    {
                        RISWorkList _workList = context.RISWorkLists.Where(x => x.ProcId == existingReport.ProcId).FirstOrDefault();

                        _workList.Status = 6;
                        context.Entry(_workList).State = EntityState.Modified;
                        context.SaveChanges();
                    }

                    return true;

                }catch(Exception ex)
                {
                    Console.Write(ex.Message);
                    return false;
                }
            }
        }

        internal void SaveShortCutKey(ShortCutKey key)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                context.ShortCutKeys.Add(key);
                context.SaveChanges();
            }
        }

        internal List<VMRISWorklist> LoadAllCompletedStudiesByBackgroundService(DateTime datefrm, DateTime dateto, int roleId, int tenantId, int consultantId, string status)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    return context.VMRISWorklists.FromSqlRaw<VMRISWorklist>(@"Exec spGetCompletedWorklistItems @datefrm, @dateto, @roleId, @TenantId, @ConsultantId, @Status", new SqlParameter("@datefrm", datefrm), new SqlParameter("@dateto", dateto), new SqlParameter("@roleId", roleId), new SqlParameter("@TenantId", tenantId), new SqlParameter("@ConsultantId", consultantId), new SqlParameter("@Status", status)).ToList();

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return null;
                }



            }
        }

        internal List<ShortCutKey> GetShortCutKeys(int rCId)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                return context.ShortCutKeys.Where(x => x.Rcid == rCId).ToList();
            }
        }
        internal List<VMRISWorklist> LoadAllIncompleteStudiesByBackgroundService(DateTime datefrm, DateTime dateto, int roleId, int tenantId, int consultantId, string status)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    return context.VMRISWorklists.FromSqlRaw<VMRISWorklist>(@"Exec spGetInCompleteWorklistItems @datefrm, @dateto, @roleId,@TenantId, @ConsultantId, @Status", new SqlParameter("@datefrm", datefrm), new SqlParameter("@dateto", dateto), new SqlParameter("@roleId", roleId), new SqlParameter("@TenantId", tenantId), new SqlParameter("@ConsultantId", consultantId), new SqlParameter("@Status", status)).ToList();

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return null;
                }



            }
        }

        internal bool SaveCTMRReport(RadiologistOpinionTwo newReport, bool isReportCompleted)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    context.RadiologistOpinionTwoes.Add(newReport);
                    context.SaveChanges();

                   
                    if (isReportCompleted)
                    {
                        RISWorkList _workList = context.RISWorkLists.Where(x => x.ProcId == newReport.ProcId).FirstOrDefault();

                        _workList.Status = 6;
                        context.Entry(_workList).State = EntityState.Modified;
                        context.SaveChanges();
                    }


                    return true;

                }
                catch
                {
                    return false;
                }

            }
        }

        internal bool UpdateCTMRReport(RadiologistOpinionTwo existingOpinion, bool isReportCompleted)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    context.Entry(existingOpinion).State = EntityState.Modified;
                    context.SaveChanges();

                    if (isReportCompleted)
                    {
                        RISWorkList _workList = context.RISWorkLists.Where(x => x.ProcId == existingOpinion.ProcId).FirstOrDefault();

                        _workList.Status = 6;
                        context.Entry(_workList).State = EntityState.Modified;
                        context.SaveChanges();
                    }

                    return true;

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return false;
                }
            }
        }

        internal RadiologistOpinionTwo CTMRExistingReportForThisConsultant(int procId, int consultantId)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                return context.RadiologistOpinionTwoes.Where(x => x.ProcId == procId && x.Rcid == consultantId).FirstOrDefault();
            }
        }

        internal bool UpdateMasterTemplate(MasterTemplate template)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                context.Entry(template).State= EntityState.Modified;
                context.SaveChanges();
                return true;
            }
        }

        internal RadiologistOpinionOne DxRxCrExistingReportForThisConsultant(int procId, int consultantId)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
               return context.RadiologistOpinionOnes.Where(x=>x.ProcId== procId && x.Rcid== consultantId).FirstOrDefault();

            }
        }

        internal MasterTemplate GetWordMasterTemplate()
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                var item = context.MasterTemplates.Where(x => x.Id == 1).FirstOrDefault();

                return item;
            }
        }

        internal ProcedureRadiologistTemplate GetWordTemplateContent(VMRISWorklist wlObj)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
               return context.ProcedureRadiologistTemplates.Where(x => x.Rcid == wlObj.ConsultantId && x.Pid== wlObj.ProcId).FirstOrDefault();

               
            }
        }

        internal MasterTemplate GetWordMasterTemplateContent()
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                var item = context.MasterTemplates.Where(x => x.Id == 1).FirstOrDefault();

                return item;
            }
        }

        internal bool SaveMasterTemplate(MasterTemplate template)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    context.MasterTemplates.Add(template);
                    context.SaveChanges();


                    return true;

                }
                catch
                {
                    return false;
                }

            }
        }

        internal bool IsMatchedSecurityCode(string securityCode)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    var item = context.MasterTemplates.Where(x => x.SecurityCode.Equals(securityCode)).FirstOrDefault();

                    if (item != null) return true;

                    return false;

                }
                catch
                {
                    return false;
                }

            }
        }
    }
}
