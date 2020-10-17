using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    class Campus
    {
        private string Code { get; set; }
        private string Name { get; set; }
        private string Description { get; set; }

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
