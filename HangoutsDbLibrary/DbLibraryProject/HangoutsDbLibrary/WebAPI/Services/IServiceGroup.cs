using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public interface IServiceGroup
    {
        void AddGroup(Group group);

        void UpdateGroup(Group group);

        Group GetGroupByName(String name, UnitOfWork unitOfWork);

        void DeleteGroup(Group group);

        List<Group> GetAllGroups();

        Group GetGroupById(int id);

        void AddPlanToAGroup(int idGroup, int idPlan);

        void AddAdminToAGroup(int idGroup, String userName);

        GroupAdmin GetAdminFromAGroup(int id);


    }
}
