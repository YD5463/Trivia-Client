using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    class GetPlayersInRoomRequest
    {
        public GetPlayersInRoomRequest(uint room_id)
        {
            this.room_id = room_id;
        }
        public uint room_id;
    }
}
