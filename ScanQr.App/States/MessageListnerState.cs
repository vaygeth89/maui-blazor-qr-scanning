using Bloc.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using ScanQr.App.Models;

namespace ScanQr.App.States;

class MessageListenerCubit<T> : Cubit<MessageListenerState>
{
    private HubConnection _hubConnection;
    private ChannelInformation _Information { get; set; }

    public MessageListenerCubit(ChannelInformation information) : base(new MessageListenerOn())
    {
        _Information = information;
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(_Information.Url)
            .Build();
    }

    private async Task ConnectListener(Action<T> handleMessage)
    {
        try
        {
            _hubConnection.On(_Information.Name, handleMessage);
            await _hubConnection.StartAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Emit(new MessageListenerFailedToConnect());
        }
    }

    private void ListenToReceivedMessages(T message)
    {
        Emit(new MessageListenerReceived<T>(message));
    }

    public override void Dispose()
    {
        _ = _hubConnection.DisposeAsync();
        Emit(new MessageListenerClosed());
    }

    public async void Listen()
    {
        await ConnectListener(ListenToReceivedMessages);
    }
}

public abstract record MessageListenerState() : BlocState;

public record MessageListenerOn() : MessageListenerState;

public record MessageListenerReceived<T>(T Message) : MessageListenerOn;

public record MessageListenerFailedToConnect() : MessageListenerState;

public record MessageListenerClosed() : MessageListenerState;