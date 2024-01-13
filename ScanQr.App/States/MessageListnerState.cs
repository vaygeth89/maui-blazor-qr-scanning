using Bloc.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace ScanQr.App.States;


class MessageListenerCubit<T> : Cubit<MessageListenerState>
{
    private HubConnection _hubConnection;

    public MessageListenerCubit(string url) : base(new MessageListenerOn())
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(url)
            .Build();
        ConnectListner<T>(ListenToReceivedMessages);
    }

    private void ListenToReceivedMessages(T message)
    {
        Emit(new MessageListenerReceived<T>(message));
    }

    private async void ConnectListner<T>(Action<T> handleMessage)
    {
        _hubConnection.On("ReceiveMessage", handleMessage);

        await _hubConnection.StartAsync();
    }

    public async Task SendAsync(T message)
    {
        await _hubConnection.SendAsync("SendMessage", message);
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