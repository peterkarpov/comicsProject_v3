﻿namespace comics.Entities
{
    using System;

    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        ////public string Role { get; set; }

        public int Rating { get; set; }

        public string Email { get; set; }

        public ARole ARole { get; set; }
    }
}