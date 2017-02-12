using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWCards.Model.Equipment
{
    public class EquipmentData
    {
        public EquipmentData(EquipmentInfo info)
        {
            Info = info;
        }

        public EquipmentInfo Info { get; private set; }

    }
}
