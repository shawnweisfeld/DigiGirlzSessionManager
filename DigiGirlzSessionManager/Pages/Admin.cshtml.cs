using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiGirlzSessionManager.Pages
{
    public class AdminModel : PageModel
    {
        private readonly RedisService _redis;
        public List<Room> Rooms { get; set; }

        public AdminModel(RedisService redis)
        {
            _redis = redis;
        }

        public async Task OnGet()
        {
            Rooms = (await _redis.GetAllRooms()).OrderBy(x => x.RoomName).ToList();
        }
    }
}
