using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using CardInput.Enums;

namespace CardInput.Converters;

public class AckStatusToPathConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        Geometry geometry = new EllipseGeometry(new Point(50, 50), 40, 40);
        geometry.Freeze(); // 优化性能

        if (value is not AckStatus status)
        {
            return geometry;
        }

        return status switch
        {
            AckStatus.Received => Application.Current.FindResource("IconAckReceived") as Geometry,
            AckStatus.Timeout => Application.Current.FindResource("IconAckTimeout") as Geometry,
            AckStatus.Waiting => Application.Current.FindResource("IconAckWaiting") as Geometry,
            _ => geometry,
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}