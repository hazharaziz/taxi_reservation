using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using System;

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

        [HttpGet]
        public JsonResult GetAllTrips()
        {
            return new JsonResult(_tripService.GetAllTrips());
        }

        [HttpGet("{tripId}")]
        public JsonResult GetTripById(int tripId)
        {
            return new JsonResult(_tripService.GetTripById(tripId));
        }

        [HttpPost("request")]
        public JsonResult RequestTrip([FromBody] TripRequest tripRequest)
        {
            try
            {
                var trip = _tripService
                    .RequestTrip(tripRequest.PassengerId, tripRequest.Origin, tripRequest.Destination);
                return new JsonResult(trip);
            }
            catch (Exception exception)
            {
                return new JsonResult(new { error = exception.Message });
            }
        }

        [HttpPut("pay/{tripId}")]
        public JsonResult PayTripPrice(int tripId, [FromQuery] long passengerId)
        {
            try
            {
                _tripService.PayTripPrice(tripId, passengerId);
                return new JsonResult(new { message = "Trip price paid successfully!" });
            }
            catch (Exception exception)
            {
                return new JsonResult(new { error = exception.Message });
            }
        }

        [HttpPut("end/{tripId}")]
        public JsonResult EndTrip(int tripId, [FromQuery] long driverId)
        {
            try
            {
                _tripService.EndTrip(tripId, driverId);
                return new JsonResult(new { message = "Trip ended successfully!" });
            }
            catch (Exception exception)
            {
                return new JsonResult(new { error = exception.Message });
            }
        }

        [HttpPut("cancel/{tripId}")]
        public JsonResult CancelTrip(int tripId, [FromQuery] long userId)
        {
            try
            {
                _tripService.CancelTrip(tripId, userId);
                return new JsonResult(new { message = "Trip canceled successfully!" });
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
        public long PassengerId { get; set; }

        public Address Origin { get; set; }

        public Address Destination { get; set; }
    }

    public class AssignDriverToTrip
    {
        public long TripId { get; set; }

        public long DriverId { get; set; }
    }
}
