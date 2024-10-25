using System.Globalization;
using System.Windows.Data;

namespace CardInput.Converters;

public class GreaterO0ToTrueConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not int countdown) return false;
        return countdown > 0;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}