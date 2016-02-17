namespace comics.BLL.Core
{
    using System.Configuration;
    using DAL.Contract;

    class Daos
    {
        static Daos()
        {
            switch (ConfigurationManager.AppSettings["DAL"])
            {
                case "sql":
                    UserDao = new DAL.SQL.UserDao();
                    SuccessDao = new DAL.SQL.SuccessDao();
                    CommentDao = new DAL.SQL.CommentDao();
                    IssueDao = new DAL.SQL.IssueDao();
                    PageDao = new DAL.SQL.PageDao();
                    break;

                default:
                    break;
            }
        }

        public static IUserDao UserDao { get; }

        public static ISuccessDao SuccessDao { get; }

        public static ICommentDao CommentDao { get; }

        public static IIssueDao IssueDao { get; }

        public static IPageDao PageDao { get; }

        public static IAuthenticationDao AutificationDao { get; } = new DAL.SQL.AuthenticationDaoSQL();
    }
}