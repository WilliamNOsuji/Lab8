using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lab8.Models;

namespace Lab8.Data
{
    public class Lab8Context : DbContext
    {
        public Lab8Context (DbContextOptions<Lab8Context> options)
            : base(options)
        {
        }

        public DbSet<Lab8.Models.Animal> Animal { get; set; } = default!;
    }
}
