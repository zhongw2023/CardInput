using System.Text.Json.Serialization;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace CardInput.Models;

public class Feature : ISuspendable
{
    public byte CmdId { get; set; } = 0x00;

    private string _title;

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    private string _editingPrice;

    [JsonIgnore]
    public string EditingPrice
    {
        get => _editingPrice;
        set
        {
            SetProperty(ref _editingPrice, value);
            //刷新CommandSend的CanExecute
            ((RelayCommand)CommandSend).NotifyCanExecuteChanged();
        }
    }

    private string _price;

    [JsonIgnore]
    public string Price
    {
        get => _price;
        set => SetProperty(ref _price, value);
    }

    public string IconKey { get; set; }

    [JsonIgnore] public ICommand CommandSend { get; set; }
}