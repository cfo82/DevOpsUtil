namespace DevOpsUtil.Wpf.Core.Converter;

using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

// http://www.broculos.net/2014/04/wpf-how-to-use-converters-without.html
public abstract class ConverterMarkupExtension<T> : MarkupExtension, IValueConverter
    where T : class, new()
{
    private static T? _converter = null;

    public ConverterMarkupExtension()
    {
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return _converter ??= new T();
    }

    public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

    public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
}