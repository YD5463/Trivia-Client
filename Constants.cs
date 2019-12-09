using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    class Constants
    {
        public const int LOGIN_ID = 101;
        public const int SIGNUP_ID = 102;
        public const int SIGNOUT_ID = 103;
        public const int GET_ROOMS_ID = 104;
        public const int GET_PLAYERS_IN_ROOM_ID = 105;
        public const int GET_HIGHSCORES_ID = 106;
        public const int JOIN_ROOM_ID = 107;
        public const int CREATE_ROOM_ID = 108;
        public const int CLOSE_ROOM_ID = 109;
        public const int START_GAME_ID = 110;
        public const int GET_ROOM_STATE_ID = 111;
        public const int LEAVE_ROOM_ID = 112;
        public const int GET_QUESTION_ID = 113;
        public const int SUBMIT_ANSWER_ID = 114;
        public const int GET_GAME_RESULTS_ID = 115;
        public const int LEAVE_GAME_ID = 116;
        public const int PORT = 8026;
        public const int SUCCESS_STATUS = 1;
        public const int FAILED_STATUS = 2;
        public const int RIGHT_ANSWER_ID = 4;
        public const int WRONG_ANSWER_ID = 5;
        public const int EXIT = -1;
    }
}
