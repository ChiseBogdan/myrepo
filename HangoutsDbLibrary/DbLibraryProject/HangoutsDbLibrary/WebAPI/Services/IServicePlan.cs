using HangoutsDbLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public interface IServicePlan
    {
        void AddPlan(Plan plan);

        List<Plan> GetAllPlans();

        Plan GetPlanById(int id);

        void UpdatePlan(Plan plan);

        void DeletePlan(Plan plan);

        List<Plan> GetAllPlansFromAGroup(int id);
    }
}
