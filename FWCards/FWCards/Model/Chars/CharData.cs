using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using FWCards.Config;
using FWCards.Model.Battle;
using Newtonsoft.Json;

namespace FWCards.Model.Chars
{
    public class CharData
    {
        //-------------  EVENTS  ----------------
        public delegate void LevelChangedHandler(byte newLevel);

        public delegate void XpAddedHandler(uint xpAmount);

        public event LevelChangedHandler LevelChanged;
        public event XpAddedHandler XpAdded;


        //-------------  CONSTRUCTOR  ----------------
        public CharData(CharInfo info)
        {
            Info = info;
            updateBaseParameter();
        }

        //-------------  FIELDS  ----------------
        [JsonProperty]
        private uint xp = 0;
        [JsonProperty]
        private byte level = 1;
        [JsonProperty]
        private byte manaCapacity = Constants.DEFAULT_MANA_CAPACITY;
        [JsonProperty]
        private byte deckCapacity = Constants.DEFAULT_DECK_CAPACITY;
        [JsonIgnore]
        private BattleParameter baseParameter = new BattleParameter();

        //-------------  PROPERTIES  ----------------
        [JsonIgnore]
        public CharInfo Info { get; }


        //-------------  METHODS  ----------------

        public uint getXpNewLevel()
            => Info.Growth.XpRequired.NextInt(level);


        public void addXp(uint xpAmount)
        {
            xp += xpAmount;
            XpAdded?.Invoke(xpAmount);
            var nextLevelXp = getXpNewLevel();
            if (xp >= nextLevelXp)
            {
                level++;
                manaCapacity = (byte)Info.Growth.ManaCapacity.NextInt(level);
                deckCapacity = (byte) Info.Growth.DeckCapacity.NextInt(level);
                updateBaseParameter();
                LevelChanged?.Invoke(level);
            }
        }

        private void updateBaseParameter()
        {
            baseParameter.Health = Info.Growth.Health.NextUShort(level);
            baseParameter.Attack = Info.Growth.Attack.NextByte(level);
            baseParameter.Agility = Info.Growth.Agility.NextByte(level);
            baseParameter.Deffense = Info.Growth.Deffense.NextByte(level);
            baseParameter.Intelligence = Info.Growth.Intelligence.NextByte(level);
            baseParameter.Luck = Info.Growth.Luck.NextByte(level);
            baseParameter.Resistance = Info.Growth.Resistance.NextByte(level);
        }

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext ctx)
        {
            updateBaseParameter();
        }

    }
}
