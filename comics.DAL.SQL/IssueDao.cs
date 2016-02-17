namespace comics.DAL.SQL
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Linq;
    using comics.DAL.Contract;
    using comics.Entities;

    public class IssueDao : IIssueDao
    {
        public bool Add(Issue issue)
        {
            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            
            using (var con = new SqlConnection(conStr))
            {
                var query = "SELECT TOP 1 i.issue_id FROM comics_tIssue as i WHERE i.issue_name = @name AND i.issue_volume = @volume AND i.issue_series = @series";

                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@name", issue.IssueName);
                command.Parameters.AddWithValue("@volume", issue.Volume);
                command.Parameters.AddWithValue("@series", issue.Series);

                con.Open();
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return false;
                }
            }
            
            int result;
            
            using (var con = new SqlConnection(conStr))
            {
                var query = "INSERT INTO comics_tIssue (issue_id, issue_name, issue_volume, issue_series) VALUES (@id, @name, @volume, @series)";
                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@id", issue.Id);
                command.Parameters.AddWithValue("@name", issue.IssueName);
                command.Parameters.AddWithValue("@volume", issue.Volume);
                command.Parameters.AddWithValue("@series", issue.Series);

                con.Open();
                result = command.ExecuteNonQuery(); // result > 0;
            }

            return result > 0;
        }

        public bool Delete(Guid issueId)
        {
            int result;

            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (var con = new SqlConnection(conStr))
            {
                var query = "DELETE FROM comics_tIssue WHERE comics_tIssue.issue_id = @id";
                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@id", issueId);

                con.Open();
                result = command.ExecuteNonQuery(); // result > 0;
            }

            return result > 0;
        }

        public bool ExistIssue(Guid id)
        {
            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (var con = new SqlConnection(conStr))
            {
                var query = "SELECT TOP 1 i.issue_id FROM comics_tIssue as i WHERE i.issue_id = @id";

                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@id", id);

                con.Open();
                var reader = command.ExecuteReader();

                return reader.Read();
            }
        }

        public Issue GetIssueById(Guid issueId)
        {
            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            Issue issue = default(Issue);

            using (var con = new SqlConnection(conStr))
            {
                var query = "SELECT TOP 1 i.issue_name, i.issue_volume, i.issue_series FROM comics_tIssue as i WHERE i.issue_id = @id";

                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@id", issueId);

                con.Open();
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    issue = new Issue
                    {
                        Id = issueId,
                        IssueName = (string)reader["issue_name"],
                        Volume = (string)reader["issue_volume"],
                        Series = (string)reader["issue_series"],
                    };
                }
            }

            return issue;
        }
        
        public IEnumerable<string> GetListOfSeries()
        {
            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            List<string> series = new List<string>().ToList();

            using (var con = new SqlConnection(conStr))
            {
                var query = "SELECT i.issue_series FROM comics_tIssue as i ORDER BY i.issue_series";

                var command = new SqlCommand(query, con);
                             
                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var seria = (string)reader["issue_series"];
                    if (series.Contains(seria)) continue;
                    series.Add(seria);
                }

                return series;
            }
        }

        public IEnumerable<string> GetListOfVolumeOnSeries(string series)
        {
            if (series == default(string))
            {
                throw new ArgumentNullException("series is null");
            }

            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            List<string> volumes = new List<string>().ToList();

            using (var con = new SqlConnection(conStr))
            {
                var query = "SELECT i.issue_volume FROM comics_tIssue as i WHERE i.issue_series = @series ORDER BY i.issue_volume";

                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@series", series);

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var volume = (string)reader["issue_volume"];
                    if (volumes.Contains(volume)) continue;
                    volumes.Add(volume);
                }

                return volumes;
            }
        }

        public IEnumerable<Issue> GetIssueOnVolume(string volume)
        {
            if (volume == default(string))
            {
                throw new ArgumentNullException("series is null");
            }

            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            List<Issue> issues = new List<Issue>().ToList();

            using (var con = new SqlConnection(conStr))
            {
                var query = "SELECT i.issue_id, i.issue_name, i.issue_volume, i.issue_series FROM comics_tIssue as i WHERE i.issue_volume = @volume ORDER BY i.issue_name";

                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@volume", volume);

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    issues.Add(new Issue
                    {
                        Id = (Guid)reader["issue_id"],
                        IssueName = (string)reader["issue_name"],
                        Volume = volume,
                        Series = (string)reader["issue_series"],

                    });
                }
                return issues;
            }
        }

        public IEnumerable<string> GetListOfIssueOnVolume(string volume)
        {
            if (volume == default(string))
            {
                throw new ArgumentNullException("series is null");
            }

            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            List<string> issues = new List<string>().ToList();

            using (var con = new SqlConnection(conStr))
            {
                var query = "SELECT i.issue_name FROM comics_tIssue as i WHERE i.issue_volume = @volume ORDER BY i.issue_name";

                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@volume", volume);

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var issue = (string)reader["issue_name"];
                    if (issues.Contains(issue)) continue;
                    issues.Add(issue);
                }

                return issues;
            }
        }

        public IEnumerable<Issue> GetAllIssue()
        {
            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            List<Issue> issues = new List<Issue>().ToList();

            using (var con = new SqlConnection(conStr))
            {
                var query = "SELECT i.issue_id, i.issue_name, i.issue_volume, i.issue_series FROM comics_tIssue as i ORDER BY i.issue_series, i.issue_volume, i.issue_name";

                var command = new SqlCommand(query, con);

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    issues.Add(new Issue
                    {
                        Id = (Guid)reader["issue_id"],
                        IssueName = (string)reader["issue_name"],
                        Volume = (string)reader["issue_volume"],
                        Series = (string)reader["issue_series"],
                    });
                }

                return issues;
            }
        }

        public bool EditIssue(Issue issue)
        {
            int result;

            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (var con = new SqlConnection(conStr))
            {
                var query = "UPDATE comics_tIssue SET issue_name = @name, issue_volume = @volume, issue_series = @series WHERE issue_id = @id";
                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@id", issue.Id);
                command.Parameters.AddWithValue("@name", issue.IssueName);
                command.Parameters.AddWithValue("@volume", issue.Volume);
                command.Parameters.AddWithValue("@series", issue.Series);

                con.Open();
                result = command.ExecuteNonQuery(); // result > 0;
            }

            return result > 0;
        }
    }
}