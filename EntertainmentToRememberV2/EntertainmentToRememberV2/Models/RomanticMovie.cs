using System;
using System.Collections.Generic;
using System.Text;

namespace EntertainmentToRememberV2.Models
{
    public class RomanticMovie : Movie
    {

        public RomanticMovie() { }

        public RomanticMovie(string name, string cast, string genre, string rating) : base(name, cast, genre, rating)
        {

        }
    }
}
