using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HangoutsDbLibrary.Model;
using WebAPI.ViewModels;

namespace WebAPI.Mappers
{
    public class GroupAdminMapper : IGroupAdminMapper
    {
        public GroupAdminViewModel FromGroupAdminToGroupAdminViewModel(GroupAdmin groupAdmin)
        {
            return new GroupAdminViewModel
            {
                GroupId = groupAdmin.GroupAdminForeignKey,
                UserName = groupAdmin.Name
            };
        }
    }
}
