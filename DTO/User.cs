using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [Serializable]
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Key { get; set; }
        public string Campus { get; set; }
        public User()
        {
        }

        public User(string id, string name, string email, string key, string campus)
        {
            Id = id;
            Name = name;
            Email = email;
            Key = key;
            Campus = campus;
        }
    }
}
