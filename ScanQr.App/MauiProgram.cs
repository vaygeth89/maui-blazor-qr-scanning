using Bloc.Models;
using Microsoft.Extensions.Logging;
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
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); })
                .UseBarcodeReader()
                ;


            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();
            return app;
        }
    }
}