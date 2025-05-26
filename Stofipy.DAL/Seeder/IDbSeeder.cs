namespace Stofipy.DAL.Seeds;

public interface IDbSeeder
{
    public void Seed();
    public Task SeedAsync(CancellationToken cancellationToken);
}
