using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace EntertainmentToRememberV2.Models
{
    [XmlRoot("SongList")]
    [XmlInclude(typeof(RockSong))]
    [XmlInclude(typeof(RomanticSong))]
    [XmlInclude(typeof(TranceSong))]
    [XmlInclude(typeof(RapSong))]
    [XmlInclude(typeof(PopSong))]
    public class SongStoreList
    {
        private List<Song> songList = null;

        [XmlArray("Songs")]
        [XmlArrayItem("Song", typeof(Song))]
        public List<Song> SongList
        {
            get => songList;
            set => songList = value;
        }

        public SongStoreList()
        {
            SongList = new List<Song>();
        }

        public void Add(Song song)
        {
            SongList.Add(song);
        }

        public void Remove(Song song)
        {
            SongList.Remove(song);
        }

        public int Count()
        {
            return SongList.Count;
        }

        public void Clear()
        {
            SongList.Clear();
        }

        public void Sort()
        {
            SongList.Sort();
        }
    }
}
