﻿using System.Collections.Generic;

namespace Chinook_Assignment.Domain.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Tracks = new HashSet<Track>();
        }

        public int GenreId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Track> Tracks { get; set; } 
    }
}
