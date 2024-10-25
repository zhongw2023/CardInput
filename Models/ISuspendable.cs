using CardInput.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace CardInput.Models;

public abstract class ISuspendable : ObservableObject
{
    [JsonIgnore] public CancellationTokenSource TokenSource { get; set; }

    private int _countdown;

    [JsonIgnore]
    public int Countdown
    {
        get => _countdown;
        set => SetProperty(ref _countdown, value);
    }

    private AckStatus _ackStatus = AckStatus.Ready;

    [JsonIgnore]
    public AckStatus AckStatus
    {
        get => _ackStatus;
        set => SetProperty(ref _ackStatus, value);
    }
}