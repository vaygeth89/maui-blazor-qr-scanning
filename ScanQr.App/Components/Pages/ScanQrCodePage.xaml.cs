﻿using Bloc.Models;
using ScanQr.App.States;
using ZXing.Net.Maui;

namespace ScanQr.App.Components.Pages;

public partial class ScanQrCodePage
{
    private readonly BlocBuilder<BarCodeScanningCubit, BarCodeScanState> _barCodeBuilder;

    public ScanQrCodePage(BlocBuilder<BarCodeScanningCubit, BarCodeScanState> barCodeBuilder)
    {
        _barCodeBuilder = barCodeBuilder;
        _barCodeBuilder.Bloc.OnStateChanged += ListenToBarCodeChanges;
        InitializeComponent();

        cameraBarcodeReaderView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.TwoDimensional,
            AutoRotate = true,
            Multiple = true
        };
    }

    private async void ListenToBarCodeChanges(BarCodeScanState state)
    {
        if (state is BarCodeScanDetected)
        {
            cameraBarcodeReaderView.IsDetecting = false;
            await Navigation.PopAsync();
        }
    }

    protected async void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        {
            Dispatcher.Dispatch(async () =>
            {
                foreach (var barcode in e.Results)
                {
                    Console.WriteLine($"Barcodes: {barcode.Format} -> {barcode.Value}");
                    await _barCodeBuilder.Bloc.ScanDetected(barcode.Value);
                }
            });
        }
    }
}