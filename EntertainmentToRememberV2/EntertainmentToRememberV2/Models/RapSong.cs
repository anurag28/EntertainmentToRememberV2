using System;
using System.Collections.Generic;
using System.Text;

namespace EntertainmentToRememberV2.Models
{
    public class RapSong : Song
    {
        public RapSong()
        {

        }

        public RapSong(string title, string singer, string genre, int year) : base(title, singer, genre, year)
        {

        }
    }
}
