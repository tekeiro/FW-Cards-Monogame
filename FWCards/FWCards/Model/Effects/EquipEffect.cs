using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWCards.Utils;

namespace FWCards.Model.Effects
{
    public enum EquipmentEffectType
    {
        AddState,
        DelState,
        DrawCards,
        RechargeCount
    }

    public class EquipEffect
    {
        public EquipEffect(EquipmentEffectType type, ushort[] parameters)
        {
            Type = type;
            Params = parameters;
        }

        public EquipmentEffectType Type { get; set; }
        public ushort[] Params { get; set; }

        public AlteredState GetAlteredState(int index)
        {
            return (AlteredState) Params[index];
        }

        public override string ToString()
        {
            return $"{{{nameof(Type)}:{Type}, {nameof(Params)}:{Converter.ArrayToString(Params)}}}";
        }
    }
}
