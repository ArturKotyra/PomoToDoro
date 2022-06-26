using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PomoToDoro.Models;

namespace PomoToDoro.Models
{
    public class PomoToDoroContext : DbContext
    {
        public PomoToDoroContext(DbContextOptions<PomoToDoroContext> options) : base(options)
        {
        }

        public DbSet<TaskModel> Tasks { get; set; }
    }
}