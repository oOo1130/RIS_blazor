using RIS_blazor.Server.Models;
using RIS_blazor.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RIS_blazor.Shared.Models.ApiModel;
using RIS_blazor.Shared.Models.VWModels;
//using RIS_blazor.Server.Models;
using RIS_blazor.Server.Repository.ServiceObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RIS_blazor.Server.Repositories
{
    public class RISRepository
    {
        string sql = string.Empty;
        private readonly CoreDbContext _context;

        public RISRepository(CoreDbContext context)
        {
            _context = context;
        }

        internal async Task<List<User>> GetAllUser()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }

        internal bool SaveOrUpdateReportConsultant(ReportConsultant consultant)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    if (consultant.Rcid > 0)
                    {
                        context.Entry(consultant).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    else
                    {
                        context.ReportConsultants.Add(consultant);
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

        internal RISWorkList GetWorkList(int procId)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                return context.RISWorkLists.Where(x => x.ProcId == procId).FirstOrDefault();
            }
        }

        internal NextCloudUser GetNextCloudUserLists(int procId)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                return context.NextCloudUserLists.Where(x => x.CloudUserId == procId).FirstOrDefault();
            }
        }

        internal bool UpdateHISProcedure(Hisprocedure hisProc)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    context.Entry(hisProc).State = EntityState.Modified;
                    context.SaveChanges();
                    return true;

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return false;
                }
            }
        }

        internal List<ProcedureStatus> GetAllStatuses()
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                return context.ProcedureStatuses.ToList();
            }
        }

        internal async Task<List<Hisprocedure>> GetAllHISProcedure()
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                return await context.Hisprocedures.ToListAsync();
            }
        }
        internal bool SaveHISProcedure(Hisprocedure hisProc)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    context.Hisprocedures.Add(hisProc);
                    context.SaveChanges();
                    return true;

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return false;
                }
            }
        }

        internal async Task<int> GetCompletedItemCount(DateTime dateFrom, DateTime dateTo, int roleId, int tenantId, int consultantId, string status, string searchFilter)
        {
            return await Task.Run(async () =>
            {

                List<int> _workListItems;

                using (var conn = _context.Database.GetDbConnection())
                {
                    try
                    {
                        using (var multi = await conn.QueryMultipleAsync("Exec spGetCompletedWorklistItems_Cnt " +
                            "@datefrm, @dateto, @roleId,@TenantId, @ConsultantId, @Status, @SearchFilter"
                              , new { datefrm = dateFrom, dateto = dateTo, roleId = roleId, TenantId = tenantId, ConsultantId = consultantId, Status = status, SearchFilter = searchFilter }))
                        {
                            _workListItems = multi.Read<int>().ToList();
                        }

                        return _workListItems[0];

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);

                        return -1;
                    }



                }
            });
        }

        internal async Task<List<VMRISWorklist>> GetSearchFilterCompletedOnePageItems(DateTime dateFrom, DateTime dateTo, int roleId, int tenantId, int consultantId, string status, string SearchFilter, int pageNo, int recsPerPage)
        {
            return await Task.Run(async () =>
            {
                List<VMRISWorklist> _workListItems;
                using (var conn = _context.Database.GetDbConnection())
                {
                    try
                    {
                        using (var multi = await conn.QueryMultipleAsync("Exec spGetCompletedWorklistItems " +
                            "@datefrm, @dateto, @roleId,@TenantId, @ConsultantId, @Status, @SearchFilter, @PageNo,  @RecsPerPage"
                              , new { datefrm = dateFrom, dateto = dateTo, roleId = roleId, TenantId = tenantId, ConsultantId = consultantId, Status = status, SearchFilter = SearchFilter, PageNo = pageNo, RecsPerPage = recsPerPage }))
                        {
                            _workListItems = multi.Read<VMRISWorklist>().ToList();
                        }
                        return _workListItems;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);

                        return null;
                    }



                }

            });
        }

        internal bool UpdateHtmlTemplate(VMUpdateTemplate template)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    HtmlTempleForReport _template = context.HtmlTempleForReports.Where(x => x.Id == template.TemplateId).FirstOrDefault();

                    if (_template != null)
                    {
                        _template.TemplateContent = template.TemplateContent;


                        context.Entry(_template).State = EntityState.Modified;
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

        internal bool UpdateRISWorklist(RISWorkList workList)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    context.Entry(workList).State = EntityState.Modified;
                    context.SaveChanges();
                    return true;

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return false;
                }

            }
        }

        internal DataSet GetAllUnAssignedWorkDataSet()
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                DataTable dataTable = new DataTable();
                DbConnection connection = context.Database.GetDbConnection();
                DbProviderFactory dbFactory = DbProviderFactories.GetFactory(connection);
                using (var cmd = dbFactory.CreateCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"Select wl.*, t.Name as HospitalName,p.HISProcDescription as    ProcedureHISName,rc.Name as ConsultantName from RISWorkLists wl inner
                              join Tenants t on wl.TenantId = t.TenantId Left
                              join [Procedures] p on UPPER(wl.ProcedureName) = UPPER(p.ModalityProcDescription)
                              Left Join ReportConsultants rc on wl.ConsultantId = rc.RCId where UPPER(wl.Status)= 'UNASSIGNED'";

                    using (DbDataAdapter adapter = dbFactory.CreateDataAdapter())
                    {
                        adapter.SelectCommand = cmd;
                        adapter.Fill(dataTable);
                    }
                }

                DataSet ds = new DataSet();
                ds.Tables.Add(dataTable);
                return ds;
            }
        }

        internal async Task<List<ShortCutKey>> GetShortcutKeyList(int consultantId)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    return await context.ShortCutKeys.Where(x => x.Rcid == consultantId).ToListAsync();

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return null;
                }

            }
        }

        internal async Task<RISWorkList> GetRISWorklist(int procId)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    return await context.RISWorkLists.Where(x => x.ProcId == procId).FirstOrDefaultAsync();

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return null;
                }

            }
        }

        internal async Task<List<HtmlTempleForReport>> GetHtmlTemplateForReport(int consultantId)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    return await context.HtmlTempleForReports.Where(x => x.RCId == consultantId).ToListAsync();

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return null;
                }

            }
        }

        internal async Task<ReportConsultant> GetReportConsultant(int ConsultantId)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    return await context.ReportConsultants.Where(x => x.Rcid == ConsultantId).FirstOrDefaultAsync();

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return null;
                }

            }
        }

        internal async Task<MasterTemplate> GetWordMasterTemplateContent()
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    return await context.MasterTemplates.FirstOrDefaultAsync();

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return null;
                }

            }
        }

        internal bool SaveNewHtmlTempalate(HtmlTempleForReport template)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {

                    context.HtmlTempleForReports.Add(template);
                    context.SaveChanges();
                    return true;

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return false;
                }

            }
        }

        internal bool UpdateHtmlTempalate(HtmlTempleForReport template)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    context.Entry(template).State = EntityState.Modified;
                    context.SaveChanges();


                    return true;

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return false;
                }
            }
        }

        internal bool DeleteHtmlTemplate(int templateId)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    context.Database.ExecuteSqlRaw(@"Delete from HtmlTempleForReports Where Id={0}", new object[] { templateId });
                    return true;

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return false;
                }

            }
        }

        internal bool SavePrintPageSetup(PrintPageSetup pagesetup)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {

                    context.PrintPageSetups.Add(pagesetup);
                    context.SaveChanges();
                    return true;

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return false;
                }

            }
        }

        internal bool AddToReferral(ReferralPhysician physician)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    context.ReferralPhysicians.Add(physician);
                    context.SaveChanges();
                    return true;

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return false;
                }

            }
        }

        internal async Task<List<ReferralPhysician>> GetTenantPhysicianList(int tenantId)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    return await context.ReferralPhysicians.Where(x => x.TenantId == tenantId).ToListAsync();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return null;
                }

            }
        }

        internal async Task<PrintPageSetup> GetPrintPageSetup(int tenantId)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                return await context.PrintPageSetups.Where(x => x.TenantId == tenantId).FirstOrDefaultAsync();
            }
        }

        internal bool UpdateProcedureStatus(VMProcIdAndStatus procstate)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    RISWorkList wlObj = context.RISWorkLists.Where(x => x.ProcId == procstate.ProcId).FirstOrDefault();
                    if (wlObj != null)
                    {
                        wlObj.Status = procstate.Status;
                        context.Entry(wlObj).State = EntityState.Modified;
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

        internal bool UpdateConsultantOpinionOnStudy(ConsultantOpinionOnStudy opinion)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    RISWorkList _wList = context.RISWorkLists.Where(x => x.ProcId == opinion.ProcId).FirstOrDefault();
                    if (_wList != null)
                    {
                        _wList.Status = 6;

                        context.Entry(_wList).State = EntityState.Modified;
                        context.SaveChanges();

                    }

                    context.Entry(opinion).State = EntityState.Modified;
                    context.SaveChanges();
                    return true;

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return false;
                }

            }
        }

        internal ConsultantOpinionOnStudy GetReportConsultantOpinionOnStudy(int ProcTd, int ConsultantId)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                return context.ConsultantOpinionOnStudies.Where(x => x.ProcId == ProcTd && x.RCId == ConsultantId).FirstOrDefault();
            }
        }
        internal bool SaveConsultantOpinionOnStudy(ConsultantOpinionOnStudy opinion)
        {

            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    context.ConsultantOpinionOnStudies.Add(opinion);
                    context.SaveChanges();


                    if (opinion.isReportComplete)
                    {

                        RISWorkList _workList = context.RISWorkLists.Where(x => x.ProcId == opinion.ProcId).FirstOrDefault();
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

        internal HismodalityProcedureMapping GetHISModaliProcedureMapObj(int tenantId, int pId)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                return context.HismodalityProcedureMappings.Where(x => x.TenantId == tenantId && x.Pid == pId).FirstOrDefault();
            }
        }

        internal List<Hisprocedure> GetHISProcedures(string modality)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                return context.Hisprocedures.Where(x => x.Modality.ToUpper().Equals(modality.ToUpper())).ToList();
            }
        }

        internal async Task<List<VMRISWorklist>> GetCompletedWorklists(DateTime datefrm, DateTime dateto, int roleId, int tenantId, int consultantId, string status)
        {

            return await Task.Run(() =>
            {
                using (CoreDbContext context = new CoreDbContext())
                {
                    try
                    {
                        return context.VMRISWorklists.FromSqlRaw<VMRISWorklist>(@"Exec spGetCompletedWorklistItems @datefrm, @dateto, @roleId,@TenantId, @ConsultantId, @Status", new SqlParameter("@datefrm", datefrm.Date), new SqlParameter("@dateto", dateto.Date), new SqlParameter("@roleId", roleId), new SqlParameter("@TenantId", tenantId), new SqlParameter("@ConsultantId", consultantId), new SqlParameter("@Status", status)).ToList();

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                        return null;
                    }



                }

            });
        }

        internal IList<ProcedureRadiologistTemplate> GetAllTemplate()
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                return context.ProcedureRadiologistTemplates.ToList();
            }
        }

        internal async Task<List<RISWorkList>> GetIncompletedWorkList()
        {

            return await Task.Run(() =>
            {

                using (CoreDbContext context = new CoreDbContext())
                {
                    return context.RISWorkLists.ToList();

                }

            });
        }


        internal IList<ProcedureRadiologistTemplate> GetTemplateByRadiologist(string radiologistId)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                int _rcId = 0; int.TryParse(radiologistId, out _rcId);
                return context.ProcedureRadiologistTemplates.Where(x => x.Rcid == _rcId).ToList();
            }
        }
        internal bool UpdateWorklist(RISWorkList wlObj)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    context.Entry(wlObj).State = EntityState.Modified;
                    context.SaveChanges();
                    return true;

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return false;
                }
            }
        }

        internal bool UpdateTenant(Tenant tnt)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    context.Entry(tnt).State = EntityState.Modified;
                    context.SaveChanges();
                    return true;

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return false;
                }
            }
        }

        internal bool CreateTemplate(ProcedureRadiologistTemplate templatemap)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    context.ProcedureRadiologistTemplates.Add(templatemap);
                    context.SaveChanges();
                    return true;

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return false;
                }
            }
        }

        internal async Task<List<VMRISWorklist>> GetAlldWorklists(DateTime datefrm, DateTime dateto, int roleId, int TenantId, int ConsultantId, string status)
        {
            return await Task.Run(async () =>
            {

                using (var conn = new SqlConnection(_context.ConnectionString))
                {
                    List<VMRISWorklist> _workListItems;
                    try
                    {

                        using (var multi = await conn.QueryMultipleAsync("Exec spGetInCompleteWorklistItems @datefrm, @dateto, @roleId,@TenantId, @ConsultantId, @Status", new { datefrm = datefrm, dateto = dateto, roleId = roleId, TenantId = TenantId, ConsultantId = ConsultantId, status = status }))
                        {
                            _workListItems = multi.Read<VMRISWorklist>().ToList();
                        }
                        return _workListItems;

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                        return null;
                    }
                }




            });
        }


        internal async Task<List<VMRISWorklist>> GetOnePagedWorklists(DateTime datefrm, DateTime dateto, int roleId, int TenantId, int ConsultantId, string status, int PageNo, int RecsPerPage)
        {
            return await Task.Run(async () =>
            {

                //using (var conn = new SqlConnection(_context.ConnectionString))
                using (var conn = _context.Database.GetDbConnection())
                {
                    List<VMRISWorklist> _workListItems;
                    try
                    {
                        using (var multi = await conn.QueryMultipleAsync("Exec spGetInCompleteWorklistItems " +
                            "@datefrm, @dateto, @roleId,@TenantId, @ConsultantId, @Status, @PageNo,  @RecsPerPage"
                              , new { datefrm = datefrm, dateto = dateto, roleId = roleId, TenantId = TenantId, ConsultantId = ConsultantId, Status = status, PageNo = PageNo, RecsPerPage = RecsPerPage }))
                        {
                            _workListItems = multi.Read<VMRISWorklist>().ToList();
                        }
                        return _workListItems;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return null;
                    }



                }

            });
        }

        internal async Task<List<VMRISWorklist>> GetSearchFilterIncompleteOnePageItems(DateTime datefrm, DateTime dateto, int roleId, int TenantId, int ConsultantId, string status, string SearchFilter, int PageNo, int RecsPerPage)
        {
            //string _searchFilter;
            //if (SearchFilter == "ww")
            //{
            //    _searchFilter = "s";
            //}
            //else { 
            //    _searchFilter = SearchFilter; 
            //}

            return await Task.Run(async () =>
            {
                List<VMRISWorklist> _workListItems;
                using (var conn = _context.Database.GetDbConnection())
                {
                    try
                    {
                        using (var multi = await conn.QueryMultipleAsync("Exec spGetInCompleteWorklistItems " +
                            "@datefrm, @dateto, @roleId, @TenantId, @ConsultantId, @Status, @SearchFilter, @PageNo,  @_RecsPerPage"
                              , new { datefrm = datefrm, dateto = dateto, roleId = roleId, TenantId = TenantId, ConsultantId = ConsultantId, Status = status, SearchFilter = SearchFilter, PageNo = PageNo, _RecsPerPage = RecsPerPage }))
                        {
                            _workListItems = multi.Read<VMRISWorklist>().ToList();
                        }
                        return _workListItems;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return null;
                    }
                }

            });
        }

        internal async Task<List<VWNextCloudUser>> GetSearchFilterIncompleteOnePageUserItems(string GroupName, string SearchFilter, int PageNo, int RecsPerPage)
        {
            return await Task.Run(async () =>
            {
                List<VWNextCloudUser> _userListItems;

                string nextCloud = string.Format(@"(SELECT RISWorkLists.ProcId, ReportConsultants.GroupName, ReportConsultants.RadNextCloudID, RISWorkLists.Share_Id, RISWorkLists.StudyInstanceUid FROM ReportConsultants INNER JOIN RISWorkLists ON RISWorkLists.ConsultantId = ReportConsultants.RCId)as[a]");

                using (var conn = _context.Database.GetDbConnection())
                {
                    try
                    {
                        if (GroupName == null)
                        {
                            sql = string.Format(@"SELECT * FROM " + nextCloud + " WHERE a.StudyInstanceUid LIKE '%" + SearchFilter + "%' AND a.ProcId <=" + PageNo + "*" + RecsPerPage + " AND a.ProcId > " + (PageNo - 1) + "*" + RecsPerPage);
                        }
                        else
                        {
                            sql = string.Format(@"SELECT * FROM " + nextCloud + " WHERE a.StudyInstanceUid LIKE '%" + SearchFilter + "%' AND a.GroupName = '" + GroupName + "' AND a.ProcId <=" + PageNo + "*" + RecsPerPage + "AND a.ProcId > " + (PageNo - 1) + "*" + RecsPerPage);
                        }
                        using (var multiUserResult = await conn.QueryMultipleAsync(sql))
                        {
                            _userListItems = multiUserResult.Read<VWNextCloudUser>().ToList();
                        }
                        return _userListItems;

                    }

                    //try
                    //{
                    //    using (var multi = await conn.QueryMultipleAsync("Exec spGetUserListItems_Paging_Filtering " +
                    //        "@SearchFilter, @PageNo,  @RecsPerPage, @GroupName"
                    //          , new {GroupName = GroupName, SearchFilter = SearchFilter, PageNo = PageNo, RecsPerPage = RecsPerPage }))
                    //    {
                    //        _userListItems = multi.Read<NextCloudUser>().ToList();
                    //    }
                    //    return _userListItems;

                    //}

                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return null;
                    }



                }

            });
        }


        internal async Task<int> GetIncompleteItemCount(DateTime datefrm, DateTime dateto, int roleId, int TenantId, int ConsultantId, string status, string SearchFilter)
        {
            return await Task.Run(async () =>
            {

                List<int> _workListItems;

                using (var conn = _context.Database.GetDbConnection())
                {
                    try
                    {
                        using (var multi = await conn.QueryMultipleAsync("Exec spGetInCompleteWorklistItems_Cnt " +
                            "@datefrm, @dateto, @roleId,@TenantId, @ConsultantId, @Status, @SearchFilter"
                              , new { datefrm = datefrm, dateto = dateto, roleId = roleId, TenantId = TenantId, ConsultantId = ConsultantId, Status = status, SearchFilter = SearchFilter }))
                        {
                            _workListItems = multi.Read<int>().ToList();
                        }

                        return _workListItems[0];


                        //var resList = context.Database.SqlQuery<int>(@"Exec spGetInCompleteWorklistItems_Cnt @datefrm, @dateto, @roleId,@TenantId, @ConsultantId, @Status,@SearchFilter", new SqlParameter("@datefrm", datefrm), new SqlParameter("@dateto", dateto), new SqlParameter("@roleId", roleId), new SqlParameter("@TenantId", TenantId), new SqlParameter("@ConsultantId", ConsultantId), new SqlParameter("@Status", status), new SqlParameter("@SearchFilter", SearchFilter)).ToList();
                        //return resList[0];

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return -1;
                    }



                }
            });

        }

        internal async Task<int> GetUserCount(string GroupName, string SearchFilter)
        {
            return await Task.Run(async () =>
            {

                int _userListItems;
                string nextCloud = string.Format(@"(SELECT RISWorkLists.ProcId, ReportConsultants.GroupName, ReportConsultants.RadNextCloudID, RISWorkLists.Share_Id, RISWorkLists.StudyInstanceUid FROM ReportConsultants INNER JOIN RISWorkLists ON RISWorkLists.ConsultantId = ReportConsultants.RCId)as[a]");


                using (var conn = _context.Database.GetDbConnection())
                {
                    try
                    {
                        if (GroupName == null)
                        {
                            sql = string.Format(@"SELECT * FROM " + nextCloud + " WHERE a.StudyInstanceUid LIKE '%" + SearchFilter + "%'");
                        }
                        else
                        {
                            sql = string.Format(@"SELECT * FROM NextCloudUser WHERE a.StudyInstanceUid LIKE '%" + SearchFilter + "%' AND a.GroupName='" + GroupName + "'");
                        }
                        using (var multiUserResult = await conn.QueryMultipleAsync(sql))
                        {
                            _userListItems = multiUserResult.Read<VWNextCloudUser>().ToList().Count;
                        }
                        return _userListItems;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);

                        return -1;
                    }



                }
            });

        }

        internal void SaveDefaultTenantRcMapping(List<TenantDefaultConsultantMapping> mapList)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                context.TenantDefaultConsultantMappings.AddRange(mapList);
                context.SaveChanges();
            }
        }

        internal async Task<List<Modality>> GetAllowedModalities()
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                return await context.Modalities.ToListAsync();
            }
        }

        internal void DeleteExistingAllowedGroups(int rCId)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                context.Database.ExecuteSqlRaw(@"Delete from AllowedModalities Where RCId={0}", new object[] { rCId });

            }
        }

        internal async Task<List<VMReportConsultant>> GetReportConsultants()
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {

                    return await context.VMReportConsultants.FromSqlRaw<VMReportConsultant>(@"Select RCId,Name,RadNextCloudID from ReportConsultants").ToListAsync(); ;


                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return null;
                }

            }
        }

        internal void SaveAllowedModalities(List<AllowedModality> rcMList)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                context.AllowedModalities.AddRange(rcMList);
                context.SaveChanges();
            }
        }

        internal bool AssignedToRadiologist(List<SelectedProcedureForAssign> selectedWorklists)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    foreach (var item in selectedWorklists)
                    {
                        RISWorkList _wListObj = context.RISWorkLists.Where(x => x.ProcId == item.ProcId).FirstOrDefault();
                        if (_wListObj != null)
                        {

                            HttpClient client;
                            using (client = new HttpClient())
                            {
                                client.BaseAddress = new Uri("https://test.emslbd.com"); //new Uri("http://115.69.214.82:8080");  
                                client.DefaultRequestHeaders.Accept.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                var username = "Administrator";
                                var password = "Admin@)@!emslbd";

                                string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                                       .GetBytes(username + ":" + password));
                                client.DefaultRequestHeaders.Add("OCS-APIRequest", "true");
                                client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);

                                //create share
                                var folder = _wListObj.StudyInstanceUid;
                                //int _tenantId = _wListObj.TenantId;

                                var expire = DateTime.Now.AddDays(7);
                                var expireDate = expire.Year + "-" + expire.Month + "-" + expire.Day;

                                var task = Task.Run(async () => await client.PostAsync("/ocs/v2.php/apps/files_sharing/api/v1/shares?path=TESTDICOMFILES/Incoming/73/" + folder + "&shareType=0&shareWith=" + item.RadNextCloudID + "&expireDate=" + expireDate, new StringContent("")));

                                HttpResponseMessage response = task.GetAwaiter().GetResult();
                                string jsonStr = Task.Run(async () => await response.Content.ReadAsStringAsync()).GetAwaiter().GetResult();

                                if (response.IsSuccessStatusCode)
                                {

                                    JObject o = JObject.Parse(jsonStr);

                                    string _idval = (string)o.SelectToken("ocs.data.id");

                                    _wListObj.ConsultantId = item.ConsultantID;
                                    _wListObj.Status = item.Status;
                                    _wListObj.Share_Id = Int32.Parse(_idval);
                                    context.Entry(_wListObj).State = EntityState.Modified;
                                    context.SaveChanges();
                                }

                            }
                        }
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

        internal bool AssignedToUser(string fileName, string userName)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    RISWorkList _risworklist = context.RISWorkLists.Where(x => x.StudyInstanceUid == fileName).FirstOrDefault();

                    if (_risworklist != null)
                    {
                        HttpClient client;
                        using (client = new HttpClient())
                        {
                            client.BaseAddress = new Uri("https://cloud.emslbd.com"); //new Uri("http://115.69.214.82:8080");  
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                            var username = "Administrator";
                            var password = "Emsl#Ris^Bd@Dec2021";

                            string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                                    .GetBytes(username + ":" + password));
                            client.DefaultRequestHeaders.Add("OCS-APIRequest", "true");
                            client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);

                            var directory = _risworklist.TenantId;

                            var expire = DateTime.Now.AddDays(7);
                            var expireDate = expire.Year + "-" + expire.Month + "-" + expire.Day;

                            var task = Task.Run(async () => await client.PostAsync("/ocs/v2.php/apps/files_sharing/api/v1/shares?path=DICOMFILES/Incoming/" + directory + "/" + fileName + "&shareType=0&shareWith=" + userName + "&expireDate=" + expireDate, new StringContent("")));

                            HttpResponseMessage response = task.GetAwaiter().GetResult();
                            string jsonStr = Task.Run(async () => await response.Content.ReadAsStringAsync()).GetAwaiter().GetResult();

                            if (response.IsSuccessStatusCode)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }

                        }

                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return false;
                }
            }
        }

        internal bool CancelAssignedToRadiologist(VMRISWorklist selectedWorklists)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    RISWorkList _wListObj = context.RISWorkLists.Where(x => x.ProcId == selectedWorklists.ProcId).FirstOrDefault();
                    if (_wListObj != null)
                    {
                        HttpClient client;
                        using (client = new HttpClient())
                        {
                            client.BaseAddress = new Uri("https://test.emslbd.com"); //new Uri("http://115.69.214.82:8080");  
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                            var username = "Administrator";
                            var password = "Admin@)@!emslbd";

                            string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                                    .GetBytes(username + ":" + password));
                            client.DefaultRequestHeaders.Add("OCS-APIRequest", "true");
                            client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);

                            var task = Task.Run(async () => await client.DeleteAsync("/ocs/v2.php/apps/files_sharing/api/v1/shares/" + _wListObj.Share_Id));
                            HttpResponseMessage response = task.GetAwaiter().GetResult();
                            if (response.IsSuccessStatusCode || !response.IsSuccessStatusCode)
                            {

                                _wListObj.ConsultantId = selectedWorklists.ConsultantId;
                                _wListObj.Status = selectedWorklists.Status;
                                _wListObj.Share_Id = null;
                                context.Entry(_wListObj).State = EntityState.Modified;
                                context.SaveChanges();
                            }
                        }


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

        internal ProcedureRadiologistTemplate GetWordTemplateContent(int templateId)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                return context.ProcedureRadiologistTemplates.Where(x => x.Id == templateId).FirstOrDefault();

            }
        }

        internal bool SaveProcedure(HismodalityProcedureMapping procObj)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    context.HismodalityProcedureMappings.Add(procObj);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return false;
                }
            }
        }

        internal void DeleteTenantRcMapping(int id)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                context.Database.ExecuteSqlRaw(@"Delete from TenantDefaultConsultantMappings Where Id={0}", new object[] { id });
            }
        }
        internal List<VMTenantRadiologistMapping> GetTenantRCMappingList(int tenantId)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    return context.VMTenantRadiologistMappings.FromSqlRaw<VMTenantRadiologistMapping>(@"Select tds.Id, t.TenantId,t.Name, rc.RCId, rc.Name as ConsultantName,tds.Modality from Tenants t Left join TenantDefaultConsultantMappings tds on t.TenantId=tds.TenantId Left Join ReportConsultants rc on tds.RCId=rc.RCId  where t.TenantId=@tenantId", new SqlParameter("@tenantId", tenantId)).ToList();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return null;
                }
            }
        }

        internal bool UpdateProcedure(HismodalityProcedureMapping upprocObj)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    context.Entry(upprocObj).State = EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return false;
                }
            }
        }

        internal Tenant GetTenant(int tenantId)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                return context.Tenants.Where(x => x.TenantId == tenantId).FirstOrDefault();
            }
        }

        internal async Task<List<Tenant>> GetAllTenants()
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                return await context.Tenants.ToListAsync();
            }
        }

        internal List<VMHISModalityProcedureMapping> GetAllHISModalityProcedures()
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                return context.VMHISModalityProcedureMappings.FromSqlRaw<VMHISModalityProcedureMapping>(@"SELECT        dbo.HISModalityProcedureMappings.*, dbo.Tenants.Name as HospitalName, dbo.HISProcedures.ProcName as HISProcedureName,dbo.HISProcedures.Modality 
        FROM            dbo.HISModalityProcedureMappings INNER JOIN
                                 dbo.Tenants ON dbo.HISModalityProcedureMappings.TenantId = dbo.Tenants.TenantId INNER JOIN
                                 dbo.HISProcedures ON dbo.HISModalityProcedureMappings.PId = dbo.HISProcedures.PId").ToList();
            }
        }

        internal async Task<int> CreateRole(Role role)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    context.Roles.Add(role);
                    context.SaveChanges();
                    return role.RoleId;

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return 0;
                }
            }
        }

        internal Role GetRoleByName(string roleName)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                return context.Roles.Where(x => x.Name.ToUpper() == roleName.ToUpper()).FirstOrDefault();
            }
        }

        internal async Task<List<Role>> GetRole()
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                return await context.Roles.ToListAsync();
            }
        }

        internal void GrantMenuPermission(List<int> selectedModules, int roleId)
        {
            List<MenuPermission> _permissionList = new List<MenuPermission>();
            foreach (int _menuId in selectedModules)
            {
                MenuPermission _mp = new MenuPermission();
                _mp.MenuId = _menuId;
                _mp.RoleId = roleId;
                _mp.IsPermissionGranted = true;

                _permissionList.Add(_mp);
            }

            using (CoreDbContext context = new CoreDbContext())
            {
                context.Database.ExecuteSqlRaw(@"Delete from MenuPermissions Where RoleId={0}", new object[] { roleId });

                context.MenuPermissions.AddRange(_permissionList);
                context.SaveChanges();
            }
        }

        internal User GetUserByConsultant(int consultantId)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                return context.Users.Where(x => x.Rcid == consultantId).FirstOrDefault();
            }
        }

        internal async Task<User> GetUserById(int userId)
        {
            try
            {
                return await _context.Users.Where(x => x.UserId == userId).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }

        }

        //internal async Task<User> GetNextCloudUserList(int userId)
        //{
        //    try
        //    {
        //        return await _context.Users.Where(x => x.UserId == userId).FirstOrDefaultAsync();

        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }

        //}

        internal void UpdateUser(User user)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        internal List<MenuPermission> GetPermittedMenusByRoleId(int roleId)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                return context.MenuPermissions.Where(x => x.RoleId == roleId).ToList();
            }
        }

        internal bool SaveTenant(Tenant tenant)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    context.Tenants.Add(tenant);
                    context.SaveChanges();
                    return true;

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return false;
                }
            }
        }

        internal bool CreateNewRemoteNode(RemoteDicomNode newNode)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    context.RemoteDicomNodes.Add(newNode);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return false;
                }
            }
        }

        internal int CreateUser(User user)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
                return user.UserId;
            }
        }

        internal async Task<IList<VMUserDetail>> GetUserDetails()
        {
            IList<VMUserDetail> val = await _context.VMUserDetails.FromSqlRaw<VMUserDetail>(@"Select u.UserId,u.LoginName,u.CloudUserName,u.FullName,u.CloudAccessLink,u.cloudPassword,u.MobileNo,u.RoleId,r.Name as RoleName,u.RCId,u.TenantId,u.Status,u.Comments,u.IsAssignToRadAllow,u.IsMainViewerAlloted,u.IsReportWriteAllow,u.IsReportViewAllow from Users u Join Roles r on u.RoleId = r.RoleId").ToListAsync();
            return val;
        }

        internal bool MapUserWithRole(UserRole urole)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                context.UserRoles.Add(urole);
                context.SaveChanges();
                return true;
            }
        }

        internal bool IsLoginNameExists(string logiName)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                User _user = context.Users.Where(x => x.LoginName.ToUpper().Equals(logiName.ToUpper())).FirstOrDefault();

                if (_user == null) return false;

                return true;
            }
        }

        internal List<VMRemoteDicomNode> GetAllRemoteDicomNodes()
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                return context.VMRemoteDicomNodes.FromSqlRaw<VMRemoteDicomNode>(@"Select n.*,t.TenantId,t.Name as TenantName from RemoteDicomNodes n join Tenants t on n.TenantId=t.TenantId").ToList();
            }
        }
        internal bool UpdateRemoteNode(RemoteDicomNode _node)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    context.Entry(_node).State = EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal RemoteDicomNode GetNodeWithSameAet(string aet)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                return context.RemoteDicomNodes.Where(x => x.NodeAet.ToUpper().Equals(aet)).FirstOrDefault();
            }
        }

        public LoginUser GetUserByName(string userName)
        {
            using (var conn = _context.Database.GetDbConnection())
            {
                try
                {
                    string strQuery = string.Format(@"Select * from Users where  LoginName like '{0}'", userName); ;

                    List<User> _userListItems;
                    using (var multiUserResult = conn.QueryMultiple(strQuery))
                    {
                        _userListItems = multiUserResult.Read<User>().ToList();
                    }

                    var anom = _userListItems[0];
                    if (anom == null) return null;

                    strQuery = string.Format(@"Select * from Roles where  RoleId = {0}", anom.RoleId); ;
                    List<Role> _roleListItems;
                    using (var multiUserResult = conn.QueryMultiple(strQuery))
                    {
                        _roleListItems = multiUserResult.Read<Role>().ToList();
                    }
                    Role role = _roleListItems[0];

                    return new LoginUser()
                    {
                        UserId = anom.UserId,
                        RoleId = anom.RoleId.GetValueOrDefault(),
                        Name = anom.LoginName,
                        Password = anom.Password,
                        Salt = anom.Salt,
                        RoleName = role.Name,
                        IsAssignToRadAllow = anom.IsAssignToRadAllow,
                        IsReportViewAllow = anom.IsReportViewAllow,
                        IsReportWriteAllow = anom.IsReportWriteAllow,
                        IsMainViewerAlloted = anom.IsMainViewerAlloted,
                        ReportCreateLocation = anom.ReportCreateLocation
                    };

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return null;
                }

            }

        }

        internal List<AllowedModality> GetAllowedModalities(int rCId)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                return context.AllowedModalities.Where(x => x.Rcid == rCId).ToList();
            }
        }

        internal List<ProjectMenu> GetPermittedMenusByUser(int userId)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                sql = string.Format(@"Select * from ProjectMenus Where MenuId in (select  m.MenuId from ProjectMenus m join  MenuPermissions mp on  m.MenuId=mp.MenuId
                           join UserRoles r on mp.RoleId=r.RoleId  where  m.IsActive=1 and   r.UserId={0})", userId);

                return context.ProjectMenus.FromSqlRaw(sql).ToList();
            }
        }

        private Role GetRoleById(int _roleId)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                return context.Roles.Where(x => x.RoleId == _roleId).FirstOrDefault();
            }
        }

        public void FillPermissionData(LoginUser user)
        {
            using (CoreDbContext context = new CoreDbContext())
            {

                var menus = context.ProjectMenus.FromSqlRaw(@"SELECT pm.* FROM [dbo].[Roles] r INNER JOIN [dbo].[MenuPermissions] mp ON r.RoleId = mp.RoleId 
                             INNER JOIN ProjectMenus pm on mp.MenuId = pm.MenuId where r.RoleId = @p0", user.RoleId).ToList();

                menus.ForEach(x => user.Menus.Add(x));


            }
        }

        internal async Task<List<ProjectMenu>> GetAllMenus()
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                return await context.ProjectMenus.ToListAsync();
            }
        }

        internal async Task<List<string>> GetGroupName()
        {
            using (var conn = _context.Database.GetDbConnection())
            {
                try
                {
                    HttpClient client;
                    using (client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://test.emslbd.com"); //new Uri("http://115.69.214.82:8080");  
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var username = "Administrator";
                        var password = "Admin@)@!emslbd";

                        string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                                .GetBytes(username + ":" + password));
                        client.DefaultRequestHeaders.Add("OCS-APIRequest", "true");
                        client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);

                        var task = await Task.Run(async () => await client.GetAsync("/ocs/v1.php/cloud/groups"));

                        string jsonStr = Task.Run(async () => await task.Content.ReadAsStringAsync()).GetAwaiter().GetResult();

                        if (task.IsSuccessStatusCode)
                        {
                            JObject o = JObject.Parse(jsonStr);
                            List<string> _group = o.SelectToken("ocs.data.groups")?.ToObject<List<string>>();
                            return _group;
                        }
                        else
                        {
                            return null;
                        }

                    }

                }
                //    string nextCloud = string.Format(@"(SELECT RISWorkLists.ProcId, ReportConsultants.GroupName, ReportConsultants.RadNextCloudID, RISWorkLists.Share_Id, RISWorkLists.StudyInstanceUid FROM ReportConsultants INNER JOIN RISWorkLists ON RISWorkLists.ConsultantId = ReportConsultants.RCId)as[a]");

                //    sql = string.Format(@"Select GroupName From " + nextCloud + " Group by a.GroupName"); 

                //    using (var multiUserResult = await conn.QueryMultipleAsync(sql))
                //    {
                //        _userListItems = multiUserResult.Read<VWNextCloudUser>().ToList();
                //    }

                //    return _userListItems;
                //}

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return null;
                }

            }
        }

        internal async void SetGroupName()
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    //List<string> cloudIds = new List<string>(new string[] { "drazim", "drakter", "dram", "drazim"});
                    //cloudIds = _context.ReportConsultants.Select(p => p.RadNextCloudID != null).;
                    //ReportConsultant t = context.ReportConsultants.Where(x => x.RadNextCloudID == "Radiologist").FirstOrDefault();
                    List<string> cloudIds = new List<string>();

                    foreach (var item in context.ReportConsultants)
                    {
                        cloudIds.Add(item.RadNextCloudID);
                    }

                    foreach (string cloudId in cloudIds)
                    {
                        if (cloudId == null || cloudId == "")
                        {
                            continue;
                        }
                        else
                        {
                            HttpClient client;
                            using (client = new HttpClient())
                            {
                                client.BaseAddress = new Uri("https://test.emslbd.com"); //new Uri("http://115.69.214.82:8080");  
                                client.DefaultRequestHeaders.Accept.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                var username = "Administrator";
                                var password = "Admin@)@!emslbd";

                                string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                                        .GetBytes(username + ":" + password));
                                client.DefaultRequestHeaders.Add("OCS-APIRequest", "true");
                                client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);

                                var task = await Task.Run(async () => await client.GetAsync("/ocs/v1.php/cloud/users/" + cloudId));

                                string jsonStr = Task.Run(async () => await task.Content.ReadAsStringAsync()).GetAwaiter().GetResult();

                                if (task.IsSuccessStatusCode)
                                {
                                    JObject o = JObject.Parse(jsonStr);
                                    string _group = (string)o.SelectToken("ocs.data.groups[0]");
                                    ReportConsultant _report = context.ReportConsultants.Where(x => x.RadNextCloudID == cloudId).FirstOrDefault();
                                    context.SaveChanges();

                                }
                            }
                        }


                    }

                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

        }

        internal async Task<List<string>> GetUserName()
        {
            using (var conn = _context.Database.GetDbConnection())
            {
                try
                {
                    HttpClient client;
                    using (client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://test.emslbd.com"); //new Uri("http://115.69.214.82:8080");  
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var username = "Administrator";
                        var password = "Admin@)@!emslbd";

                        string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                                .GetBytes(username + ":" + password));
                        client.DefaultRequestHeaders.Add("OCS-APIRequest", "true");
                        client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);

                        var task = await Task.Run(async () => await client.GetAsync("/ocs/v1.php/cloud/users"));

                        string jsonStr = Task.Run(async () => await task.Content.ReadAsStringAsync()).GetAwaiter().GetResult();

                        if (task.IsSuccessStatusCode)
                        {
                            JObject o = JObject.Parse(jsonStr);
                            List<string> _userList = o.SelectToken("ocs.data.users")?.ToObject<List<string>>();
                            return _userList;
                        }
                        else
                        {
                            return null;
                        }

                    }

                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return null;
                }

            }

        }
        internal async Task<List<string>> GetUserNameOfGroup(string groupName)
        {
            using (var conn = _context.Database.GetDbConnection())
            {
                try
                {
                    HttpClient client;
                    using (client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://test.emslbd.com"); //new Uri("http://115.69.214.82:8080");  
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var username = "Administrator";
                        var password = "Admin@)@!emslbd";

                        string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                                .GetBytes(username + ":" + password));
                        client.DefaultRequestHeaders.Add("OCS-APIRequest", "true");
                        client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);

                        var task = await Task.Run(async () => await client.GetAsync("/ocs/v1.php/cloud/groups/" + groupName));

                        string jsonStr = Task.Run(async () => await task.Content.ReadAsStringAsync()).GetAwaiter().GetResult();

                        if (task.IsSuccessStatusCode)
                        {
                            JObject o = JObject.Parse(jsonStr);
                            List<string> _userList = o.SelectToken("ocs.data.users")?.ToObject<List<string>>();
                            return _userList;
                        }
                        else
                        {
                            return null;
                        }

                    }

                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return null;
                }

            }

        }

        internal async Task<string> GetGroupNameOfUser(string user)
        {
            using (var conn = _context.Database.GetDbConnection())
            {
                try
                {
                    HttpClient client;
                    using (client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://test.emslbd.com"); //new Uri("http://115.69.214.82:8080");  
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var username = "Administrator";
                        var password = "Admin@)@!emslbd";

                        string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                                .GetBytes(username + ":" + password));
                        client.DefaultRequestHeaders.Add("OCS-APIRequest", "true");
                        client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);

                        var task = await Task.Run(async () => await client.GetAsync("/ocs/v1.php/cloud/users/" + user + "/groups"));

                        string jsonStr = Task.Run(async () => await task.Content.ReadAsStringAsync()).GetAwaiter().GetResult();

                        if (task.IsSuccessStatusCode)
                        {
                            JObject o = JObject.Parse(jsonStr);
                            string _group = o.SelectToken("ocs.data.groups[0]")?.ToObject<string>();
                            return _group;
                        }
                        else
                        {
                            return null;
                        }

                    }

                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return null;
                }

            }

        }

        internal async Task<List<OcsResponse>> GetUserInfo()
        {
            using (var conn = _context.Database.GetDbConnection())
            {
                try
                {
                    HttpClient client;
                    using (client = new HttpClient())
                    {
                        List<OcsResponse> ocsresponse;
                        client.BaseAddress = new Uri("https://test.emslbd.com"); //new Uri("http://115.69.214.82:8080");  
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var username = "Administrator";
                        var password = "Admin@)@!emslbd";

                        string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                                .GetBytes(username + ":" + password));
                        client.DefaultRequestHeaders.Add("OCS-APIRequest", "true");
                        client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);

                        var task = await Task.Run(async () => await client.GetAsync("/ocs/v2.php/apps/files_sharing/api/v1/shares"));
                        //OcsResponse ocs = (OcsResponse)response.G
                        string jsonStr = Task.Run(async () => await task.Content.ReadAsStringAsync()).GetAwaiter().GetResult();

                        if (task.IsSuccessStatusCode)
                        {
                            JObject o = JObject.Parse(jsonStr);
                            ocsresponse = o.SelectToken("ocs.data").ToObject<List<OcsResponse>>();
                            Console.WriteLine(ocsresponse);
                            return ocsresponse;
                        }
                        else
                        {
                            return null;
                        }

                    }

                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return null;
                }

            }

        }

        internal async Task<bool> CancelAssignedToUser(int shareId)
        {
            using (var conn = _context.Database.GetDbConnection())
            {
                try
                {
                    HttpClient client;
                    using (client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://test.emslbd.com"); //new Uri("http://115.69.214.82:8080");  
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var username = "Administrator";
                        var password = "Admin@)@!emslbd";

                        string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                                .GetBytes(username + ":" + password));
                        client.DefaultRequestHeaders.Add("OCS-APIRequest", "true");
                        client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);

                        var task = await Task.Run(async () => await client.DeleteAsync("/ocs/v2.php/apps/files_sharing/api/v1/shares/" + shareId));

                        string jsonStr = Task.Run(async () => await task.Content.ReadAsStringAsync()).GetAwaiter().GetResult();

                        if (task.IsSuccessStatusCode)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }

                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return false;
                }

            }

        }

        internal async Task<string> GetFilePathFromName(string fileName)
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    RISWorkList _risworklist = await context.RISWorkLists.Where(x => x.StudyInstanceUid == fileName).FirstOrDefaultAsync();
                    string filePath = "DICOMFILES/Incoming/" + _risworklist.TenantId + "/" + fileName;
                    return filePath;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return null;
                }
            }
        }

        internal async Task<List<MainMenuItem>> GetMenuItems()
        {
            using (CoreDbContext context = new CoreDbContext())
            {
                try
                {
                    List<MainMenuItem> menuItems = await context.MenuItems.ToListAsync();
                    var json = JsonConvert.SerializeObject(menuItems);
                    return menuItems;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return null;
                }
            }
        }

        internal bool DeleteUser(int userId)
        {
            try
            {
                using (CoreDbContext context = new CoreDbContext())
                {
                    User? user = context.Users.Find(userId);
                    if (user != null)
                    {
                        context.Users.Remove(user);
                        context.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }

        internal async Task<string> GetConsultantFromId(int id)
        {

            return await Task.Run(async () =>
            {

                using (CoreDbContext context = new CoreDbContext())
                {
                    ReportConsultant rad =await context.ReportConsultants.Where(x => x.Rcid == id).FirstOrDefaultAsync();
                    return rad.Name;
                }

            });
        }


    }
}
