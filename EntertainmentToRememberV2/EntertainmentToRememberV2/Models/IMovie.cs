using System;
using System.Collections.Generic;
using System.Text;

namespace EntertainmentToRememberV2.Models
{
    public interface IMovie : IComparable<IMovie>
    {
        public string Name { get; set; }
        public string Cast { get; set; }
        public string Genre { get; set; }
        public string Rating { get; set; }
    }
}
