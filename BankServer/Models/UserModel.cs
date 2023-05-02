using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Models
{
    public class UserModel : BaseModel
    {
        public UserModel()
        {
            Id = 0;
            Name = "None";
            Password = "None";
        }

        public UserModel(decimal id, string name, string password)
        {
            Id = id;
            Name = name;
            Password = password;
        }

        public string Name { get; set; }
        public string Password { get; set; }
    }
}
