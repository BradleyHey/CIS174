using Microsoft.EntityFrameworkCore;

namespace FirstResponsiveWebAppHey.Models.Olympics
{
    public class OlympicsContext : DbContext
    {
        public OlympicsContext(DbContextOptions<OlympicsContext> options)
            : base(options)
        { }

        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Game>().HasData(
                new Game { GameID = "summer", Name = "Summer Olympics" },
                new Game { GameID = "winter", Name = "Winter Olympics" },
                new Game { GameID = "paralympics", Name = "Paralympics" },
                new Game { GameID = "youth", Name = "Youth Olympic Games" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryID = "indoor", Name = "Indoor" },
                new Category { CategoryID = "outdoor", Name = "Outdoor" }
            );

            modelBuilder.Entity<Country>().HasData(
                new { CountryID = "can", Name = "Canada", GameID = "winter", CategoryID = "indoor", Sport = "Curling", LogoImage = "can.png" },
                new { CountryID = "swe", Name = "Sweden", GameID = "winter", CategoryID = "indoor", Sport = "Curling", LogoImage = "swe.png" },
                new { CountryID = "gbr", Name = "Great Britain", GameID = "winter", CategoryID = "indoor", Sport = "Curling", LogoImage = "gbr.png" },
                new { CountryID = "jam", Name = "Jamaica", GameID = "winter", CategoryID = "outdoor", Sport = "Bobsleigh", LogoImage = "jam.png" },
                new { CountryID = "ita", Name = "Italy", GameID = "winter", CategoryID = "outdoor", Sport = "Bobsleigh", LogoImage = "ita.png" },
                new { CountryID = "jpn", Name = "Japan", GameID = "winter", CategoryID = "outdoor", Sport = "Bobsleigh", LogoImage = "jpn.png" },
                new { CountryID = "ger", Name = "Germany", GameID = "summer", CategoryID = "indoor", Sport = "Diving", LogoImage = "ger.png" },
                new { CountryID = "chn", Name = "China", GameID = "summer", CategoryID = "indoor", Sport = "Diving", LogoImage = "chn.png" },
                new { CountryID = "mex", Name = "Mexico", GameID = "summer", CategoryID = "indoor", Sport = "Diving", LogoImage = "mex.png" },
                new { CountryID = "bra", Name = "Brazil", GameID = "summer", CategoryID = "outdoor", Sport = "Road Cycling", LogoImage = "bra.png" },
                new { CountryID = "ned", Name = "Netherlands", GameID = "summer", CategoryID = "outdoor", Sport = "Cycling", LogoImage = "ned.png" },
                new { CountryID = "usa", Name = "USA", GameID = "summer", CategoryID = "outdoor", Sport = "Road Cycling", LogoImage = "usa.png" },
                new { CountryID = "tha", Name = "Thailand", GameID = "paralympics", CategoryID = "indoor", Sport = "Archery", LogoImage = "tha.png" },
                new { CountryID = "ury", Name = "Uruguay", GameID = "paralympics", CategoryID = "indoor", Sport = "Archery", LogoImage = "ury.png" },
                new { CountryID = "ukr", Name = "Ukraine", GameID = "paralympics", CategoryID = "indoor", Sport = "Archery", LogoImage = "ukr.png" },
                new { CountryID = "aut", Name = "Austria", GameID = "paralympics", CategoryID = "outdoor", Sport = "Canoe Sprint", LogoImage = "aut.png" },
                new { CountryID = "pak", Name = "Pakistan", GameID = "paralympics", CategoryID = "outdoor", Sport = "Canoe Sprint", LogoImage = "pak.png" },
                new { CountryID = "zwe", Name = "Zimbabwe", GameID = "paralympics", CategoryID = "outdoor", Sport = "Canoe Sprint", LogoImage = "zwe.png" },
                new { CountryID = "fra", Name = "France", GameID = "youth", CategoryID = "indoor", Sport = "Breakdancing", LogoImage = "fra.png" },
                new { CountryID = "cyp", Name = "Cyprus", GameID = "youth", CategoryID = "indoor", Sport = "Breakdancing", LogoImage = "cyp.png" },
                new { CountryID = "rus", Name = "Russia", GameID = "youth", CategoryID = "indoor", Sport = "Breakdancing", LogoImage = "rus.png" },
                new { CountryID = "fin", Name = "Finland", GameID = "youth", CategoryID = "outdoor", Sport = "Skateboarding", LogoImage = "fin.png" },
                new { CountryID = "svk", Name = "Slovakia", GameID = "youth", CategoryID = "outdoor", Sport = "Skateboarding", LogoImage = "svk.png" },
                new { CountryID = "prt", Name = "Portugal", GameID = "youth", CategoryID = "outdoor", Sport = "Skateboarding", LogoImage = "prt.png" }
            );
        }
    }
}