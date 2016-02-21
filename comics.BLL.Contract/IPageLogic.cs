namespace comics.BLL.Contract
{
    using System;
    using System.Collections.Generic;
    using comics.Entities;

    public interface IPageLogic
    {
        bool Add(Page page);

        int GetCountPagesOnIssue(Guid issueId);

        IEnumerable<Page> GetAllPagesOnIssue(Guid issueId);

        Page GetOnePagesOnIssue(Guid issueId, int number);

        bool Delete(Guid pageId);

        bool DeleteAllPagesOnIssue(Guid issueId);
    }
}
