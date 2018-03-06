using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HangoutsDbLibrary.Model;
using WebAPI.ViewModels;

namespace WebAPI.Mappers
{
    public class LocationMapper : ILocationMapper
    {
        public LocationViewModel FromLocationToLocationViewModel(Location activity)
        {
            return new LocationViewModel
            {
                Adress = activity.Adress,
                City = activity.City
            };
        }

        public Location FromLocationViewModelToLocation(LocationViewModel activityViewModel)
        {
            return new Location
            {
                Adress = activityViewModel.Adress,
                City = activityViewModel.City
            };
        }
    }
}
