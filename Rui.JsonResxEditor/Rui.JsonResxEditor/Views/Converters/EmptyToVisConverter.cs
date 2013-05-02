using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Rui.JsonResxEditor.Views
{
    public class EmptyToVisConverter : IValueConverter
    {
        public bool Reverse { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Reverse ? Visibility.Visible : Visibility.Collapsed;
            }
            PropertyInfo propertyInfo = value.GetType().GetProperty("Count");
            if (propertyInfo != null)
            {
                int count = (int)propertyInfo.GetValue(value, null);
                if (count > 0)
                {
                    return Reverse ? Visibility.Collapsed : Visibility.Visible;
                }
            }
            return Reverse ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
