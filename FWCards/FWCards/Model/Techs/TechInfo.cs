using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using FWCards.Model.Cards;
using FWCards.Model.Effects;
using Newtonsoft.Json;
using Nez;

namespace FWCards.Model.Techs
{
    public class ManaRequirement
    {
        public ManaType Type { get; set; }
        public byte Count { get; set; }
    }

    public class TechInfo
    {
        private CardInfo cardRef = null;

        public uint Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte Flags { get; set; }
        public TargetType TargetType { get; set; }
        public byte TargetCount { get; set; }
        public ushort Icon { get; set; }
        public string HitAnim { get; set; }
        public string HitSound { get; set; }
        public uint Price { get; set; }
        public uint RequiredCardId { get; set; }
        public List<Effect> Effects { get; set; } = new List<Effect>();
        public List<ManaRequirement> ManaRequirements { get; set; } = new List<ManaRequirement>();

        [JsonIgnore]
        public CardInfo RequiredCard
            => cardRef;

        public bool isMagic() 
            => (Flags & 1) == 1;

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext ctx)
        {
            var gameDb = Core.services.GetService<GameDB>();
            cardRef = gameDb.Cards.findById(RequiredCardId);
        }

    }
}
