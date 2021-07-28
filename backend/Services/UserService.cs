using System.Collections.Generic;
using System.Linq;
using backend.Models;

namespace backend.Services
{
    public class UserService : IUserService
    {
        BackendContext db;
        public UserService(BackendContext _db)
        {
            db = _db;
        }
        public int Add(User user)
        {
            if (db != null)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return user.Id;

            }

            return 0;
        }

        public int Delete(int id)
        {
            int result = 0;

            if (db != null)
            {

                var exitsingClient = db.Users.Find(id);

                if (exitsingClient != null)
                {

                    db.Users.Remove(exitsingClient);


                    result = db.SaveChanges();
                }
                return result;
            }

            return result;
        }

        public void Edit(User user)
        {
              if (db != null)
            {
                //Delete that post
                db.Users.Update(user);

                //Commit the transaction
                db.SaveChanges();
            }
        }

        public User GetUser(int id)
        {
            return db.Users.SingleOrDefault(u=>u.Id == id);
        }

        public List<User> GetUsers()
        {
            if (db != null)
            {
                return db.Users.ToList();
            }
            return null;
        }
    }
}