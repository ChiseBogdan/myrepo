using HangoutsDbLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.ViewModels;

namespace WebAPI.Mappers
{
    public class ActivityMapper : IActivityMapper
    {
        public Activity FromActivityViewModelToActivity(ActivityViewModel activityViewModel)
        {
            return new Activity
            {
                Description = activityViewModel.Description,
                BeginTimeActivity = activityViewModel.BeginningTime,
                EndTimeActivity = activityViewModel.EndingTime
            };
        }

        public ActivityViewModel FromActivityToActivityViewModel(Activity activity)
        {
            return new ActivityViewModel
            {
                Description = activity.Description,
                BeginningTime = activity.BeginTimeActivity,
                EndingTime = activity.EndTimeActivity,
                IdActivity = activity.Id
            };
        }
    }
}
