using System.Globalization;
using System.Windows.Data;

namespace CardInput.Converters;

public class StringFormatConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length != 2 || values[0] is not float number || values[1] is not string format)
            return Binding.DoNothing;
        return number.ToString(format, culture);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}