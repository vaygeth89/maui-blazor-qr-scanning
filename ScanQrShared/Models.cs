namespace ScanQrShared;

public record Person(string FirstName, string LastName);

public record ResponseStatus(bool IsSuccessful);

public record QrVerificationMessage(bool IsSuccessful, Person Person);
