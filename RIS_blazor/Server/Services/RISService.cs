using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RIS_blazor.Shared.Models;
using RIS_blazor.Server.Models;
using RIS_blazor.Shared.Models.ApiModel;
using RIS_blazor.Shared.Models.VWModels;
using RIS_blazor.Server.Repositories;
using RIS_blazor.Server.Repository.ServiceObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIS_blazor.Server.Services
{
    public class RISService
    {

        private readonly CoreDbContext _context;
        string _username;
        string _password;

        public RISService(CoreDbContext context)
        {
            _context = context;
        }


        public bool SaveOrUpdateReportConsultant(ReportConsultant consultant)
        {
            return new RISRepository(_context).SaveOrUpdateReportConsultant(consultant);
        }

        internal async Task<List<VMReportConsultant>> GetReportConsultants()
        {
            return await new RISRepository(_context).GetReportConsultants();
        }



        internal void DeleteExistingAllowedGroups(int rCId)
        {
            new RISRepository(_context).DeleteExistingAllowedGroups(rCId);
        }

        internal RISWorkList GetWorkList(int procId)
        {
            return new RISRepository(_context).GetWorkList(procId);
        }

        internal DataSet GetAllUnAssignedWorkDataSet()
        {
            return new RISRepository(_context).GetAllUnAssignedWorkDataSet();
        }

        internal async Task<List<Modality>> GetAllowedModalities()
        {
            return await new RISRepository(_context).GetAllowedModalities();
        }

        internal bool SaveHISProcedure(Hisprocedure hisProc)
        {
            return new RISRepository(_context).SaveHISProcedure(hisProc);
        }

        internal bool UpdateHISProcedure(Hisprocedure hisProc)
        {
            return new RISRepository(_context).UpdateHISProcedure(hisProc);
        }

        internal async Task<List<VMRISWorklist>> GetAlldWorklists(DateTime datefrm, DateTime dateto, int roleId, int TenantId, int ConsultantId, string status)
        {
            return await new RISRepository(_context).GetAlldWorklists(datefrm, dateto, roleId, TenantId, ConsultantId, status);
        }

        internal async Task<List<VMRISWorklist>> GetOnePagedWorklists(DateTime datefrm, DateTime dateto, int roleId, int TenantId, int ConsultantId, string status, int PageNo, int RecsPerPage)
        {
            return await new RISRepository(_context).GetOnePagedWorklists(datefrm, dateto, roleId, TenantId, ConsultantId, status, PageNo, RecsPerPage);
        }

        internal async Task<List<VMRISWorklist>> GetSearchFilterIncompleteOnePageItems(DateTime datefrm, DateTime dateto, int roleId, int TenantId, int ConsultantId, string status, string SearchFilter, int PageNo, int RecsPerPage)
        {
            return await new RISRepository(_context).GetSearchFilterIncompleteOnePageItems(datefrm, dateto, roleId, TenantId, ConsultantId, status, SearchFilter, PageNo, RecsPerPage);
        }

        internal async Task<int> GetCompletedItemCount(DateTime dateFrom, DateTime dateTo, int roleId, int tenantId, int consultantId, string status, string SearchFilter)
        {
            return await new RISRepository(_context).GetCompletedItemCount(dateFrom, dateTo, roleId, tenantId, consultantId, status, SearchFilter);
        }

        internal async Task<List<VMRISWorklist>> GetSearchFilterCompletedOnePageItems(DateTime dateFrom, DateTime dateTo, int roleId, int tenantId, int consultantId, string status, string searchFilter, int pageNo, int recsPerPage)
        {
            return await new RISRepository(_context).GetSearchFilterCompletedOnePageItems(dateFrom, dateTo, roleId, tenantId, consultantId, status, searchFilter, pageNo, recsPerPage);
        }

        internal async Task<int> GetIncompleteItemCount(DateTime datefrm, DateTime dateto, int roleId, int TenantId, int ConsultantId, string status, string SearchFilter)
        {
            return await new RISRepository(_context).GetIncompleteItemCount(datefrm, dateto, roleId, TenantId, ConsultantId, status, SearchFilter);
        }

        internal async Task<int> GetUserCount(string GroupName, string SearchFilter)
        {
            return await new RISRepository(_context).GetUserCount(GroupName, SearchFilter);
        }

        internal List<ProcedureStatus> GetAllStatuses()
        {
            return new RISRepository(_context).GetAllStatuses();
        }

        internal async Task<List<Hisprocedure>> GetAllHISProcedure()
        {
            return await new RISRepository(_context).GetAllHISProcedure();
        }

        internal bool UpdateTenant(Tenant tnt)
        {
            return new RISRepository(_context).UpdateTenant(tnt);
        }

        internal IList<ProcedureRadiologistTemplate> GetAllTemplate()
        {
            return new RISRepository(_context).GetAllTemplate();
        }

        internal bool SaveConsultantOpinionOnStudy(ConsultantOpinionOnStudy opinion)
        {
            return new RISRepository(_context).SaveConsultantOpinionOnStudy(opinion);
        }

        internal bool CancelAssignedToRadiologist(List<SelectedProcedureForAssign> selectedWorklists)
        {
            return new RISRepository(_context).CancelAssignedToRadiologist(selectedWorklists);
        }

        internal bool UpdateRISWorklist(RISWorkList workList)
        {
            return new RISRepository(_context).UpdateRISWorklist(workList);
        }

        internal List<Hisprocedure> GetHISProcedures(string modality)
        {
            return new RISRepository(_context).GetHISProcedures(modality);
        }

        internal bool UpdateHtmlTemplate(VMUpdateTemplate template)
        {
            return new RISRepository(_context).UpdateHtmlTemplate(template);
        }

        internal ConsultantOpinionOnStudy GetReportConsultantOpinionOnStudy(int ProcTd, int ConsultantId)
        {
            return new RISRepository(_context).GetReportConsultantOpinionOnStudy(ProcTd, ConsultantId);
        }

        internal bool UpdateConsultantOpinionOnStudy(ConsultantOpinionOnStudy opinion)
        {
            return new RISRepository(_context).UpdateConsultantOpinionOnStudy(opinion);
        }

        internal async Task<List<ProjectMenu>> GetAllMenus()
        {
            return await new RISRepository(_context).GetAllMenus();
        }

        internal void SaveAllowedModalities(List<AllowedModality> rcMList)
        {
            new RISRepository(_context).SaveAllowedModalities(rcMList);
        }

        internal IList<ProcedureRadiologistTemplate> GetTemplateByRadiologist(string radiologistId)
        {
            return new RISRepository(_context).GetTemplateByRadiologist(radiologistId);
        }

        internal bool UpdateWorklist(RISWorkList wlObj)
        {
            return new RISRepository(_context).UpdateWorklist(wlObj);
        }

        public bool CheckLogin(string userName, string password, out LoginUser user)
        {
            _username = userName;
            _password = password;
            user = new RISRepository(_context).GetUserByName(userName);
            if (user == null) return false;
            string passwordToCheck = HashGenerator.GenerateSlatedHash(password, user.Salt);
            bool isLoginOk = string.Compare(user.Password, passwordToCheck) == 0;
            if (!isLoginOk) return isLoginOk;
            new RISRepository(_context).FillPermissionData(user);
            return isLoginOk;
        }


        Dictionary<string, string> d;
        internal Dictionary<string, string> GetTextMacrosList(int consultantId)
        {
            d = new Dictionary<string, string>();

            string m, e;

            List<ShortCutKey> _shortKeys = new ReportService().GetShortCutKeys(consultantId);

            d.Clear();

            foreach (var item in _shortKeys)
            {
                m = item.Textmacro.ToUpper(); //convert to upper case
                e = item.Expandedtext.ToString();

                d.Add(m, e);
            }

            return d;
        }

        internal async Task<RISWorkList> GetRISWorklist(int procId)
        {
            return await new RISRepository(_context).GetRISWorklist(procId);
        }

        internal async Task<List<HtmlTempleForReport>> GetHtmlTemplateForReport(int consultantId)
        {
            return await new RISRepository(_context).GetHtmlTemplateForReport(consultantId);
        }

        internal async Task<ReportConsultant> GetReportConsultant(int ConsultantId)
        {
            return await new RISRepository(_context).GetReportConsultant(ConsultantId);
        }

        internal async Task<MasterTemplate> GetWordMasterTemplateContent()
        {
            return await new RISRepository(_context).GetWordMasterTemplateContent();
        }

        internal HismodalityProcedureMapping GetHISModaliProcedureMapObj(int tenantId, int pId)
        {
            return new RISRepository(_context).GetHISModaliProcedureMapObj(tenantId, pId);
        }

        internal async Task<List<ShortCutKey>> GetShortcutKeyList(int consultantId)
        {
            return await new RISRepository(_context).GetShortcutKeyList(consultantId);
        }

        internal async Task<List<RISWorkList>> GetIncompletedWorkList()
        {
            return await new RISRepository(_context).GetIncompletedWorkList();
        }

        internal async Task<List<Tenant>> GetAllTenants()
        {
            return await new RISRepository(_context).GetAllTenants();
        }

        internal bool SaveProcedure(HismodalityProcedureMapping procObj)
        {
            return new RISRepository(_context).SaveProcedure(procObj);
        }

        internal async Task<User> GetUserById(int userId)
        {
            return await new RISRepository(_context).GetUserById(userId);
        }

        //internal async Task<User> GetNextCloudUserLists(int userId)
        //{
        //    return await new RISRepository(_context).GetNextCloudUserLists(userId);
        //}

        internal User GetUserByConsultant(int consultantId)
        {
            return new RISRepository(_context).GetUserByConsultant(consultantId);
        }

        internal async Task<List<VMRISWorklist>> GetCompletedWorklists(DateTime datefrm, DateTime dateto, int roleId, int tenantId, int consultantId, string status)
        {
            return await new RISRepository(_context).GetCompletedWorklists(datefrm, dateto, roleId, tenantId, consultantId, status);
        }

        internal async Task<bool> CheckLogin(string userName, string password)
        {
            LoginUser _user;
            bool bRetValue = (new RISService(_context).CheckLogin(userName, password, out _user));
            //string jsonData = @"{'success': "+bRetValue.ToString()+",'token':"+token+"}";
            //string response= JObject.Parse(jsonData).ToString();
            return (bRetValue);

        }

        internal UserModel VerifyToken(string userName)
        {
            UserModel user = new UserModel();
            if (_username != "") user.username = _username;
            if (_password != "") user.password = _password;

            return user;
        }

        internal bool UpdateProcedureStatus(VMProcIdAndStatus procstate)
        {
            return new RISRepository(_context).UpdateProcedureStatus(procstate);
        }

        internal bool SaveNewHtmlTempalate(HtmlTempleForReport template)
        {
            return new RISRepository(_context).SaveNewHtmlTempalate(template);
        }

        internal bool UpdateProcedure(HismodalityProcedureMapping upprocObj)
        {
            return new RISRepository(_context).UpdateProcedure(upprocObj);
        }

        internal async Task<int> CreateRole(Role role)
        {
            return await new RISRepository(_context).CreateRole(role);
        }

        internal bool UpdateHtmlTempalate(HtmlTempleForReport template)
        {
            return new RISRepository(_context).UpdateHtmlTempalate(template);
        }

        internal bool DeleteHtmlTemplate(int templateId)
        {
            return new RISRepository(_context).DeleteHtmlTemplate(templateId);
        }

        internal bool CreateTemplate(ProcedureRadiologistTemplate templatemap)
        {
            return new RISRepository(_context).CreateTemplate(templatemap);
        }

        internal bool SaveTenant(Tenant tenant)
        {
            return new RISRepository(_context).SaveTenant(tenant);
        }

        internal bool SavePrintPageSetup(PrintPageSetup pagesetup)
        {
            return new RISRepository(_context).SavePrintPageSetup(pagesetup);
        }

        internal List<VMHISModalityProcedureMapping> GetAllHISModalityProcedures()
        {
            return new RISRepository(_context).GetAllHISModalityProcedures();
        }

        internal Task<PrintPageSetup> GetPrintPageSetup(int tenantId)
        {
            return new RISRepository(_context).GetPrintPageSetup(tenantId);
        }

        public List<ProjectMenu> GetPermittedMenusByUser(int userId)
        {
            return new RISRepository(_context).GetPermittedMenusByUser(userId);
        }

        internal Role GetRoleByName(string _roleName)
        {
            return new RISRepository(_context).GetRoleByName(_roleName);
        }

        internal void SaveDefaultTenantRcMapping(List<TenantDefaultConsultantMapping> mapList)
        {
            new RISRepository(_context).SaveDefaultTenantRcMapping(mapList);
        }

        internal bool AddToReferral(ReferralPhysician physician)
        {
            return new RISRepository(_context).AddToReferral(physician);
        }

        internal void UpdateUser(User user)
        {
            new RISRepository(_context).UpdateUser(user);
        }

        internal async Task<List<ReferralPhysician>> GetTenantPhysicianList(int tenantId)
        {
            return await new RISRepository(_context).GetTenantPhysicianList(tenantId);
        }

        internal void GrantMenuPermission(List<int> selectedModules, int roleId)
        {
            new RISRepository(_context).GrantMenuPermission(selectedModules, roleId);
        }

        internal List<AllowedModality> GetAllowedModalities(int rCId)
        {
            return new RISRepository(_context).GetAllowedModalities(rCId);
        }

        internal RemoteDicomNode GetNodeWithSameAet(string aet)
        {
            return new RISRepository(_context).GetNodeWithSameAet(aet);
        }

        internal bool CreateNewRemoteNode(RemoteDicomNode newNode)
        {
            return new RISRepository(_context).CreateNewRemoteNode(newNode);
        }

        internal bool UpdateRemoteNode(RemoteDicomNode _node)
        {
            return new RISRepository(_context).UpdateRemoteNode(_node);
        }

        internal List<VMRemoteDicomNode> GetAllRemoteDicomNodes()
        {
            return new RISRepository(_context).GetAllRemoteDicomNodes();
        }

        internal bool IsLoginNameExists(string logiName)
        {
            return new RISRepository(_context).IsLoginNameExists(logiName);
        }

        internal async Task<List<Role>> GetRoles()
        {
            return await new RISRepository(_context).GetRole();
        }

        internal int CreateUser(User user)
        {
            return new RISRepository(_context).CreateUser(user);
        }

        internal bool MapUserWithRole(UserRole urole)
        {
            return new RISRepository(_context).MapUserWithRole(urole);
        }

        internal async Task<IList<VMUserDetail>> GetUserDetails()
        {
            return await new RISRepository(_context).GetUserDetails();
        }

        internal List<MenuPermission> GetPermittedMenusByRoleId(int roleId)
        {
            return new RISRepository(_context).GetPermittedMenusByRoleId(roleId);
        }

        internal Tenant GetTenant(int tenantId)
        {
            return new RISRepository(_context).GetTenant(tenantId);
        }

        internal List<VMTenantRadiologistMapping> GetTenantRCMappingList(int tenantId)
        {
            return new RISRepository(_context).GetTenantRCMappingList(tenantId);
        }

        internal void DeleteTenantRcMapping(int Id)
        {
            new RISRepository(_context).DeleteTenantRcMapping(Id);
        }

        internal ProcedureRadiologistTemplate GetWordTemplateContent(int templateId)
        {
            return new RISRepository(_context).GetWordTemplateContent(templateId);
        }

        internal bool AssignedToRadiologist(List<SelectedProcedureForAssign> selectedWorklists)
        {
            return new RISRepository(_context).AssignedToRadiologist(selectedWorklists);
        }

        internal Task<List<string>> GetGroupName()
        {
            return new RISRepository(_context).GetGroupName();
        }
        internal void SetGroupName()
        {
            new RISRepository(_context).SetGroupName();
        }

        internal Task<List<string>> GetUserName()
        {
            return new RISRepository(_context).GetUserName();
        }
        internal Task<List<string>> GetUserNameOfGroup(string groupName)
        {
            return new RISRepository(_context).GetUserNameOfGroup(groupName);
        }

        internal Task<string> GetGroupNameOfUser(string user)
        {
            return new RISRepository(_context).GetGroupNameOfUser(user);
        }

        internal Task<List<OcsResponse>> GetUserInfo()
        {
            return new RISRepository(_context).GetUserInfo();
        }

        internal Task<bool> CancelAssignedToUser(int shareId)
        {
            return new RISRepository(_context).CancelAssignedToUser(shareId);
        }

        internal bool AssignedToUser(string fileName, string userName)
        {
            return new RISRepository(_context).AssignedToUser(fileName, userName);
        }

        internal async Task<List<MainMenuItem>> GetMenuItems()
        {
            return await new RISRepository(_context).GetMenuItems();
        }

        internal async Task<List<User>> GetAllUser()
        {
            return await new RISRepository(_context).GetAllUser();
        }
        internal bool DeleteUser(int id)
        {
            return new RISRepository(_context).DeleteUser(id);
        }
        internal async Task<string> GetConsultantFromId(int id)
        {
            return await new RISRepository(_context).GetConsultantFromId(id);
        }
    }
}
