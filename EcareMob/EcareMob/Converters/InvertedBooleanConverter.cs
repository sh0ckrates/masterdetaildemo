﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace EcareMob.Converters
{

    public class InverseBooleanConverter : IValueConverter
    {
        #region IValueConverter implementation
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
        #endregion

    }

}
