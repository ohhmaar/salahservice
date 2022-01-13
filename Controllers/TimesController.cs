using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalahService.Model;
using SalahService.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace SalahService.Controllers
{

    [ApiController]
    [Route("api/prayertimes")]

    public partial class TimesController : ControllerBase
    {
        private readonly IAlAdhanService _alAdhanService;

        private readonly ILogger _log;

        public TimesController(ILogger<TimesController> logger, IAlAdhanService alAdhanService)
        {
            _log = logger;
            _alAdhanService = alAdhanService;
        }

        [HttpGet]
        [Route("today")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Timings), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPrayerTimesByLongitudeandLatitude(string latitude, string longitude)
        {
            try
            {
                _log.LogTrace("Getting prayer times for supplied latitude and longitude");
                var times = _alAdhanService.GetTimings(latitude, longitude);

                return new OkObjectResult(times);
            }
            catch (Exception e)
            {
                _log.LogError("An unhandled exception occured on /today.", e);
                throw;
            }
        }
    }
}
