using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiGirlzSessionManager
{
    public class AppSettings
    {
        public string RedisConnection { get; set; }
        public string RoomURLBase { get; set; }
        public string RoomNameBase { get; set; }
        public int NumberOfRooms { get; set; }
        public string Auditorium { get; set; }
        public bool AuditoriumOn { get; set; }

    }
}
