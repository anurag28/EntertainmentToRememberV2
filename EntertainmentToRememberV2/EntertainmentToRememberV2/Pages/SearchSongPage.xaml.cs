using EntertainmentToRememberV2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

namespace EntertainmentToRememberV2.Pages
{
    /// <summary>
    /// Interaction logic for SearchSongPage.xaml
    /// </summary>
    public partial class SearchSongPage : Page
    {
        private ObservableCollection<Song> displaySongs = null;

        public ObservableCollection<Song> DisplaySongs 
        {
            get => displaySongs; 
            set => displaySongs=value; 
        }

        SongStoreList songList = new SongStoreList();

        public SearchSongPage()
        {
            InitializeComponent();
            displaySongs = new ObservableCollection<Song>();
            DataContext = this;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            songList.Clear();
            ReadFromFile();
            DisplaySongs.Clear();
            if (txtTitle.Text != "" && txtSinger.Text == "" && cmbGenre.SelectedIndex == -1)
            {
                SearchByTitle(txtTitle.Text);
            }

            else if (txtTitle.Text == "" && txtSinger.Text != "" && cmbGenre.SelectedIndex == -1)
            {
                SearchBySinger(txtSinger.Text);
            }

            else if (txtTitle.Text == "" && txtSinger.Text == "" && cmbGenre.SelectedIndex != -1)
            {
                SearchByGenre(this.cmbGenre.SelectionBoxItem.ToString());
            }

            else if (txtTitle.Text != "" && txtSinger.Text != "" && cmbGenre.SelectedIndex == -1)
            {
                SearchByTitleAndSinger(txtTitle.Text, txtSinger.Text);
            }

            else if (txtTitle.Text != "" && txtSinger.Text == "" && cmbGenre.SelectedIndex != -1)
            {
                SearchByTitleAndGenre(txtTitle.Text, this.cmbGenre.SelectionBoxItem.ToString());
            }

            else if (txtTitle.Text == "" && txtSinger.Text != "" && cmbGenre.SelectedIndex != -1)
            {
                SearchBySingerAndGenre(txtSinger.Text, this.cmbGenre.SelectionBoxItem.ToString());
            }

            else if (txtTitle.Text != "" && txtSinger.Text != "" && cmbGenre.SelectedIndex != -1)
            {
                SearchByTitleSIngerAndGenre(txtTitle.Text, txtSinger.Text, this.cmbGenre.SelectionBoxItem.ToString());
            }

            if (DisplaySongs.Count == 0)
            {
                MessageBox.Show("No Results Found..");
                ClearForm();
            }
            else
            {
                grdDisplaySongs.ItemsSource = DisplaySongs;
                ClearForm();
            }

        }

        private void ClearForm()
        {
            txtTitle.Text = string.Empty;
            txtSinger.Text = string.Empty;
            cmbGenre.SelectedIndex = -1;
        }


        private void ReadFromFile()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SongStoreList));
            TextReader _reader = new StreamReader("songs.xml");
            songList = (SongStoreList)serializer.Deserialize(_reader);
            _reader.Close();
        }



        private void SearchByTitle (string title)
        {
            var query = from song in songList.SongList
                        where song.Title.Contains(title)
                        select song;
            foreach (Song song in query)
            {
                DisplaySongs.Add(song);
            }
        }

        private void SearchBySinger(string singer)
        {
            var query = from song in songList.SongList
                        where song.Singer.Contains(singer)
                        select song;
            foreach (Song song in query)
            {
                DisplaySongs.Add(song);
            }
        }


        private void SearchByGenre(string genre)
        {
            var query = from song in songList.SongList
                        where song.Genre.Contains(genre)
                        select song;
            foreach (Song song in query)
            {
                DisplaySongs.Add(song);
            }
        }

        private void SearchByTitleAndSinger(string title, string singer)
        {
            var query = from song in songList.SongList
                        where song.Title.Contains(title)
                        && song.Singer.Contains(singer)
                        select song;
            foreach (Song song in query)
            {
                DisplaySongs.Add(song);
            }
        }

        private void SearchByTitleAndGenre(string title, string genre)
        {
            var query = from song in songList.SongList
                        where song.Title.Contains(title)
                        && song.Genre.Contains(genre)
                        select song;
            foreach (Song song in query)
            {
                DisplaySongs.Add(song);
            }
        }

        private void SearchBySingerAndGenre(string singer, string genre)
        {
            var query = from song in songList.SongList
                        where song.Singer.Contains(singer)
                        && song.Genre.Contains(genre)
                        select song;
            foreach (Song song in query)
            {
                DisplaySongs.Add(song);
            }
        }

        private void SearchByTitleSIngerAndGenre(string title, string singer, string genre)
        {
            var query = from song in songList.SongList
                        where song.Title.Contains(title) &&
                        song.Singer.Contains(singer) &&
                        song.Genre.Contains(genre)
                        select song;

            foreach(Song song in query)
            {
                DisplaySongs.Add(song);
            }
        }


        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HomePage());
        }
    }
}
