namespace comics.Entities
{
    using System;

    public class Issue
    {
        public Guid Id { get; set; }

        public string IssueName { get; set; }

        public string Volume { get; set; }

        public string Series { get; set; }
    }
}
