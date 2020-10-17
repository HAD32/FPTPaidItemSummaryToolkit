using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class StaffType
    {
        private string Id { get; set; }
        private string Name { get; set; }

        public StaffType()
        {
        }

        public StaffType(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
