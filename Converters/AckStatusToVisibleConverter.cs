using System.Globalization;
using System.Windows;
using System.Windows.Data;
using CardInput.Enums;

namespace CardInput.Converters;

public class AckStatusToVisibleConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not AckStatus status) return Visibility.Collapsed;

        if (Enum.TryParse(parameter?.ToString(), out AckStatus target))
        {
            // 根据传入的参数值来决定是否启用对应的组件
            return status == target ? Visibility.Collapsed : Visibility.Visible;
        }

        return Visibility.Collapsed;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}