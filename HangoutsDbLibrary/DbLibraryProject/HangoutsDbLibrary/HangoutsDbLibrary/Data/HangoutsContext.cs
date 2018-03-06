using HangoutsDbLibrary.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsDbLibrary.Data
{
    public class HangoutsContext : DbContext
    {
        public HangoutsContext(DbContextOptions<HangoutsContext> options) : base(options)
        {
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<GroupAdmin> GroupAdmins { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<GroupActivity> GroupActivities { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Plan>().ToTable("Plan");
            modelBuilder.Entity<Group>().ToTable("Group");
            modelBuilder.Entity<Chat>().ToTable("Chat");
            modelBuilder.Entity<Activity>().ToTable("Activity");
            modelBuilder.Entity<Interest>().ToTable("Interest");
            modelBuilder.Entity<GroupAdmin>().ToTable("GroupAdmin");
            modelBuilder.Entity<Location>().ToTable("Location");
            modelBuilder.Entity<UserGroup>().ToTable("UserGroup");
            modelBuilder.Entity<GroupActivity>().ToTable("GroupActivity");


            //Ignored Chat type
            modelBuilder.Ignore<Chat>();

            modelBuilder.Entity<User>()
                .Property(u => u.Fullname)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired();

            //Ont To Many Relationship           
            
            //One Group to Many Plans
            modelBuilder.Entity<Plan>()
                .HasOne(g => g.Group)
                .WithMany(p => p.Plans);

            //One Location to Many Activities
            modelBuilder.Entity<Activity>()
                .HasOne(l => l.Location)
                .WithMany(a => a.Activities);

            //One User to Many Interests
            modelBuilder.Entity<Interest>()
               .HasOne(u => u.User)
               .WithMany(i => i.Interests);

            //One to one
            //GroupAdmin - Group
            modelBuilder.Entity<Group>()
                .HasOne(g => g.Admin)
                .WithOne(ga => ga.Group)
                .HasForeignKey<GroupAdmin>(g => g.GroupAdminForeignKey); //always specify a foreing key for one-to-one rel.

            //Many To Many
            //Group - UserGroup - User

            //Many to many User -> Group(via UserGroup)

            //User - Group Many to many

            modelBuilder.Entity<UserGroup>()
               .HasKey(ug => new { ug.UserId, ug.GroupId });

            modelBuilder.Entity<UserGroup>()
                .HasOne(ug => ug.Group)
                .WithMany(u => u.UserGroups)
                .HasForeignKey(ug => ug.GroupId);

            modelBuilder.Entity<UserGroup>()
                .HasOne(ug => ug.User)
                .WithMany(u => u.UserGroups)
                .HasForeignKey(ug => ug.UserId);

            //Group - Activity many-to-many

            modelBuilder.Entity<GroupActivity>()
                .HasKey(ga => new { ga.ActivityId, ga.GroupId });

            modelBuilder.Entity<GroupActivity>()
                .HasOne(ga => ga.Group)
                .WithMany(g => g.GroupActivity)
                .HasForeignKey(ga => ga.GroupId);

            modelBuilder.Entity<GroupActivity>()
                .HasOne(ga => ga.Activity)
                .WithMany(a => a.GroupActivity)
                .HasForeignKey(ga => ga.ActivityId);


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.ConnectionString);
        }
    }
}
