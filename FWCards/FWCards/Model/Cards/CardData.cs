using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWCards.Model.Cards
{
    public class CardData
    {
        public CardData(CardInfo info)
        {
            Info = info;
        }

        public CardInfo Info { get; }
        public byte Value { get; set; }

        public bool isBeingUsed()
        {
            // TODO Implement this method
            return false;
        }
    }
}
