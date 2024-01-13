using Microsoft.AspNetCore.SignalR;

namespace ScanQrApi;

public class ChatHub : Hub
{
    public void Send(string user, string message)
    {
        // Call the "OnMessage" method to update clients.
        Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}