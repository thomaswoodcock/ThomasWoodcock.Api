using System;
using System.Data.Common;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ThomasWoodcock.Service.Infrastructure.UnitTests.Persistence
{
    /// <inheritdoc />
    /// <summary>
    ///     Allows a class to interact with an Entity Framework SQLite in-memory database.
    /// </summary>
    /// <typeparam name="T">
    ///     The <see cref="DbContext" /> type with which to initialize the database.
    /// </typeparam>
    public abstract class EfInMemoryDatabaseFixture<T> : IDisposable
        where T : DbContext
    {
        private readonly DbConnection _connection;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EfInMemoryDatabaseFixture{T}" /> class.
        /// </summary>
        protected EfInMemoryDatabaseFixture()
        {
            this._connection = new SqliteConnection("Filename=:memory:");
            this._connection.Open();

            this.ContextOptions = new DbContextOptionsBuilder<T>().UseSqlite(this._connection)
                .Options;
        }

        /// <summary>
        ///     Gets the options for the database context.
        /// </summary>
        internal DbContextOptions<T> ContextOptions { get; }

        /// <inheritdoc />
        public void Dispose()
        {
            this._connection.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
