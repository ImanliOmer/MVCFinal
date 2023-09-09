using Common.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Slider> Sliders { get; set;}
        public DbSet<OurVision> OurVisions { get; set;}
        public DbSet<OurVisionGoal> OurVisionGoal { get; set;}
        public DbSet<AboutUsImages> AboutUsImages { get; set;}
        public DbSet<AboutUsViewComponent> AboutUsViewComponent { get; set;}
        public DbSet<WhatWeDo> WhatWeDo { get; set;}
        public DbSet<WhatWeDoComponent> WhatWeDoComponent { get;set;}


    }
}
