namespace comics.DAL.SQL
{
    using System;
    using System.Configuration;
    using System.Data.SqlClient;
    using comics.DAL.Contract;

    public class AuthenticationDaoSQL : IAuthenticationDao
    {
        static public string GetPasswordHashForUser(string login)
        {
            if (login == default(string))
            {
                throw new ArgumentNullException("Argument is null");
            }

            string password;

            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (var con = new SqlConnection(conStr))
            {
                var query = "SELECT TOP 1 p.password FROM TableOfAuthentication as p WHERE p.login = @login";

                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@login", login);

                con.Open();
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return password = (string)reader["password"];
                }
            }

            return null;
        }

        public bool Registration(string login, string password)
        {
            int result;

            if ((login == default(string)) | (password == default(string)))
            {
                throw new ArgumentNullException("login or password is null");
            }

            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (var con = new SqlConnection(conStr))
            {
                var query = "INSERT INTO TableOfAuthentication (login, password) VALUES (@login, @password)";

                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@login", login);
                command.Parameters.AddWithValue("@password", password);

                con.Open();
                try
                {
                    result = command.ExecuteNonQuery(); // result > 0;
                }
                catch
                {
                    result = 0;
                }
            }

            return result > 0;
        }

        public bool Edit(string oldLogin, string login, string password)
        {
            int result;

            if ((login == default(string)) | (password == default(string)))
            {
                throw new ArgumentNullException("login or password is null");
            }

            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (var con = new SqlConnection(conStr))
            {
                var query = "UPDATE TableOfAuthentication SET login = @login, password = @password WHERE login = @oldLogin";
                
                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@oldLogin", oldLogin);

                command.Parameters.AddWithValue("@login", login);
                command.Parameters.AddWithValue("@password", password);

                con.Open();
                try
                {
                    result = command.ExecuteNonQuery(); // result > 0;
                }
                catch
                {
                    result = 0;
                }
            }

            return result > 0;
        }

        public bool Delete(string login)
        {
            int result;

            if (login == default(string))
            {
                throw new ArgumentNullException("login or password is null");
            }

            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (var con = new SqlConnection(conStr))
            {
                var query = "DELETE FROM TableOfAuthentication WHERE login = @login";

                var command = new SqlCommand(query, con);
                
                command.Parameters.AddWithValue("@login", login);

                con.Open();
                try
                {
                    result = command.ExecuteNonQuery(); // result > 0;
                }
                catch
                {
                    result = 0;
                }
            }

            return result > 0;
        }
    }
}