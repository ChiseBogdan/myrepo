using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HangoutsDbLibrary.Model;
using WebAPI.ViewModels;

namespace WebAPI.Mappers
{
    public class PlanMapper : IPlanMapper
    {
        public PlanViewModel FromPlanToPlanViewModel(Plan plan)
        {
            return new PlanViewModel
            {
                Description = plan.Description,
                BeginnigTimePlan = plan.BeginnigTimePlan,
                EndTimePlan = plan.EndTimePlan
            };
        }

        public Plan FromPlanViewModelToPlan(PlanViewModel planViewModel)
        {
            return new Plan
            {
                Description = planViewModel.Description,
                BeginnigTimePlan = planViewModel.BeginnigTimePlan,
                EndTimePlan = planViewModel.EndTimePlan,

            };
        }
    }
}
