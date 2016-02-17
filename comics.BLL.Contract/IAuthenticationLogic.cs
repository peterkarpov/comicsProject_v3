namespace comics.BLL.Contract
{
    public interface IAuthenticationLogic
    {
        bool Validate(string login, string password);

        bool Registration(string login, string password);

        bool Edit(string oldLogin, string login, string password);

        bool Delete(string login);
    }
}
