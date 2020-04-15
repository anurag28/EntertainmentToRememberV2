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
using System.Net;
using System.Net.Mail;

namespace EntertainmentToRememberV2.Pages
{
    /// <summary>
    /// Interaction logic for RecommendViaEmailPage.xaml
    /// </summary>
    public partial class RecommendViaEmailPage : Page
    {
        
        string emailBody = null;
        public RecommendViaEmailPage(string emailBody)
        {
            InitializeComponent();
            this.emailBody = emailBody;
            txtBody.Text = emailBody;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            
            string username = txtFrom.Text;
            string password = txtPassword.Password;
            string smtp = "smtp.gmail.com";
            string toEmail = txtTo.Text;
            string subject = txtSubject.Text;

            SmtpClient cv = new SmtpClient(smtp, 587);
            cv.EnableSsl = true;
            cv.Credentials = new NetworkCredential(username, password);

            try
            {
                cv.Send(username, toEmail, subject, emailBody);
                ClearForm();
                MessageBox.Show("Email Sent Successfully", "INFO");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Incorrect email id or password.\n" + ex.Message, "ERROR!!");
            }
            
        }

        private void ClearForm()
        {
            txtBody.Text = string.Empty;
            txtFrom.Text = string.Empty;
            txtPassword.Password = string.Empty;
            txtTo.Text = string.Empty;
            emailBody = string.Empty;
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HomePage());
        }
    }
}
