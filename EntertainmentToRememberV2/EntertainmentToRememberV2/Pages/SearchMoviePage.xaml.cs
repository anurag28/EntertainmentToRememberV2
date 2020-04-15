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
using System.Linq;
using System.Xml.Serialization;
using System.IO;

namespace EntertainmentToRememberV2.Pages
{
    /// <summary>
    /// Interaction logic for SearchMoviePage.xaml
    /// </summary>
    public partial class SearchMoviePage : Page
    {

        MovieStoreList movieList = new MovieStoreList();

        List<Movie> movieToRecommend = new List<Movie>();

        private ObservableCollection<Movie> displayMovies = null;

        public ObservableCollection<Movie> DisplayMovies
        {
            get => displayMovies;
            set => displayMovies = value;
        }

        public SearchMoviePage()
        {
            InitializeComponent();

            displayMovies = new ObservableCollection<Movie>();

            DataContext = this;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            movieList.Clear();
            ReadFromFile();
            DisplayMovies.Clear();

            if(txtName.Text!="" && txtCast.Text =="" && cmbGenre.SelectedIndex == -1)
            {
                SearchByName(txtName.Text);
            }

            else if(txtName.Text=="" && txtCast.Text!="" && cmbGenre.SelectedIndex == -1)
            {
                SearchByCast(txtCast.Text);
            }

            else if(txtName.Text=="" && txtCast.Text=="" && cmbGenre.SelectedIndex != -1)
            {
                SearchByGenre(this.cmbGenre.SelectionBoxItem.ToString());
            }

            else if(txtName.Text!="" && txtCast.Text!="" && cmbGenre.SelectedIndex == -1)
            {
                SearchByNameAndCast(txtName.Text, txtCast.Text);
            }
            
            else if(txtName.Text != "" && txtCast.Text == "" && cmbGenre.SelectedIndex != -1)
            {
                SearchByNameAndGenre(txtName.Text, this.cmbGenre.SelectionBoxItem.ToString());
            }

            else if (txtName.Text == "" && txtCast.Text != "" && cmbGenre.SelectedIndex != -1)
            {
                SearchByCastAndGenre(txtCast.Text, this.cmbGenre.SelectionBoxItem.ToString());
            }

            else if(txtName.Text!="" && txtCast.Text!="" && cmbGenre.SelectedIndex != -1)
            {
                SearchByNameCastANdGenre(txtName.Text, txtCast.Text, this.cmbGenre.SelectionBoxItem.ToString());
            }

            if (DisplayMovies.Count == 0)
            {
                MessageBox.Show("No Results Found..");
                ClearForm();
            }
            else
            {
                grdDisplayMovies.ItemsSource = DisplayMovies;
                ClearForm();
            }
            
        }

        private void ClearForm()
        {
            txtName.Text = string.Empty;
            txtCast.Text = string.Empty;
            cmbGenre.SelectedIndex = -1;
        }

        private void SearchByName(string name)
        {
            var query = from movie in movieList.MovieList
                        where movie.Name.Contains(name)
                        select movie;
            foreach(Movie movie in query)
            {
                DisplayMovies.Add(movie);
            }
        }

        private void SearchByCast(string cast)
        {
            var query = from movie in movieList.MovieList
                        where movie.Cast.Contains(cast)
                        select movie;
            foreach (Movie movie in query)
            {
                DisplayMovies.Add(movie);
            }
        }


        private void SearchByGenre(string genre)
        {
            var query = from movie in movieList.MovieList
                        where movie.Genre.Contains(genre)
                        select movie;
            foreach (Movie movie in query)
            {
                DisplayMovies.Add(movie);
            }
        }

        private void SearchByNameAndCast(string name, string cast)
        {
            var query = from movie in movieList.MovieList
                        where movie.Name.Contains(name)
                        && movie.Cast.Contains(cast)
                        select movie;
            foreach (Movie movie in query)
            {
                DisplayMovies.Add(movie);
            }
        }

        private void SearchByNameAndGenre(string name, string genre)
        {
            var query = from movie in movieList.MovieList
                        where movie.Name.Contains(name)
                        && movie.Genre.Contains(genre)
                        select movie;
            foreach (Movie movie in query)
            {
                DisplayMovies.Add(movie);
            }
        }

        private void SearchByCastAndGenre(string cast, string genre)
        {
            var query = from movie in movieList.MovieList
                        where movie.Cast.Contains(cast)
                        && movie.Genre.Contains(genre)
                        select movie;
            foreach (Movie movie in query)
            {
                DisplayMovies.Add(movie);
            }
        }

        private void SearchByNameCastANdGenre(string name, string cast, string genre)
        {
            var query = from movie in movieList.MovieList
                        where movie.Cast.Contains(cast) &&
                         movie.Genre.Contains(genre)
                        && movie.Name.Contains(name)
                        select movie;
            foreach (Movie movie in query)
            {
                DisplayMovies.Add(movie);
            }
        }

        private void ReadFromFile()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MovieStoreList));
            TextReader _reader = new StreamReader("movies.xml");
            movieList = (MovieStoreList)serializer.Deserialize(_reader);
            _reader.Close();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HomePage());
        }

        private void btnRecommend_Click(object sender, RoutedEventArgs e)
        {

            foreach (Movie movie in grdDisplayMovies.ItemsSource)
            {
                if (((CheckBox)chkRecommend.GetCellContent(movie)).IsChecked == true)
                {
                    movieToRecommend.Add(movie);
                }
            }

            if (movieToRecommend.Count == 0)
            {
                MessageBox.Show("No Movies Are Selected To Recommend", "ERROR!!");
            }

            else
            {

                this.NavigationService.Navigate(new RecommendViaEmailPage(CreateMessageBody()));
            }
        }

        private string CreateMessageBody()
        {
            string emailBody = "Hey friend,\n\n\nDon't forget to watch these amazing movies:\n";
            foreach (Movie movie in movieToRecommend)
            {
                emailBody = emailBody + "\n Name : " + movie.Name + "      Genre : "
                    + movie.Genre + "        Cast: " + movie.Cast + "     Rating: " + movie.Rating+"\n";
            }
            return emailBody + "\n\nCheers!";
        }
    }
}
