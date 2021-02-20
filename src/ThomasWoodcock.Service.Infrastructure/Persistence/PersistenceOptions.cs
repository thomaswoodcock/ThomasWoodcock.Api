namespace ThomasWoodcock.Service.Infrastructure.Persistence
{
    /// <summary>
    ///     Represents persistence-related application options.
    /// </summary>
    internal sealed class PersistenceOptions
    {
        /// <summary>
        ///     Gets or sets the database name to use when connecting via Cosmos DB.
        /// </summary>
        public string DatabaseName { get; init; }

        /// <summary>
        ///     Gets or sets the value indicating whether a local SQLite database should be used instead of Cosmos DB.
        /// </summary>
        public bool UseSqliteDatabase { get; init; }
    }
}
