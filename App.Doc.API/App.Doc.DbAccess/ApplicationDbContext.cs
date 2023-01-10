using App.Doc.DbAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Doc.DbAccess
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ChannelCenter> ChannelCenter { get; set; }
    }
}