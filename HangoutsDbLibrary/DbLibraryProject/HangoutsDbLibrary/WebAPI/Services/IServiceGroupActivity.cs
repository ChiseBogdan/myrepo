using HangoutsDbLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public interface IServiceGroupActivity
    {
        void AddActivityToAGroup(int idActivity, int idGroup);
        List<Activity> GetAllActivitiesFromAGroup(int groupId);
    }
}
