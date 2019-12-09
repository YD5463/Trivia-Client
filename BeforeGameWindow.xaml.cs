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

namespace Client
{
    /// <summary>
    /// Interaction logic for RoomAdmin.xaml
    /// </summary>
    public partial class BeforeGameWindow : Window
    {
        Helper helper = new Helper();
        private uint question_count;
        private uint answer_timeout;
        public BeforeGameWindow(string room_name,bool is_admin)
        {
            InitializeComponent();
            TextBlock title = new TextBlock();
            title.FontSize = 30;
            title.Margin = new Thickness(101,30,0,0);
            title.Inlines.Add(new Underline(new Bold(new Run("Conected to " + room_name))));
            BeforeGameWindowCanvas.Children.Add(title);
            string request = Constants.GET_ROOM_STATE_ID.ToString() + "0";
            string response = helper.SendAndRecive(request);
            GetRoomStateResponse getRoomStateResponse = new JavaScriptSerializer().Deserialize<GetRoomStateResponse>(response);
            TextBlock textBlock = new TextBlock();
            textBlock.FontSize = 23;
            textBlock.Foreground = Brushes.Blue;
            textBlock.Margin = new Thickness(534, 175, 0, 0);
            textBlock.Inlines.Add(new Bold(new Run("Room Detailes")));
            BeforeGameWindowCanvas.Children.Add(textBlock);
            this.question_count = getRoomStateResponse.question_count;
            this.answer_timeout = getRoomStateResponse.answer_timeout;
            add_textBlock(new Thickness(534, 225, 0, 0), "Number Of Question - " + getRoomStateResponse.question_count.ToString());
            add_textBlock(new Thickness(534, 275, 0, 0), "Time Per Question - " + getRoomStateResponse.answer_timeout.ToString());
            foreach (string player in getRoomStateResponse.players)
            {
                ListBoxItem listBoxItem = new ListBoxItem();
                listBoxItem.Content = player;
                playersList.Items.Add(listBoxItem);
            }
            set_buttons(is_admin);
        }
        private void set_buttons(bool is_admin)
        {
            if (is_admin)
            {
                Button CloseRoomButton = new Button();
                CloseRoomButton.Click += CloseRoom;
                CloseRoomButton.FontSize = 30;
                CloseRoomButton.Margin = new Thickness(95, 363, 0, 0);
                CloseRoomButton.Width = 185;
                CloseRoomButton.Height = 60;
                CloseRoomButton.Content = "Close Room";
                BeforeGameWindowCanvas.Children.Add(CloseRoomButton);

                Button StartGameButton = new Button();
                StartGameButton.Click += StartGame;
                StartGameButton.FontSize = 30;
                StartGameButton.Margin = new Thickness(320, 363, 0, 0);
                StartGameButton.Width = 185;
                StartGameButton.Height = 60;
                StartGameButton.Content = "Start Game";
                BeforeGameWindowCanvas.Children.Add(StartGameButton);

            }
            else
            {
                Button LeaveRoomButton = new Button();
                LeaveRoomButton.Click += LeaveRoomButton_Click;
                LeaveRoomButton.FontSize = 30;
                LeaveRoomButton.Margin = new Thickness(200, 363, 0, 0);
                LeaveRoomButton.Width = 185;
                LeaveRoomButton.Height = 60;
                LeaveRoomButton.Content = "Leave Room";
                BeforeGameWindowCanvas.Children.Add(LeaveRoomButton);
            }
        }
        private void LeaveRoomButton_Click(object sender, RoutedEventArgs e)
        {
            string request = Constants.LEAVE_ROOM_ID.ToString();
            string response =  helper.SendAndRecive(request);

        }

        private void add_textBlock(Thickness thickness, string text)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.FontSize = 18;
            textBlock.Foreground = Brushes.Red;
            textBlock.Margin = thickness;
            textBlock.Text = text;
            BeforeGameWindowCanvas.Children.Add(textBlock);
        }
        private void CloseRoom(object sender, RoutedEventArgs e)
        {
            string request = Constants.CLOSE_ROOM_ID.ToString() + "0";
            string response = helper.SendAndRecive(request);
            CloseRoomResponse signupResponse = new JavaScriptSerializer().Deserialize<CloseRoomResponse>(response);

            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
            this.Close();
        }

        private void StartGame(object sender, RoutedEventArgs e)
        {

            string request = Constants.START_GAME_ID.ToString() + "0";
            string response = helper.SendAndRecive(request);
            StartGameResponse signupResponse = new JavaScriptSerializer().Deserialize<StartGameResponse>(response);

            GameWindow gameWindow = new GameWindow(question_count, answer_timeout);
            gameWindow.Show();
            this.Close();
        }
    }
}
