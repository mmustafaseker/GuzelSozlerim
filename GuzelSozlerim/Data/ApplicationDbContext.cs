using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuzelSozlerim.Data
{
    public class ApplicationDbContext : IdentityDbContext<Kullanici>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //çoka çok ilişki oluşturma
            builder.Entity<KullaniciSoz>().HasKey(x => new { x.GuzelSozId, x.KullaniciId });
            base.OnModelCreating(builder);
        }
        public DbSet<KullaniciSoz> KullaniciSozler{ get; set; }
        public DbSet<GuzelSoz> GuzelSozler { get; set; }
    }
}
