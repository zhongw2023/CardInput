using System.Globalization;
using System.Windows;
using System.Windows.Data;
using CardInput.Enums;

namespace CardInput.Converters;

public class AckStatusToTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not AckStatus status)
        {
            return DependencyProperty.UnsetValue;
        }

        return status switch
        {
            AckStatus.Received => "收到确认消息",
            AckStatus.Timeout => "响应超时",
            AckStatus.Waiting => "等待响应消息",
            _ => "准备就绪"
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}