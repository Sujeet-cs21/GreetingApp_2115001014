using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Context
{
    public class GreetingContext : DbContext
    {
        public GreetingContext(DbContextOptions<GreetingContext> options)
                    : base(options)
        {
        }
        public virtual DbSet<Entity.GreetingEntity> Greetings { get; set; }
    }
}
