using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RIS_blazor.Server.Models;
using RIS_blazor.Shared.Models;
using RIS_blazor.Server.Services;

namespace RIS_blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RisworkController : ControllerBase
    {
        private readonly ILogger<RisworkController> _logger;
        private readonly CoreDbContext _context;
        public RisworkController(CoreDbContext context, ILogger<RisworkController> logger)
        {
            _logger = logger;
            _context = context;

        }


        //[HttpGet("pagedata")]

        //public async Task<List<VMRISWorklist>> GetOnePageItemsFromLogin(string userName, string userPassword, DateTime dateFrom, DateTime dateTo, string _status, int PageNo, int RecsPerPage)
        //{
        //    List<VMRISWorklist> res = await (new RISPagingService(_context)).GetOnePageItemsFromLogin(userName, userPassword, dateFrom, dateTo, _status, PageNo, RecsPerPage);

        //    return res;

        //}

        //[HttpGet("GetServerDateTime")]
        //public DateTime GetServerDateTime()
        //{
        //    return Utils.GetServerDateAndTime();
        //}

        //[HttpGet("GetIncompleteItemCount")]
        //public async Task<ActionResult<int>> GetIncompleteItemCount(DateTime _dateFrom, DateTime _dateTo, int roleId, int tenantId, int consultantId, string _status, string SearchFilter = "")
        //{

        //    return Ok(await (new RISPagingService(_context)).GetIncompleteItemCount(_dateFrom, _dateTo, roleId, tenantId, consultantId, _status, SearchFilter));
        //}

        //[HttpGet("GetUserCount")]
        //public async Task<ActionResult<int>> GetUserCount(string GroupName, string SearchFilter = "")
        //{

        //    return Ok(await (new RISPagingService(_context)).GetUserCount(GroupName, SearchFilter));
        //}


        //[HttpGet("GetCompletedItemCount")]
        //public async Task<ActionResult<int>> GetCompletedItemCount(DateTime _dateFrom, DateTime _dateTo, int roleId, int tenantId, int consultantId, string _status, string SearchFilter = "")
        //{

        //    return Ok(await (new RISPagingService(_context)).GetCompletedItemCount(_dateFrom, _dateTo, roleId, tenantId, consultantId, _status, SearchFilter));
        //}


        //[HttpGet("GetOnePageItems")]
        //public async Task<ActionResult> GetOnePageItems(DateTime _dateFrom, DateTime _dateTo, int roleId, int tenantId, int consultantId, string _status, int PageNo, int RecsPerPage)
        //{
        //    List<VMRISWorklist> _workListItems = await (new RISPagingService(_context)).GetOnePageItems(_dateFrom, _dateTo, roleId, tenantId, consultantId, _status, PageNo, RecsPerPage);
        //    if (_workListItems == null)
        //        return NotFound();
        //    return Ok(_workListItems);
        //}

        //[HttpGet("GetSearchFilterIncompleteOnePageItems")]
        //public async Task<List<VMRISWorklist>> GetSearchFilterIncompleteOnePageItems(DateTime _dateFrom, DateTime _dateTo, int roleId, int tenantId, int consultantId, string _status, string SearchFilter, int PageNo, int RecsPerPage)
        //{
        //    return await (new RISService(_context)).GetSearchFilterIncompleteOnePageItems(_dateFrom, _dateTo, roleId, tenantId, consultantId, _status, SearchFilter, PageNo, RecsPerPage);
        //}

        //[HttpGet("GetSearchFilterIncompleteOnePageUserItems")]
        //public async Task<List<VWNextCloudUser>> GetSearchFilterIncompleteOnePageUserItems(string GroupName, string SearchFilter, int PageNo, int RecsPerPage)
        //{
        //    return await (new RISPagingService(_context)).GetSearchFilterIncompleteOnePageUserItems(GroupName, SearchFilter, PageNo, RecsPerPage);
        //}



        //[HttpGet("GetSearchFilterCompletedOnePageItems")]
        //public async Task<List<VMRISWorklist>> GetSearchFilterCompletedOnePageItems(DateTime _dateFrom, DateTime _dateTo, int roleId, int tenantId, int consultantId, string _status, string SearchFilter, int PageNo, int RecsPerPage)
        //{

        //    return await (new RISPagingService(_context)).GetSearchFilterCompletedOnePageItems(_dateFrom, _dateTo, roleId, tenantId, consultantId, _status, SearchFilter, PageNo, RecsPerPage);
        //}


        //[HttpPost("AssignedToRadiologist")]
        //public IActionResult AssignedToRadiologist(List<SelectedProcedureForAssign> _selectedWorklists)
        //{
        //    if (_selectedWorklists == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (new RISService(_context).AssignedToRadiologist(_selectedWorklists))
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        //[HttpPost("CreateUser")]
        //public int AssignedToRadiologist(User user)
        //{
        //    return new RISService(_context).CreateUser(user);
        //}


        //[HttpPost("CancelAssignedToRadiologist")]
        //public IActionResult CancelAssignedToRadiologist(List<SelectedProcedureForAssign> _selectedWorklists)
        //{
        //    if (_selectedWorklists == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (new RISService(_context).CancelAssignedToRadiologist(_selectedWorklists))
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}


        //[HttpPost("UpdateRISWorklist")]
        //public IActionResult UpdateRISWorklist(RISWorkList _workList)
        //{
        //    if (_workList == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (new RISService(_context).UpdateRISWorklist(_workList))
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}


        //[HttpPost("UpdateHtmlTemplate")]
        //public IActionResult UpdateHtmlTemplate(VMUpdateTemplate _template)
        //{
        //    if (_template == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (new RISService(_context).UpdateHtmlTemplate(_template))
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}


        //[HttpPost("SaveConsultantStudyOpinions")]
        //public IActionResult SaveConsultantStudyOpinions(ConsultantOpinionOnStudy opinion)
        //{
        //    if (opinion == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (new RISService(_context).SaveConsultantOpinionOnStudy(opinion))
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}


        //[HttpGet("GetReportConsultantOpinionOnStudy")]
        //public ConsultantOpinionOnStudy GetReportConsultantOpinionOnStudy(int ProcTd, int ConsultantId)
        //{

        //    return (new RISService(_context)).GetReportConsultantOpinionOnStudy(ProcTd, ConsultantId);
        //}


        //[HttpPost("UpdateConsultantOpinionOnStudy")]
        //public IActionResult UpdateConsultantOpinionOnStudy(ConsultantOpinionOnStudy opinion)
        //{

        //    if (opinion == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (new RISService(_context).UpdateConsultantOpinionOnStudy(opinion))
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }

        //}

        //[HttpGet("GetUserById")]
        //public async Task<User> GetUserById(int userId)
        //{

        //    return await (new RISService(_context)).GetUserById(userId);
        //}

        ////[HttpGet("GetNextCloudUserLists")]
        ////public async Task<User> GetNextCloudUserLists(int userId)
        ////{

        ////    return await (new RISService(_context)).GetNextCloudUserLists(userId);
        ////}

        //[HttpGet("GetAllRadiologists")]
        //public async Task<List<VMReportConsultant>> GetAllRadiologists()
        //{
        //    List<VMReportConsultant> _cList = await (new RISService(_context)).GetReportConsultants();

        //    return _cList;
        //}


        //[HttpGet("GetUserDetails")]
        //public async Task<List<VMUserDetail>> GetUserDetails()
        //{
        //    IList<VMUserDetail> _userList = await new RISService(_context).GetUserDetails();

        //    return _userList.ToList();
        //}


        //[HttpGet("GetMasterTemplate")]
        //public async Task<MasterTemplate> GetMasterTemplate()
        //{

        //    return await (new RISService(_context)).GetWordMasterTemplateContent();
        //}


        //[HttpGet("GetTextMacrosList")]
        //public Dictionary<string, string> GetTextMacrosList(int ConsultantId)
        //{

        //    return (new RISService(_context)).GetTextMacrosList(ConsultantId);
        //}


        //[HttpGet("GetReportConsultant")]
        //public async Task<ReportConsultant> GetReportConsultant(int ConsultantId)
        //{
        //    return await (new RISService(_context)).GetReportConsultant(ConsultantId);
        //}


        //[HttpGet("GetHtmlTemplateForReport")]
        //public async Task<List<HtmlTempleForReport>> GetHtmlTemplateForReport(int ConsultantId)
        //{
        //    return await (new RISService(_context)).GetHtmlTemplateForReport(ConsultantId);
        //}



        //[HttpGet("GetShortcutKeyList")]
        //public async Task<List<ShortCutKey>> GetShortcutKeyList(int ConsultantId)
        //{
        //    return await (new RISService(_context)).GetShortcutKeyList(ConsultantId);
        //}


        //[HttpGet("GetRISWorklist")]
        //public async Task<RISWorkList> GetRISWorklist(int ProcId)
        //{
        //    return await (new RISService(_context)).GetRISWorklist(ProcId);
        //}


        //[HttpPost("AddEditRadiologist")]
        //public IActionResult AddEditRadiologist(ReportConsultant consultant)
        //{
        //    if (consultant == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (new RISService(_context).SaveOrUpdateReportConsultant(consultant))
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        //[HttpPost("DeleteExistingAllowedGroups")]
        //public void DeleteExistingAllowedGroups(int RCId)
        //{
        //    new RISService(_context).DeleteExistingAllowedGroups(RCId);
        //}


        //[HttpGet("Login")]
        //public async Task<string> Login(string userName, string password)
        //{
        //    bool isOk = await (new RISService(_context)).CheckLogin(userName, password);
        //    if (isOk)
        //        return userName;
        //    else
        //        return null;
        //}

        //[HttpGet("verify_token")]
        //public UserModel VerifyToken(string userName)
        //{
        //    return new RISService(_context).VerifyToken(userName);
        //}

        //[HttpPost("UpdateProcedureStatus")]
        //public IActionResult UpdateProcedureStatus(VMProcIdAndStatus procstate)
        //{
        //    if (procstate == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (new RISService(_context).UpdateProcedureStatus(procstate))

        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        //[HttpPost("SaveNewHtmlTempalate")]
        //public IActionResult SaveNewHtmlTempalate(HtmlTempleForReport template)
        //{
        //    if (template == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (new RISService(_context).SaveNewHtmlTempalate(template))

        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        //[HttpPost("UpdateHtmlTempalate")]
        //public IActionResult UpdateHtmlTempalate(HtmlTempleForReport template)
        //{
        //    if (template == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (new RISService(_context).UpdateHtmlTempalate(template))

        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}


        //[HttpPost("DeleteHtmlTemplate")]
        //public IActionResult DeleteHtmlTemplate(HtmlTempleForReport template) //TId=> TemplateId
        //{
        //    if (template == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (new RISService(_context).DeleteHtmlTemplate(template.Id))

        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}


        //[HttpPost("SavePrintPageSetup")]
        //public IActionResult SavePrintPageSetup(PrintPageSetup pagesetup) //TId=> TemplateId
        //{
        //    if (pagesetup == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (new RISService(_context).SavePrintPageSetup(pagesetup))

        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}


        //[HttpGet("GetPrintPageSetup")]
        //public async Task<PrintPageSetup> GetPrintPageSetup(int tenantId)
        //{
        //    return await (new RISService(_context)).GetPrintPageSetup(tenantId);
        //}


        //[HttpPost("AddToReferral")]
        //public IActionResult AddToReferral(ReferralPhysician physician)
        //{
        //    if (physician == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (new RISService(_context).AddToReferral(physician))
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        //[HttpGet("GetTenantPhysicianList")]
        //public async Task<List<ReferralPhysician>> GetTenantPhysicianList(int TenantId)
        //{
        //    return await new RISService(_context).GetTenantPhysicianList(TenantId);
        //}

        //[HttpGet("GetGroupName")]
        //public async Task<List<string>> GetGroupName()
        //{
        //    return await (new RISService(_context)).GetGroupName();
        //}

        //[HttpGet("SetGroupName")]
        //public void SetGroupName()
        //{
        //    new RISService(_context).SetGroupName();
        //}

        //[HttpGet("GetUserName")]
        //public async Task<List<string>> GetUserName()
        //{
        //    return await (new RISService(_context)).GetUserName();
        //}

        //[HttpGet("GetGroupNameOfUser")]
        //public async Task<string> GetGroupNameOfUser(string user)
        //{
        //    return await (new RISService(_context)).GetGroupNameOfUser(user);
        //}

        //[HttpGet("GetUserInfo")]
        //public async Task<List<OcsResponse>> GetUserInfo()
        //{
        //    return await (new RISService(_context)).GetUserInfo();
        //}

        //[HttpGet("GetUserNameOfGroup")]
        //public async Task<List<string>> GetUserNameOfGroup(string groupName)
        //{
        //    return await (new RISService(_context)).GetUserNameOfGroup(groupName);
        //}

        //[HttpGet("AssignedToUser")]
        //public bool AssignedToUser(string fileName, string userName)
        //{
        //    return (new RISService(_context)).AssignedToUser(fileName, userName);
        //}

        //[HttpGet("CancelAssignedToUser")]
        //public Task<bool> CancelAssignedToUser(int shareId)
        //{
        //    return (new RISService(_context)).CancelAssignedToUser(shareId);
        //}

        //[HttpGet("GetMenuItems")]
        //public async Task<List<MenuItem>> GetMenuItems()
        //{
        //    return await (new RISService(_context).GetMenuItems());
        //}

        //[HttpGet("GetRoles")]
        //public async Task<List<Role>> GetRoles()
        //{
        //    return await (new RISService(_context).GetRoles());
        //}

        //[HttpGet("GetHospitals")]
        //public async Task<List<Hisprocedure>> GetAllHISProcedure()
        //{
        //    return await (new RISService(_context).GetAllHISProcedure());
        //}

        //[HttpGet("GetAllTenants")]
        //public async Task<List<Tenant>> GetAllTenants()
        //{
        //    return await (new RISService(_context).GetAllTenants());
        //}

        [HttpGet("GetAllUsers")]
        public List<User> GetAllUser()
        {
            return new RISService(_context).GetAllUser();
        }

    }
}
