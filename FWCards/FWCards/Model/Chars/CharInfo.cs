using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWCards.Model.Chars
{
    public class CharInfo
    {

        public string Name { get; set; }
        public CharGrowth Growth { get; set; } = new CharGrowth();

        public override string ToString()
        {
            return $"{nameof(CharInfo)}={{{nameof(Name)}: {Name}, {nameof(Growth)}: {Growth}}}";
        }
    }
}
