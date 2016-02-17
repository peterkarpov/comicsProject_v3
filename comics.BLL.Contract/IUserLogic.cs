namespace comics.BLL.Contract
{
    using System;
    using System.Collections.Generic;
    using comics.Entities;

    public interface IUserLogic
    {
        IEnumerable<User> GetAllUsers();

        User GetUserById(Guid userId);

        bool ExistUser(Guid userId);

        ////string GetRoleByUser(Guid userId);
        bool ChangeRole(Guid userId, string role);

        int GetRatingByUser(Guid userId);

        bool ChangeRating(Guid userId, int rating);

        bool Add(User user);

        bool Delete(Guid userId);

        bool Edit(Guid userId, User newUser);

        User GetUserByName(string userName);

        string[] GetARoleByUser(string userName);
    }
}
