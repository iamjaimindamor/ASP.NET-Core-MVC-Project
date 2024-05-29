using Microsoft.EntityFrameworkCore;

namespace ASPForm.Models
{
    public class AbhaDBContext : DbContext
    {
        public AbhaDBContext(DbContextOptions options) : base(options)
        {
                
        }

        public DbSet<ABHAModel> abhaTab { get; set; }
    }
}
