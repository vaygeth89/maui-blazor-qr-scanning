using ScanQrShared;

namespace ScanQr.App.Models;

public record ChannelInformation(string Name, string Url);

public record NotificationMessage(string Title, string Message, Uri? Uri, string? Icon);

public delegate void OnMessageReceived<in T>(T message) ;