using System.Collections;

namespace ScanQr.App.Configurations;

public class Endpoint
{
    public required string QrCode { get; set; }
}

public class ApiKey
{
    public required string Service { get; set; }
    public required string Key { get; set; }
}

public class AppSettings
{
    public  List<Endpoint> Endpoints { get; set; } = new List<Endpoint>();
    public  List<ApiKey> ApiKeys { get; set; } = new List<ApiKey>();
}

public class AppSettingsDictionary
{
    public AppSettings AppSettings { get; set; }

    public AppSettingsDictionary(bool isDevelopment)
    {
        AppSettings = isDevelopment ? BuildDevelopmentAppSettings() : BuildAppSettings();
    }

    public static AppSettings BuildDevelopmentAppSettings()
    {
        return new AppSettings
        {
            Endpoints = new List<Endpoint>
            {
                new Endpoint
                {
                    QrCode = "http://172.16.1.150:5026/persons"
                }
            },
            ApiKeys = new() { }
        };
    }

    public static AppSettings BuildAppSettings()
    {
        return new AppSettings
        {
            Endpoints = new List<Endpoint>
            {
                new Endpoint
                {
                    QrCode = "http://172.16.1.150:5026/persons"
                }
            },
            ApiKeys = new() { }
        };
    }
}