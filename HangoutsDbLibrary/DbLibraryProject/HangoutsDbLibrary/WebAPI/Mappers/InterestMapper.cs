using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HangoutsDbLibrary.Model;
using WebAPI.ViewModels;

namespace WebAPI.Mappers
{
    public class InterestMapper : IInterestMapper
    {
        public InterestViewModel FromInterestToInterestViewModel(Interest interest)
        {
            return new InterestViewModel
            {
                Description = interest.Description
            };
        }

        public Interest FromInterestViewModelToInterest(InterestViewModel interestViewModel)
        {
            return new Interest
            {
                Description = interestViewModel.Description
            };
        }
    }
}
