namespace comics.Entities
{
    using System;

    [Flags]
    public enum ARole
    {
        none = 0,
        user = 1,
        moderator = 2,
        admin = 4,
    }
}