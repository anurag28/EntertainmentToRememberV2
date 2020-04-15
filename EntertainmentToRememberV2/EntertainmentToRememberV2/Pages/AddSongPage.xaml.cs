using EntertainmentToRememberV2.Models;
using System;
using System.Collections.Generic;
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

namespace EntertainmentToRememberV2.Pages
{
    /// <summary>
    /// Interaction logic for AddSongPage.xaml
    /// </summary>
    public partial class AddSongPage : Page
    {
        SongStoreList songList = new SongStoreList();
        SongStoreList savedSongList = new SongStoreList();

        private List<Song> enterSong = null;

        public List<Song> EnterSong
        { 
            get => enterSong; 
            set => enterSong = value; 
        }

        public AddSongPage()
        {
            InitializeComponent();
            enterSong = new List<Song>();
            if (File.Exists("songs.xml"))
            {
                ReadFromFileSongsAndAddToCurrentList();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Song newSong = new Song();

            switch (cmbGenre.SelectedIndex)
            {
                case 1:
                    newSong = new Song(txtTitle.Text, txtSinger.Text, "Rock", int.Parse(txtYear.Text));
                    break;
                case 2:
                    newSong = new Song(txtTitle.Text, txtSinger.Text, "Romantic", int.Parse(txtYear.Text));
                    break;
                case 3:
                    newSong = new Song(txtTitle.Text, txtSinger.Text, "Trance", int.Parse(txtYear.Text));
                    break;
                case 4:
                    newSong = new Song(txtTitle.Text, txtSinger.Text, "Rap", int.Parse(txtYear.Text));
                    break;
                case 5:
                    newSong = new Song(txtTitle.Text, txtSinger.Text, "Pop", int.Parse(txtYear.Text));
                    break;
                default:
                    MessageBox.Show("Invalid Genre Selected!!", "ERROR");
                    return;
            }


            EnterSong.Add(newSong);
            ClearForm();
            songList.Clear();

            foreach (Song s in EnterSong)
            {
                songList.Add(s);
            }

            if (SaveToFile())
            {
                lblSuccessMsg.Content = "Song has become imortal";
            }
        }

        private void ClearForm()
        {
            txtTitle.Text = string.Empty;
            txtSinger.Text = string.Empty;
            cmbGenre.SelectedIndex = 0;
            txtYear.Text = "";

        }

        public bool SaveToFile()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SongStoreList));
            TextWriter _writer = new StreamWriter("songs.xml");
            serializer.Serialize(_writer, songList);
            _writer.Close();
            return true;
        }

        private void ReadFromFileSongsAndAddToCurrentList()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SongStoreList));
            TextReader _reader = new StreamReader("songs.xml");
            savedSongList = (SongStoreList)serializer.Deserialize(_reader);
            _reader.Close();
            for (int i = 0; i < savedSongList.Count(); i++)
            {
                EnterSong.Add(savedSongList.SongList[i]);
            }
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HomePage());
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SearchSongPage());
        }
    }
}
