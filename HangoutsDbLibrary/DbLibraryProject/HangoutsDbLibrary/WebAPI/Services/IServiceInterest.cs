using HangoutsDbLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public interface IServiceInterest
    {
        void AddInterest(Interest interest);

        List<Interest> GetAllInterests();

        Interest GetInterestById(int id);

        void UpdateInterest(Interest interest);

        void DeleteInterest(Interest interest);

        List<Interest> GetAllInterestsFromAUser(int id);
    }
}
