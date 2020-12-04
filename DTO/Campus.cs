using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [Serializable]
    public class Campus
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Campus()
        {
        }

        public Campus(string code, string name, string description)
        {
            Code = code;
            Name = name;
            Description = description;
        }
    }
}
