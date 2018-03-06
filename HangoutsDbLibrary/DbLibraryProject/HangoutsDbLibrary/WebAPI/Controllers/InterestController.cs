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
    [Route("api/Interest")]
    [Produces("application/json")]

    public class InterestController : Controller
    {
        private readonly IServiceInterest serviceInterest;

        private readonly IInterestMapper interestMapper;

        private readonly IUrlHelper urlHelper;

        public InterestController(
            IServiceInterest serviceInterest,
            IInterestMapper interestMapper,
            IUrlHelper urlHelper)
        {
            this.serviceInterest = serviceInterest;
            this.interestMapper = interestMapper;
            this.urlHelper = urlHelper;
        }



        [HttpDelete("{id}", Name = "DeleteInterest")]
        public IActionResult Delete(int id)
        {
            try
            {
                serviceInterest.DeleteInterest(serviceInterest.GetInterestById(id));

                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPost(Name = "CreateInterest")]
        public IActionResult Create([FromBody] InterestViewModel interestViewModel)
        {
            try
            {
                Interest interest = interestMapper.FromInterestViewModelToInterest(interestViewModel);
                serviceInterest.AddInterest(interest);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpGet(Name = "GetAllInterests")]
        public IActionResult Get()
        {
            try
            {
                List<Interest> interests = serviceInterest.GetAllInterests();

                return Ok(ToOutputModelList(interests));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("User/{id}", Name = "InterestsFromAUser")]
        public IActionResult GetInterestsFromAUser(int id)
        {
            try
            {
                List<Interest> interests = serviceInterest.GetAllInterestsFromAUser(id);

                return Ok(ToOutputModelList(interests));
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet("{id}", Name = "GetOneInterest")]
        public IActionResult GetActivityById(int id)
        {
            try
            {
                Interest interest = serviceInterest.GetInterestById(id);
                if (interest == null)
                {
                    return NotFound();
                }

                return Ok(ToOutputModel(interest));
            }

            catch
            {
                return BadRequest();
            }

        }

        [HttpPut("{id}", Name = "UpdateInterest")]
        public IActionResult Update([FromBody] InterestViewModel interestViewModel, int id)
        {
            try
            {

                Interest interest = serviceInterest.GetInterestById(id);

                interest.Description = interestViewModel.Description;

                serviceInterest.UpdateInterest(interest);

                return Ok(ToOutputModel(interest));
            }
            catch
            {
                return BadRequest();
            }

        }


        private LinksWrapperList<InterestViewModel>

            ToOutputModelList(List<Interest> interests)
        {
            LinksWrapperList<InterestViewModel> listOfValues = new LinksWrapperList<InterestViewModel>();
            listOfValues.Values = new List<LinksWrapper<InterestViewModel>>();

            foreach (Interest interest in interests)
            {
                var interestView = ToOutputModel(interest);
                listOfValues.Values.Add(interestView);
            }
            return listOfValues;
        }

        private LinksWrapper<InterestViewModel>
           ToOutputModel(Interest interest)
        {
            return new LinksWrapper<InterestViewModel>
            {
                Value = interestMapper.FromInterestToInterestViewModel(interest),
                Links = GetLinks_Output(interest)
            };
        }

        private List<LinkInfo> GetLinks_Output(Interest interest)
        {
            var links = new List<LinkInfo>();

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("GetOneInterest", new { id = interest.Id }),
                Rel = "self",
                Method = "GET"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("CreateInterest", new { }),
                Rel = "create-interest",
                Method = "POST"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("UpdateInterest", new { id = interest.Id }),
                Rel = "update-interest",
                Method = "PUT"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("DeleteInterest", new { id = interest.Id }),
                Rel = "delete-interest",
                Method = "DELETE"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("GetAllInterests", new { }),
                Rel = "get-all-interests",
                Method = "GET"
            });

            return links;
        }





    }
}
