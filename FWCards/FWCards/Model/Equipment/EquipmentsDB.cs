using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWCards.Config;
using FWCards.Model.Techs;
using Newtonsoft.Json;

namespace FWCards.Model.Equipment
{
    public class EquipmentsDB
    {
        private Dictionary<uint, EquipmentInfo> equips = new Dictionary<uint, EquipmentInfo>();


        public void loadEquipments()
        {
            using (StreamReader file = File.OpenText(Constants.EQUIPS_PATH))
            {
                var serializer = new JsonSerializer();
                EquipmentInfo[] equipsArray = serializer.Deserialize<EquipmentInfo[]>(new JsonTextReader(file));
                equips.Clear();

                foreach (var equipItem in equipsArray)
                {
                    equips[equipItem.Id] = equipItem;
                }
            }
        }


        public EquipmentInfo findById(uint id)
            => equips[id];

        public EquipmentInfo findByName(string name)
        {
            foreach (var equipItem in equips.Values)
            {
                if (equipItem.Name == name)
                    return equipItem;
            }
            return null;
        }

        public EquipmentData CreateEquipment(EquipmentInfo info)
        {
            return new EquipmentData(info);
        }
    }
}
