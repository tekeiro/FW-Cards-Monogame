using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWCards.Model.Cards;

namespace FWCards.Model
{
    public class GameDB
    {
        public CardsDB Cards { get; } = new CardsDB();
    }
}
