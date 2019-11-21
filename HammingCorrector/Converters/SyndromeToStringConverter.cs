using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HammingCorrector.Converters
{
    public class SyndromeToStringConverter : IValueConverter
    {
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            byte[] syndromeBytes = value as byte[];

            if (syndromeBytes == null)
                throw new InvalidOperationException("Этот конвертер может использоваться только с байтовыми массивами!");

            if (syndromeBytes.All(b => b == 0))
            {
                return "Нет ошибок";
            }
            else if (syndromeBytes.First() == 1)
            {
                if (!syndromeBytes.Skip(1).All(b => b == 0))
                {
                    return "Исправима 1 ошибка";
                }
                else
                {
                    return "Ошибка в бите чётности";
                }
            }

            return "Не исправить";//четное число ошибок
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
