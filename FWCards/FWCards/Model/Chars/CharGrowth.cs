using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWCards.Model.Chars
{
    public class CharGrowth
    {

        public ParameterGrowth Health { get; set; } = new ParameterGrowth();
        public ParameterGrowth Attack { get; set; } = new ParameterGrowth();
        public ParameterGrowth Deffense { get; set; } = new ParameterGrowth();
        public ParameterGrowth Intelligence { get; set; } = new ParameterGrowth();
        public ParameterGrowth Resistance { get; set; } = new ParameterGrowth();
        public ParameterGrowth Agility { get; set; } = new ParameterGrowth();
        public ParameterGrowth Luck { get; set; } = new ParameterGrowth();
        public ParameterGrowth ManaCapacity { get; set; } = new ParameterGrowth();
        public ParameterGrowth DeckCapacity { get; set; } = new ParameterGrowth();
        public ParameterGrowth XpRequired { get; set; } = new ParameterGrowth();

    }
}
