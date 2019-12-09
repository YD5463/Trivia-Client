using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    class SubmitAnswerRequest
    {
        public SubmitAnswerRequest(uint answer_id)
        {
            this.answer_id = answer_id;
        }
        public uint answer_id;
    }
}
