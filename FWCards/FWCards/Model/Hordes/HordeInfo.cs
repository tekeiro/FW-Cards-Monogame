using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWCards.Model.Enemies;
using Nez;

namespace FWCards.Model.Hordes
{
    public class HordeInfo
    {
        public uint Id { get; set; }
        public uint[] Enemies { get; set; }


        public EnemyInfo GetEnemy(int index)
        {
            var enemyId = Enemies[index];
            var gameDb = Core.services.GetService<GameDB>();

            return gameDb.Enemies.findById(enemyId);
        }

        public override string ToString()
        {
            return $"{nameof(HordeInfo)}={{{nameof(Id)}: {Id}, {nameof(Enemies)}: {Enemies}}}";
        }
    }
}
