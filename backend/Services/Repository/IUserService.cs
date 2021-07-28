using System;
using backend.Models;
using System.Collections.Generic;

namespace backend.Services{
    public interface IUserService{
        List<User> GetUsers(); 
        User GetUser(int id);
        int Add(User user);
        void Edit(User user);  
        int Delete(int id);
    }
}