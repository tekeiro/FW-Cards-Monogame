using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWCards.Utils
{
    public static class Converter
    {

        public static float ParseFloat(string toConvert, float defaultValue)
        {
            try
            {
                return float.Parse(toConvert);
            }
            catch (Exception ex)
            {
                return defaultValue;
            }
        }

        public static int ParseInt(string toConvert, int defaultValue)
        {
            try
            {
                return int.Parse(toConvert);
            }
            catch (Exception ex)
            {
                return defaultValue;
            }
        }
    }
}
