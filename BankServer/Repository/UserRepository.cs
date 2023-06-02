using BankServer.DataBase;
using BankServer.Interfaces;
using BankServer.Models;
using Microsoft.EntityFrameworkCore;

namespace BankServer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext dbContext;
        public UserRepository(AppDbContext DbContext)
        {
            dbContext = DbContext;

            dbContext.Database.EnsureCreated();
            dbContext.Users.Load();
        }

        public async Task<bool> Create(UserModel item)
        {
            try
            {
                await dbContext.Users.AddAsync(item);
                await dbContext.SaveChangesAsync();

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
                dbContext.Users.Remove(item);
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<UserModel?> GetById(ulong id)
        {
            return await dbContext.Users.FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<UserModel?> GetByName(string name)
        {
            return await dbContext.Users.FirstOrDefaultAsync(item => item.Name == name);
        }

        public async Task<UserModel> Normalization(UserModel item)
        {
           var dbUserModel = await dbContext.Users.FirstOrDefaultAsync(dbItem => dbItem.Equals(item));

            if (dbUserModel is not null)
                return dbUserModel;

            return item;
        }

        public async Task<IEnumerable<UserModel>> Select()
        {
            return await dbContext.Users.ToListAsync();
        }

        public async Task<UserModel> Update(UserModel item)
        {
            dbContext.Users.Update(item);
            await dbContext.SaveChangesAsync();

            return item;
        }
    }
}