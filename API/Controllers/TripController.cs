using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/trip")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly TripService _tripService;

        public TripController(TripService tripService)
        {
            _tripService = tripService;
        }


        [HttpPost("request")]
        public JsonResult RequestTrip([FromBody] TripRequest tripRequest)
        {
            try
            {
                var trip = _tripService.RequestTrip(tripRequest.Passenger, tripRequest.Origin, tripRequest.Destination);
                return new JsonResult(trip);
            }
            catch (Exception exception)
            {
                return new JsonResult(new { error = exception.Message });
            }
        }

        [HttpPut("assign-driver")]
        public JsonResult AssignDriverToTrip([FromQuery] AssignDriverToTrip requestModel)
        {
            try
            {
                var trip = _tripService.AssignDriverToTrip(requestModel.TripId, requestModel.DriverId);
                return new JsonResult(trip);
            }
            catch (Exception exception)
            {
                return new JsonResult(new { error = exception.Message });
            }
        }
    }

    public class TripRequest
    {
        public Passenger Passenger { get; set; }

        public Address Origin { get; set; }

        public Address Destination { get; set; }
    }

    public class AssignDriverToTrip
    {
        public long TripId { get; set; }

        public long DriverId { get; set; }
    }
}
