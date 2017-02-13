using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWCards.Utils;

namespace FWCards.Model.Effects
{
    public enum EffectType
    {
        Attack,
        Heal,
        AttackMana,
        AddState,
        DelState,
        AddBuff,
        DelBuff,
        AddDebuff,
        DelDebuff,
        Scape,
        Take,
        ExtraDismantle,
        Steal,
        Resurrect,
        RecoverCards    
    }


    public class Effect
    {
        public Effect(EffectType type, ushort[] mParams)
        {
            Type = type;
            Params = mParams;
        }

        public EffectType Type { get; set; }
        public ushort[] Params { get; set; }

        public ManaType GetMana(int index)
        {
            return (ManaType)Params[index];
        }

        public AlteredState GetAlteredState(int index)
        {
            return (AlteredState) Params[index];
        }

        public BattleParameters GetParameters(int index)
        {
            return (BattleParameters) Params[index];
        }

        public override string ToString()
        {
            return $"{nameof(EquipEffect)}={{Type={Type.ToString()}, Params={Converter.ArrayToString(Params)}}}";
        }
    }
}
