using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [Serializable]
    public class PaidItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public float Rate { get; set; }
        public float UnitValue { get; set; }
        public int TypeId { get; set; }
        public string AcaLevelCode { get; set; }

        public PaidItem()
        {

        }

        public PaidItem(string id, string name, float rate, float unitValue, int typeId, string acaLevelCode)
        {
            Id = id;
            Name = name;
            Rate = rate;
            UnitValue = unitValue;
            TypeId = typeId;
            AcaLevelCode = acaLevelCode;
        }
    }
}
