namespace comics.DAL.SQL
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Linq;
    using comics.DAL.Contract;
    using comics.Entities;

    public class CommentDao : ICommentDao
    {
        public bool Add(Comment comment)
        {
            int result;

            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (var con = new SqlConnection(conStr))
            {
                var query = "INSERT INTO comics_tComment (comment_id, comment_author, comment_time, comment_text, comment_issue) VALUES (@id, @author, @time, @text, @issue)";
                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@id", comment.Id);
                command.Parameters.AddWithValue("@author", comment.AuthorID);
                command.Parameters.AddWithValue("@time", comment.CreationTime);
                command.Parameters.AddWithValue("@text", comment.Text);
                command.Parameters.AddWithValue("@issue", comment.IssueID);

                con.Open();
                result = command.ExecuteNonQuery(); // result > 0;
            }

            return result > 0;
        }

        public bool Delete(Guid CommentId)
        {
            int result;

            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (var con = new SqlConnection(conStr))
            {
                var query = "DELETE FROM comics_tComment WHERE comics_tComment.comment_id = @id";
                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@id", CommentId);

                con.Open();
                result = command.ExecuteNonQuery(); // result > 0;
            }

            return result > 0;
        }

        public bool ExistComment(Guid commentId)
        {
            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (var con = new SqlConnection(conStr))
            {
                var query = "SELECT TOP 1 c.comment_id FROM comics_tComment as c WHERE c.comment_id = @userId";

                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@userId", commentId);

                con.Open();
                var reader = command.ExecuteReader();

                return reader.Read();
            }
        }

        public IEnumerable<Comment> GetAllForIssue(Guid issueId)
        {
            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            List<Comment> comments = new List<Comment>().ToList();

            using (var con = new SqlConnection(conStr))
            {
                var query = "SELECT comment_id, comment_author, comment_time, comment_text FROM comics_tComment as c WHERE c.comment_issue = @issueId ORDER BY comment_time DESC";
                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@issueId", issueId);

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    comments.Add(new Comment
                    {
                        Id = (Guid)reader["comment_id"],
                        AuthorID = (Guid)reader["comment_author"],
                        CreationTime = (DateTime)reader["comment_time"],
                        IssueID = issueId,
                        Text = (string)reader["comment_text"],
                    });
                }
            }

            return comments;
        }

        public IEnumerable<Comment> GetAllForUser(Guid userId)
        {
            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            List<Comment> comments = new List<Comment>().ToList();

            using (var con = new SqlConnection(conStr))
            {
                var query = "SELECT comment_id, comment_issue, comment_time, comment_text FROM comics_tComment as c WHERE c.comment_author = @userId ORDER BY comment_time DESC";
                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@userId", userId);

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    comments.Add(new Comment
                    {
                        Id = (Guid)reader["comment_id"],
                        AuthorID = userId,
                        CreationTime = (DateTime)reader["comment_time"],
                        IssueID = (Guid)reader["comment_issue"],
                        Text = (string)reader["comment_text"],
                    });
                }
            }

            return comments;
        }
    }
}
