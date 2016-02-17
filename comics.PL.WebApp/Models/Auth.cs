namespace comics.PL.WebApp.Models
{
    public static class Auth
    {
        public static bool CanLogin(string login, string password)
        {
            ////if ((login == "admin") == (true)) return true;

            return new BLL.Core.AutificationLogic().Validate(login, password);
        }
    }
}