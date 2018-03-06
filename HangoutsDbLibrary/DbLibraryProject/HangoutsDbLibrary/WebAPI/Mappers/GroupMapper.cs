using HangoutsDbLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.ViewModels;

namespace WebAPI.Mappers
{
    public class GroupMapper : IGroupMapper
    {

        public GroupViewModel FromGroupToGroupViewModel(Group group)
        {
            return new GroupViewModel() { Name = group.Name, IdGroup = group.Id };
        }

        public Group FromGroupViewModelToGroup(GroupViewModel groupView)
        {
            return new Group { Name = groupView.Name };
        }



    }
}
