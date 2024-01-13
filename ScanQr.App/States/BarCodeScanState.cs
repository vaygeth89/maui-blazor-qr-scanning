using Bloc.Models;

namespace ScanQr.App.States;


public class BarCodeScanningCubit : Cubit<BarCodeScanState>
{
    public BarCodeScanningCubit() : base(new BarCodeScanInitialState())
    {
    }
    
    public async Task Scan()
    {
        Emit(new BarCodeScanningState());
    }

    public async Task ScanDetected(string barCode)
    {
        Emit(new BarCodeScanDetected(barCode));
    }
}

public abstract record BarCodeScanState() : BlocState;

public record BarCodeScanInitialState : BarCodeScanState;

public record BarCodeScanningState : BarCodeScanState;

public record BarCodeScanDetected(string BarCode) : BarCodeScanState;

public record BarCodeScanError(string BarCode) : BarCodeScanState;