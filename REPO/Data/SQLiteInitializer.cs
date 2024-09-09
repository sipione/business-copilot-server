using REPO.Data;
public class SQLiteInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        if (context.Database.EnsureCreated())
        {
            Console.WriteLine("Database created");
        }
    }
}
        