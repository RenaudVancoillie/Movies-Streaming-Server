using System;
using System.Collections.Generic;

#nullable disable

namespace Movies_DAL.Models
{
    public partial class Person
    {
        public Person()
        {
            MovieRoles = new HashSet<MovieRole>();
        }

        public long Id { get; set; }
        public string ImdbId { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }

        public virtual ICollection<MovieRole> MovieRoles { get; set; }
    }
}
