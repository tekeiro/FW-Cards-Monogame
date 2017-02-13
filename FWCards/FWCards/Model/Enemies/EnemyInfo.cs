using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWCards.Model.Battle;
using FWCards.Model.Cards;
using FWCards.Model.Effects;
using Nez;

namespace FWCards.Model.Enemies
{

    public class EnemyCard
    {
        public ushort StealProbability { get; set; }
        public byte BaseValue { get; set; }
        public uint CardId { get; set; }

        public CardInfo Card
        {
            get
            {
                var gameDb = Core.services.GetService<GameDB>();
                return gameDb.Cards.findById(CardId);
            }
        }

        public override string ToString()
        {
            return $"({nameof(StealProbability)}: {StealProbability}, {nameof(BaseValue)}: {BaseValue}, {nameof(CardId)}: {CardId})";
        }
    }

    public class AlteredStateResistance
    {
        public AlteredState State { get; set; }
        public ushort Probability { get; set; }

        public override string ToString()
        {
            return $"({nameof(State)}: {State}, {nameof(Probability)}: {Probability})";
        }
    }

    public class ParameterDecreaseResistance
    {
        public BattleParameters Parameter { get; set; }
        public ushort Probability { get; set; }

        public override string ToString()
        {
            return $"({nameof(Parameter)}: {Parameter}, {nameof(Probability)}: {Probability})";
        }
    }

    public class EnemyAttack
    {
        public string Name { get; set; }
        public byte ManaRequired { get; set; }
        public List<Effect> Effects { get; set; } 
            = new List<Effect>();

        public override string ToString()
        {
            return $"({nameof(Name)}: {Name}, {nameof(ManaRequired)}: {ManaRequired}, {nameof(Effects)}: {Effects})";
        }
    }

    public class EnemyInfo
    {

        public uint Id { get; set; }
        public string Name { get; set; }
        public BattleParameter BaseParameter { get; set; }
        public byte Level { get; set; }
        public uint Xp { get; set; }
        public uint Money { get; set; }
        public byte MaxCardsAllowedToSteal { get; set; }
        public string IAScript { get; set; }
        public byte MaximumMana { get; set; }
        public List<EnemyCard> CardsCanBeStolen { get; set; }
            = new List<EnemyCard>();
        public List<AlteredStateResistance> DiseasesResistances { get; set; }
            = new List<AlteredStateResistance>();
        public List<ParameterDecreaseResistance> ParametersResistances { get; set; } 
            = new List<ParameterDecreaseResistance>();
        public List<EnemyAttack> EnemyAttacks { get; set; }
            = new List<EnemyAttack>();

        public override string ToString()
        {
            return $"{nameof(EnemyInfo)}={{{nameof(Id)}: {Id}," +
                   $" {nameof(Name)}: {Name}, " +
                   $"{nameof(BaseParameter)}: {BaseParameter}, " +
                   $"{nameof(Level)}: {Level}, {nameof(Xp)}: {Xp}, " +
                   $"{nameof(Money)}: {Money}, {nameof(MaxCardsAllowedToSteal)}: {MaxCardsAllowedToSteal}, " +
                   $"{nameof(IAScript)}: {IAScript}, {nameof(MaximumMana)}: {MaximumMana}," +
                   $" {nameof(CardsCanBeStolen)}: {CardsCanBeStolen}, " +
                   $"{nameof(DiseasesResistances)}: {DiseasesResistances}," +
                   $" {nameof(ParametersResistances)}: {ParametersResistances}," +
                   $" {nameof(EnemyAttacks)}: {EnemyAttacks}}}";
        }
    }

}
