using Microsoft.AspNetCore.SignalR;
using ScanQrShared;

namespace ScanQrApi;

public class ChatHub : Hub
{
    public void Send(Person person)
    {
        Clients.All.SendAsync("ReceiveMessage", person);
    }
}