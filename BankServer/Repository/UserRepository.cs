using BankServer.Interfaces;
using BankServer.Models;

namespace BankServer.Repository
{
    public class UserRepository : IUserRepository
    {
        //Временно лист
        private List<UserModel> users = new();

        public async Task<bool> Create(UserModel item)
        {
            try
            {
                users.Add(item);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(UserModel item)
        {
            try
            {
                users.Remove(item);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<UserModel?> GetById(ulong id)
        {
            return users.FirstOrDefault(item => item.Id == id, null);
        }

        public async Task<UserModel?> GetByName(string name)
        {
            return users.FirstOrDefault(item => item.Name == name, null);
        }

        public async Task<IEnumerable<UserModel>> Select()
        {
            return users;
        }

        public async Task<UserModel> Update(UserModel item)
        {
            await Delete(await GetById(item.Id));
            await Create(item);
            return item;
        }
    }
}