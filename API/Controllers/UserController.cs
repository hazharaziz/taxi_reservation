using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public JsonResult GetAllUsers()
        {
            return new JsonResult(_userService.GetAllUsers());
        }

        [HttpGet("{userId}")]
        public JsonResult GetUserById(long userId)
        {
            return new JsonResult(_userService.GetUserById(userId));
        }
    }
}
