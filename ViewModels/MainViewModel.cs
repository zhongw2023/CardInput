using System.Collections.ObjectModel;
using System.Windows;
using CardInput.Enums;
using CardInput.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mar.Cheese;

namespace CardInput.ViewModels;

public class MainViewModel : ObservableObject
{
    public MainViewModel()
    {
        #region 初始化Features

        Features =
        [
            new Feature
            {
                CmdId = 0x01, Title = "皇冠之重", IconKey = "IconCrown"
            },
            new Feature
            {
                CmdId = 0x02, Title = "国之神盾", IconKey = "IconShield"
            },
            new Feature
            {
                CmdId = 0x05, Title = "没有警报", IconKey = "IconAlarm"
            },
            new Feature
            {
                CmdId = 0x06, Title = "电影剧集", IconKey = "IconMovie"
            },
            new Feature
            {
                CmdId = 0x07, Title = "未来趋势", IconKey = "IconFinance"
            },
            new Feature
            {
                CmdId = 0x09, Title = "可食用的", IconKey = "IconCake"
            },
            new Feature
            {
                CmdId = 0x0A, Title = "其他", IconKey = "IconApps"
            }
        ];

        _cx01 = Features.SingleOrDefault(i => i.CmdId == 0x01);
        if (_cx01 != null) _cx01.CommandSend = new RelayCommand(Control0X01_ExecuteSendAsync);
        _cx02 = Features.SingleOrDefault(i => i.CmdId == 0x02);
        if (_cx02 != null) _cx02.CommandSend = new RelayCommand(Control0X02_ExecuteSendAsync);
        _cx05 = Features.SingleOrDefault(i => i.CmdId == 0x05);
        if (_cx05 != null) _cx05.CommandSend = new RelayCommand(Control0X05_ExecuteSendAsync);
        _cx06 = Features.SingleOrDefault(i => i.CmdId == 0x06);
        if (_cx06 != null) _cx06.CommandSend = new RelayCommand(Control0X06_ExecuteSendAsync);
        _cx07 = Features.SingleOrDefault(i => i.CmdId == 0x07);
        if (_cx07 != null) _cx07.CommandSend = new RelayCommand(Control0X07_ExecuteSendAsync);
        _cx09 = Features.SingleOrDefault(i => i.CmdId == 0x09);
        if (_cx09 != null) _cx09.CommandSend = new RelayCommand(Control0X09_ExecuteSendAsync);
        _cx0A = Features.SingleOrDefault(i => i.CmdId == 0x0A);
        if (_cx0A != null) _cx0A.CommandSend = new RelayCommand(Control0X0A_ExecuteSendAsync);

        #endregion

        CommandMakeAck = new RelayCommand(() =>
        {
            foreach (var item in Features)
            {
                item.TokenSource?.Cancel();
                item.Countdown = 0;
                item.AckStatus = AckStatus.Received;
            }
        });
    }

    #region 控制参数-Commands

    private async void Control0X01_ExecuteSendAsync() => await Control_ExecuteSendAsync(_cx01);
    private async void Control0X02_ExecuteSendAsync() => await Control_ExecuteSendAsync(_cx02);
    private async void Control0X05_ExecuteSendAsync() => await Control_ExecuteSendAsync(_cx05);
    private async void Control0X06_ExecuteSendAsync() => await Control_ExecuteSendAsync(_cx06);
    private async void Control0X07_ExecuteSendAsync() => await Control_ExecuteSendAsync(_cx07);
    private async void Control0X09_ExecuteSendAsync() => await Control_ExecuteSendAsync(_cx09);
    private async void Control0X0A_ExecuteSendAsync() => await Control_ExecuteSendAsync(_cx0A);

    private async Task Control_ExecuteSendAsync(Feature item)
    {
        if (!int.TryParse(item.EditingPrice, out var newPrice)) return;
        if (newPrice is < 0 or > 255) return;
        item.TokenSource = new CancellationTokenSource();
        $"{item.CmdId:X2} {newPrice}".PrintGreen();
        item.AckStatus = AckStatus.Waiting;

        await PostSend(item);
    }

    private static async Task PostSend(ISuspendable item)
    {
        item.Countdown = 30; // 设置初始倒计时
        try
        {
            await Task.Delay(1000, item.TokenSource.Token); // 等待 1 秒
            // 检查是否取消
            if (item.TokenSource.Token.IsCancellationRequested)
            {
                return; // 如果请求取消，则退出
            }

            for (var i = item.Countdown; i > 0; i--)
            {
                await Task.Delay(1000, item.TokenSource.Token); // 等待 1 秒

                // 检查是否取消
                if (item.TokenSource.Token.IsCancellationRequested)
                {
                    return; // 如果请求取消，则退出
                }

                Application.Current.Dispatcher.Invoke(() => item.Countdown--); // 更新倒计时
            }

            item.AckStatus = AckStatus.Timeout;
        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
            // 恢复按钮状态
            Application.Current.Dispatcher.Invoke(() => item.Countdown = 0); // 重置倒计时
        }
    }

    #endregion

    public ObservableCollection<Feature> Features { get; set; }
    private Feature _cx01, _cx02, _cx05, _cx06, _cx07, _cx09, _cx0A;
    public IRelayCommand CommandMakeAck { get; set; }
}