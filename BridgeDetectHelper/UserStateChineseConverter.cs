using System;
using System.Globalization;
using System.Windows.Data;
using BDH.Manage;

namespace BridgeDetectHelper
{
    public class UserStateChineseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            UserState state = (UserState)value;

            string ch_str = "";
            switch (state)
            {
                case UserState.OnDuty:
                    ch_str = "在岗";
                    break;
                case UserState.BeOut:
                    ch_str = "外出";
                    break;
                case UserState.Dimission:
                    ch_str = "离职";
                    break;
            }

            return ch_str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
