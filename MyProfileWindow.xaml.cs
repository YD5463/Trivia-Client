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
    /// Interaction logic for MyProfileWindow.xaml
    /// </summary>
    public partial class MyProfileWindow : Window
    {
        Helper helper = new Helper();
        public MyProfileWindow()
        {
            InitializeComponent();
            string request = Constants.GET_HIGHSCORES_ID.ToString() + "0";
            string response = helper.SendAndRecive(request);
            MessageBox.Show(response);
            HighscoreResponse highscoreResponse = new JavaScriptSerializer().Deserialize<HighscoreResponse>(response);
            add_details(new Thickness(360, 120, 0, 0), highscoreResponse.num_games.ToString());
            add_details(new Thickness(360, 200, 0, 0), highscoreResponse.right_answers.ToString());
            add_details(new Thickness(360, 280, 0, 0), highscoreResponse.worng_answers.ToString());
            add_details(new Thickness(360, 360, 0, 0), highscoreResponse.averag_time_for_answer.ToString());
        }
        private void add_details(Thickness thickness,string detailes)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.FontSize = 26;
            textBlock.Margin = thickness;
            textBlock.Width = 60;
            textBlock.Inlines.Add(new Bold(new Run(detailes)));
            MyPrefomanceCanvas.Children.Add(textBlock);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
            this.Close();
        }
    }
}
