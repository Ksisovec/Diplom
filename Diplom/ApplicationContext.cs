
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    using Diplom.Models;
    using System.Data.Entity;
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("Diplom") { }

        public DbSet<Departament> Departaments { get; set; }

        public DbSet<Worker> Workers { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractType> ContractTypes { get; set; }
        public DbSet<ArtistCategory> ArtistCategorys { get; set; }

        public DbSet<LessonMarks> LessonMarks { get; set; }
        public DbSet<LessonEvent> LessonEvents { get; set; }


        public DbSet<ConcertMarks> ConcertMarks { get; set; }
        public DbSet<ConcertEvent> ConcertEvents { get; set; }
        public DbSet<ConcertType> ConcertTypes { get; set; }
        public DbSet<ConcertPlaceType> ConcertPlaceTypes { get; set; }


    }
}
