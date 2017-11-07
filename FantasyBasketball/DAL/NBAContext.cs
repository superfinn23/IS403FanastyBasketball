using FantasyBasketball.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FantasyBasketball.DAL
{
    public class NBAContext : DbContext
    {
        public NBAContext() : base("NBAContext")
        {

        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Position> Positions { get; set; }
    }
}