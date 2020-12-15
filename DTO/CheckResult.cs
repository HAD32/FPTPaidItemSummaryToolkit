using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CheckResult
    {
        public string Name { get; set; }
        public float Maximum { get; set; }
        public float Minimum { get; set; }

        public CheckResult()
        {
        }

        public CheckResult(string name, float maximum, float minimum)
        {
            Name = name;
            Maximum = maximum;
            Minimum = minimum;
        }
    }
}
