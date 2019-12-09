using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    class SignUpRequest
    {
        public string _username;
        public string _password;
        public string _email;
        public SignUpRequest(string username,string password,string email)
        {
            _username = username;
            _password = password;
            _email = email;
        }
    }
}
