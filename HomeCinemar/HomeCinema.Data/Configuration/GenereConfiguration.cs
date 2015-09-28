using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCinema.Entities;

namespace HomeCinema.Data.Configuration
{
    public class GenereConfiguration : EntityBaseConfiguration<Genre>
    {
        public GenereConfiguration()
        {
            Property(g => g.Name).IsRequired().HasMaxLength(50);
        }
    }
}
