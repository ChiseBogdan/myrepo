using HangoutsDbLibrary.Data;
using HangoutsDbLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsDbLibrary.Repository
{
    public class UnitOfWork : IDisposable
    {
        private HangoutsContext context;
        private InterfaceRepository<User> userRepository;
        private InterfaceRepository<Group> groupRepository;
        private InterfaceRepository<UserGroup> userGroupRepository;
        private InterfaceRepository<GroupAdmin> groupAdminRepository;
        private InterfaceRepository<Interest> interestRepository;
        private InterfaceRepository<Activity> activityRepository;
        private InterfaceRepository<GroupActivity> groupActivityRepository;
        private InterfaceRepository<Location> locationRepository;
        private InterfaceRepository<Plan> planRepository;

        private bool disposed = false;

        public UnitOfWork(HangoutsContext context)
        {
            this.context = context;
        }

        public InterfaceRepository<GroupActivity> GroupActivityRepository
        {
            get
            {
                if (this.groupActivityRepository == null)
                {
                    this.groupActivityRepository = new AbstractRepository<GroupActivity>(context);
                }
                return groupActivityRepository;
            }
        }

        public InterfaceRepository<Location> LocationRepository
        {
            get
            {
                if (this.locationRepository == null)
                {
                    this.locationRepository = new AbstractRepository<Location>(context);
                }
                return locationRepository;
            }
        }



        public InterfaceRepository<Activity> ActivityRepository
        {
            get
            {
                if (this.activityRepository == null)
                {
                    this.activityRepository = new AbstractRepository<Activity>(context);
                }
                return activityRepository;
            }
        }

        public InterfaceRepository<User> UserRepository
        {
            get
            {
                if(this.userRepository == null)
                {
                    this.userRepository = new AbstractRepository<User>(context);
                }
                return userRepository;
            }
        }

        public InterfaceRepository<UserGroup> UserGroupRepository
        {
            get
            {
                if (this.userGroupRepository == null)
                {
                    this.userGroupRepository = new AbstractRepository<UserGroup>(context);
                }
                return userGroupRepository;
            }
        }

        public InterfaceRepository<Interest> InterestRepository
        {
            get
            {
                if (this.interestRepository == null)
                {
                    this.interestRepository = new AbstractRepository<Interest>(context);
                }
                return interestRepository;
            }
        }

        public InterfaceRepository<Plan> PlanRepository
        {
            get
            {
                if (this.planRepository == null)
                {
                    this.planRepository = new AbstractRepository<Plan>(context);
                }
                return planRepository;
            }
        }

        public InterfaceRepository<Group> GroupRepository
        {
            get
            {
                if (this.groupRepository == null)
                {
                    this.groupRepository = new AbstractRepository<Group>(context);
                }
                return groupRepository;
            }
        }

        public InterfaceRepository<GroupAdmin> GroupAdminRepository
        {
            get
            {
                if (this.groupAdminRepository == null)
                {
                    this.groupAdminRepository = new AbstractRepository<GroupAdmin>(context);
                }
                return groupAdminRepository;
            }
        }

        public void save()
        {
            context.SaveChanges();
        }

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }
            }
            this.disposed = true;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }

}

