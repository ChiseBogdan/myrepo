using HangoutsDbLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public interface IServiceLocation
    {
        void AddLocation(Location location);

        List<Location> GetAllLocations();

        Location GetLocationById(int id);

        void UpdateLocation(Location location);

        void DeleteLocation(Location location);
    }
}
