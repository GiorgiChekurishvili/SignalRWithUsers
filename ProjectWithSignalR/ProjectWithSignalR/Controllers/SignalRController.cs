using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ProjectWithSignalR.Hubs;
using ProjectWithSignalR.StaticResources;

namespace ProjectWithSignalR.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignalRController : ControllerBase
    {
        public readonly IHubContext<ChatHub> _hubContext;
        public SignalRController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }
        [HttpPost]
        public async Task<IActionResult> PostMessage(string message)
        {
            if (message == null)
            {
                return BadRequest();
            }
            await _hubContext.Clients.All.SendAsync("ReceiveMessageFromApi", "api2", message);
            return Ok();
        }
        [HttpPost("PostPrivateMessage")]
        public async Task<IActionResult> PostPrivateMessage(string connectionid,string message)
        {
            if (message == null)
            {
                return BadRequest();
            }
            await _hubContext.Clients.User(connectionid).SendAsync("GetPrivateMessageFromApi", message);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return  Ok(ChatMemory._connectedUsers.ToList());
        }

    }
}
