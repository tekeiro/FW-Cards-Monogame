using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWCards.Model
{
    public enum ManaType
    {
        None,
        Fire,
        Water,
        Ice,
        Earth,
        Air,
        Thunder,
        Light,
        Dark,
        Natura,
        Purity,
        TimeSpace,
        Will
    }

    public enum AlteredState
    {
        Burnt=0,
        Frozen=1,
        Paralized=2,
        Blind,
        Muted,
        Confused,
        Sleepy,
        Blessed,
        Poisoned,
        Wet,
        Inspired
    }

    public enum BattleParameters
    {
        Health,
        Attack,
        Deffense,
        Intelligence,
        Resistance,
        Agility,
        Luck
    }

    public enum TargetType
    {
        Me,
        Ally,
        Enemy,
        Both
    }
}
