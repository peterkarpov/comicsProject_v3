namespace comics.BLL.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using comics.BLL.Contract;
    using comics.Entities;

    public class UserLogic : IUserLogic
    {
        public bool Add(User user)
        {
            if (user.Id == default(Guid))
            {
                user.Id = Guid.NewGuid();
            }
            
            if (user.Rating == default(int))
            {
                user.Rating = 0;
            }
            
            if ((user.Name != "") & (user.Rating.ToString() != ""))
            {
                return Daos.UserDao.Add(user);
            }
            else
            {
                return false;
            }
        }

        public bool Delete(Guid userId)
        {
            if (Daos.UserDao.GetUserById(userId).Name == "admin") return false;

            if (this.ExistUser(userId))
            {
                return Daos.UserDao.Delete(userId);
            }
            else
            {
                throw new ArgumentException("No have this user");
            }
        }

        public bool ChangeRating(Guid userId, int rating)
        {
            if ((rating <= 100) & (rating >= -100))
            {
                return Daos.UserDao.ChangeRating(userId, rating);
            }
            else
            {
                throw new ArgumentException("No valide rating of User");
            }
        }

        public bool ChangeRole(Guid userId, string role)
        {
            if (Daos.UserDao.GetUserById(userId).Name == "admin") return false;

            if ((role == "none") | (role == "user") | (role == "moderator") | (role == "admin"))
            {
                int roleNumber = 0;
                if (role == "none") roleNumber = 0;
                if (role == "user") roleNumber = 1;
                if (role == "moderator") roleNumber = 3;
                if (role == "admin") roleNumber = 7;

                return Daos.UserDao.ChangeARole(userId, roleNumber);
            }
            else
            {
                throw new ArgumentException("can't change role by user");
            }
        }

        public bool ExistUser(Guid userId)
        {
            return Daos.UserDao.ExistUser(userId);
        }

        public IEnumerable<User> GetAllUsers()
        {
            if (Daos.UserDao.GetAllUsers() != null)
            {
                return Daos.UserDao.GetAllUsers().ToList();
            }
            else
            {
                throw new ArgumentException("Not any User");
            }
        }

        public int GetRatingByUser(Guid userId)
        {
            if (this.ExistUser(userId))
            {
                return Daos.UserDao.GetRatingByUser(userId);
            }
            else
            {
                throw new ArgumentException("No have this ID");
            }
        }
        
        public User GetUserById(Guid userId)
        {
            if (this.ExistUser(userId))
            {
                return Daos.UserDao.GetUserById(userId);
            }
            else
            {
                ////throw new ArgumentException("No have this ID");
                return default(User);
            }
        }

        public User GetUserByName(string userName)
        {
            if (userName != "")
            {
                return Daos.UserDao.GetUserByName(userName);
            }
            else
            {
                throw new ArgumentException("No have this User");
            }
        }

        public string[] GetARoleByUser(string userName)
        {
            if (userName != "")
            {
                return Daos.UserDao.GetARoleByUser(userName).ToString().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }

            return null;
        }

        public string GetOneARoleByUser(string userName)
        {
            if (userName != "")
            {
                var arr = Daos.UserDao.GetARoleByUser(userName).ToString().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                return arr[arr.Length];
            }

            return null;
        }

        public bool Edit(Guid userId, User newUser)
        {
            return Daos.UserDao.Edit(userId, newUser);
        }
    }
}
