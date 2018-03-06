using HangoutsDbLibrary.Data;
using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public class ServiceUserGroup : IServiceUserGroup
    {
        private HangoutsContext context;
        private ServiceGroup serviceGroup;
        private ServiceUser serviceUser;

        public ServiceUserGroup()
        {
            serviceUser = new ServiceUser();
            serviceGroup = new ServiceGroup();
        }
        private UnitOfWork CreateUnitOfWork()
        {
            DesignTimeDbContextFactory designTimeDbContextFactory = new DesignTimeDbContextFactory();
            context = designTimeDbContextFactory.CreateDbContext(new string[] { });
            return new UnitOfWork(context);
        }

        public void AddUserToAGroup(int idGroup, int idUser)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {

                User user = serviceUser.GetUserById(idUser, unitOfWork);
                Group group = serviceGroup.GetGroupById(idGroup, unitOfWork);

                unitOfWork.UserGroupRepository.Add(new UserGroup { Group = group, User = user });

                unitOfWork.save();
            }
        }

        public List<Group> GetAllGroupsFromAUser(int userId)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                List<Group> groups = new List<Group>();

                foreach (UserGroup ug in unitOfWork.UserGroupRepository.GetAllBy(ug => ug.UserId==userId).ToList())
                {
                    groups.Add(unitOfWork.GroupRepository.FindBy(g => g.Id == ug.GroupId));
                }

                return groups;

            }

        }

        public List<User> GetAllUsersFromAGroup(int groupId)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                List<User> users = new List<User>();

                foreach(UserGroup ug in unitOfWork.UserGroupRepository.GetAllBy(ug => ug.GroupId == groupId).ToList())
                {
                    users.Add(unitOfWork.UserRepository.FindBy(u => u.Id==ug.UserId));
                }

                return users;
                
            }

        }

        public void RemoveUserFromAGroup(int userId, int groupId)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {

                User user = serviceUser.GetUserById(userId, unitOfWork);
                Group group = serviceGroup.GetGroupById(groupId, unitOfWork);

                UserGroup userGroupToDelete = unitOfWork.UserGroupRepository.FindBy(userGroup => userGroup.GroupId == groupId && userGroup.UserId == userId);

                unitOfWork.UserGroupRepository.Delete(userGroupToDelete);

                unitOfWork.save();
            }
        }
    }
}
