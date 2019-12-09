using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Web.Script.Serialization;

namespace Client
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        Helper helper = new Helper();
        public MenuWindow()
        {
            InitializeComponent();
            this.Closing += window_Closing;
            TextBlock textBlock = new TextBlock();
            textBlock.FontSize = 17;
            textBlock.TextWrapping = TextWrapping.Wrap;
            textBlock.Margin = new Thickness(27,23,0,0);
            string content = "hello " + Helper.get_username();
            textBlock.Inlines.Add(new Bold(new Run(content)));
            MenuCanvas.Children.Add(textBlock);
        }
        void window_Closing(object sender, global::System.ComponentModel.CancelEventArgs e)
        {
            //helper.CloseSocket();
        }

        private void CreateRoomClick(object sender, RoutedEventArgs e)
        {
            CraeteRoomWindow craeteRoomWindow = new CraeteRoomWindow();
            craeteRoomWindow.Show();
            this.Close();
        }

        private void JoinRoomClick(object sender, RoutedEventArgs e)
        {
            JoinRoomWindow joinRoomWindow = new JoinRoomWindow();
            joinRoomWindow.Show();
            this.Close();
        }

        private void BestScoresClick(object sender, RoutedEventArgs e)
        {
            BestScoreWindow bestScoreWindow = new BestScoreWindow();
            bestScoreWindow.Show();
            this.Close();

        }

        private void MyProfileClick(object sender, RoutedEventArgs e)
        {
            MyProfileWindow profileWindow = new MyProfileWindow();
            profileWindow.Show();
            this.Close();

        }

        private void SignOutClick(object sender, RoutedEventArgs e)
        {
            string request = Constants.SIGNOUT_ID.ToString()+"0";
            string response = helper.SendAndRecive(request);
            LogoutResponse loginResponse = new JavaScriptSerializer().Deserialize<LogoutResponse>(response);
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
