using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiGirlzSessionManager
{
    public class Room
    {
        private readonly AppSettings _appSettings;

        public Room(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public int RoomId { get; set; }
        public string RoomName 
        {
            get
            {
                return string.Format(_appSettings.RoomNameBase, RoomId);
            }
        }
        public string RoomUrl
        {
            get
            {
                return string.Format(_appSettings.RoomURLBase, RoomId);
            }
        }
        public double Count { get; set; }
    }
}
