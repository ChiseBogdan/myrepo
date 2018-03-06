using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Data;
using HangoutsDbLibrary.Repository;

namespace WebAPI.Services
{
    public class ServiceInterest : IServiceInterest
    {

        private HangoutsContext context;

        private UnitOfWork CreateUnitOfWork()
        {
            DesignTimeDbContextFactory designTimeDbContextFactory = new DesignTimeDbContextFactory();
            context = designTimeDbContextFactory.CreateDbContext(new string[] { });
            return new UnitOfWork(context);
        }

        public void AddInterest(Interest interest)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                unitOfWork.InterestRepository.Add(interest);
                unitOfWork.save();
            }
        }

        public void DeleteInterest(Interest interest)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                unitOfWork.InterestRepository.Delete(interest);
                unitOfWork.save();
            }
        }

        public List<Interest> GetAllInterests()
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                return unitOfWork.InterestRepository.GetAll().ToList();
            }
        }

        public Interest GetInterestById(int id, UnitOfWork unitOfWork)
        {
            return unitOfWork.InterestRepository.FindBy(i => i.Id == id);
        }

        public Interest GetInterestById(int id)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                return GetInterestById(id, unitOfWork);
            }
        }

        public void UpdateInterest(Interest interest)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                unitOfWork.InterestRepository.Update(interest);
                unitOfWork.save();
            }
        }

        public List<Interest> GetAllInterestsFromAUser(int id)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                return unitOfWork.InterestRepository.GetAllBy(i => i.User.Id == id).ToList();
                
            }
        }
    }
}
