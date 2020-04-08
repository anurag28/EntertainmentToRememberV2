using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace EntertainmentToRememberV2.Models
{
    public class Song : ISong
    {
        private string title;
        private string singer;
        private string genre;
        private int year;

        public string Title { get => title; set => title = value; }
        public string Singer { get => singer; set => singer = value; }
        public string Genre { get => genre; set => genre = value; }
        public int Year { get => year; set => year = value; }

        public Song()
        {
            this.title = "";
            this.singer = "";
            this.genre = "";
            this.year = 0;
        }

        public Song(string title, string singer, string genre, int year) 
        {
            this.title = title;
            this.singer = singer;
            this.genre = genre;
            this.year = year;
        }

        public int CompareTo(ISong other)
        {
            return year.CompareTo(other.Year);
        }

        public override string ToString()
        {
            return string.Format("Song title is {0}, singer is {1}, genre is {2} and year is ", title, singer, genre, year);
        }
    }
}
