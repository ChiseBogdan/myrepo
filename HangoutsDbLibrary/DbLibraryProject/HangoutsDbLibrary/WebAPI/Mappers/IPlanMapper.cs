using HangoutsDbLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.ViewModels;

namespace WebAPI.Mappers
{
    public interface IPlanMapper
    {
        Plan FromPlanViewModelToPlan(PlanViewModel planViewModel);
        PlanViewModel FromPlanToPlanViewModel(Plan plan);
    }
}
