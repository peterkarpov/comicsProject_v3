namespace comics.Entities
{
    using System;

    public class Success
    {
        public Guid Id { get; set; }

        public string SuccessDiscription { get; set; }

        public string Status { get; set; }

        public DateTime CreationTime { get; set; }

        public Guid CuratorId { get; set; }

        public Guid PerformerId { get; set; }
    }
}
