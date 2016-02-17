namespace comics.DAL.Contract
{
    using System;
    using System.Collections.Generic;
    using Entities;

    public interface IPageDao
    {
        bool Add(Page page);

        int GetCountPagesOnIssue(Guid issueId);

        IEnumerable<Page> GetAllPagesOnIssue(Guid issueId);

        Page GetOnePagesOnIssue(Guid issueId, int number);

        bool Delete(Guid pageId);

        bool DeleteAllPagesOnIssue(Guid issueId);
    }
}