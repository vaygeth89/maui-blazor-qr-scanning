using Bloc.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace ScanQr.App.States;


class MessageListenerCubit<T> : Cubit<MessageListenerState>
{
    private HubConnection _hubConnection;
    private string Channel { get; set; }
    private string Url { get; set; }
    public MessageListenerCubit(string channel,string url) : base(new MessageListenerOn())
    {
        Channel = channel;
        Url = url;
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(Url)
            .Build();
        ConnectListener(ListenToReceivedMessages);
    }

    private async void ConnectListener(Action<T> handleMessage)
    {
        _hubConnection.On(Channel, handleMessage);
        await _hubConnection.StartAsync();
    }

    private void ListenToReceivedMessages(T message)
    {
        Emit(new MessageListenerReceived<T>(message));
    }
    public async Task SendAsync(T message)
    {
        await _hubConnection.SendAsync(Channel, message);
    }

    public override void Dispose()
    {
        _ = _hubConnection.DisposeAsync();
        Emit(new MessageListenerClosed());
    }
}

public abstract record MessageListenerState() : BlocState;

public record MessageListenerOn() : MessageListenerState;

public record MessageListenerReceived<T>(T Message) : MessageListenerOn;

public record MessageListenerClosed() : MessageListenerState;