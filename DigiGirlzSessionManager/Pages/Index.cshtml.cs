using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiGirlzSessionManager.Pages
{
    public class IndexModel : PageModel
    {
        private readonly RedisService _redis;
        private readonly AppSettings _appSettings;
        public string RedirectURL { get; set; }
        public string RoomName { get; set; }

        public IndexModel(RedisService redis,
            AppSettings appSettings)
        {
            _redis = redis;
            _appSettings = appSettings;
        }

        public async Task OnGet()
        {
            if (_appSettings.AuditoriumOn)
            {
                RedirectURL = _appSettings.Auditorium;
                RoomName = "the DigiGirlz Auditorium";
            }
            else
            {
                var room  = await _redis.GetNextRoom();
                RedirectURL = room.RoomUrl;
                RoomName = room.RoomName;
            }
        }
    }
}
