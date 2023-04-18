using Microsoft.AspNetCore.Mvc;
using RoadsideAssistant.Data.Entities.ApiModel;
using RoadsideAssistant.Manager;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoadsideAssistantController : ControllerBase
    {
        private readonly ILogger<RoadsideAssistantController> _logger; // Log exception , request or response.
        private readonly IRoadsideAssistantManager _roadsideAssistantManager;

        public RoadsideAssistantController(IRoadsideAssistantManager roadsideAssistantService, ILogger<RoadsideAssistantController> logger)
        {
            _logger = logger;
            _roadsideAssistantManager = roadsideAssistantService;
        }

        [HttpGet("{longitude}/{latitude}/{limit}")]
        public IActionResult Get(string longitude, string latitude, string limit)
        {
            if (!ValidateRequest(longitude, latitude, limit))
                return BadRequest("Invalid Geolocation or limit");

            var serviceAssistantProviders = _roadsideAssistantManager.FindNearestAssistants(GetGeoLocationCoordinates(longitude, latitude), Convert.ToInt32(limit));

            return Ok(serviceAssistantProviders);
        }

        [HttpPost("Update")]
        public IActionResult UpdateAssistantLocation([FromBody] UpdateAssistantRequest updateServiceAssistantRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var serviceAssistant = _roadsideAssistantManager.GetRoadsideServiceAssistant(updateServiceAssistantRequest.BusinessName,
                        updateServiceAssistantRequest.Zip);

                if (serviceAssistant == null)
                    return NotFound("Roadside Service Assistant not found");

                _roadsideAssistantManager.UpdateAssistantLocation(serviceAssistant, updateServiceAssistantRequest.Location);
            }

            catch (Exception e)
            {
               // _logger.Log();
                return e.Message == "Not Found" ? NotFound("Service Assistance not found") : StatusCode(500, "Internal error occurred. " + e.Message);
            }

            return Ok("Location updated successfully");
        }

        [HttpPost("Reserve")]
        public IActionResult ReserveAssistant([FromBody] CustomerRoadSideServiceAssistance reserveCustomerRoadAssistant)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var serviceAssistant = _roadsideAssistantManager.GetRoadsideServiceAssistant(reserveCustomerRoadAssistant.RoadsideServiceAssistantName,
                    reserveCustomerRoadAssistant.Zip);

                if (serviceAssistant == null)
                    return NotFound("Roadside Service Assistant not found");

                var assistant = _roadsideAssistantManager.ReserveAssistant(reserveCustomerRoadAssistant.Customer, serviceAssistant);
                return Ok(assistant);
            }

            catch (Exception e)
            {
                // _logger.Log();
                return e.Message == "Not Found" ? NotFound("Service Assistance not found") : StatusCode(500, "Internal error occurred. " + e.Message);
            }
        }

        [HttpPost("Release")]
        public IActionResult ReleaseAssistant([FromBody] CustomerRoadSideServiceAssistance reserveCustomerRoadAssistant)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var serviceAssistant = _roadsideAssistantManager.GetRoadsideServiceAssistant(reserveCustomerRoadAssistant.RoadsideServiceAssistantName,
                    reserveCustomerRoadAssistant.Zip);

                if (serviceAssistant == null)
                    return NotFound("Roadside Service Assistant not found");

                _roadsideAssistantManager.ReleaseAssistant(reserveCustomerRoadAssistant.Customer, serviceAssistant);
                return Ok("Roadside assistant release successfully");
            }

            catch (Exception e)
            {
                // _logger.Log();
                return e.Message == "Not Found" ? NotFound("Service Assistance not found") : StatusCode(500, "Internal error occurred. " + e.Message);
            }
        }

        private GeoLocation GetGeoLocationCoordinates(string longitude, string latitude)
        {
            return new GeoLocation
            {
                Latitude = Convert.ToDouble(latitude),
                Longitude = Convert.ToDouble(longitude)
            };
        }

        private bool ValidateRequest(string longitude, string latitude, string limit = "0")
        {
            if (!double.TryParse(longitude, out _))
                return false;

            if (!double.TryParse(latitude, out _))
                return false;

            if (!int.TryParse(limit, out _))
                return false;

            return true;

        }
    }
}