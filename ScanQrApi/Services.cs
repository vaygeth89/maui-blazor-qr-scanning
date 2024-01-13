using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ScanQrShared;

namespace ScanQrApi;

public class QrCodeVerification : IQrCodeVerification
{
    private readonly IHubContext<ChatHub> _hubContext;

    public QrCodeVerification(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }
    
    public async Task<ResponseStatus> VerifyQrCode(string code)
    {
        await Task.Delay(TimeSpan.FromSeconds(2));
        return new ResponseStatus(true);
    }

    public async Task<Person> SendPersonInformationMessage()
    {
        await Task.Delay(TimeSpan.FromSeconds(5));
        await _hubContext.Clients.All.SendAsync("ReceiveMessage", _persons.First());
        return _persons.First();
    }

    public async Task<IEnumerable<Person>> GetPersons()
    {
        return _persons;
    }

    private List<Person> _persons = new List<Person>()
    {
        new Person("John", "Doe"),
        new Person("Jane", "Smith"),
    };

}