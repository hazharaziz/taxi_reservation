using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class User
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public GenderType Gender { get; set; }

        public double Balance { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
