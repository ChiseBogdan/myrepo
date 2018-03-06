using HangoutsDbLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HangoutsDbLibrary.Data
{
    public static class DbInitializer
    {
        public static void Initialize(HangoutsContext context)
        {
            context.Database.EnsureCreated();

            //Look for any Users.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
                new User{Username = "Mihai", Fullname ="Ghita"},
                new User{Username = "Adrian", Fullname ="Ghita"},
                new User{Username = "George", Fullname ="Ghita"},
                new User{Username = "Tudor", Fullname ="Ghita"},
            };

            foreach (User user in users)
                context.Users.Add(user);

            context.SaveChanges();

            var activities = new Activity[]
            {
                new Activity{Description = "Football"},
                new Activity{Description = "Dancing"}
            };


            foreach (Activity activity in activities)
                context.Activities.Add(activity);

            context.SaveChanges();

            var plans = new Plan[]
            {
                new Plan{ Description = "Teatru la Cluj", BeginnigTimePlan = "5/12/2007 5:05 PM", EndTimePlan = "5/12/2007 7:05 PM" },
                new Plan{ Description = "Pizza la Sibiu", BeginnigTimePlan = "1/09/2007 9:05 AM", EndTimePlan = "1/09/2007 9:05 AM 11:05 AM" }
            };

            foreach (Plan plan in plans)
                context.Plans.Add(plan);

            context.SaveChanges();


            var interests = new Interest[]
            {
                new Interest{Description = "Cycling"},
                new Interest{Description = "Dancing"},
                new Interest{Description = "Film"}
            };

            foreach (Interest intere in interests)
                context.Interests.Add(intere);

            context.SaveChanges();

            var groups = new Group[]
            {
                new Group{Name = "Group1"},
                new Group{Name = "Group2"},
                new Group{Name = "Group3"}
            };

            foreach (Group group in groups)
                context.Groups.Add(group);

            context.SaveChanges();

            //Adding one Group Admin
            Group group2 = context.Groups.FirstOrDefault();
            User user2 = context.Users.FirstOrDefault();
            GroupAdmin groupAdmin = new GroupAdmin() { GroupAdminForeignKey = user2.Id, Name = user2.Fullname, Group = group2 };
            context.GroupAdmins.Add(groupAdmin);
            group2.Admin = groupAdmin;
            context.SaveChanges();

            //Adding some Interests to Users
            User user3 = context.Users.FirstOrDefault();
            Interest interest = context.Interests.FirstOrDefault();
            Interest interest2 = context.Interests.SingleOrDefault(inter => inter.Description == "Dancing");

            user3.Interests.Add(interest);
            user3.Interests.Add(interest2);
            interest.User = user3;
            interest2.User = user3;
            context.SaveChanges();

            //ADDING SOME GROUP ACTIVITIES
            context.GroupActivities.Add(new GroupActivity { GroupId = context.Groups.SingleOrDefault(g => g.Name == "Group2").Id, ActivityId = context.Activities.SingleOrDefault(a => a.Description == "Football").Id });
            context.GroupActivities.Add(new GroupActivity { GroupId = context.Groups.SingleOrDefault(g => g.Name == "Group1").Id, ActivityId = context.Activities.SingleOrDefault(a => a.Description == "Dancing").Id });
            context.SaveChanges();


            //Many to many seed 1st way
            var user1 = context.Users.FirstOrDefault();
            var userGroup = new UserGroup { GroupId = context.Groups.FirstOrDefault().Id, UserId = context.Users.FirstOrDefault().Id };
            user1.UserGroups.Add(userGroup);
            context.Groups.FirstOrDefault().UserGroups.Add(userGroup);
            context.SaveChanges();

            //Many to many seed 2nd way
            context.UserGroups.Add(new UserGroup { GroupId = context.Groups.SingleOrDefault(g => g.Name == "Group2").Id, UserId = context.Users.SingleOrDefault(u => u.Username == "Adrian").Id });
            context.SaveChanges();
        }
    }
}
