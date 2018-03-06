using HangoutsDbLibrary.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAPI.Mappers;
using WebAPI.Services;
using WebAPI.Utils;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    [Route("api/Location")]
    [Produces("application/json")]
    public class LocationController : Controller
    {

        private readonly IServiceLocation serviceLocation;

        private readonly ILocationMapper locationMapper;

        private readonly IUrlHelper urlHelper;

        public LocationController(
            IServiceLocation serviceLocation,
            ILocationMapper locationMapper,
            IUrlHelper urlHelper
            )
        {
            this.serviceLocation = serviceLocation;
            this.locationMapper = locationMapper;
            this.urlHelper = urlHelper;
        }

        [HttpGet(Name = "GetAllLocations")]
        public IActionResult Get()
        {
            List<Location> locations = serviceLocation.GetAllLocations();

            return Ok(ToOutputModelList(locations));
        }


        [HttpPut("{id}", Name = "UpdateLocation")]
        public IActionResult Update([FromBody] LocationViewModel locationViewModel, int id)
        {
            try
            {
                Location location = serviceLocation.GetLocationById(id);
                location.City = locationViewModel.City;
                location.Adress = locationViewModel.Adress;

                serviceLocation.UpdateLocation(location);
           
                return Ok(ToOutputModel(location));
            }
            catch
            {
                return BadRequest();
            }

        }


        [HttpPost(Name = "CreateLocation")]
        public IActionResult Create([FromBody] LocationViewModel locationViewModel)
        {
            try
            {
                Location location = locationMapper.FromLocationViewModelToLocation(locationViewModel);
                serviceLocation.AddLocation(location);
           
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }


        [HttpDelete("{id}", Name = "DeleteLocation")]
        public IActionResult Delete(int id)
        {
            try
            {
                serviceLocation.DeleteLocation(serviceLocation.GetLocationById(id));
                
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }


        [HttpGet("{id}", Name = "GetOneLocation")]
        public IActionResult GetById(int id)
        {
            Location location = serviceLocation.GetLocationById(id);


            if (location == null)
            {
                return NotFound();
            }
            return Ok(ToOutputModel(location));
        }

        private LinksWrapperList<LocationViewModel>

            ToOutputModelList(List<Location> locations)
        {
            LinksWrapperList<LocationViewModel> listOfValues = new LinksWrapperList<LocationViewModel>();
            listOfValues.Values = new List<LinksWrapper<LocationViewModel>>();

            foreach (Location location in locations)
            {
                var locationView = ToOutputModel(location);
                listOfValues.Values.Add(locationView);
            }
            return listOfValues;
        }

        private LinksWrapper<LocationViewModel>
           ToOutputModel(Location location)
        {
            return new LinksWrapper<LocationViewModel>
            {
                Value = locationMapper.FromLocationToLocationViewModel(location),
                Links = GetLinks_Output(location)
            };
        }

        private List<LinkInfo> GetLinks_Output(Location location)
        {
            var links = new List<LinkInfo>();

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("GetOneLocation", new { id = location.Id }),
                Rel = "self",
                Method = "GET"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("CreateLocation", new { }),
                Rel = "create-location",
                Method = "POST"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("UpdateLocation", new { id = location.Id }),
                Rel = "update-location",
                Method = "PUT"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("DeleteLocation", new { id = location.Id }),
                Rel = "delete-location",
                Method = "DELETE"
            });

            links.Add(new LinkInfo
            {
                Href = urlHelper.Link("GetAllLocations", new { }),
                Rel = "get-all-locations",
                Method = "GET"
            });

            return links;
        }

    }
}
