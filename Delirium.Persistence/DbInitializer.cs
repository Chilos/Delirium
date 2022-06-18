namespace Delirium.Persistence;

public sealed class DbInitializer
{
    public static void Initialize(DeliriumDbContext context)
    {
        context.Database.EnsureCreated();
    }
}