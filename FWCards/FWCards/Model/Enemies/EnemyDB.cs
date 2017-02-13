using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWCards.Config;
using FWCards.Model.Equipment;
using Newtonsoft.Json;

namespace FWCards.Model.Enemies
{
    public class EnemyDB
    {
        private Dictionary<uint, EnemyInfo> enemies = new Dictionary<uint, EnemyInfo>();


        public void loadEnemies()
        {
            using (StreamReader file = File.OpenText(Constants.ENEMIES_PAH))
            {
                var serializer = new JsonSerializer();
                EnemyInfo[] enemiesArray = serializer.Deserialize<EnemyInfo[]>(new JsonTextReader(file));
                enemies.Clear();

                foreach (var enemItem in enemiesArray)
                {
                    enemies[enemItem.Id] = enemItem;
                }
            }
        }

        public EnemyInfo findById(uint id)
            => enemies[id];

        public EnemyInfo findByName(string name)
        {
            foreach (var enemItem in enemies.Values)
            {
                if (enemItem.Name == name)
                    return enemItem;
            }
            return null;
        }

        public EnemyData createEnemy(EnemyInfo info)
        {
            return new EnemyData(info);
        }
    }
}
