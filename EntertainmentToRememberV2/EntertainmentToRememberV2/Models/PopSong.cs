using System;
using System.Collections.Generic;
using System.Text;

namespace EntertainmentToRememberV2.Models
{
    public class PopSong : Song
    {
        public PopSong()
        {

        }

        public PopSong(string title, string singer, string genre, int year) : base(title, singer, genre, year)
        {

        }
    }
}
