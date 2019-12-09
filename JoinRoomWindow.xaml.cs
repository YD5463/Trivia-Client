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
using Newtonsoft.Json;

namespace Client
{
    /// <summary>
    /// Interaction logic for JoinRoomWindow.xaml
    /// </summary>
    public partial class JoinRoomWindow : Window
    {
        Helper helper = new Helper();
        List<RoomData> room_detailes = new List<RoomData>();
        public JoinRoomWindow()
        {
            InitializeComponent();
            get_rooms();
        }

        private void Room_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string name = (Room_list.SelectedItem as ListBoxItem).Content.ToString();
            foreach(RoomData room_data in room_detailes)
            {
                if(room_data.name==name)
                {
                    this.Width = 800;
                    TextBlock textBlock = new TextBlock();
                    textBlock.FontSize = 23;
                    textBlock.Foreground = Brushes.Blue;
                    textBlock.Margin = new Thickness(570,100, 0, 0);
                    textBlock.Inlines.Add(new Bold(new Run("Room Detailes")));
                    JoinRoomCanvas.Children.Add(textBlock);
                    add_textBlock(new Thickness(570, 160, 0, 0), "Max Players - " + room_data.max_players.ToString());
                    add_textBlock(new Thickness(570, 220, 0, 0), "Number Of Question - " + room_data.question_count.ToString());
                    add_textBlock(new Thickness(570, 280, 0, 0), "Time Per Question - " + room_data.time_per_question.ToString());
                    add_textBlock(new Thickness(570, 340, 0, 0), "Is Active - " + room_data.is_active.ToString());
                    get_players(room_data);
                }
            }
        }
        private void get_rooms()
        {
            room_detailes.Clear();
            string request = Constants.GET_ROOMS_ID.ToString() + "0";
            string response = helper.SendAndRecive(request);
            MessageBox.Show(response);
            //GetRoomResponse roomResponse= JsonConvert.DeserializeObject<GetRoomResponse>(response);
            GetRoomResponse getRoomResponse = new JavaScriptSerializer().Deserialize<GetRoomResponse>(response);
            ListBoxItem listBoxItem = new ListBoxItem();
            foreach (string room in getRoomResponse.rooms)
            {
                RoomData tempRoomData = new JavaScriptSerializer().Deserialize<RoomData>(room);
                listBoxItem.Content = tempRoomData.name;
                Room_list.Items.Add(listBoxItem);
                room_detailes.Add(tempRoomData);
            }
        }
        private void get_players(RoomData room_data)
        {
            GetPlayersInRoomRequest getPlayersInRoomRequest = new GetPlayersInRoomRequest(room_data.get_id());
            var json = new JavaScriptSerializer().Serialize(getPlayersInRoomRequest);
            string msg = Constants.GET_PLAYERS_IN_ROOM_ID.ToString() + json.Length.ToString() + json;
            string response = helper.SendAndRecive(msg);
            GetPlayersInRoomResponse getPlayersInRoomResponse = new JavaScriptSerializer().Deserialize<GetPlayersInRoomResponse>(response);

            ListBox listBox = new ListBox();
            listBox.FontSize = 23;
            listBox.Margin = new Thickness(570, 370, 0, 0);
            listBox.Width = 150;
            listBox.Height = 100;

            foreach (string player in getPlayersInRoomResponse.players)
            {
                ListBoxItem listBoxItem = new ListBoxItem();
                listBoxItem.Content = player;
                listBox.Items.Add(listBoxItem);
            }
        }
        private void add_textBlock(Thickness thickness,string text)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.FontSize = 18;
            textBlock.Foreground = Brushes.Red;
            textBlock.Margin = thickness;
            textBlock.Text = text;
            JoinRoomCanvas.Children.Add(textBlock);
        }

        private void JoinClick(object sender, RoutedEventArgs e)
        {
            string name = (Room_list.SelectedItem as ListBoxItem).Content.ToString();
            foreach (RoomData room_data in room_detailes)
            {
                if (room_data.name == name)
                {
                    JoinRoomRequest joinRoomRequest = new JoinRoomRequest(room_data.get_id());
                    var json = new JavaScriptSerializer().Serialize(joinRoomRequest);
                    string request = Constants.JOIN_ROOM_ID.ToString() + json.Length.ToString()+json;
                    string response = helper.SendAndRecive(request);
                    JoinRoomResponse joinRoomResponse = new JavaScriptSerializer().Deserialize<JoinRoomResponse>(response);
                    if (joinRoomResponse.status == Constants.SUCCESS_STATUS)
                    {
                        BeforeGameWindow beforeGameWindow = new BeforeGameWindow(name, false);
                        beforeGameWindow.Show();
                        this.Close();
                    }
                }
            }
        }

        private void RefreshClick(object sender, RoutedEventArgs e)
        {
            get_rooms();
        }
    }
}
