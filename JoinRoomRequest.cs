using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    class JoinRoomRequest
    {
        public JoinRoomRequest(uint room_id)
        {
            this.room_id = room_id;
        }
        public uint room_id;
    }
}
