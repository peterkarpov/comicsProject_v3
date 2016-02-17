namespace comics.DAL.Contract
{
    using System;
    using System.Collections.Generic;
    using comics.Entities;

    public interface IUserDao
    {
        IEnumerable<User> GetAllUsers();

        User GetUserById(Guid userId);

        bool ExistUser(Guid userId);
        
        int GetRatingByUser(Guid userId);

        bool ChangeRating(Guid userId, int rating);

        bool Add(User user);

        bool Delete(Guid userId);

        bool Edit(Guid userId, User newUser);

        User GetUserByName(string userName);

        ARole GetARoleByUser(string userName);

        bool ChangeARole(Guid userId, int role);
    }
}
