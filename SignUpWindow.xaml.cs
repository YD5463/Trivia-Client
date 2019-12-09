using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Web.Script.Serialization;
using System.ComponentModel;

namespace Client
{
    /// <summary>
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        Helper helper = new Helper();
        public SignUpWindow()
        {
            InitializeComponent();
            this.Closing += new CancelEventHandler(Helper.Window_Closing);
        }
        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;
            string email = emailTextBox.Text;
            if (username != "" && password != "" && email!="")
            {
                SignUpRequest signUpRequest = new SignUpRequest(username, password, email);
                var json = new JavaScriptSerializer().Serialize(signUpRequest);
                string msg = Constants.SIGNUP_ID.ToString() + json.Length.ToString() + json;
                string response = helper.SendAndRecive(msg);
                SignupResponse signupResponse = new JavaScriptSerializer().Deserialize<SignupResponse>(response);
                if (signupResponse.status == Constants.SUCCESS_STATUS)//success status
                {
                    Helper.set_username(username);
                    MenuWindow menuWindow = new MenuWindow();
                    menuWindow.Show();
                    this.Close();
                    //MessageBox.Show("Success!");
                }
                else
                {
                    MessageBox.Show("User name is elready used!");
                }
            }
            else
            {

                TextBlock textBlock = new TextBlock();
                textBlock.FontSize = 16;
                textBlock.Foreground = Brushes.Red;
                textBlock.Margin = new Thickness(175,315,0,0);
                textBlock.Inlines.Add(new Bold(new Run("some detailes are missing!")));
                signupCanvas.Children.Add(textBlock);
                if(username=="")
                    usernameTextBox.BorderBrush = System.Windows.Media.Brushes.Red;

                if(password == "")
                    passwordTextBox.BorderBrush = System.Windows.Media.Brushes.Red;

                if(email == "")
                    emailTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
                
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
