using Shiny;
using Shiny.Hosting;

namespace ScanQr.App;

public class PushRegistration
{
    public async Task CheckPermission()
    {
        var push = Host.Current.Services.GetService<IPushManager>();
        var result = await push.RequestAccess();
        if (result.Status == AccessState.Available)
        {
            // good to go

            // you should send this to your server with a userId attached if you want to do custom work
            var value = result.RegistrationToken;
        }
    }
}

public class PushDelegate : IPushDelegate
{
    public async Task OnEntry(PushEntryArgs args)
    {
        // fires when the user taps on a push notification
    }

    public async Task OnReceived(IDictionary<string, string> data)
    {
        // fires when a push notification is received 
        // iOS: set content-available: 1  or this won't fire
        // Android: Set data portion of payload
    }

    public async Task OnTokenChanged(string token)
    {
        // fires when a push registration token change is set by the operating system or provider
    }
}