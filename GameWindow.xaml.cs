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
using System.Windows.Threading;
using System.Timers;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        
        Helper helper = new Helper();

        public GameWindow(uint question_count, uint answer_timeout)
        {
            InitializeComponent();
            set_question();
        }
        private void set_question()
        {
            string request = Constants.GET_QUESTION_ID.ToString() + "0";
            string response = helper.SendAndRecive(request);
            GetQuestionResponse getQuestionResponse = new JavaScriptSerializer().Deserialize<GetQuestionResponse>(response);

            if (getQuestionResponse.status == Constants.SUCCESS_STATUS)
            {
                questionTextBlock.Text = getQuestionResponse.question;//set question
                try
                {
                    firstAnswer.Content = getQuestionResponse.answers[0];//set first optional answer
                    secondAnswer.Content = getQuestionResponse.answers[1];//set second optional answer
                    thirdAnswer.Content = getQuestionResponse.answers[2];//set third optional answer
                    fourthAnswer.Content = getQuestionResponse.answers[3];//set fourth optional answer
                }
                catch { }
                firstAnswer.Foreground = Brushes.Black;
                secondAnswer.Foreground = Brushes.Black;
                thirdAnswer.Foreground = Brushes.Black;
                fourthAnswer.Foreground = Brushes.Black;

                firstAnswer.FontWeight = FontWeights.Bold;//making the text bold
                secondAnswer.FontWeight = FontWeights.Bold;//making the text bold
                thirdAnswer.FontWeight = FontWeights.Bold;//making the text bold
                fourthAnswer.FontWeight = FontWeights.Bold;//making the text bold
            }
            
        }
        private void show_game_result()
        {
            string request = Constants.GET_GAME_RESULTS_ID.ToString() + "0";
            string response = helper.SendAndRecive(request);
            GetGameResultsResponse getGameResultsResponse = new JavaScriptSerializer().Deserialize<GetGameResultsResponse>(response);
            string str_game_results = "";
            foreach (string Key in getGameResultsResponse.results.Keys)
            {
                str_game_results += "Scores of Username:" + Key + "\n";
                foreach (uint result in getGameResultsResponse.results[Key])
                {

                }
            }
            MessageBox.Show(str_game_results);
        }
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            string request = Constants.LEAVE_GAME_ID.ToString() + "0";
            string response = helper.SendAndRecive(request);
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
            this.Close();

        }
        /*
         when user submit one of 4 answer the function called
        */
        private void SubmitAnswer(object sender, RoutedEventArgs e)
        {
            var answer = sender as Button;
            SubmitAnswerRequest submitAnswerRequest = new SubmitAnswerRequest(helper.get_answer_id(answer.Name));
            var json = new JavaScriptSerializer().Serialize(submitAnswerRequest);
            string request = Constants.SUBMIT_ANSWER_ID.ToString() + json.Length.ToString() + json;
            string response = helper.SendAndRecive(request);
            SubmitAnswerResponse submitAnswerResponse = new JavaScriptSerializer().Deserialize<SubmitAnswerResponse>(response);
            if (submitAnswerResponse.is_right)
            {
                answer.Foreground = Brushes.Green;
            }
            else
            {
                answer.Foreground = Brushes.Red;
            }
            set_question();
        }
    }
}
