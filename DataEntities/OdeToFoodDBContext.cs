using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Data_Entities
{
    public class OdeToFoodDBContext:DbContext
    {
        public OdeToFoodDBContext(DbContextOptions<OdeToFoodDBContext> options):base(options)
        {
            

        }
       public DbSet<Resturant> Resturants { get; set; }

    }
}
