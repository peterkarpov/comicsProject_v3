namespace comics.Entities
{
    using System;

    public class Comment
    {
        public Guid Id { get; set; }

        public Guid AuthorID { get; set; }

        public DateTime CreationTime { get; set; }

        public string Text { get; set; }

        public Guid IssueID { get; set; }
    }
}
