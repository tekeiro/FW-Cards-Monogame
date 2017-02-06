using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWCards.Utils
{
    public static class Extensions
    {

        public static string GetOrElse(this Dictionary<string, string> properties, string propertyName,
            string notFoundValue = null)
        {
            return (properties.ContainsKey(propertyName))
                ? properties[propertyName]
                : notFoundValue;
        }

    }
}
