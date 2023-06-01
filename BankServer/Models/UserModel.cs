using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Models
{
    [Serializable]
    public class UserModel : BaseModel
    {
        //Поля
        public string Name { get; set; }
        public string Password { get; set; }

        //Методы
        public UserModel()
        {
            Name = "None";
            Password = "None";
        }

        public UserModel(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public override bool Equals(object? obj)
        {
            if (obj is UserModel userModel)
            {
                return Name == userModel.Name;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}