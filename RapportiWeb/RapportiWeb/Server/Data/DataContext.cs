using Microsoft.EntityFrameworkCore;
using RapportiWeb.Shared;
using System.Collections.Generic;

namespace RapportiWeb.Server.Data
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration _conf;

        public DataContext(IConfiguration configuration, DbContextOptions<DataContext> options) : base(options)
        {
            _conf = configuration; //leggiamo la string di connessione al DB

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_conf.GetConnectionString("DataBase")); //ci colleghiamo al DB Sqlite
        }

        public DbSet<Cliente> Clienti { get; set; } //DBSet che contiene tutti i clienti
        public DbSet<Richiesta> Richieste { get; set; } //DBSet che contiene tutti le richieste
        public DbSet<Rapporto> Rapporti { get; set; } //DBSet che contiene tutti i rapporti
        public DbSet<Fad> Fad { get; set; } //DBSet che contiene tutte le Formazioni A Distanza [FAD]
        public DbSet<User> Utenti { get; set; } //DBSet che contiene tutti i utenti
    }
}
