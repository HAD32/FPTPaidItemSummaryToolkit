using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [Serializable]
    public class Pension
    {
        public string PensionName { get; set; }
        public float PensionValue { get; set; }

        public Pension()
        {
        }

        public Pension(string pensionName, float pensionValue)
        {
            PensionName = pensionName;
            PensionValue = pensionValue;
        }
    }
}
