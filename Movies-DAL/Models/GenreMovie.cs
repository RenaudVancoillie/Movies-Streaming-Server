using System;
using System.Collections.Generic;

#nullable disable

namespace Movies_DAL.Models
{
    public partial class GenreMovie
    {
        public long MovieId { get; set; }
        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
