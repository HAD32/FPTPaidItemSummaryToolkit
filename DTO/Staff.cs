using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [Serializable]
    public class Staff
    {
        public string Account { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Email { get; set; }
        public string Major { get; set; }
        public string Contact { get; set; }
        public string HighestAcademicLevel { get; set; }

        public Staff()
        {
        }

        public Staff(string account, string id, string name, string typeId, string email, string major, string contact, string highestAcademicLevel)
        {
            Account = account;
            Id = id;
            Name = name;
            Type = typeId;
            Email = email;
            Major = major;
            Contact = contact;
            HighestAcademicLevel = highestAcademicLevel;
        }
    }
}
