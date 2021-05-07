using System;
using System.Collections.Generic;

#nullable disable

namespace Movies_DAL.Models
{
    public partial class Genre
    {
        public Genre()
        {
            GenreMovies = new HashSet<GenreMovie>();
        }

        public int Id { get; set; }
        public string ImdbName { get; set; }
        public string Name { get; set; }

        public virtual ICollection<GenreMovie> GenreMovies { get; set; }
    }
}
