using System.Globalization;
using System.Windows.Data;

namespace DesktopUI.Utilities.Converters
{
	public class MaxHeightConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			double totalHeight = (double)value;

			// Учитываем высоту кнопки или других элементов интерфейса
			return totalHeight - 60; // Например, вычитаем 50 пикселей для других элементов
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
