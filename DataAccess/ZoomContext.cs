namespace Zoom;

public class ZoomContext : DatabaseContext
{
    public override string ConnectionStringName => "Zoom";

    public DbSet<Zoom.Meeting> Meetings { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
