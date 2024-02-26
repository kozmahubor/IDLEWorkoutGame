using System;
using System.Globalization;
using System.Windows.Data;

namespace RpgGame.Services;

public class GoldConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string gold = value.ToString();
        double goldAmount = double.Parse(gold);
        int roundedValue = 0;
        string tag = "";

        if (goldAmount > 999)
        {
            if (goldAmount > 999999)
            {
                goldAmount = goldAmount / 999999;
                roundedValue = (int)Math.Round(goldAmount);
                tag = "M";
            }
            else
            {
                goldAmount = goldAmount / 999;
                roundedValue = (int)Math.Round(goldAmount);
                tag = "K";
            }
            return roundedValue.ToString() + tag;
        }
        else
        {
            roundedValue = (int)goldAmount;
            return roundedValue.ToString();
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}