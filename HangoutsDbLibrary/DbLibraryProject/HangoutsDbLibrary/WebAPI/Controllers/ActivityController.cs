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
    [Route("api/Activity")]
    [Produces("application/json")]

    public class ActivityController : Controller
    {
        private readonly IServiceActivity serviceActivity;
        private readonly IServiceGroupActivity serviceGroupActivity;

        private readonly IActivityMapper activityMapper;

        private readonly IUrlHelper urlHelper;

        private Random rnd = new Random();

        public ActivityController(
            IServiceActivity serviceActivity,
            IServiceGroupActivity serviceGroupActivity,
            IActivityMapper activityMapper,
            IUrlHelper urlHelper)
        {
            this.serviceActivity = serviceActivity;
            this.serviceGroupActivity = serviceGroupActivity;
            this.activityMapper = activityMapper;
            this.urlHelper = urlHelper;
        }

        [HttpGet("Group/{id}", Name = "ActivitiesFromAGroup")]
        public IActionResult GetActivitiesFromAGroup(int id)
        {
            try
            {
                List<Activity> activities = serviceGroupActivity.GetAllActivitiesFromAGroup(id);

                return Ok(ToOutputModelList(activities));
            }
            catch
            {
                return BadRequest();
            }

        }


        [HttpDelete("{id}", Name = "DeleteActivity")]
        public IActionResult Delete(int id)
        {
            try
            {
                serviceActivity.DeleteActivity(serviceActivity.GetActivityById(id));
                return Ok("s-a sters");
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPost(Name = "CreateActivity")]
        public IActionResult Create([FromBody] ActivityViewModel activityViewModel)
        {
            try
            {
                Activity activity = activityMapper.FromActivityViewModelToActivity(activityViewModel);
                serviceActivity.AddActivity(activity);
                return Ok(ToOutputModel(activity));
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpGet(Name = "GetAllActivities")]
        public IActionResult Get()
        {
            try
            {
                List<Activity> activities = serviceActivity.GetAllActivities();

                return Ok(ToOutputModelList(activities));
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet("random/{id}", Name = "GetRandomActivity")]
        public IActionResult GetRandomActivity(int id)
        {
            try
            {
                List<Activity> activities = serviceActivity.GetAllActivities();
                Activity activity = activities[rnd.Next(0, activities.Count)];

                return Ok(ToOutputModel(activity));
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpGet("{id}", Name = "GetOneActivity")]
        public IActionResult GetActivityById(int id)
        {
            try
            {
                Activity activity = serviceActivity.GetActivityById(id);
                if (activity == null)
                {
                    return NotFound();
                }

                return Ok(ToOutputModel(activity));
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPut("{id}", Name = "UpdateActivity")]

        public IActionResult Update([FromBody] ActivityViewModel activityViewModel, int id)
        {
            try
            {
                Activity activity = serviceActivity.GetActivityById(id);
                activity.Description = activityViewModel.Description;
                activity.BeginTimeActivity = activityViewModel.BeginningTime;
                activity.EndTimeActivity = activityViewModel.EndingTime;

                serviceActivity.UpdateActivity(activity);

                return Ok(ToOutputModel(activity));
            }
            catch
            {
                return BadRequest();
            }

        }


        private LinksWrapperList<ActivityViewModel>

            ToOutputModelList(List<Activity> activities)
        {
            LinksWrapperList<ActivityViewModel> listOfValues = new LinksWrapperList<ActivityViewModel>();
            listOfValues.Values = new List<LinksWrapper<ActivityViewModel>>();

            foreach (Activity activity in activities)
            {
                var activityView = ToOutputModel(activity);
                listOfValues.Values.Add(activityView);
            }
            return listOfValues;
        }

        private LinksWrapper<ActivityViewModel>
           ToOutputModel(Activity activity)
        {
            return new LinksWrapper<ActivityViewModel>
            {
                Value = activityMapper.FromActivityToActivityViewModel(activity),
                Links = GetLinks_Output(activity)
            };
        }

        private List<LinkInfo> GetLinks_Output(Activity activity)
        {
            var links = new List<LinkInfo>();

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("GetOneActivity", new { id = activity.Id }),
                Rel = "self",
                Method = "GET"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("CreateActivity", new { }),
                Rel = "create-activity",
                Method = "POST"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("UpdateActivity", new { id = activity.Id }),
                Rel = "update-activity",
                Method = "PUT"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("DeleteActivity", new { id = activity.Id }),
                Rel = "delete-activity",
                Method = "DELETE"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("GetAllActivities", new { }),
                Rel = "get-all-activities",
                Method = "GET"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("AddActivityToAGroup", new { }),
                Rel = "add-activity-to-a-group",
                Method = "POST"
            });


            return links;
        }


    }
}
