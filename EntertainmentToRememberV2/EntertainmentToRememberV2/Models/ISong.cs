using System;
using System.Collections.Generic;
using System.Text;

namespace EntertainmentToRememberV2.Models
{
    public interface ISong : IComparable<ISong>
    {
        public string Title { get; set; }
        public string Singer { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
    }

}
