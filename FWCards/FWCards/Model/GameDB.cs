using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWCards.Config;
using FWCards.Model.Cards;
using FWCards.Model.Chars;
using FWCards.Model.Enemies;
using FWCards.Model.Equipment;
using FWCards.Model.Techs;
using Newtonsoft.Json;

namespace FWCards.Model
{
    public class GameDB
    {
        
        public CardsDB Cards { get; } = new CardsDB();
        public TechsDB Techs { get; } = new TechsDB();
        public EquipmentsDB Equipments { get; } = new EquipmentsDB();
        public EnemyDB Enemies { get; } = new EnemyDB();
        
        //----- CHARS  ------
        public CharInfo Gray { get; private set; }
        public CharInfo Ginger { get; private set; }
        public CharInfo Blonde { get; private set; }
        public CharInfo Guy { get; private set; }


        public void loadDatabase()
        {
            Cards.loadCards();
            Techs.loadTechs();
            Equipments.loadEquipments();
            Enemies.loadEnemies();
            loadChars();
        }

        private void loadChars()
        {
            using (StreamReader file = File.OpenText(Constants.CHARS_PATH))
            {
                var serializer = new JsonSerializer();
                CharInfo[] charsArray = serializer.Deserialize<CharInfo[]>(new JsonTextReader(file));
                Gray = (charsArray.Length > 0) ? charsArray[0] : null;
                Ginger = (charsArray.Length > 1) ? charsArray[1] : null;
                Blonde = (charsArray.Length > 2) ? charsArray[2] : null;
                Guy = (charsArray.Length > 3) ? charsArray[3] : null;
            }
        }
    }
}
