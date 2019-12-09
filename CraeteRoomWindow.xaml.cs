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
    /// Interaction logic for CraeteRoomWindow.xaml
    /// </summary>
    public partial class CraeteRoomWindow : Window
    {
        Helper helper = new Helper();
        public CraeteRoomWindow()
        {
            InitializeComponent();
        }
        //need to chenge to server to make this work
        private void BackClick(object sender, RoutedEventArgs e)
        {
            MenuWindow menu = new MenuWindow();
            menu.Show();
            this.Close();
        }

        private void CreateClick(object sender, RoutedEventArgs e)
        {
            string room_name = room_name_box.Text;
            int max_players;
            int question_count;
            int answer_timeout;
            if(int.TryParse(number_of_players_box.Text, out max_players))
            {
                if(int.TryParse(number_of_players_box.Text, out question_count))
                {
                    if(int.TryParse(number_of_players_box.Text, out answer_timeout))
                    {
                        CreateRoomRequest createRoomRequest = new CreateRoomRequest(room_name,max_players, question_count, answer_timeout);
                        var json = new JavaScriptSerializer().Serialize(createRoomRequest);
                        string request = Constants.CREATE_ROOM_ID.ToString() + json.Length.ToString() + json;
                        string response = helper.SendAndRecive(request);
                        CreateRoomResponse createRoomResponse = new JavaScriptSerializer().Deserialize<CreateRoomResponse>(response);

                        BeforeGameWindow beforeGameWindow = new BeforeGameWindow(room_name,true);
                        beforeGameWindow.Show();
                        this.Close();
                    }
                }
            }
            else
            {
                

            }
        }

    }
}
