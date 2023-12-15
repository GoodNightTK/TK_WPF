using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows;

namespace TK_WPF.Convert
{
    public class CrcProgressBarArcConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double value = (double)values[0];
            double thickness = (double)values[1];
            double radius = ((double)values[2])/2;
            bool run = (bool)values[3];
            double max = (double)values[4];

            value = run ? max/5 : value;
            double perimeter = Math.PI * (2 * radius - thickness) / thickness;

            double showPrecent = value / max * perimeter;

            var converter = TypeDescriptor.GetConverter(typeof(DoubleCollection));

            return (DoubleCollection)converter.ConvertFrom($"{showPrecent} {perimeter}");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
