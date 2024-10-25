using System.Globalization;
using System.Windows.Data;

namespace CardInput.Converters;

public class CountToProgressConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not int countdown) return 0;
        if (parameter is not string totalString || !int.TryParse(totalString, out var total)) return 0;

        // Prevent division by zero and negative values
        if (total <= 0 || countdown <= 0) return 0;
        // ensure the countdown is not greater than the total
        if (countdown > total) return 100;
        return countdown * 1.0f / total * 100;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}