using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    class GetQuestionResponse
    {
        public uint status;
        public string question;
        public List<string> answers;
    }
}
