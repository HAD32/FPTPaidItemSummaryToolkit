using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class LecturerType
    {
        private string Id { get; set; }
        private string Name { get; set; }

        public LecturerType()
        {
        }

        public LecturerType(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
