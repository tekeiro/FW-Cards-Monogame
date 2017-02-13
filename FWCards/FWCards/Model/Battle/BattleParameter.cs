using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FWCards.Model.Battle
{
    public class BattleParameter
    {
        
        public ushort Health { get; set; }
        public byte Attack { get; set; }
        public byte Deffense { get; set; }
        public byte Intelligence { get; set; }
        public byte Resistance { get; set; }
        public byte Agility { get; set; }
        public byte Luck { get; set; }

        [JsonIgnore]
        public byte this[BattleParameters parameter]
        {
            get
            {
                switch (parameter)
                {
                    case BattleParameters.Attack:
                        return Attack;
                    case BattleParameters.Agility:
                        return Agility;
                    case BattleParameters.Deffense:
                        return Deffense;
                    case BattleParameters.Intelligence:
                        return Intelligence;
                    case BattleParameters.Luck:
                        return Luck;
                    case BattleParameters.Resistance:
                        return Resistance;
                }
                return (byte) 0;
            }
            set
            {
                switch (parameter)
                {
                    case BattleParameters.Attack:
                        Attack = value;
                        break;
                    case BattleParameters.Agility:
                        Agility = value;
                        break;
                    case BattleParameters.Deffense:
                        Deffense = value;
                        break;
                    case BattleParameters.Intelligence:
                        Intelligence = value;
                        break;
                    case BattleParameters.Luck:
                        Luck = value;
                        break;
                    case BattleParameters.Resistance:
                        Resistance = value;
                        break;
                }
            }
        }

        public void addParameter(BattleParameters parameter, ushort amount)
        {
            if (parameter != BattleParameters.Health)
            {
                this[parameter] = (byte) ((byte) this[parameter] + (byte) amount);
            }
            else
            {
                Health += amount;
            }
        }


        public override string ToString()
        {
            return $"{nameof(BattleParameter)}={{{nameof(Health)}: {Health}, {nameof(Attack)}: {Attack}, {nameof(Deffense)}: {Deffense}, {nameof(Intelligence)}: {Intelligence}, {nameof(Resistance)}: {Resistance}, {nameof(Agility)}: {Agility}, {nameof(Luck)}: {Luck}}}";
        }
    }
}
