using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWCards.Config;
using Newtonsoft.Json;
using Random = Nez.Random;

namespace FWCards.Model.Cards
{
    public class CardsDB
    {
        private Dictionary<uint, CardInfo> cards = new Dictionary<uint, CardInfo>();


        public void loadCards()
        {
            using (StreamReader file = File.OpenText(Constants.CARDS_PATH))
            {
                var serializer = new JsonSerializer();
                CardInfo[] cardsArray = serializer.Deserialize<CardInfo[]>(new JsonTextReader(file));
                cards.Clear();

                foreach (var cardItem in cardsArray)
                {
                    cards[cardItem.Id] = cardItem;
                }
            }
        }

        public CardInfo findById(uint id)
            => cards[id];

        public CardInfo findByName(string name)
        {
            foreach (var cardItem in cards.Values)
            {
                if (cardItem.Name == name)
                    return cardItem;
            }
            return null;
        }

        public CardData createCard(CardInfo info)
        {
            return createCard(info, (byte)Random.nextInt(10));
        }

        public CardData createCard(CardInfo info, byte value)
        {
            CardData card = new CardData(info);
            card.Value = value;
            return card;
        }

        
    }
}
