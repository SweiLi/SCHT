using System;
using System.Globalization;
using System.Windows.Data;

namespace BridgeDetectHelper
{
    public class ProjectStateChineseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int val = (int)value;

            string ch_str = "";
            switch (val)
            {
                case 0:
                    ch_str = "在建";
                    break;
                case 1:
                    ch_str = "已完结";
                    break;
                case 2:
                    ch_str = "审核不通过";
                    break;
            }

            return ch_str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = (string)value;
            int val = -1;
            switch (str)
            {
                case "在建":
                    val = 0;
                    break;
                case "已完结":
                    val = 1;
                    break;
                case "审核不通过":
                    val = 2;
                    break;
            }

            return val;
        }
    }
}
