using System.Globalization;

namespace MyProject.Utility.Converters
{
    public static class NumberConverter
    {
        public static decimal ParseDecimal(string value)
        {
            value = value.Replace(',', '.');
            return decimal.Parse(value, NumberStyles.Float, CultureInfo.InvariantCulture);
        }

        public static bool TryParseDecimal(string value, out decimal output)
        {
            var result = decimal.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out output);
            return result;
        }

        public static double ParseDouble(string value)
        {
            value = value.Replace(',', '.');
            return double.Parse(value, NumberStyles.Float, CultureInfo.InvariantCulture);
        }

        public static bool TryParseDouble(string value, out double output)
        {
            var result = double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out output);
            return result;
        }
    }
}
