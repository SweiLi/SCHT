using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BridgeDetectHelper
{
    public class BridgeQueryParamConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            String bridgeName = values[0].ToString();
            String bridgePile = values[1].ToString();
            Tuple<string, string> tp = new Tuple<string, string>(bridgeName,bridgePile);
            return tp;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
