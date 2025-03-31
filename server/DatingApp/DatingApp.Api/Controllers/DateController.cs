using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DateController : ControllerBase
    {

        private readonly ILogger<DateController> _logger;

        public DateController(ILogger<DateController> logger)
        {
            _logger = logger;
        }

		[Authorize(Policy = "user")]
		[HttpGet]
        public int GetDateUsers()
        {
            return 0;
        }
    }
}
