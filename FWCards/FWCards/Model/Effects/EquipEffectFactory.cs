using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWCards.Model.Effects
{
    public static class EquipEffectFactory
    {

        public static EquipEffect AddState(AlteredState state, ushort turns, ushort successPercentage)
        {
            return new EquipEffect(EquipmentEffectType.AddState, 
                new []{ (ushort)state, turns, successPercentage });
        }

        public static EquipEffect DelState(AlteredState state, ushort successPercentage)
        {
            return new EquipEffect(EquipmentEffectType.DelState, new []{ (ushort)state, successPercentage });
        }

        public static EquipEffect DrawCards(ushort cardsCount)
        {
            return new EquipEffect(EquipmentEffectType.DrawCards, new []{ cardsCount });
        }

        public static EquipEffect RechargeCount(ushort rechargePercentage)
        {
            return new EquipEffect(EquipmentEffectType.RechargeCount, new []{ rechargePercentage });
        }

    }
}
