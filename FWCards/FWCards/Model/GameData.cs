using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWCards.Model.Chars;
using Nez;

namespace FWCards.Model
{
    public class GameData
    {
        public uint Money { get; private set; }
        public CharData Gray { get; private set; }
        public CharData Ginger { get; private set; }
        public CharData Blonde { get; private set; }
        public CharData Guy { get; private set; }


        public void addMoney(uint moneyAmount)
        {
            Money += moneyAmount;
        }

        public void initialize()
        {
            var gameDb = Core.services.GetService<GameDB>();
            Gray = new CharData(gameDb.Gray);
            Ginger = new CharData(gameDb.Ginger);
            Blonde = new CharData(gameDb.Blonde);
            Guy = new CharData(gameDb.Guy);
        }
    }
}
