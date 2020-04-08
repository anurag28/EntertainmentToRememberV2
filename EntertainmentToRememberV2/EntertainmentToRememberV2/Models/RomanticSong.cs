using System;
using System.Collections.Generic;
using System.Text;

namespace EntertainmentToRememberV2.Models
{
    public class RomanticSong : Song
    {
        public RomanticSong()
        {

        }

        public RomanticSong(string title, string singer, string genre, int year) : base(title, singer, genre, year)
        {

        }
    }
}
