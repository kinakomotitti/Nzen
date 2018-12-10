namespace Nzen.Hubs
{
    #region using
    using Microsoft.AspNetCore.SignalR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    #endregion

    public class ChatHub : Hub
    {
        #region single client

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public Task SendMessageToCaller(string message)
        {
            return Clients.Caller.SendAsync("ReceiveMessage", message);
        }

        #endregion

        #region group client

        public Task JoinGroup(string groupId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        }

        public Task SendMessageToGroups(string user, string message, string group)
        {
            List<string> groups = new List<string>() { group };
            return Clients.Groups(groups).SendAsync("ReceiveMessage", user, message, Context.ConnectionId);
        }

        #endregion

        #region Controller

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            await Clients.Caller.SendAsync("GetConnectionId", Context.ConnectionId);
            Manager.Log.Logger.Debug($"{Context.ConnectionId} Connected");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            //await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }

        #endregion
    }
}
