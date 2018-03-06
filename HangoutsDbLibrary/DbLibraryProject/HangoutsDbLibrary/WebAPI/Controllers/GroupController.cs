using HangoutsDbLibrary.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
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
    [Produces("application/json")]
    public class GroupController : Controller
    {
        private readonly IServiceGroup serviceGroup;
        private readonly IServiceUserGroup serviceUserGroup;
        private readonly IServiceGroupActivity serviceGroupActivity;

        private readonly IGroupMapper groupMapper;
        private readonly IGroupAdminMapper groupAdminMapper;
        private readonly IActivityMapper activityMapper;

        private readonly IUrlHelper urlHelper;


        public GroupController(
            IServiceGroup serviceGroup,
            IServiceUserGroup serviceUserGroup,
            IServiceGroupActivity serviceGroupActivity,
            IGroupMapper groupMapper,
            IGroupAdminMapper groupAdminMapper,
            IActivityMapper activityMapper,
            IUrlHelper urlHelper)
        {
            this.serviceGroup = serviceGroup;
            this.serviceGroupActivity = serviceGroupActivity;
            this.serviceUserGroup = serviceUserGroup;
            this.groupMapper = groupMapper;
            this.groupAdminMapper = groupAdminMapper;
            this.activityMapper = activityMapper;
            this.urlHelper = urlHelper;
        }

        [HttpPost("api/UserGroup", Name = "AddUserToAGroup")]
        public IActionResult Create([FromBody] UserGroupViewModel userGroupViewModel)
        {
            try
            {
                serviceUserGroup.AddUserToAGroup(userGroupViewModel.GroupId, userGroupViewModel.UserId);
                string output = "userul " + userGroupViewModel.UserId + "a fost adaugat in grupa" + userGroupViewModel.GroupId;
                return Ok(output);
            }
            catch (Exception e)
            {
                return BadRequest();
            }


        }

        [HttpPost("api/GroupPlan", Name = "AddPlanToAGroup")]
        public IActionResult Create([FromBody] GroupPlanViewModel groupPlanViewModel)
        {
            try
            {
                serviceGroup.AddPlanToAGroup(groupPlanViewModel.GroupId, groupPlanViewModel.PlanId);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }


        }

        [HttpPost("api/GroupAdmin", Name = "AddAdminToAGroup")]
        public IActionResult Create([FromBody] GroupAdminViewModel groupAdminViewModel)
        {
            try
            {
                serviceGroup.AddAdminToAGroup(groupAdminViewModel.GroupId, groupAdminViewModel.UserName);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }


        }

        [HttpGet("api/GroupAdmin/Group/{id}", Name = "GetAdminFromAGroup")]
        public IActionResult GetAdminFromAGroup(int id)
        {
            try
            {
                return Ok(groupAdminMapper.FromGroupAdminToGroupAdminViewModel(serviceGroup.GetAdminFromAGroup(id)));
            }
            catch (Exception e)
            {
                return BadRequest();
            }


        }

        [HttpGet("api/Activity/User/{id}", Name = "GetAllActivitiesFromAUser")]
        public IActionResult GetAllActivitiesFromAUser(int id)
        {
            try
            {
                List<Group> groups = serviceUserGroup.GetAllGroupsFromAUser(id);
                List<Activity> activities = new List<Activity>();

                foreach(Group group in groups){
                    List<Activity> allActivitiesFromAGroup = serviceGroupActivity.GetAllActivitiesFromAGroup(group.Id);
                    foreach(Activity activity in allActivitiesFromAGroup)
                    {
                        activities.Add(activity);
                    }
                }               

                return Ok(ToOutputModelList(activities));
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet("api/Group", Name = "GetAllGroups")]
        public IActionResult Get()
        {
            List<Group> groups = serviceGroup.GetAllGroups();

            return Ok(ToOutputModelList(groups));
        }

        
        [HttpGet("api/Group/User/{id}", Name = "GroupsFromAUser")]
        public IActionResult GetGroupsFromAUser(int id)
        {
            try
            {
                List<Group> groups = serviceUserGroup.GetAllGroupsFromAUser(id);
                return Ok(ToOutputModelList(groups));
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPost("api/GroupActivity", Name = "AddActivityToAGroup")]
        public IActionResult Create([FromBody] GroupActivityViewModel groupActivityViewModel)
        {
            try
            {
                serviceGroupActivity.AddActivityToAGroup(groupActivityViewModel.ActivityId, groupActivityViewModel.GroupId);
                string output = "s-a aduagat activitatea " + groupActivityViewModel.ActivityId + " la grupa " + groupActivityViewModel.GroupId;
                return Ok(output);
            }
            catch (Exception e)
            {
                return BadRequest();
            }


        }

        [HttpPost("api/GroupAdmin", Name = "AddAdminToAGroup")]
        public IActionResult CreateAdminToGroup([FromBody] GroupAdminViewModel groupAdminViewModel)
        {
            try
            {
                serviceGroup.AddAdminToAGroup(groupAdminViewModel.GroupId, groupAdminViewModel.UserName);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }


        }

        //[HttpGet("Group/{id}", Name = "GetAdminFromAGroup")]
        //[Route("api/GroupAdmin")]
        //public IActionResult GetAdminFromAGroup(int id)
        //{
        //    try
        //    {
        //        return Ok(groupAdminMapper.FromGroupAdminToGroupAdminViewModel(serviceGroup.GetAdminFromAGroup(id)));
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest();
        //    }


        //
        [HttpPut("api/Group/{id}", Name = "UpdateGroup")]
        public IActionResult Update([FromBody] GroupViewModel groupViewModel, int id)
        {
            try
            {
                Group group = serviceGroup.GetGroupById(id);
                group.Name = groupViewModel.Name;
                serviceGroup.UpdateGroup(group);

                return Ok(ToOutputModel(group));
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPost ("api/Group", Name = "CreateGroup")]
        
        public IActionResult Create([FromBody] GroupViewModel groupViewModel)
        {
            try
            {
                Group group = groupMapper.FromGroupViewModelToGroup(groupViewModel);
                serviceGroup.AddGroup(group);
                return Ok(ToOutputModel(group));
            }
            catch
            {
                return BadRequest();
            }

        }

        
        [HttpDelete("api/Group/{id}", Name = "DeleteGroup")]
        public IActionResult Delete(int id)
        {
            try
            {
                serviceGroup.DeleteGroup(serviceGroup.GetGroupById(id));
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

        int getFirstId(int id)
        {
            int finalNumber = 0;
            int zece = 1;

            while (id % 10 != 0)
            {
                finalNumber = id % 10 * zece + finalNumber;
                zece *= 10;
                id /= 10;
            }
            return finalNumber;
        }

        int getLastId(int id)
        {
            while (id % 10 != 0)
            {
                id /= 10;
            }

            return id /= 10;
        }

        [HttpDelete("api/User/Group/{id}", Name = "DeleteUserFromAGroup")]
        public IActionResult DeleteUserFromAGroup(int id)
        {
            try
            {
                int groupId = getFirstId(id);
                int userId = getLastId(id);
                serviceUserGroup.RemoveUserFromAGroup(userId, groupId);
                string output = "s-a sters userul " + userId + " din grupul " + groupId;
                return Ok(output);
            }
            catch
            {
                return BadRequest();
            }

        }


        [HttpGet("api/Group/{id}", Name = "GetOneGroup")]
        public IActionResult GetById(int id)
        {

            Group group = serviceGroup.GetGroupById(id);

            if (group == null)
            {
                return NotFound();
            }
            return Ok(ToOutputModel(group));
        }

        private LinksWrapperList<GroupViewModel>

            ToOutputModelList(List<Group> groups)
        {
            LinksWrapperList<GroupViewModel> listOfValues = new LinksWrapperList<GroupViewModel>();
            listOfValues.Values = new List<LinksWrapper<GroupViewModel>>();

            foreach (Group group in groups)
            {
                var groupView = ToOutputModel(group);
                listOfValues.Values.Add(groupView);
            }
            return listOfValues;
        }

        private LinksWrapper<GroupViewModel>
           ToOutputModel(Group group)
        {
            return new LinksWrapper<GroupViewModel>
            {
                Value = groupMapper.FromGroupToGroupViewModel(group),
                Links = GetLinks_Output(group)
            };
        }

        private List<LinkInfo> GetLinks_Output(Group group)
        {
            var links = new List<LinkInfo>();
                
            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("GetOneGroup", new { id = group.Id }),
                Rel = "self",
                Method = "GET"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("CreateGroup", new { }),
                Rel = "create-group",
                Method = "POST"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("UpdateGroup", new { id = group.Id }),
                Rel = "update-group",
                Method = "PUT"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("DeleteGroup", new { id = group.Id }),
                Rel = "delete-group",
                Method = "DELETE"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("GetAllGroups", new { }),
                Rel = "get-all-groups",
                Method = "GET"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("AddUserToAGroup", new { }),
                Rel = "add-user-to-group",
                Method = "GET"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("GroupsFromAUser", new { id = group.Id }),
                Rel = "get-all-groups-from-a-user",
                Method = "GET"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("UsersFromAGroup", new { id = group.Id }),
                Rel = "get-all-users-from-a-group",
                Method = "GET"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("AddActivityToAGroup", new { }),
                Rel = "add-activity-to-a-group",
                Method = "POST"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("ActivitiesFromAGroup", new { id = group.Id }),
                Rel = "get-activities-from-a-group",
                Method = "GET"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("AddPlanToAGroup", new { }),
                Rel = "add-plan-to-a-group",
                Method = "POST"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("PlansFromAGroup", new { id = group.Id }),
                Rel = "get-plans-from-a-group",
                Method = "GET"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("AddAdminToAGroup", new { }),
                Rel = "add-admin-to-a-group",
                Method = "POST"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("GetAdminFromAGroup", new { id = group.Id }),
                Rel = "get-admin-from-a-group",
                Method = "GET"
            });

            return links;
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
