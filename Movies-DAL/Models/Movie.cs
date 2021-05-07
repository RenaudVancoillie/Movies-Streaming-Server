using System;
using System.Collections.Generic;

#nullable disable

namespace Movies_DAL.Models
{
    public partial class Movie
    {
        public Movie()
        {
            GenreMovies = new HashSet<GenreMovie>();
            MovieRoles = new HashSet<MovieRole>();
        }

        public long Id { get; set; }
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string CoverUrl { get; set; }
        public int Year { get; set; }
        public string OriginalAirDate { get; set; }
        public string Kind { get; set; }
        public decimal Rating { get; set; }
        public string Plot { get; set; }
        public int Top250Rank { get; set; }

        public virtual ICollection<GenreMovie> GenreMovies { get; set; }
        public virtual ICollection<MovieRole> MovieRoles { get; set; }
    }
}
