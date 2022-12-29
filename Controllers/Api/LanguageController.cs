using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AdminBlog.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : Controller
    {
        private readonly IStringLocalizer<LanguageController> _localizer;

        public LanguageController(IStringLocalizer<LanguageController> localizer)
        {
            _localizer = localizer;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_localizer["Welcome"]);
        }
    }
}
