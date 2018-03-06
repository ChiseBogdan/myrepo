using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Repository;
using HangoutsDbLibrary.Data;

namespace WebAPI.Services
{
    public class ServicePlan : IServicePlan
    {
        private HangoutsContext context;

        private UnitOfWork CreateUnitOfWork()
        {
            DesignTimeDbContextFactory designTimeDbContextFactory = new DesignTimeDbContextFactory();
            context = designTimeDbContextFactory.CreateDbContext(new string[] { });
            return new UnitOfWork(context);
        }

        public void AddPlan(Plan plan)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                unitOfWork.PlanRepository.Add(plan);
                unitOfWork.save();
            }
        }

        public void DeletePlan(Plan plan)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                unitOfWork.PlanRepository.Delete(plan);
            }
        }

        public List<Plan> GetAllPlans()
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                return unitOfWork.PlanRepository.GetAll().ToList();
            }
        }

        public Plan GetPlanById(int id, UnitOfWork unitOfWork)
        {
           
            return unitOfWork.PlanRepository.FindBy(p => p.Id == id);
            
        }

        public Plan GetPlanById(int id)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                return GetPlanById(id, unitOfWork);
            }
        }

        public void UpdatePlan(Plan plan)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                unitOfWork.PlanRepository.Update(plan);
                unitOfWork.save();
            }
        }

        public List<Plan> GetAllPlansFromAGroup(int id)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                return unitOfWork.PlanRepository.GetAllBy(p => p.Group.Id == id).ToList();
                
            }
        }
    }
}
