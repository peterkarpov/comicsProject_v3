namespace comics.DAL.Contract
{
    using System;
    using System.Collections.Generic;
    using comics.Entities;

    public interface IIssueDao
    {
        bool Add(Issue issue);

        bool Delete(Guid issueId);

        bool EditIssue(Issue issue);

        Issue GetIssueById(Guid issueId);

        bool ExistIssue(Guid id);

        IEnumerable<Issue> GetIssueOnVolume(string volume);

        IEnumerable<string> GetListOfSeries();

        IEnumerable<string> GetListOfVolumeOnSeries(string series);

        IEnumerable<string> GetListOfIssueOnVolume(string volume);
        
        IEnumerable<Issue> GetAllIssue();
    }
}
