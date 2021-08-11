using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiGirlzSessionManager.Pages
{
    public class ResetModel : PageModel
    {
        private readonly RedisService _redis;

        public ResetModel(RedisService redis)
        {
            _redis = redis;
        }

        public async Task<ActionResult> OnGet()
        {
            await _redis.Reset();
            return RedirectToPage("Admin");
        }
    }
}
