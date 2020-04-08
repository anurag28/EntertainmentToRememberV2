using System;
using System.Collections.Generic;
using System.Text;

namespace EntertainmentToRememberV2.Models
{
    public class DramaMovie : Movie
    {
        public DramaMovie()
        {

        }

        public DramaMovie(string name, string cast, string genre, string rating) : base(name, cast, genre, rating)
        {

        }
    }
}
