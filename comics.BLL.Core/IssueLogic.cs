namespace comics.BLL.Core
{
    using comics.BLL.Contract;
    using comics.Entities;
    using System;
    using System.Collections.Generic;
    
    public class IssueLogic : IIssueLogic
    {
        public bool Add(Issue issue)
        {
            if (issue.Id == default(Guid))
            {
                issue.Id = Guid.NewGuid();
            }
            
            if ((issue.IssueName != "") | (issue.Volume != "") | (issue.Series != ""))
            {
                return Daos.IssueDao.Add(issue);
            }
            else
            {
                ////throw new ArgumentException("No valide filePath of Issue");
                return false;
            }
        }

        public bool Delete(Guid issueId)
        {
            return Daos.IssueDao.Delete(issueId);
        }

        public bool ExistIssue(Guid issueId)
        {
            return Daos.IssueDao.ExistIssue(issueId);
        }

        public Issue GetIssueById(Guid issueId)
        {
            return Daos.IssueDao.GetIssueById(issueId);
        }
        
        public IEnumerable<string> GetListOfVolumeOnSeries(string series)
        {
            if (series != "")
            {
                return Daos.IssueDao.GetListOfVolumeOnSeries(series);
            }
            else
            {
                return default(string[]);
            }
        }

        public IEnumerable<Issue> GetAllIssue()
        {
            return Daos.IssueDao.GetAllIssue();
        }

        public IEnumerable<string> GetListOfSeries()
        {
            return Daos.IssueDao.GetListOfSeries();
        }

        public IEnumerable<Issue> GetIssueOnVolume(string volume)
        {
            if (volume != "")
            {
                return Daos.IssueDao.GetIssueOnVolume(volume);
            }
            else
            {
                throw new ArgumentException("No valide volume of Issue");
            }
        }

        public IEnumerable<string> GetListOfIssueOnVolume(string volume)
        {
            if (volume != "")
            {
                return Daos.IssueDao.GetListOfIssueOnVolume(volume);
            }
            else
            {
                return default(string[]);
            }
        }

        public bool EditIssue(Issue issue)
        {
            return Daos.IssueDao.EditIssue(issue);
        }
    }
}
