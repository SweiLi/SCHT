using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Globalization;

namespace BridgeDetectHelper
{
    public class ProjectQueryParamConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string name = values[0].ToString();
            DateTime? dt = null;
            if (values[1] != null) dt = (DateTime?)values[1];
            string ctact = values[2].ToString();
            int state_id = -1;
            if (values[3] != null)
            {
                string state_str = (string)values[3];
                if (state_str.Equals("在建")) state_id = 0;
                else if (state_str.Equals("已完结")) state_id = 1;
                else if (state_str.Equals("审核未通过")) state_id = 2;
            }

            Tuple<string, DateTime?, string, int> tp =
                new Tuple<string, DateTime?, string, int>(name, dt, ctact, state_id);

            return tp;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
