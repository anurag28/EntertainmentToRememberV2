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
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using EntertainmentToRememberV2.Models;
using System.Collections.ObjectModel;

namespace EntertainmentToRememberV2.Pages
{
    /// <summary>
    /// Interaction logic for SearchShowPage.xaml
    /// </summary>
    public partial class SearchShowPage : Page
    {
        List<Show> showToRecommend = new List<Show>();

        ShowStoreList showList = new ShowStoreList();

        private ObservableCollection<Show> displayShows = null;

        public ObservableCollection<Show> DisplayShows 
        { 
            get=>displayShows; 
            set=>displayShows=value; 
        }

        public SearchShowPage()
        {
            InitializeComponent();
            displayShows= new ObservableCollection<Show>(); ;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            showList.Clear();
            ReadFromFile();
            DisplayShows.Clear();
            if (txtName.Text != "" && txtCast.Text == "" && cmbGenre.SelectedIndex == -1)
            {
                SearchByName(txtName.Text);
            }

            else if (txtName.Text == "" && txtCast.Text != "" && cmbGenre.SelectedIndex == -1)
            {
                SearchByCast(txtCast.Text);
            }

            else if (txtName.Text == "" && txtCast.Text == "" && cmbGenre.SelectedIndex != -1)
            {
                SearchByGenre(this.cmbGenre.SelectionBoxItem.ToString());
            }

            else if (txtName.Text != "" && txtCast.Text != "" && cmbGenre.SelectedIndex == -1)
            {
                SearchByNameAndCast(txtName.Text, txtCast.Text);
            }

            else if (txtName.Text != "" && txtCast.Text == "" && cmbGenre.SelectedIndex != -1)
            {
                SearchByNameAndGenre(txtName.Text, this.cmbGenre.SelectionBoxItem.ToString());
            }

            else if (txtName.Text == "" && txtCast.Text != "" && cmbGenre.SelectedIndex != -1)
            {
                SearchByCastAndGenre(txtCast.Text, this.cmbGenre.SelectionBoxItem.ToString());
            }

            if (DisplayShows.Count == 0)
            {
                MessageBox.Show("No Results Found..");
                ClearForm();
            }
            else
            {
                grdDisplayShows.ItemsSource = DisplayShows;
                ClearForm();
            }

        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HomePage());
        }

        private void ReadFromFile()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ShowStoreList));
            TextReader _reader = new StreamReader("shows.xml");
            showList = (ShowStoreList)serializer.Deserialize(_reader);
            _reader.Close();
        }

        private void ClearForm()
        {
            txtName.Text = string.Empty;
            txtCast.Text = string.Empty;
            cmbGenre.SelectedIndex = -1;
        }

        private void SearchByName(string name)
        {
            var query = from show in showList.ShowList
                        where show.Name.Contains(name)
                        select show;
            foreach (Show show in query)
            {
                DisplayShows.Add(show);
            }
        }

        private void SearchByCast(string cast)
        {
            var query = from show in showList.ShowList
                        where show.Cast.Contains(cast)
                        select show;
            foreach (Show show in query)
            {
                DisplayShows.Add(show);
            }
        }


        private void SearchByGenre(string genre)
        {
            var query = from show in showList.ShowList
                        where show.Genre.Contains(genre)
                        select show;
            foreach (Show show in query)
            {
                DisplayShows.Add(show);
            }
        }

        private void SearchByNameAndCast(string name, string cast)
        {
            var query = from show in showList.ShowList
                        where show.Name.Contains(name)
                        && show.Cast.Contains(cast)
                        select show;
            foreach (Show show in query)
            {
                DisplayShows.Add(show);
            }
        }

        private void SearchByNameAndGenre(string name, string genre)
        {
            var query = from show in showList.ShowList
                        where show.Name.Contains(name)
                        && show.Genre.Contains(genre)
                        select show;
            foreach (Show show in query)
            {
                DisplayShows.Add(show);
            }
        }

        private void SearchByCastAndGenre(string cast, string genre)
        {
            var query = from show in showList.ShowList
                        where show.Cast.Contains(cast)
                        && show.Genre.Contains(genre)
                        select show;
            foreach (Show show in query)
            {
                DisplayShows.Add(show);
            }
        }

        private void SearchByNameCastANdGenre(string name, string cast, string genre)
        {
            var query = from show in showList.ShowList
                        where show.Cast.Contains(cast) &&
                         show.Genre.Contains(genre)
                        && show.Name.Contains(name)
                        select show;
            foreach (Show show in query)
            {
                DisplayShows.Add(show);
            }
        }

        private void btnRecommend_Click(object sender, RoutedEventArgs e)
        {

            foreach (Show show in grdDisplayShows.ItemsSource)
            {
                if (((CheckBox)chkRecommend.GetCellContent(show)).IsChecked == true)
                {
                    showToRecommend.Add(show);
                }
            }

            if (showToRecommend.Count == 0)
            {
                MessageBox.Show("No Shows Are Selected To Recommend", "ERROR!!");
            }

            else
            {

                this.NavigationService.Navigate(new RecommendViaEmailPage(CreateMessageBody()));
            }
        }

        private string CreateMessageBody()
        {
            string emailBody = "Hey friend,\n\n\nDon't forget to watch these amazing shows:\n";
            foreach (Show show in showToRecommend)
            {
                emailBody = emailBody + "\n Name : " + show.Name + "      Genre : "
                    + show.Genre + "        Cast: " + show.Cast + "     Rating: " + show.Rating;
            }
            return emailBody + "\n\n\nCheers!";
        }
    }
}
