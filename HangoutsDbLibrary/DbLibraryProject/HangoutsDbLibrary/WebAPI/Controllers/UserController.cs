using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HangoutsDbLibrary.Model;
using WebAPI.Services;
using WebAPI.ViewModels;
using WebAPI.Mappers;
using WebAPI.Utils;

namespace WebAPI.Controllers
{
    [Produces("application/json")]

    public class UserController : Controller
    {
        private readonly IServiceUser serviceUser;
        private readonly IServiceUserGroup serviceUserGroup;

        private readonly IUserMapper userMapper;

        private readonly IUrlHelper urlHelper;

        public UserController(
             IServiceUser serviceUser,
             IServiceUserGroup serviceUserGroup,
             IUserMapper userMapper,
             IUrlHelper urlHelper)
        {
            this.serviceUser = serviceUser;
            this.serviceUserGroup = serviceUserGroup;
            this.userMapper = userMapper;
            this.urlHelper = urlHelper;

        }

        [HttpGet ("api/User", Name = "GetAllUsers")]
        public IActionResult Get()
        {
            List<User> users = serviceUser.GetAllUsers();

            return Ok(ToOutputModelList(users));
        }

        [HttpGet("api/User/Group/{id}", Name = "UsersFromAGroup")]
        public IActionResult GetUsersFromAGroup(int id)
        {
            try
            {
                List<User> users = serviceUserGroup.GetAllUsersFromAGroup(id);  
                return Ok(ToOutputModelList(users));
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPost("api/UserInterest", Name = "AddInterestToAUser")]
        public IActionResult Create([FromBody] UserInterestViewModel userInterestViewModel)
        {
            try
            {
                serviceUser.AddInterestToAUser(userInterestViewModel.UserName, userInterestViewModel.IdInterest);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("api/User/Login", Name = "LoginUser")]
        public IActionResult Login([FromBody] UserViewModel userViewModel)
        {
            try
            {
                User user = serviceUser.GetUserByUsername(userViewModel.UserName);
                int userId = -1;
                if(user.Password == userViewModel.Password)
                {
                    userId = user.Id;
                }

                return Ok(userId);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost ("api/User", Name = "CreateUser")]
        public IActionResult Create([FromBody] UserViewModel userViewModel)
        {
            try
            {
                User user = userMapper.FromUserViewModelToUser(userViewModel);
                serviceUser.AddUser(user);
                return Ok(user.Id);
            }
            catch
            {
                return BadRequest();
            }
            
            
        }

        [HttpPut("api/User/{id}", Name = "UpdateUser")]
        public IActionResult Update([FromBody] UserViewModel userViewModel, int id)
        {
            try
            {
                User user = serviceUser.GetUserById(id);
                user.Fullname = userViewModel.Fullname;
                serviceUser.UpdateUser(user);

                return Ok(ToOutputModel(user));
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpDelete("api/User/{id}", Name = "DeleteUser")]
        public IActionResult Delete(int id)
        {
            try
            {
                serviceUser.DeleteUser(serviceUser.GetUserById(id));
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
            
        }


        [HttpGet("api/User/{id}", Name ="GetOneUser")]
        public IActionResult GetById(int id)
        {
          
            User user = serviceUser.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(ToOutputModel(user));
        }

        private LinksWrapperList<UserViewModel>

            ToOutputModelList(List<User> users)
        {
            LinksWrapperList<UserViewModel> listOfValues = new LinksWrapperList<UserViewModel>();
            listOfValues.Values = new List<LinksWrapper<UserViewModel>>();

            foreach (User user in users)
            {
                var userView = ToOutputModel(user);
                listOfValues.Values.Add(userView);
            }
            return listOfValues;
        }

        private LinksWrapper<UserViewModel>
           ToOutputModel(User user)
        {
            return new LinksWrapper<UserViewModel>
            {
                Value = userMapper.FromUserToUserViewModel(user),
                Links = GetLinks_Output(user)
            };
        }

        private List<LinkInfo> GetLinks_Output(User user)
        {
            var links = new List<LinkInfo>();

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("GetOneUser", new { id = user.Id }),
                Rel = "self",
                Method = "GET"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("CreateUser", new { }),
                Rel = "create-user",
                Method = "POST"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("UpdateUser", new { id = user.Id }),
                Rel = "update-user",
                Method = "PUT"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("DeleteUser", new { id = user.Id }),
                Rel = "delete-user",
                Method = "DELETE"
            });
            

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("GetAllUsers", new { }),
                Rel = "get-all-users",
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
                Href = urlHelper.Link("AddInterestToAUser", new { }),
                Rel = "add-interest-to-user",
                Method = "POST"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("InterestsFromAUser", new { id = user.Id }),
                Rel = "all-interests-from-a-user",
                Method = "GET"
            });

            return links;
        }
    }
}