using HangoutsDbLibrary.Data;
using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public class ServiceActivity : IServiceActivity
    {
        private HangoutsContext context;
        private UnitOfWork CreateUnitOfWork()
        {
            DesignTimeDbContextFactory designTimeDbContextFactory = new DesignTimeDbContextFactory();
            context = designTimeDbContextFactory.CreateDbContext(new string[] { });
            return new UnitOfWork(context);
        }

        public void AddActivity(Activity activity)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                unitOfWork.ActivityRepository.Add(activity);
                unitOfWork.save();
            }
        }

        public List<Activity> GetAllActivities()
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                return unitOfWork.ActivityRepository.GetAll().ToList();
            }
        }

        public Activity GetActivityById(int id)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                return GetActivityById(id, unitOfWork);
            }
        }

        public Activity GetActivityById(int id, UnitOfWork unitOfWork)
        {           
            return unitOfWork.ActivityRepository.FindBy(a => a.Id == id);
        }

        public void UpdateActivity(Activity activity)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                unitOfWork.ActivityRepository.Update(activity);
                unitOfWork.save();
            }
        }

        public void DeleteActivity(Activity activity)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                unitOfWork.ActivityRepository.Delete(activity);
                unitOfWork.save();
            }
        }
    }
}
