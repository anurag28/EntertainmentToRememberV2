using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace EntertainmentToRememberV2.Models
{
    [XmlRoot("MovieList")]
    [XmlInclude(typeof(ActionMovie))]
    [XmlInclude(typeof(DramaMovie))]
    [XmlInclude(typeof(ThrillerMovie))]
    [XmlInclude(typeof(RomanticMovie))]
    [XmlInclude(typeof(ComedyMovie))]
    public class MovieStoreList
    {
        private List<Movie> movieList = null;

        [XmlArray("Movies")]
        [XmlArrayItem("Movie", typeof(Movie))]
        public List<Movie>MovieList { 
            get => movieList; 
            set=> movieList=value; }

        public MovieStoreList()
        {
            MovieList = new List<Movie>();
        }

        public void Add(Movie movie)
        {
            MovieList.Add(movie);
        }

        public void Remove(Movie movie)
        {
            MovieList.Remove(movie);
        }

        public int Count()
        {
            return MovieList.Count;
        }

        public void Clear()
        {
            MovieList.Clear();
        }

        public void Sort()
        {
            MovieList.Sort();
        }
    }
}
