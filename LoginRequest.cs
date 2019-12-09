using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class LoginRequest
    {
        public LoginRequest(string name, string pass)
        {
            _username = name;
            _password = pass;
        }
        public string _username;
        public string _password;
    }
}
