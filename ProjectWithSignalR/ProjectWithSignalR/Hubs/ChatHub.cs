using Microsoft.AspNetCore.SignalR;
using ProjectWithSignalR.StaticResources;

namespace ProjectWithSignalR.Hubs
{
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            ChatMemory._userscount++;
            string userid = Context.ConnectionId;
            ChatMemory._connectedUsers.Add(userid, userid);

            await Clients.All.SendAsync("userconnected", userid, ChatMemory._userscount);
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            ChatMemory._userscount--;
            string userid = Context.ConnectionId;
            ChatMemory._connectedUsers.Remove(userid);

            await Clients.All.SendAsync("userdisconnected", userid, ChatMemory._userscount);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
