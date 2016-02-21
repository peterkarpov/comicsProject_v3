namespace comics.Entities
{
    using System;

    public class Page
    {
        public Guid Id;

        public byte[] img;

        public string fileName;

        public string MIME;

        public Guid IssueId;
    }
}
