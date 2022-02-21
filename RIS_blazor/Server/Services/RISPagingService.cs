using RIS_blazor.Shared.Models;
using RIS_blazor.Server.Models;
using RIS_blazor.Shared.Models.VWModels;
using RIS_blazor.Server.Repositories;
using RIS_blazor.Server.Repository.ServiceObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RIS_blazor.Server.Services
{
    public class RISPagingService
    {
        private readonly CoreDbContext _context;

        public RISPagingService(CoreDbContext context)
        {
            _context = context;
        }
        public async Task<int> GetIncompleteItemCount(DateTime dateFrom, DateTime dateTo, int roleId, int tenantId, int consultantId, string _status, string SearchFilter)
        {
            return await (new RISService(_context)).GetIncompleteItemCount(dateFrom, dateTo, roleId, tenantId, consultantId, _status, SearchFilter);
        }

        public async Task<int> GetUserCount(string GroupName, string SearchFilter)
        {
            return await (new RISService(_context)).GetUserCount( GroupName, SearchFilter);
        }

        public int GetPageCount(int totalItemCount, int pagePerItemCount)
        {
            return (totalItemCount % pagePerItemCount == 0) ? totalItemCount / pagePerItemCount : totalItemCount / pagePerItemCount + 1;
        }

        public async Task<List<VMRISWorklist>> GetOnePageItems(DateTime dateFrom, DateTime dateTo, int roleId, int tenantId, int consultantId, string _status, int PageNo, int RecsPerPage)
        {
            return await new RISService(_context).GetOnePagedWorklists(dateFrom, dateTo, roleId, tenantId, consultantId, _status, PageNo, RecsPerPage);
        }

        public async Task<List<VMRISWorklist>> GetOnePageItemsFromLogin(string userName, string userPassword, DateTime dateFrom, DateTime dateTo, string _status, int PageNo, int RecsPerPage)
        {
            LoginUser _user;
            
            if ((new RISService(_context).CheckLogin(userName, userPassword, out _user)) )
            {
                User _curUser = await new RISService(_context).GetUserById(_user.UserId);
                return await new RISService(_context).GetOnePagedWorklists(dateFrom, dateTo, (int)_curUser.RoleId, _curUser.TenantId, (int)_curUser.Rcid, _status, PageNo, RecsPerPage);
            }
            return null;
            
        }

        public async Task<List<VWNextCloudUser>> GetSearchFilterIncompleteOnePageUserItems(string GroupName, string SearchFilter, int PageNo, int RecsPerPage)
        {
            return await new RISRepository(_context).GetSearchFilterIncompleteOnePageUserItems(GroupName, SearchFilter, PageNo, RecsPerPage);
        }

        internal async Task<object> GetCompletedItemCount(DateTime dateFrom, DateTime dateTo, int roleId, int tenantId, int consultantId, string _status, string SearchFilter)
        {
            return await(new RISService(_context)).GetCompletedItemCount(dateFrom, dateTo, roleId, tenantId, consultantId, _status, SearchFilter);
        }

        internal async Task<List<VMRISWorklist>> GetSearchFilterCompletedOnePageItems(DateTime dateFrom, DateTime dateTo, int roleId, int tenantId, int consultantId, string status, string searchFilter, int pageNo, int recsPerPage)
        {
            return await new RISService(_context).GetSearchFilterCompletedOnePageItems(dateFrom, dateTo, roleId, tenantId, consultantId, status, searchFilter, pageNo, recsPerPage);
        }
    }
}
