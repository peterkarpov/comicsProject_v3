namespace comics.DAL.SQL
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Linq;
    using comics.DAL.Contract;
    using comics.Entities;

    public class UserDao : IUserDao
    {
        public bool Add(User user)
        {
            int result;

            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (var con = new SqlConnection(conStr))
            {
                var query = "INSERT INTO comics_tUser (user_id, user_name, user_rating, user_email) VALUES (@id, @name, @rating, @email)";
                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@id", user.Id);
                command.Parameters.AddWithValue("@name", user.Name);
                ////command.Parameters.AddWithValue("@role", user.Role);
                command.Parameters.AddWithValue("@rating", user.Rating);
                command.Parameters.AddWithValue("@email", user.Email);

                con.Open();
                result = command.ExecuteNonQuery(); // result > 0;
            }

            return result > 0;
        }

        public bool Delete(Guid userId)
        {
            int result;

            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (var con = new SqlConnection(conStr))
            {
                var query = "DELETE FROM comics_tUser WHERE comics_tUser.user_id = @id";
                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@id", userId);

                con.Open();
                result = command.ExecuteNonQuery(); // result > 0;
            }

            return result > 0;
        }

        public bool ChangeRating(Guid userId, int rating)
        {
            int result;

            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (var con = new SqlConnection(conStr))
            {
                var query = "UPDATE comics_tUser SET user_rating = @rating WHERE user_id = @userId";
                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@rating", rating);

                con.Open();
                result = command.ExecuteNonQuery(); // result > 0;
            }

            return result > 0;
        }
        
        public bool ExistUser(Guid userId)
        {
            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (var con = new SqlConnection(conStr))
            {
                var query = "SELECT TOP 1 u.user_id FROM comics_tUser as u WHERE u.user_id = @userId";

                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@userId", userId);

                con.Open();
                var reader = command.ExecuteReader();

                return reader.Read();
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            List<User> users = new List<User>().ToList();

            using (var con = new SqlConnection(conStr))
            {
                var query = "SELECT u.user_id, u.user_name, u.user_rating, u.user_arole FROM comics_tUser as u ORDER BY u.user_name";
                var command = new SqlCommand(query, con);

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new User
                    {
                        Id = (Guid)reader["user_id"],
                        Name = (string)reader["user_name"],
                        ARole = (ARole)reader["user_arole"],
                        Rating = (int)reader["user_rating"],
                    });
                }
            }

            return users;
        }

        public int GetRatingByUser(Guid userId)
        {
            return this.GetUserById(userId).Rating;
        }
        
        public ARole GetARoleByUser(string userName)
        {
            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (var con = new SqlConnection(conStr))
            {
                var query = "SELECT TOP 1 u.user_arole FROM comics_tUser as u WHERE u.user_name = @userName";

                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@userName", userName);

                con.Open();
                var reader = command.ExecuteReader();

                return (ARole)(reader.Read() ? (int)reader["user_arole"] : 0);
            }
        }

        public User GetUserById(Guid userId)
        {
            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            User user = default(User);

            using (var con = new SqlConnection(conStr))
            {
                var query = "SELECT TOP 1 u.user_name, u.user_arole, u.user_rating FROM comics_tUser as u WHERE u.user_id = @userId";

                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@userId", userId);

                con.Open();
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    user = new User
                    {
                        Id = userId,
                        Name = (string)reader["user_name"],
                        ARole = (ARole)reader["user_arole"],
                        Rating = (int)reader["user_rating"],
                    };
                }
            }

            return user;
        }

        public User GetUserByName(string userName)
        {
            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            User user = default(User);

            using (var con = new SqlConnection(conStr))
            {
                var query = "SELECT TOP 1 u.user_id, u.user_arole, u.user_rating FROM comics_tUser as u WHERE u.user_name = @userName";

                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@userName", userName);

                con.Open();
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    user = new User
                    {
                        Id = (Guid)reader["user_id"],
                        Name = userName,
                        ARole = (ARole)reader["user_arole"],
                        Rating = (int)reader["user_rating"],
                    };
                }
            }

            return user;
        }

        public bool ChangeARole(Guid userId, int role)
        {
            if ((role != 0) & (role != 1) & (role != 3) & (role != 7))
            {
                return false;
            }

            int result;

            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (var con = new SqlConnection(conStr))
            {
                var query = "UPDATE comics_tUser SET user_arole = @role WHERE user_id = @userId";
                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@role", role);

                con.Open();
                result = command.ExecuteNonQuery(); // result > 0;
            }

            return result > 0;
        }

        public bool Edit(Guid userId, User newUser)
        {
            int result;

            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (var con = new SqlConnection(conStr))
            {
                var query = "UPDATE comics_tUser SET user_name = @name, user_email = @email WHERE user_id = @userId";
                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@email", newUser.Email);
                command.Parameters.AddWithValue("@name", newUser.Name);

                command.Parameters.AddWithValue("@userId", userId);
                
                con.Open();
                result = command.ExecuteNonQuery(); // result > 0;
            }

            return result > 0;
        }
    }
}
