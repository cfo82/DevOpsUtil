namespace DevOpsUtil.Wpf.Core.Converter;

using System.Globalization;
using System.Windows;
using System.Windows.Data;

[ValueConversion(typeof(bool), typeof(Visibility))]
public class BoolToVisibilityConverter : ConverterMarkupExtension<BoolToVisibilityConverter>
{
    public BoolToVisibilityConverter()
    {
    }

    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value != null && value is bool)
        {
            bool actualVisibility = (bool)value;

            if (parameter is string && string.Equals(((string)parameter).ToLowerInvariant(), "true"))
            {
                actualVisibility = !actualVisibility;
            }

            return actualVisibility ? Visibility.Visible : Visibility.Collapsed;
        }

        return Visibility.Visible;
    }

    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException("Conversion from Visibility to bool is not supported");
    }
}