using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [Serializable]
    public class PaitItemType
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public PaitItemType()
        {
        }

        public PaitItemType(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
