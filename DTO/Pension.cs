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
        public string PensionFormula { get; set; }

        public Pension()
        {
        }

        public Pension(string pensionName, float pensionValue)
        {
            PensionName = pensionName;
            PensionValue = pensionValue;
        }

        public Pension(string pensionName, float pensionValue, string pensionFormula) : this(pensionName, pensionValue)
        {
            PensionFormula = pensionFormula;
        }
    }
}
