using HangoutsDbLibrary.Data;
using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Mappers;
using WebAPI.ViewModels;

namespace WebAPI.Services
{
    public class ServiceUser : IServiceUser
    {
        private HangoutsContext context;

        private UnitOfWork CreateUnitOfWork()
        {
            DesignTimeDbContextFactory designTimeDbContextFactory = new DesignTimeDbContextFactory();
            context = designTimeDbContextFactory.CreateDbContext(new string[] { });
            return new UnitOfWork(context);
        }

        public void UpdateUser(User user)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                unitOfWork.UserRepository.Update(user);
                unitOfWork.save();
            }
        }

        public void AddUser(User user)
        {
            //Moare unit of work dupa fiecare folosire de metode (speranta lui de viata e mica haha)
            using(UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                unitOfWork.UserRepository.Add(user);
                unitOfWork.save();
            }
        }

        public User GetUserById(int id)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                return unitOfWork.UserRepository.FindBy(u => u.Id == id);
                
            }
        }

        public User GetUserById(int id, UnitOfWork unitOfWork)
        {
            return unitOfWork.UserRepository.FindBy(u => u.Id == id);
        }

        public User GetUserByUsername(string Username)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                return unitOfWork.UserRepository.FindBy(u => u.Username == Username);

            }
        }


        public User GetUserByUsername(string userName, UnitOfWork unitOfWork)
        {
            return unitOfWork.UserRepository.FindBy(u => u.Username == userName);
        }

        public void DeleteUser(User user)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                unitOfWork.UserRepository.Delete(user);
                unitOfWork.save();
            }
        }

        public List<User> GetAllUsers()
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {
                return unitOfWork.UserRepository.GetAll().ToList();
            }
        }

        public void AddInterestToAUser(string userName, int idInterest)
        {
            using (UnitOfWork unitOfWork = CreateUnitOfWork())
            {

                ServiceInterest serviceInterest = new ServiceInterest();

                User user = GetUserByUsername(userName, unitOfWork);

                user.Interests.Add(serviceInterest.GetInterestById(idInterest, unitOfWork));

                unitOfWork.save();

            }
        }

    }
}
