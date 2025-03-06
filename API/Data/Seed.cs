using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Seed
{
    public static async Task SeedUser(DataContext context){
        if(await context.Users.AnyAsync()) return;
        var userdata=await File.ReadAllTextAsync("Data/UserSeedData.json");
        var option= new JsonSerializerOptions{PropertyNameCaseInsensitive=true};
        var users=JsonSerializer.Deserialize<List<AppUser>>(userdata, option);
        if(users == null)return;
        foreach(var user in users){
            using var hmc=new HMACSHA512();
            user.UserName=user.UserName.ToLower();
            user.PasswordHash=hmc.ComputeHash(Encoding.UTF8.GetBytes("pa$$w0rd"));
            user.PasswordSalt=hmc.Key;
            context.Users.Add(user);
        }
        await context.SaveChangesAsync();
    }

}
