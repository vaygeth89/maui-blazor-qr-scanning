namespace ScanQrShared;

public interface INotificationGenerator<T>
{
    int GetId();
    T Data();

    string Title();
    string Description();
}

public record Person(string FirstName, string LastName) : INotificationGenerator<Person>
{
    public int GetId()
    {
        return 1;
    }

    public Person Data() => this;
    public string Title() => "Customer Verified";
    public string Description() => $"{FirstName} {LastName}";
}

public record ResponseStatus(bool IsSuccessful);

public record QrVerificationMessage(bool IsSuccessful, Person Person);