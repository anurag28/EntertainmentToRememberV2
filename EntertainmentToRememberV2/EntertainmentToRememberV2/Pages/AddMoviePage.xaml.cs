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

namespace EntertainmentToRememberV2.Pages
{
    /// <summary>
    /// Interaction logic for AddMoviePage.xaml
    /// </summary>
    public partial class AddMoviePage : Page
    {
        MovieStoreList movieList = new MovieStoreList();
        MovieStoreList savedMovieList = new MovieStoreList();

        private List<Movie> enterMovies = null;

        public List<Movie> EnterMovies
        {
            get => enterMovies;
            set => enterMovies = value;
        }

        public AddMoviePage()
        {
            InitializeComponent();
            enterMovies = new List<Movie>();
            ReadFromFileMoviesAndAddToCurrentList();
            
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            Movie newMovie = new Movie();

            switch (cmbGenre.SelectedIndex)
            {
                case 0:
                    newMovie = new ActionMovie(txtName.Text, txtCast.Text, "Action", this.cmbRating.SelectionBoxItem.ToString());
                    break;
                case 1:
                    newMovie = new DramaMovie(txtName.Text, txtCast.Text, "Drama", this.cmbRating.SelectionBoxItem.ToString());
                    break;
                case 2:
                    newMovie = new ThrillerMovie(txtName.Text, txtCast.Text, "Thriller", this.cmbRating.SelectionBoxItem.ToString());
                    break;
                case 3:
                    newMovie = new RomanticMovie(txtName.Text, txtCast.Text, "Romantic", this.cmbRating.SelectionBoxItem.ToString());
                    break;
                case 4:
                    newMovie = new ComedyMovie(txtName.Text, txtCast.Text, "Comedy", this.cmbRating.SelectionBoxItem.ToString());
                    break;
                default:
                    MessageBox.Show("Invalid Genre Selected!!", "ERROR");
                    return;
            }


            EnterMovies.Add(newMovie);
            ClearForm();
            movieList.Clear();

            foreach(Movie m in EnterMovies)
            {
                movieList.Add(m);
            }

            if (SaveToFile())
            {
                lblSuccessMsg.Content="Movie has become imortal";
            }
        }

        private void ClearForm()
        {
            txtName.Text = string.Empty;
            txtCast.Text = string.Empty;
            cmbGenre.SelectedIndex = -1;
            cmbRating.SelectedIndex = -1;

        }

        private bool SaveToFile()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MovieStoreList));
            TextWriter _writer = new StreamWriter("movies.xml");
            serializer.Serialize(_writer, movieList);
            _writer.Close();
            return true;
        }

        private void ReadFromFileMoviesAndAddToCurrentList()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MovieStoreList));
            TextReader _reader = new StreamReader("movies.xml");
            savedMovieList = (MovieStoreList)serializer.Deserialize(_reader);
            _reader.Close();
            for (int i = 0; i < savedMovieList.Count(); i++)
            {
                EnterMovies.Add(savedMovieList.MovieList[i]);
            }
        }

        private void txtName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == "")
            {
                lblSuccessMsg.Content = string.Empty;
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SearchMoviePage());
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HomePage());
        }
    }
}
