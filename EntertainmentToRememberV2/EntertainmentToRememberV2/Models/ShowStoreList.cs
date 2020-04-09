using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace EntertainmentToRememberV2.Models
{
    [XmlRoot("ShowList")]
    public class ShowStoreList
    {
        private List<Show> showList = null;

        [XmlArray("Shows")]
        [XmlArrayItem("Show")]
        public List<Show> ShowList { get => showList; set => showList = value; }
        public ShowStoreList()
        {
            ShowList = new List<Show>();
        }

        public void Add(Show show)
        {
            ShowList.Add(show);
        }

        public void Remove(Show show)
        {
            ShowList.Remove(show);
        }

        public int Count()
        {
            return ShowList.Count;
        }

        public void Clear()
        {
            ShowList.Clear();
        }

        public void Sort()
        {
            ShowList.Sort();
        }


    }
}
