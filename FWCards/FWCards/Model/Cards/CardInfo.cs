using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWCards.Model.Effects;

namespace FWCards.Model.Cards
{
    public class CardInfo
    {

        public uint Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte Flags { get; set; }
        public ManaType ManaType { get; set; }
        public byte TargetCount { get; set; }
        public TargetType TargetType { get; set; }
        public ushort Icon { get; set; }
        public string HitAnim { get; set; }
        public string HitSound { get; set; }
        public uint Price { get; set; }
        public List<Effect> Effects { get; set; } = new List<Effect>();

        public bool isMagic()
            => (Flags & 1) == 1;

        public bool isConsumable()
            => (Flags & 2) == 2;

        public bool canBeDismantled()
            => (Flags & 4) == 4;


        public override string ToString()
        {
            return $"{nameof(CardInfo)}={{{nameof(Id)}: {Id}, " +
                   $"{nameof(Name)}: {Name}, " +
                   $"{nameof(Description)}: {Description}," +
                   $" {nameof(Flags)}: {Flags}, {nameof(ManaType)}: {ManaType}," +
                   $" {nameof(TargetCount)}: {TargetCount}, {nameof(TargetType)}: {TargetType}," +
                   $" {nameof(Icon)}: {Icon}, {nameof(HitAnim)}: {HitAnim}, " +
                   $"{nameof(HitSound)}: {HitSound}, {nameof(Price)}: {Price}}}";
        }
    }
}
