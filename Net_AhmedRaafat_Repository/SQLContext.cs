using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Net_AhmedRaafat_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Net_AhmedRaafat_Repository
{
    public class SQLContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public SQLContext(DbContextOptions<SQLContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
        }


        public DbSet<PersonalDiary> PersonalDiaries { get; set; }
        public DbSet<ToDo> ToDo { get; set; }

    }

}
