using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

public class UserRepository : EfRepository<User>,IUserRepository
{
    //private readonly MovieShopDbContext _dbContext;
    public UserRepository(MovieShopDbContext dbContext):base(dbContext)
    {
       // _dbContext = dbContext;
    }

    //================================= Get User By Email ===============================//
    public async Task<User> GetUserByEmail(string email)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }

   

}