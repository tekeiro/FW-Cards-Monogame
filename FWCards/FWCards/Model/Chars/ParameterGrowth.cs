using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nez;

namespace FWCards.Model.Chars
{
    /// <summary>
    /// Represents following formula:
    ///   Scalar * ( X ^ Exponent ) + Adding
    /// X is variable in range 1 to 100
    /// Values of function will be possitive and 
    /// greater than 0.
    /// </summary>
    public class ParameterGrowth
    {
        public ParameterGrowth()
        {
            Scalar = 1f;
            Exponent = 1f;
            Adding = 0f;
        }


        public float Scalar { get; set; } 
        public float Exponent { get; set; }
        public float Adding { get; set; }

        public float NextValue(uint index)
            => (Scalar*(Mathf.pow((float) index, Exponent))) + Adding;

        public uint NextInt(uint index)
            => (uint) NextValue(index);

        public ushort NextUShort(uint index)
            => (ushort) NextValue(index);

        public byte NextByte(uint index)
            => (byte) NextValue(index);
    }
}
