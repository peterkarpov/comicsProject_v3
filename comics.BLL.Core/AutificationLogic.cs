namespace comics.BLL.Core
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using comics.BLL.Contract;

    public class AutificationLogic : IAuthenticationLogic
    {
        public bool Delete(string login)
        {
            if (login == "admin") return false;

            if (login != "")
            {
                return Daos.AutificationDao.Delete(login);
            }
            else
            {
                return false;
            }
        }

        public bool Edit(string oldLogin, string login, string password)
        {
            if ((login != "") & (password != ""))
            {
                var passwordHash = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(password)));

                return Daos.AutificationDao.Edit(oldLogin, login, passwordHash);
            }
            else
            {
                return false;
            }
        }

        public bool Registration(string login, string password)
        {
            if ((login != "") & (password != ""))
            {
                var passwordHash = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(password)));

                return Daos.AutificationDao.Registration(login, passwordHash);
            }
            else
            {
                return false;
            }
        }

        public bool Validate(string login, string password)
        {
            string hash = comics.DAL.SQL.AuthenticationDaoSQL.GetPasswordHashForUser(login);

            var passwordHash = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(password)));

            return hash == passwordHash;
        }
    }
}
