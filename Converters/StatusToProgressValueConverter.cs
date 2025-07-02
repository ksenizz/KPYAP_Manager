using System;
using System.Globalization;
using System.Windows.Data;

namespace UsersApp.Converters
{
    public class StatusToProgressValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int statusId)
            {
                switch (statusId)
                {
                    case 1: // Подготовка
                        return 25;
                    case 2: // Разработка
                        return 50;
                    case 3: // Тестирование
                        return 75;
                    case 4: // Внедрение
                        return 100;
                    default:
                        return 0;
                }
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}