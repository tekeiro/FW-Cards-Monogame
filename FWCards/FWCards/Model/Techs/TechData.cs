using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWCards.Model.Cards;

namespace FWCards.Model.Techs
{
    public class TechData
    {
        public TechData(TechInfo info, CardData card)
        {
            Info = info;
            Card = card;
        }

        public TechInfo Info { get; }
        public CardData Card { get; }


    }
}
