using EduMax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduMax.Repository
{
    public class UserRepository : Repository<User>
    {
        public User GetUser(int id)
        {
            //Returning the user with the help of id
            return new UserRepository().Get(id);
        }
    }
}