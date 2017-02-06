using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Nez;

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

        /// <summary>
        /// Parse Hex Tiled Color with format #AARRGGBB
        /// </summary>
        /// <param name="hexColor"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Color ParseTiledHexColor(string hex, Color defaultValue)
        {
            try
            {
                float a = (ColorExt.hexToByte(hex[1])*16 + ColorExt.hexToByte(hex[2])) / 255f;
                float r = (ColorExt.hexToByte(hex[3])*16 + ColorExt.hexToByte(hex[4])) / 255f;
                float g = (ColorExt.hexToByte(hex[5]) * 16 + ColorExt.hexToByte(hex[6])) / 255f;
                float b = (ColorExt.hexToByte(hex[7]) * 16 + ColorExt.hexToByte(hex[8])) / 255f;

                return new Color(r, g, b, a);
            }
            catch (Exception ex)
            {
                return defaultValue;
            }
        }

        public static string ArrayToString(Array array)
        {
            var str = new StringBuilder();
            str.Append("[");
            foreach (var item in array)
            {
                str.Append(item.ToString());
                str.Append(", ");
            }
            str.Append("]");
            return str.ToString();
        }
    }
}
