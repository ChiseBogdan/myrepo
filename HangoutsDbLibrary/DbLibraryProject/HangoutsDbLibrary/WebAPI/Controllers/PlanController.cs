using HangoutsDbLibrary.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Mappers;
using WebAPI.Services;
using WebAPI.Utils;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    [Route("api/Plan")]
    [Produces("application/json")]
    public class PlanController : Controller
    {

        private readonly IServicePlan servicePlan;

        private readonly IPlanMapper planMapper;

        private readonly IUrlHelper urlHelper;

        public PlanController(
            IServicePlan servicePlan,
            IPlanMapper planMapper,
            IUrlHelper urlHelper)
        {
            this.servicePlan = servicePlan;
            this.planMapper = planMapper;
            this.urlHelper = urlHelper;
        }

        [HttpDelete("{id}", Name = "DeletePlan")]
        public IActionResult Delete(int id)
        {
            try
            {
                servicePlan.DeletePlan(servicePlan.GetPlanById(id));
                
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPost(Name = "CreatePlan")]
        public IActionResult Create([FromBody] PlanViewModel planViewModel)
        {
            try
            {
                Plan plan = planMapper.FromPlanViewModelToPlan(planViewModel);
                servicePlan.AddPlan(plan);
                
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpGet(Name = "GetAllPlans")]
        public IActionResult Get()
        {
            try
            {
                List<Plan> plans = servicePlan.GetAllPlans();
                
                return Ok(ToOutputModelList(plans));
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet("Group/{id}", Name = "PlansFromAGroup")]
        public IActionResult GetPlansFromAGroup(int id)
        {
            try
            {
                List<Plan> plans = servicePlan.GetAllPlansFromAGroup(id);

                return Ok(ToOutputModelList(plans));
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet("{id}", Name = "GetOnePlan")]
        public IActionResult GetActivityById(int id)
        {
            try
            {
                Plan plan = servicePlan.GetPlanById(id);
                
                if (plan == null)
                {
                    return NotFound();
                }

                return Ok(ToOutputModel(plan));
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPut("{id}", Name = "UpdatePlan")]
        public IActionResult Update([FromBody] PlanViewModel planViewModel, int id)
        {
            try
            {
                Plan plan = servicePlan.GetPlanById(id);
                plan.Description = planViewModel.Description;
                plan.BeginnigTimePlan = planViewModel.BeginnigTimePlan;
                plan.EndTimePlan = planViewModel.EndTimePlan;

                servicePlan.UpdatePlan(plan);
                
                return Ok(ToOutputModel(plan));
            }
            catch
            {
                return BadRequest();
            }

        }


        private LinksWrapperList<PlanViewModel>

            ToOutputModelList(List<Plan> plans)
        {
            LinksWrapperList<PlanViewModel> listOfValues = new LinksWrapperList<PlanViewModel>();
            listOfValues.Values = new List<LinksWrapper<PlanViewModel>>();

            foreach (Plan plan in plans)
            {
                var planView = ToOutputModel(plan);
                listOfValues.Values.Add(planView);
            }
            return listOfValues;
        }

        private LinksWrapper<PlanViewModel>
           ToOutputModel(Plan plan)
        {
            return new LinksWrapper<PlanViewModel>
            {
                Value = planMapper.FromPlanToPlanViewModel(plan),
                Links = GetLinks_Output(plan)
            };
        }

        private List<LinkInfo> GetLinks_Output(Plan plan)
        {
            var links = new List<LinkInfo>();

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("GetOnePlan", new { id = plan.Id }),
                Rel = "self",
                Method = "GET"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("CreatePlan", new { }),
                Rel = "create-plan",
                Method = "POST"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("UpdatePlan", new { id = plan.Id }),
                Rel = "update-plan",
                Method = "PUT"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("DeletePlan", new { id = plan.Id }),
                Rel = "delete-plan",
                Method = "DELETE"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("GetAllPlans", new { }),
                Rel = "get-all-plans",
                Method = "GET"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("AddPlanToAGroup", new { }),
                Rel = "add-plan-to-a-group",
                Method = "POST"
            }); 


            return links;
        }
    }
}
