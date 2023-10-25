namespace Nover.DB;

public static class DBDbProperties
{
    public static string DbTablePrefix { get; set; } = "DB";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "DB";
}
