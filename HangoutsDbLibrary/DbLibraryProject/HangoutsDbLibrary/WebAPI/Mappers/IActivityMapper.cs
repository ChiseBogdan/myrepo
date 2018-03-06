using HangoutsDbLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.ViewModels;

namespace WebAPI.Mappers
{
    public interface IActivityMapper
    {
        Activity FromActivityViewModelToActivity(ActivityViewModel activityViewModel);
        ActivityViewModel FromActivityToActivityViewModel(Activity activity);
    }
}
