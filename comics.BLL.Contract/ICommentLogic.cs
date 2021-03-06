﻿namespace comics.BLL.Contract
{
    using System;
    using System.Collections.Generic;
    using comics.Entities;

    public interface ICommentLogic
    {
        bool Add(Comment comment);

        bool Delete(Guid CommentId);

        IEnumerable<Comment> GetAllForIssue(Guid issueId);

        IEnumerable<Comment> GetAllForUser(Guid userId);

        bool ExistComment(Guid commentId);
    }
}
