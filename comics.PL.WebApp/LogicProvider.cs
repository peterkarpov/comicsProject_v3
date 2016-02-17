namespace comics.PL.WebApp
{
    public class LogicProvider
    {
        public static BLL.Core.UserLogic GetUserLogicForWeb { get; } = new BLL.Core.UserLogic();

        public static BLL.Core.CommentLogic GetCommentLogicForWeb { get; } = new BLL.Core.CommentLogic();

        public static BLL.Core.IssueLogic GetIssueLogicForWeb { get; } = new BLL.Core.IssueLogic();

        public static BLL.Core.SuccessLogic GetSuccessLogicForWeb { get; } = new BLL.Core.SuccessLogic();

        public static BLL.Core.PageLogic GetPageLogicForWeb { get; } = new BLL.Core.PageLogic();

        public static BLL.Core.AutificationLogic GetAutificationLogicForWeb { get; } = new BLL.Core.AutificationLogic();
    }
}