using RIS_blazor.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIS_blazor.Server.Repository.ServiceObjects
{
    public class MenuCollection : List<ProjectMenu>
    {
        public bool IsMenuExists(string menuName)
        {
            return Exists(x => string.Compare(menuName, x.Name, true) == 0);
        }
    }
}
