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
    /// Interaction logic for AddShowPage.xaml
    /// </summary>
    public partial class AddShowPage : Page
    {

        ShowStoreList showList = new ShowStoreList();
        ShowStoreList savedShowList = new ShowStoreList();

        private List<Show> enterShows = null;

        public List<Show> EnterShows
        {
            get => enterShows;
            set => enterShows = value;
        }
        public AddShowPage()
        {
            InitializeComponent();
            enterShows = new List<Show>();
            ReadFromFileShowsAndAddToCurrentList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Show newShow = new Show();

            switch (cmbGenre.SelectedIndex)
            {
                case 0:
                    newShow = new Show(txtName.Text,txtCast.Text,"Action", this.cmbGenre.SelectionBoxItem.ToString());
                    break;
                case 1:
                    newShow = new Show(txtName.Text, txtCast.Text, "Drama", this.cmbGenre.SelectionBoxItem.ToString());
                    break;
                case 2:
                    newShow = new Show(txtName.Text, txtCast.Text, "Thriller", this.cmbGenre.SelectionBoxItem.ToString());
                    break;
                case 3:
                    newShow = new Show(txtName.Text, txtCast.Text, "Romantic", this.cmbGenre.SelectionBoxItem.ToString());
                    break;
                case 4:
                    newShow = new Show(txtName.Text, txtCast.Text, "Comedy", this.cmbGenre.SelectionBoxItem.ToString());
                    break;
                case 5:
                    newShow = new Show(txtName.Text, txtCast.Text, "Sitcom", this.cmbGenre.SelectionBoxItem.ToString());
                    break;
                case 6:
                    newShow = new Show(txtName.Text, txtCast.Text, "Sci-Fi", this.cmbGenre.SelectionBoxItem.ToString());
                    break;
                default:
                    MessageBox.Show("Invalid Genre Selected!!", "ERROR");
                    return;
            }
            EnterShows.Add(newShow);
            ClearForm();
            showList.Clear();

            foreach (Show s in EnterShows)
            {
                showList.Add(s);
            }

            if (SaveToFile())
            {
                lblSuccessMsg.Content = "Show has become imortal";
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
            XmlSerializer serializer = new XmlSerializer(typeof(ShowStoreList));
            TextWriter _writer = new StreamWriter("shows.xml");
            serializer.Serialize(_writer, showList);
            _writer.Close();
            return true;
        }

        private void ReadFromFileShowsAndAddToCurrentList()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ShowStoreList));
            TextReader _reader = new StreamReader("shows.xml");
            savedShowList = (ShowStoreList)serializer.Deserialize(_reader);
            _reader.Close();
            for (int i = 0; i < savedShowList.Count(); i++)
            {
                EnterShows.Add(savedShowList.ShowList[i]);
            }
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HomePage());
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == "")
            {
                lblSuccessMsg.Content = string.Empty;
            }
        }
    }
}
