﻿using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppWithDocker.DbContext
{ 
        public class AppDBContext : IdentityDbContext
        {
            private readonly DbContextOptions _options;

            public AppDBContext(DbContextOptions options) : base(options)
            {
                _options = options;
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

            }
        }
}
