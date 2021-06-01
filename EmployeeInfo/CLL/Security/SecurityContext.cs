using System;
using System.Collections.Generic;
using System.Text;
using CLL.Security.Identity;
namespace CLL.Security
{
    public static class SecurityContext
    {
        static User _user = null;

        public static User GetUser()
        {
            return _user;
        }

        public static void SetUser(User user)
        {
            _user = user;
        }


    }
}
