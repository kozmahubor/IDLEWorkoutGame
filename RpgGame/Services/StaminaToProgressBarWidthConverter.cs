using RpgGame.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace RpgGame.Services
{
    internal class StaminaToProgressBarWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int stamina = (int)value;

            // Adjust the width based on the stamina value
            if (stamina >= 500)
                return 300; // Large width (e.g., 300 pixels)
            else if (stamina <= 100)
                return 30; // Small width (e.g., 30 pixels)
            else
                return (stamina - 100) * 2; // Linearly adjust the width between 30 and 300 pixels
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
