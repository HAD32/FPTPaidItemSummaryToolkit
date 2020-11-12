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
        public float UnitValue { get; set; } //=0 if rate by hour
        public int TypeId { get; set; }
        public string AcadLevelCode { get; set; }
        public float Value { get; set; }

        public PaidItem()
        {

        }

        public PaidItem(string id, string name, float rate, float unitValue, int typeId, string acadLevelCode)
        {
            Id = id;
            Name = name;
            Rate = rate;
            UnitValue = unitValue;
            TypeId = typeId;
            AcadLevelCode = acadLevelCode;
        }

        public PaidItem(string id, string name, float rate, float unitValue, int typeId, string acadLevelCode, float value)
        {
            Id = id;
            Name = name;
            Rate = rate;
            UnitValue = unitValue;
            TypeId = typeId;
            AcadLevelCode = acadLevelCode;
            Value = value;
        }
    }
}
