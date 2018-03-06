using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Data;
using HangoutsDbLibrary.Repository;

namespace WebAPI.Services
{
    public class ServiceGroupActivity : IServiceGroupActivity
    {
        private HangoutsContext context;
        private ServiceGroup serviceGroup;
        private ServiceActivity serviceActivity;

        public ServiceGroupActivity()
        {
            serviceActivity = new ServiceActivity();
            serviceGroup = new ServiceGroup();
        }
        private UnitOfWork CreateUnitOfWork()
        {
            DesignTimeDbContextFactory designTimeDbContextFactory = new DesignTimeDbContextFactory();
            context = designTimeDbContextFactory.CreateDbContext(new string[] { });
            return new UnitOfWork(context);
        }

        public void AddActivityToAGroup(int idActivity, int idGroup)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {

                Activity activity = serviceActivity.GetActivityById(idActivity, unitOfWork);
                Group group = serviceGroup.GetGroupById(idGroup, unitOfWork);

                unitOfWork.GroupActivityRepository.Add(new GroupActivity { Group = group, Activity = activity });

                unitOfWork.save();
            }
        }

        public List<Activity> GetAllActivitiesFromAGroup(int groupId)
        {
            // kind of inner join
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                List<Activity> activties = new List<Activity>();

                foreach (GroupActivity ga in unitOfWork.GroupActivityRepository.GetAllBy(ga => ga.GroupId == groupId).ToList())
                {
                    activties.Add(unitOfWork.ActivityRepository.FindBy(a => a.Id == ga.ActivityId));
                }

                return activties;

            }
        }

    }
}
