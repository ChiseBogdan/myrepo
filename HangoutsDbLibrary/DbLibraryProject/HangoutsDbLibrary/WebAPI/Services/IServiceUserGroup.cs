using HangoutsDbLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public interface IServiceUserGroup
    {
        void AddUserToAGroup(int idGroup, int idUser);
        List<Group> GetAllGroupsFromAUser(int userId);
        List<User> GetAllUsersFromAGroup(int groupId);
        void RemoveUserFromAGroup(int userId, int groupId);
    }
}
