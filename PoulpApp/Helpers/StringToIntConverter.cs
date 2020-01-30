using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PoulpApp.Helpers
{
    public class StringToIntConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int quantity && parameter is string format)
            {
                try
                {
                    return string.Format(format, quantity);
                }
                catch (Exception)
                {
                }
            }
            return string.Empty;
        }

        //Must implement this if Binding with Mode=TwoWay
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

