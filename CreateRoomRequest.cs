using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    class CreateRoomRequest
    {
        public CreateRoomRequest(string room_name, int max_users, int question_count, int answer_timeout)
        {
            this.room_name = room_name;
            this.max_users = max_users;
            this.question_count = question_count;
            this.answer_timeout = answer_timeout;
        }
        public string room_name;
        public int max_users;
        public int question_count;
        public int answer_timeout;
    }
}
