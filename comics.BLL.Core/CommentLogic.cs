namespace comics.BLL.Core
{
    using Contract;
    using Entities;
    using System;
    using System.Collections.Generic;
    
    public class CommentLogic : ICommentLogic
    {
        public bool Add(Comment comment)
        {
            if (comment.Id == default(Guid))
            {
                comment.Id = Guid.NewGuid();
            }

            if ((comment.Text != "") | (comment.CreationTime.ToString() != "") | (Daos.UserDao.ExistUser(comment.AuthorID) | Daos.IssueDao.ExistIssue(comment.IssueID)))
            {
                return Daos.CommentDao.Add(comment);
            }
            else
            {
                throw new ArgumentException("No valide Text or CreationTime or Author or Issue of Comment");
            }
        }

        public bool Delete(Guid commentId)
        {
            if (this.ExistComment(commentId))
            {
                return Daos.CommentDao.Delete(commentId);
            }
            else
            {
                throw new ArgumentException("No have this ID");
            }
        }

        public bool ExistComment(Guid commentId)
        {
            return Daos.CommentDao.ExistComment(commentId);
        }

        public IEnumerable<Comment> GetAllForIssue(Guid issueId)
        {
            if (Daos.IssueDao.ExistIssue(issueId))
            {
                return Daos.CommentDao.GetAllForIssue(issueId);
            }
            else
            {
                throw new ArgumentException("No have this ID");
            }
        }

        public IEnumerable<Comment> GetAllForUser(Guid userId)
        {
            if (Daos.UserDao.ExistUser(userId))
            {
                return Daos.CommentDao.GetAllForUser(userId);
            }
            else
            {
                throw new ArgumentException("No have this ID");
            }
        }
    }
}
