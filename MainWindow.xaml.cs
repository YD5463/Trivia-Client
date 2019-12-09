using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Sockets;
using System.Web.Script.Serialization;
using System.Net;
using System.ComponentModel;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    public partial class MainWindow : Window
    {
        Helper helper = new Helper();
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                helper.ConnectSocket();
            }
            catch
            {
                MessageBox.Show("server not work!");
                this.Close();
            }
            this.Closing += new CancelEventHandler(Helper.Window_Closing);
        }
        private void LoginInClick(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordBox.Password;
            if (helper.validate_input(username) && helper.validate_input(password))
            {
                LoginRequest login_request = new LoginRequest(username, password);
                var json = new JavaScriptSerializer().Serialize(login_request);
                string msg = Constants.LOGIN_ID.ToString() + json.Length.ToString() + json;
                string response = helper.SendAndRecive(msg);
                LoginResponse loginResponse = new JavaScriptSerializer().Deserialize<LoginResponse>(response);
                if (loginResponse.status == Constants.SUCCESS_STATUS)//success status
                {
                    Helper.set_username(username);
                    MenuWindow menuWindow = new MenuWindow();
                    menuWindow.Show();
                    this.Close();
                    //MessageBox.Show("Success!");
                }
                else
                {
                    MessageBox.Show("Try again!");
                }
            }
            else
            {
                MessageBox.Show("input contain only latters and digits");
            }
        }
        //sign up
        private void SignUpClick(object sender, RoutedEventArgs e)
        {
            SignUpWindow signUpWindow = new SignUpWindow();
            signUpWindow.Show();
            this.Close();
        }

        private void ForgotPasswordClick(object sender, RoutedEventArgs e)
        {
            Forgot_Password_Window forgot_Password_Window = new Forgot_Password_Window();
            forgot_Password_Window.Show();
            this.Close();
        }
    }
}
