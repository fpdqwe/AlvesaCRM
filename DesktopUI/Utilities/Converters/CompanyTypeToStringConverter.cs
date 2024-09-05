using Domain.Enums;
using System.Globalization;
using System.Windows.Data;

namespace DesktopUI.Utilities.Converters
{
    class CompanyTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CompanyType type)
            {
                return type switch
                {
                    CompanyType.Individual => "Физ. лицо",
                    CompanyType.IE => "Индивидуальный предприниматель",
                    CompanyType.LE => "Юридическое лицо"
                };
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringType)
            {
                return stringType switch
                {
                    "Физ. лицо" => CompanyType.Individual,
                    "Индивидуальный предприниматель" => CompanyType.IE,
                    "Юридическое лицо" => CompanyType.LE,
                    _ => throw new ArgumentException("Неизвестный вариант", nameof(value))
                };
            }
            throw new InvalidOperationException("Неверный тип ввода для перобразования.");
        }
    }
}
