using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Repository;
using RepositoriesAndUnitOfWork.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoriesAndUnitOfWork
{
    public class ConsoleUI
    {
        private static UnitOfWork unitOfWork;
        private MenuCommand menuCommand = new MenuCommand("The beginning is here");

        public ConsoleUI(UnitOfWork unitOfWork)
        {

            ConsoleUI.unitOfWork = unitOfWork;
        }

        public void start()
        {
            int comanda;
            createMenu();
            while (true)
            {
                System.Console.WriteLine();
                menuCommand.Execute();
                comanda = Convert.ToInt32(System.Console.ReadLine());

                if (comanda < menuCommand.size() + 1)
                {
                    if (menuCommand.getAllCommands()[comanda - 1] is MenuCommand)
                    {
                        menuCommand = (MenuCommand)menuCommand.getAllCommands()[comanda - 1];
                    }
                    else
                    {
                        menuCommand.getAllCommands()[comanda - 1].Execute();
                    }
                }
            }
        }


        private void createMenu()
        {
            MenuCommand UserMenu = new MenuCommand("Menu User");
            MenuCommand GroupMenu = new MenuCommand("Menu Group");
            MenuCommand GroupAdminMenu = new MenuCommand("Menu GroupAdmin");
            MenuCommand InterestMenu = new MenuCommand("Menu Interest");

            createMenuUser(UserMenu);
            createMenuGroup(GroupMenu);
            createMenuGroupAdmin(GroupAdminMenu);
            createMenuInterest(InterestMenu);

            menuCommand.addCommand("1.Menu User", UserMenu);
            menuCommand.addCommand("2.Menu Group", GroupMenu);
            menuCommand.addCommand("3.Menu Admin for a Group", GroupAdminMenu);
            menuCommand.addCommand("4.Menu Interst", InterestMenu);
            menuCommand.addCommand("5.Exit", new ExitCommand());
        }

        private void createMenuInterest(MenuCommand InterestMenu)
        {
            InterestMenu.addCommand("1.Add Interest", new AddInterestCommand());
            InterestMenu.addCommand("2.Add Interest to a user", new AddInterestToAUserCommand());
            InterestMenu.addCommand("3.Print all Interests", new PrinAllInterestsCommand());            
            InterestMenu.addCommand("4.Principal Menu", menuCommand);
        }

        private void createMenuGroupAdmin(MenuCommand GroupAdminMenu)
        {
            GroupAdminMenu.addCommand("1.Add Admin to a Group", new AddAdminToAGroupCommand());
            GroupAdminMenu.addCommand("2.Print all Admins", new PrintAllAdminsCommand());
            GroupAdminMenu.addCommand("3.Delete Admin from a Group", new DeleteAdminToAGroupCommand());
            GroupAdminMenu.addCommand("4.Find Admin from a Group", new FindAdminToAGroupCommand());
            GroupAdminMenu.addCommand("5.Principal menu", menuCommand);

        }
        public class PrinAllInterestsCommand : ICommand
        {
            public void Execute()
            {
                foreach(Interest interest in unitOfWork.InterestRepository.GetAll())
                {
                    System.Console.WriteLine(interest.ToString());
                }
            }
        }

        public class AddInterestCommand : ICommand
        {
            public void Execute()
            {
                System.Console.WriteLine("Select the descroption of the interest: ");
                String description = System.Console.ReadLine();

                unitOfWork.InterestRepository.Add(new Interest() {Description = description });
                unitOfWork.save();
            }
        }


        public class AddInterestToAUserCommand : ICommand
        {
            public void Execute()
            {
                PrinAllInterestsCommand prinAllInterestsCommand = new PrinAllInterestsCommand();
                PrintAllUsersCommand printAllUsers = new PrintAllUsersCommand();
                printAllUsers.Execute();

                System.Console.WriteLine("Select the id of the user you want to add an interest: ");
                int idUser = Convert.ToInt32(System.Console.ReadLine());

                prinAllInterestsCommand.Execute();
                System.Console.WriteLine("Select the id of the interst you want to add: ");
                int idInterest = Convert.ToInt32(System.Console.ReadLine());


                User user = unitOfWork.UserRepository.FindBy(u => u.Id == idUser);
                Interest interest = unitOfWork.InterestRepository.FindBy(inter => inter.Id == idInterest);

                user.Interests.Add(interest);
                interest.User = user;

                unitOfWork.save();

            }
        }
       

        public class FindAdminToAGroupCommand : ICommand
        {
            public void Execute()
            {
                PrintAllGroupsCommand printAllGroups = new PrintAllGroupsCommand();
                printAllGroups.Execute();

                System.Console.WriteLine("Select the id of the group you want to delete the admin: ");
                int idGrupa = Convert.ToInt32(System.Console.ReadLine());

                System.Console.WriteLine(unitOfWork.GroupAdminRepository.FindBy(grA => grA.Group.Id == idGrupa).ToString());

            }
        }


        public class PrintAllAdminsCommand : ICommand
        {
            public void Execute()
            {
                foreach(GroupAdmin groupAdmin in unitOfWork.GroupAdminRepository.GetAll() )   
                    System.Console.WriteLine(unitOfWork.GroupRepository.FindBy(gr => gr.Id==groupAdmin.GroupAdminForeignKey).ToString() + " has the admin " + groupAdmin.ToString());
            }
        }

        public class DeleteAdminToAGroupCommand : ICommand
        {
            public void Execute()
            {
                PrintAllAdminsCommand printAllAdmins = new PrintAllAdminsCommand();
                printAllAdmins.Execute();

                System.Console.WriteLine("Select the id of the group you want to delete the admin: ");
                int idGrupa = Convert.ToInt32(System.Console.ReadLine());

                GroupAdmin groupAdmin = unitOfWork.GroupAdminRepository.FindBy(grA => grA.Group.Id == idGrupa);
                unitOfWork.GroupAdminRepository.Delete(groupAdmin);
                unitOfWork.save();

            }
        }
        public class AddAdminToAGroupCommand : ICommand
        {
            public void Execute()
            {
                PrintAllGroupsCommand printAllGroups = new PrintAllGroupsCommand();
                PrintAllUsersCommand printAllUsers = new PrintAllUsersCommand();

                printAllGroups.Execute();

                System.Console.WriteLine();

                System.Console.WriteLine("Select the id of the group you want to add an admin: ");
                int idGrupa = Convert.ToInt32(System.Console.ReadLine());

                Group group = unitOfWork.GroupRepository.FindBy(g => g.Id == idGrupa);

                //inner join pe idGrupa
                foreach (UserGroup userGroup in unitOfWork.UserGroupRepository.GetAll())
                {
                    if (userGroup.GroupId == idGrupa)
                    {
                        System.Console.WriteLine(unitOfWork.UserRepository.FindBy(u => u.Id == userGroup.UserId));
                    }
                }


                System.Console.WriteLine("Select the id of the user from the group you want to add as admin: ");
                int idUser = Convert.ToInt32(System.Console.ReadLine());

                User user = unitOfWork.UserRepository.FindBy(u => u.Id == idUser);

                GroupAdmin groupAdmin  = new GroupAdmin() { GroupAdminForeignKey = user.Id, Name = user.Fullname, Group = group };
                unitOfWork.GroupAdminRepository.Add(groupAdmin);
                group.Admin = groupAdmin;
                unitOfWork.save();

            }
        }



            private void createMenuGroup(MenuCommand GroupMenu)
            {
                GroupMenu.addCommand("1.Add Group", new CreateGroupCommand());
                GroupMenu.addCommand("2.Delete Group", new DeleteGroupCommand());
                GroupMenu.addCommand("3.Update Group", new UpdateGroupCommand());
                GroupMenu.addCommand("4.Show all Groups", new PrintAllGroupsCommand());
                GroupMenu.addCommand("5.Add users to group", new AddUsersToGroup());
                GroupMenu.addCommand("6.Print all users from a group", new PrintAllUsersFromAGroupCommand());
                GroupMenu.addCommand("7.Print all groups for a user", new PrintAllGroupsForAUserCommand());
                GroupMenu.addCommand("8.Principal menu", menuCommand);
            }

            public class PrintAllGroupsForAUserCommand : ICommand
            {
                public void Execute()
                {
                    PrintAllUsersCommand printAllUsers = new PrintAllUsersCommand();
                    printAllUsers.Execute();

                    System.Console.WriteLine();
                    System.Console.WriteLine("Select the id of the user: ");
                    int idUser = Convert.ToInt32(System.Console.ReadLine());

                    foreach (UserGroup userGroup in unitOfWork.UserGroupRepository.GetAllBy(ug => ug.UserId == idUser))
                    {
                        System.Console.WriteLine(unitOfWork.GroupRepository.FindBy(group => group.Id == userGroup.GroupId));
                    }

                }
            }

            public class PrintAllUsersFromAGroupCommand : ICommand
            {
                public void Execute()
                {
                    PrintAllGroupsCommand printAllGroups = new PrintAllGroupsCommand();
                    printAllGroups.Execute();

                    System.Console.WriteLine();
                    System.Console.WriteLine("Select the id of the group: ");
                    int idGrupa = Convert.ToInt32(System.Console.ReadLine());

                    foreach (UserGroup userGroup in unitOfWork.UserGroupRepository.GetAllBy(ug => ug.GroupId == idGrupa))
                    {
                        System.Console.WriteLine(unitOfWork.UserRepository.FindBy(user => user.Id == userGroup.UserId));
                    }


                }
            }

            public class AddUsersToGroup : ICommand
            {
                public void Execute()
                {
                    PrintAllGroupsCommand printAllGroups = new PrintAllGroupsCommand();
                    printAllGroups.Execute();

                    PrintAllUsersCommand printAllUsers = new PrintAllUsersCommand();

                    System.Console.WriteLine("Select the id of the group you want to add users");
                    int idGrupa = Convert.ToInt32(System.Console.ReadLine());

                    Group group = unitOfWork.GroupRepository.FindBy(g => g.Id == idGrupa);

                    printAllUsers.Execute();
                    System.Console.WriteLine();
                    System.Console.WriteLine("Select the id of the users you want to add in the selected group and when you are done enter 0");

                    List<User> selectedUsers = new List<User>();

                    int idUser;
                    do
                    {
                        idUser = Convert.ToInt32(System.Console.ReadLine());
                        User user = unitOfWork.UserRepository.FindBy(u => u.Id == idUser);
                        if (!selectedUsers.Contains(user) && idUser != 0)
                        {
                            selectedUsers.Add(user);
                        }


                    } while (idUser != 0);


                    for (int index = 0; index < selectedUsers.Count; index++)
                    {
                        UserGroup userGroup = new UserGroup() { UserId = selectedUsers[index].Id, GroupId = group.Id, Group = group, User = selectedUsers[0] };
                        selectedUsers[index].UserGroups.Add(userGroup);
                        group.UserGroups.Add(userGroup);
                        unitOfWork.UserGroupRepository.Add(userGroup);
                    }

                    unitOfWork.save();

                }
            }

            public class CreateGroupCommand : ICommand
            {
                public void Execute()
                {
                    System.Console.WriteLine("Name of the group: ");
                    String groupName = System.Console.ReadLine();

                    Group group = new Group() { Name = groupName };

                    unitOfWork.GroupRepository.Add(group);
                    unitOfWork.save();
                }
            }

            public class DeleteGroupCommand : ICommand
            {
                public void Execute()
                {
                    PrintAllGroupsCommand printAllGroups = new PrintAllGroupsCommand();
                    printAllGroups.Execute();

                    System.Console.WriteLine("Select the id of the group you want to delete: ");
                    int id = Convert.ToInt32(System.Console.ReadLine());

                    Group group = unitOfWork.GroupRepository.FindBy(g => g.Id == id);
                    unitOfWork.GroupRepository.Delete(group);
                    unitOfWork.save();
                }
            }

            public class UpdateGroupCommand : ICommand
            {
                public void Execute()
                {
                    PrintAllGroupsCommand printAllGroups = new PrintAllGroupsCommand();
                    printAllGroups.Execute();

                    System.Console.WriteLine("Select the id of the group you want to update: ");
                    int id = Convert.ToInt32(System.Console.ReadLine());

                    System.Console.WriteLine("Give the new name to the group: ");
                    String newName = System.Console.ReadLine();

                    Group group = unitOfWork.GroupRepository.FindBy(g => g.Id == id);
                    group.Name = newName;
                    unitOfWork.GroupRepository.Update(group);
                    unitOfWork.save();
                }
            }

            public class ExitCommand : ICommand
            {
                public void Execute()
                {
                    Environment.Exit(0);
                }
            }

            private void createMenuUser(MenuCommand UserMenu)
            {
                UserMenu.addCommand("1.Add User", new CreateUserCommand());
                UserMenu.addCommand("2.Delete User", new DeleteUserCommand());
                UserMenu.addCommand("3.Update User", new UpdateUserCommand());
                UserMenu.addCommand("4.Show all Users", new PrintAllUsersCommand());
                UserMenu.addCommand("5.Principal menu", menuCommand);
            }

            public class UpdateUserCommand : ICommand
            {
                public void Execute()
                {
                    PrintAllUsersCommand printAllUsers = new PrintAllUsersCommand();

                    printAllUsers.Execute();
                    System.Console.WriteLine();

                    System.Console.WriteLine("Select the username of the user you want to update: ");
                    String username = System.Console.ReadLine();

                    User user = unitOfWork.UserRepository.FindBy(u => u.Username == username);

                    System.Console.WriteLine("Change the username: ");
                    String newUername = System.Console.ReadLine();

                    user.Username = newUername;
                    unitOfWork.UserRepository.Update(user);
                    unitOfWork.save();
                }
            }

            public class PrintAllUsersCommand : ICommand
            {
                public void Execute()
                {
                    System.Console.WriteLine("ID  |  USERNAME  |  FULLNAME");
                    foreach (User user in unitOfWork.UserRepository.GetAll())
                    {
                        System.Console.WriteLine(user.ToString() + "\n");
                    }
                }
            }

            public class CreateUserCommand : ICommand
            {
                public void Execute()
                {
                    System.Console.WriteLine("Your Fullname: ");
                    String fullname = System.Console.ReadLine();

                    String username;
                    User searchedUser;

                    do
                    {
                        System.Console.WriteLine("Your username: ");
                        username = System.Console.ReadLine();
                        searchedUser = unitOfWork.UserRepository.FindBy(u => u.Username == username);

                        if (searchedUser != null)
                            System.Console.WriteLine("Your username already exist. Try another one!");

                    } while (searchedUser != null);



                    User user = new User() { Fullname = fullname, Username = username };

                    unitOfWork.UserRepository.Add(user);
                    unitOfWork.save();

                }
            }

            public class DeleteUserCommand : ICommand
            {
                public void Execute()
                {
                    System.Console.WriteLine("Select the username you want to delete: ");
                    String username = System.Console.ReadLine();

                    User user = unitOfWork.UserRepository.FindBy(u => u.Username == username);
                    unitOfWork.UserRepository.Delete(user);
                    unitOfWork.save();
                }
            }


            public void addAdmin()
            {
                System.Console.WriteLine("Choose the group you want to add an admin:");
                int id = Convert.ToInt32(System.Console.ReadLine());
                Group group = unitOfWork.GroupRepository.FindBy(g => g.Id == id);
            }

            public class PrintAllGroupsCommand : ICommand
            {
                public void Execute()
                {
                    foreach (Group group in unitOfWork.GroupRepository.GetAll())
                    {
                        System.Console.WriteLine(group.ToString() + "\n");
                    }
                }
            }

        }
    }

