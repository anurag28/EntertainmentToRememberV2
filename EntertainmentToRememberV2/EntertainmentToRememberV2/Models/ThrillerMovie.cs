using System;
using System.Collections.Generic;
using System.Text;

namespace EntertainmentToRememberV2.Models
{
    public class ThrillerMovie : Movie
    {

        public ThrillerMovie()
        {

        }

        public ThrillerMovie(string name, string cast, string genre, string rating) : base(name, cast, genre, rating)
        {

        }
    }
}
