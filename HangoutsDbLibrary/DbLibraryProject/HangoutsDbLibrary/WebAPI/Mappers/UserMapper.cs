using HangoutsDbLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.ViewModels;

namespace WebAPI.Mappers
{
    public class UserMapper : IUserMapper
    {
        public UserViewModel FromUserToUserViewModel(User user)
        {
            return new UserViewModel() { UserName = user.Username, Fullname = user.Fullname, idUser = user.Id };
        }

        public User FromUserViewModelToUser(UserViewModel userView)
        {
            return new User() { Username = userView.UserName, Fullname = userView.Fullname, Password = userView.Password };
        }
    }
}
