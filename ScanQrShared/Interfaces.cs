using Refit;

namespace ScanQrShared;

public interface IQrCodeVerification
{

    [Get($"/verify-qr-code")]
    Task<ResponseStatus> VerifyQrCode([Body] string code);

    [Get($"/person-information-message")]
    Task<Person> SendPersonInformationMessage();


    [Get($"/persons")]
    Task<IEnumerable<Person>> GetPersons();
}

public interface IQrCodeVerificationClient
{

    [Get($"/verify-qr-code")]
    Task<ResponseStatus> VerifyQrCode([Body] string code);

    [Get($"/person-information-message")]
    Task<ApiResponse<Person>> SendPersonInformationMessage();


    [Get($"/persons")]
    Task<IEnumerable<Person>> GetPersons();
}