using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWCards.Model.Effects
{
    public static class EffectFactory
    {

        public static Effect Attack(ushort power, ushort successPercentage, 
            ManaType attackElement = ManaType.None, bool ignoreDeffense = false)
        {
            return new Effect(EffectType.Attack, new []
            {
                (ushort) attackElement,
                power,
                successPercentage,
                (ushort)((ignoreDeffense) ? 1 : 0)
            });
        }

        public static Effect Heal(ushort lifeCount = 0, ushort lifePercentage = 0)
        {
            return new Effect(EffectType.Heal, new [] { lifeCount, lifePercentage});
        }

        public static Effect AttackMana(ushort count)
        {
            return new Effect(EffectType.AttackMana, new []{ count });
        }

        public static Effect AddState(AlteredState state, ushort turns, ushort successPercentage)
        {
            return new Effect(EffectType.AddState, new []{ (ushort)state, turns, successPercentage });
        }

        public static Effect DelState(AlteredState state, ushort successPercentage)
        {
            return new Effect(EffectType.DelState, new []{ successPercentage });
        }

        public static Effect AddBuff(BattleParameters parameter, ushort turns, ushort successPercentage)
        {
            return new Effect(EffectType.AddBuff, new []{ (ushort)parameter, turns, successPercentage });
        }

        public static Effect AddDeBuff(BattleParameters parameter, ushort turns, ushort successPercentage)
        {
            return new Effect(EffectType.AddDebuff, new[] { (ushort)parameter, turns, successPercentage });
        }

        public static Effect DelBuff(BattleParameters parameter, ushort successPercentage)
        {
            return new Effect(EffectType.DelBuff, new []{ (ushort)parameter, successPercentage });
        }

        public static Effect DelDeBuff(BattleParameters parameter, ushort successPercentage)
        {
            return new Effect(EffectType.DelDebuff, new[] { (ushort)parameter, successPercentage });
        }

        public static Effect Scape(ushort probability)
        {
            return new Effect(EffectType.Scape, new []{ probability });
        }

        public static Effect Take(ushort count)
        {
            return new Effect(EffectType.Take, new []{ count });
        }

        public static Effect ExtraDismantle(ushort extraActions)
        {
            return new Effect(EffectType.ExtraDismantle, new []{ extraActions });
        }

        public static Effect StealCards(ushort cardsCount, ushort successPercentage)
        {
            return new Effect(EffectType.Steal, new []{ cardsCount, successPercentage });
        }

        public static Effect Resurrect(ushort successPercentage, ushort lifePercentage)
        {
            return new Effect(EffectType.Resurrect, new []{ successPercentage, lifePercentage });
        }

        public static Effect RecoverCards(ushort cardsCount)
        {
            return new Effect(EffectType.RecoverCards, new []{ cardsCount });
        }
    }
}
