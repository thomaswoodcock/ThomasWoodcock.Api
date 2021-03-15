namespace ThomasWoodcock.Service.Infrastructure.Persistence
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents persistence-related application options.
    /// </summary>
    internal sealed record PersistenceOptions
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
