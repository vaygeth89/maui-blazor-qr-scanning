using System.Text.Json;
using Bloc.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Refit;
using ScanQr.App.Configurations;
using ScanQr.App.Repositories;
using ScanQr.App.States;
using ZXing.Net.Maui.Controls;

namespace ScanQr.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.Services.AddScoped(sp =>
                new BlocBuilder<BarCodeScanningCubit, BarCodeScanState>(new BarCodeScanningCubit()));
            builder.Services.AddSingleton(sp =>
                new BlocBuilder<MessageListenerCubit<QrVerificationMessage>, MessageListenerState>(new(
                    //Todo add url
                    ""
                )));
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); })
                .UseBarcodeReader();

            AppSettings appSettings = new AppSettingsDictionary(true).AppSettings;
            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            builder.Services
                .AddRefitClient<IQrCodeVerification>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(appSettings.Endpoints.First().QrCode));
            var app = builder.Build();
            return app;
        }
    }
}