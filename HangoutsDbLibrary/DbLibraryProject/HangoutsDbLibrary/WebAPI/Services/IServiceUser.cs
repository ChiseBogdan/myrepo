using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public interface IServiceUser
    {
        void AddUser(User user);

        User GetUserById(int id);

        void UpdateUser(User user);

        User GetUserByUsername(string userName, UnitOfWork unitOfWork);

        User GetUserByUsername(string userName);

        void DeleteUser(User user);

        List<User> GetAllUsers();

        void AddInterestToAUser(string userName, int idInterest);
    }
}
