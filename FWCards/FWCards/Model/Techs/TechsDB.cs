using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWCards.Config;
using FWCards.Model.Cards;
using Newtonsoft.Json;

namespace FWCards.Model.Techs
{
    public class TechsDB
    {
        private Dictionary<uint, TechInfo> techs = new Dictionary<uint, TechInfo>();


        public void loadTechs()
        {
            using (StreamReader file = File.OpenText(Constants.TECHS_PATH))
            {
                var serializer = new JsonSerializer();
                TechInfo[] techsArray = serializer.Deserialize<TechInfo[]>(new JsonTextReader(file));
                techs.Clear();

                foreach (var techItem in techsArray)
                {
                    techs[techItem.Id] = techItem;
                }
            }
        }

        public TechInfo findById(uint id)
            => techs[id];

        public TechInfo findByName(string name)
        {
            foreach (var techItem in techs.Values)
            {
                if (techItem.Name == name)
                    return techItem;
            }
            return null;
        }

        public TechData createTech(TechInfo info, CardData card)
        {
            return new TechData(info, card);
        }
    }
}
