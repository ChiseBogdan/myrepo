using HangoutsDbLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public interface IServiceActivity
    {
        void AddActivity(Activity activity);

        List<Activity> GetAllActivities();

        Activity GetActivityById(int id);

        void UpdateActivity(Activity activity);

        void DeleteActivity(Activity activity);
    }
}
