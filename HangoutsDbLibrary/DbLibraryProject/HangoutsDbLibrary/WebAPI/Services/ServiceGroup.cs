using HangoutsDbLibrary.Data;
using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public class ServiceGroup : IServiceGroup
    {
        private HangoutsContext context;
        private UnitOfWork CreateUnitOfWork()
        {
            DesignTimeDbContextFactory designTimeDbContextFactory = new DesignTimeDbContextFactory();
            context = designTimeDbContextFactory.CreateDbContext(new string[] { });
            return new UnitOfWork(context);
        }

        public void UpdateGroup(Group group)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                unitOfWork.GroupRepository.Update(group);
                unitOfWork.save();
            }
        }

        public void AddGroup(Group group)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                unitOfWork.GroupRepository.Add(group);
                unitOfWork.save();
            }
        }

        public Group GetGroupByName(String name, UnitOfWork unitOfWork)
        {
            return unitOfWork.GroupRepository.FindBy(u => u.Name == name);
        }

        public void DeleteGroup(Group group)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                unitOfWork.GroupRepository.Delete(group);
                unitOfWork.save();
            }
        }

        public List<Group> GetAllGroups()
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                return unitOfWork.GroupRepository.GetAll().ToList();
            }
        }

        public Group GetGroupById(int id)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                return GetGroupById(id, unitOfWork);
            }
        }

        public Group GetGroupById(int id, UnitOfWork unitOfWork)
        {
            
            return unitOfWork.GroupRepository.FindBy(g => g.Id == id);
            
        }

        public void AddPlanToAGroup(int idGroup, int idPlan)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                ServicePlan servicePlan = new ServicePlan();

                Group group = GetGroupById(idGroup, unitOfWork);

                group.Plans.Add(servicePlan.GetPlanById(idPlan, unitOfWork));

                unitOfWork.save();

            }
        }

        public void AddAdminToAGroup(int idGroup, String userName)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {              
                Group group = GetGroupById(idGroup, unitOfWork);               

                group.Admin = new GroupAdmin { Name = userName};

                unitOfWork.save();

            }
        }


        public GroupAdmin GetAdminFromAGroup(int id)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                return unitOfWork.GroupAdminRepository.FindBy(g => g.GroupAdminForeignKey == id);
            }
        }

    }
}
