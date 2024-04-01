using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SystemWebAdapters;

namespace CoreWebApp.Pages
{
    [Session]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(ILogger<IndexModel> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnGet()
        {
            _httpContextAccessor.HttpContext.Session.LoadAsync();
            var isAuthenticated = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
