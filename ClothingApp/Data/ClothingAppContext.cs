using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ClothingApp.Data;

namespace ClothingApp.Data
{
    public class ClothingAppContext(DbContextOptions<ClothingAppContext> options) : IdentityDbContext<ClothingAppUser>(options)
    {
        public DbSet<ClothingApp.Models.ClothingItem> ClothingItem { get; set; } = default!;

    }
}
