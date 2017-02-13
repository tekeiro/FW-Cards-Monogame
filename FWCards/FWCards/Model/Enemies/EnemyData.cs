using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWCards.Model.Cards;
using Nez;

namespace FWCards.Model.Enemies
{
    public class EnemyData
    {
        public EnemyData(EnemyInfo info)
        {
            Info = info;
            CardsStolen = 0;
        }

        public byte CardsStolen { get; private set; }
        public EnemyInfo Info { get; set; }


        public CardData stealCard()
        {
            var gameDb = Core.services.GetService<GameDB>();
            if (CardsStolen > Info.MaxCardsAllowedToSteal)
            {
                // TODO Implement this method
                return null;
            }
            else
                return null;
        }
    }
}
