namespace comics.DAL.SQL
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Linq;
    using comics.DAL.Contract;
    using comics.Entities;

    public class PageDao : IPageDao
    {
        public bool Add(Page page)
        {
            int result;

            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (var con = new SqlConnection(conStr))
            {
                var query = "INSERT INTO comics_tPage (page_id, page_img, page_name, page_mime, page_issue) VALUES (@id, @img, @name, @mime, @issue)";
                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@id", page.Id);
                command.Parameters.AddWithValue("@img", page.img);
                command.Parameters.AddWithValue("@name", page.fileName);
                command.Parameters.AddWithValue("@mime", page.MIME);
                command.Parameters.AddWithValue("@issue", page.IssueId);

                con.Open();
                result = command.ExecuteNonQuery(); // result > 0;
            }

            return result > 0;
        }

        public Page GetOnePagesOnIssue(Guid issueId, int number)
        {
            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            Page page = default(Page);

            using (var con = new SqlConnection(conStr))
            {
                ////http://www.sql.ru/blogs/decolores/213
                var query = "SELECT * FROM (select row_number() over(order by (comics_tPage.page_name)) as number, * FROM comics_tPage WHERE comics_tPage.page_issue = @issueId) q WHERE number BETWEEN @number AND @number";

                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@issueId", issueId);
                command.Parameters.AddWithValue("@number", number);

                con.Open();
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    page = new Page
                    {
                        Id = (Guid)reader["page_id"],
                        IssueId = issueId,
                        img = (byte[])reader["page_img"],
                        fileName = (string)reader["page_name"],
                        MIME = (string)reader["page_mime"]
                    };
                }
            }

            return page;
        }

        public int GetCountPagesOnIssue(Guid issueId)
        {
            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            int result = default(int);

            using (var con = new SqlConnection(conStr))
            {
                var query = "SELECT count(*) as count FROM comics_tPage as p WHERE p.page_issue = @issueId";

                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@issueId", issueId);

                con.Open();
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    result = (int)reader["count"];
                }

                return result;
            }
        }

        public IEnumerable<Page> GetAllPagesOnIssue(Guid issueId)
        {
            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            List<Page> pages = new List<Page>().ToList();

            using (var con = new SqlConnection(conStr))
            {
                var query = "SELECT page_img, page_name, page_mime FROM comics_tPage as p WHERE p.page_issue = @issueId";

                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@issueId", issueId);

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    pages.Add(new Page
                    {
                        Id = (Guid)reader["page_id"],
                        fileName = (string)reader["page_name"],
                        img = (byte[])reader["page_img"],
                        MIME = (string)reader["page_mime"],
                        IssueId = issueId,
                    });
                }

                return pages;
            }
        }

        public bool Delete(Guid pageId)
        {
            int result;

            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (var con = new SqlConnection(conStr))
            {
                var query = "DELETE FROM comics_tPage WHERE comics_tPage.page_id = @pageId";
                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@pageId", pageId);
               
                con.Open();
                result = command.ExecuteNonQuery(); // result > 0;
            }

            return result > 0;
        }

        public bool DeleteAllPagesOnIssue(Guid issueId)
        {
            int result;

            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (var con = new SqlConnection(conStr))
            {
                var query = "DELETE FROM comics_tPage WHERE comics_tPage.page_issue = @issueId";
                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@issueId", issueId);
                
                con.Open();
                result = command.ExecuteNonQuery(); // result > 0;
            }

            return result > 0;
        }
    }
}