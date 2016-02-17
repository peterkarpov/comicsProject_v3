namespace comics.DAL.Contract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAuthenticationDao
    {
        bool Registration(string login, string password);

        bool Edit(string oldLogin, string login, string password);

        bool Delete(string login);
    }
}
