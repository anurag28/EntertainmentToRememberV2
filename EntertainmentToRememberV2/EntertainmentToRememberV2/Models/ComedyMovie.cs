using System;
using System.Collections.Generic;
using System.Text;

namespace EntertainmentToRememberV2.Models
{
    public class ComedyMovie : Movie
    {
        public ComedyMovie()
        {

        }

        public ComedyMovie(string name, string cast, string genre, string rating) : base(name, cast, genre, rating)
        {

        }
    }
}
