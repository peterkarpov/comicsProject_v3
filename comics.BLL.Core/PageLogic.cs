namespace comics.BLL.Core
{
    using comics.BLL.Contract;
    using comics.Entities;
    using System;
    using System.Collections.Generic;
    
    public class PageLogic : IPageLogic
    {
        public bool Add(Page page)
        {
            if (page.Id == default(Guid))
            {
                page.Id = Guid.NewGuid();
            }

            if ((page.MIME != "")
                & (page.IssueId != default(Guid))
                & (page.img != default(byte[]))
                & (page.fileName != ""))
            {
                return Daos.PageDao.Add(page);
            }
            else
            {
                return false;
                ////throw new ArgumentException("No valide argument of Page");
            }
        }

        public IEnumerable<Page> GetAllPagesOnIssue(Guid issueId)
        {
            return Daos.PageDao.GetAllPagesOnIssue(issueId);
        }

        public Page GetOnePagesOnIssue(Guid issueId, int number)
        {
            if (number > 0)
            {
                return Daos.PageDao.GetOnePagesOnIssue(issueId, number);
            }
            else
            {
                throw new ArgumentException("No valide number of Page");
            }
        }

        public int GetCountPagesOnIssue(Guid issueId)
        {
            return Daos.PageDao.GetCountPagesOnIssue(issueId);
        }

        public bool Delete(Guid pageId)
        {
            return Daos.PageDao.Delete(pageId);
        }

        public bool DeleteAllPagesOnIssue(Guid issueId)
        {
            return Daos.PageDao.DeleteAllPagesOnIssue(issueId);
        }
    }
}
