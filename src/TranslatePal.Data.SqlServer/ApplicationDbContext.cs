﻿using Microsoft.Data.Entity;

namespace TranslatePal.Data.SqlServer
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
