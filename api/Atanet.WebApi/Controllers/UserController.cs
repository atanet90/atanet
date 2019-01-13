namespace Atanet.WebApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.ApiResult;
    using Services.Authentication;
    using Services.Scoring;

    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IApiResultService apiResultService;

        private readonly IUserService userService;

        private readonly IScoreService scoreService;

        public UserController(IApiResultService apiResultService, IUserService userService, IScoreService scoreService)
        {
            this.apiResultService = apiResultService;
            this.userService = userService;
            this.scoreService = scoreService;
        }

        [Authorize]
        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(long userId)
        {
            this.userService.DeleteUser(userId);
            return this.apiResultService.Ok("User deleted");
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAtanetUser()
        {
            var currentUserId = this.userService.GetCurrentUserId();
            return this.GetAtanetUser(currentUserId);
        }

        [Authorize]
        [HttpGet("scoreboard")]
        public IActionResult GetScoreBoard()
        {
            var result = this.userService.GetUsersSortedByScore();
            return this.apiResultService.Ok(result);
        }

        [Authorize]
        [HttpGet("{userId}")]
        public IActionResult GetAtanetUser(long userId)
        {
            var info = this.userService.GetUserInfo(userId);
            return this.apiResultService.Ok(info);
        }

        [Authorize]
        [HttpGet("picture")]
        public IActionResult GetPicture(long? id)
        {
            var userId = id ?? this.userService.GetCurrentUserId();
            var picture = this.userService.GetUserProfilePicture(userId);
            return this.File(picture.Data, picture.ContentType);
        }
    }
}
