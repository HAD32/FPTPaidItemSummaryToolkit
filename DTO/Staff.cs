using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Staff
    {
        private string Id { get; set; }
        private string Name { get; set; }
        private string TypeId { get; set; }
        private string Email { get; set; }
        private string Major { get; set; }

        public Staff()
        {
        }

        public Staff(string id, string name, string typeId, string email, string major)
        {
            Id = id;
            Name = name;
            TypeId = typeId;
            Email = email;
            Major = major;
        }
    }
}
