using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Data;
using HangoutsDbLibrary.Repository;

namespace WebAPI.Services
{
    public class ServiceLocation : IServiceLocation
    {

        private HangoutsContext context;

        private UnitOfWork CreateUnitOfWork()
        {
            DesignTimeDbContextFactory designTimeDbContextFactory = new DesignTimeDbContextFactory();
            context = designTimeDbContextFactory.CreateDbContext(new string[] { });
            return new UnitOfWork(context);
        }

        public void AddLocation(Location location)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                unitOfWork.LocationRepository.Add(location);
                unitOfWork.save();
            }
        }

        public void DeleteLocation(Location location)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                unitOfWork.LocationRepository.Delete(location);
                unitOfWork.save();
            }
        }

        public List<Location> GetAllLocations()
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                return unitOfWork.LocationRepository.GetAll().ToList();
            }
        }

        public Location GetLocationById(int id)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                return unitOfWork.LocationRepository.FindBy(l => l.Id == id);
            }
        }

        public void UpdateLocation(Location location)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                unitOfWork.LocationRepository.Update(location);
                unitOfWork.save();
            }
        }
    }
}
