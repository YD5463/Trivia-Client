using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    class GetRoomStateResponse
    {
        public uint status;
        public bool has_game_begun;
        public List<string> players;
        public uint question_count;
        public uint answer_timeout;
    }
}
