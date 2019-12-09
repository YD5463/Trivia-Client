using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
//using System.Web.Script.Serialization;
using System.Net;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Documents;
namespace Client
{
    class Helper
    {
        public static object Uilock = new object();
        public static Socket client_socket = null;
        private const string host_name = "127.0.0.1";
        private static string Username;
        public static string get_username()
        {
            return Username;
        }
        public static void set_username(string name)
        {
            Username = name;
        }
        public bool validate_input(string statement)
        {
            foreach(char c in statement)
	        {
                if (!((c >= '0' && c <= '9') || (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z')))
                {
                    return false;
                }
            }
            return true;
        }
        public void ConnectSocket()
        {
            byte[] bytes = new byte[1024];
            try
            {
                client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse(host_name);
                System.Net.IPEndPoint remoteEP = new IPEndPoint(ipAdd, Constants.PORT);
                client_socket.Connect(remoteEP);
            }
            catch
            {
                throw;
            }
        }
        public uint get_answer_id(string name)
        {
            if (name == "firstAnswer")
            {
                return 0;
            }
            else if (name == "secondAnswer")
            {
                return 1;
            }
            else if (name == "thirdAnswer")
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }
        public string SendAndRecive(string str_msg)
        {
            byte[] bytes = new byte[1024];

            byte[] msg = Encoding.ASCII.GetBytes(str_msg);
            string result = "";
            try
            {
                client_socket.Send(msg);

                int bytesRec = client_socket.Receive(bytes);
                result = Encoding.ASCII.GetString(bytes, 0, bytesRec);
            }
            catch
            {
                throw;
            }
            return result;
        }
        public void CloseSocket()
        {
            byte[] msg = Encoding.ASCII.GetBytes(Constants.EXIT.ToString());
            client_socket.Send(msg);
            client_socket.Close();
        }
        public static void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            /*
            MessageBoxResult result = MessageBox.Show("Are you sure you want to close?", "SandBox", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
            */
        }
    }
}
