using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWCards.Model.Battle;
using FWCards.Model.Effects;

namespace FWCards.Model.Equipment
{
    public enum EquipmentType
    {
        Head,
        Chest,
        Legs,
        Feet,
        Accesory
    }

    public class EquipmentInfo
    {

        public uint Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EquipmentType Type { get; set; }
        public uint Price { get; set; }
        public BattleParameter Parameters { get; set; }
        public List<EquipEffect> Effects { get; set; } = new List<EquipEffect>();


        public override string ToString()
        {
            return $"{{{nameof(Id)}: {Id}, " +
                   $"{nameof(Name)}: {Name}, " +
                   $"{nameof(Description)}: {Description}, " +
                   $"{nameof(Type)}: {Type}, {nameof(Price)}: {Price}," +
                   $" {nameof(Parameters)}: {Parameters}," +
                   $" {nameof(Effects)}: {Effects}}}";
        }
    }
}
