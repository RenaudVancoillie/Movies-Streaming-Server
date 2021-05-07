using System;
using System.Collections.Generic;

#nullable disable

namespace Movies_DAL.Models
{
    public partial class MovieRole
    {
        public long MovieId { get; set; }
        public long PersonId { get; set; }
        public string Role { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Person Person { get; set; }
    }
}
