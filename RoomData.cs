using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    class RoomData
    {
        public uint get_id()
        {
            return id;
        }
        public string name;
        public static uint id;
        public uint max_players;
        public uint time_per_question;
        public uint question_count;
        public bool is_active;
    }
}
