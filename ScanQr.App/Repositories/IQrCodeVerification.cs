using Refit;

namespace ScanQr.App.Repositories;


public interface IQrCodeVerification
{
    [Get("verify-qr-code")]
    Task<ResponseStatus> VerifyQrCode(string code);
}

public record ResponseStatus(bool IsSuccessful);
