using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Movies_DAL.DTO.Movies;
using Movies_DAL.Models;

namespace Movies_DAL.Repositories.Movies.Asynchronous
{
    public sealed class SqlMoviesRepository : IAsynchronousMoviesRepository
    {
        private static readonly SqlClientFactory sqlClientFactory = SqlClientFactory.Instance;

        private static DbConnection CreateDbConnection()
        {
            DbConnection dbConnection = sqlClientFactory.CreateConnection();
            dbConnection.ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=Movies;Integrated Security=True";
            return dbConnection;
        }

        public async Task<IEnumerable<Movie>> GetAll()
        {
            DbConnection dbConnection = CreateDbConnection();
            DbCommand dbCommand = null;
            try
            {
                await dbConnection.OpenAsync().ConfigureAwait(false);
                dbCommand = CreateCommandForGetAll(dbConnection);
                DbDataReader dbDataReader = await dbCommand.ExecuteReaderAsync().ConfigureAwait(false);
                return MapToMovies(dbDataReader);
            }
            finally
            {
                dbCommand?.Dispose();
                dbConnection.Dispose();
            }
        }

        private static IEnumerable<Movie> MapToMovies(DbDataReader dbDataReader)
        {
            IList<Movie> movies = new List<Movie>();

            while (dbDataReader.Read())
            {
                movies.Add(new Movie() { 
                    Id = (long) dbDataReader[0],
                    ImdbId = (string) dbDataReader[1],
                    Title = (string) dbDataReader[2],
                    CoverUrl = (string) dbDataReader[3],
                    Year = (int) dbDataReader[4],
                    OriginalAirDate = (string) dbDataReader[5],
                    Kind = (string) dbDataReader[6],
                    Rating = (decimal) dbDataReader[7],
                    Plot = (string) dbDataReader[8],
                    Top250Rank = (int) dbDataReader[9]
                });
            }

            return movies;
        }

        public IAsyncEnumerable<MovieDTO> GetAllStreaming()
        {
            DbConnection dbConnection = CreateDbConnection();
            DbCommand dbCommand = CreateCommandForGetAll(dbConnection);
            return GetStreaming(dbConnection, dbCommand);
        }

        public IAsyncEnumerable<MovieDTO> GetFirstMoviesStreaming(int count)
        {
            DbConnection dbConnection = CreateDbConnection();
            DbCommand dbCommand = CreateCommandForGetFirstCount(dbConnection, count);
            return GetStreaming(dbConnection, dbCommand);
        }

        public IAsyncEnumerable<MovieDTO> GetMoviesBeforeStreaming(int count, int before)
        {
            DbConnection dbConnection = CreateDbConnection();
            DbCommand dbCommand = CreateCommandForGetBefore(dbConnection, count, before);
            return GetStreaming(dbConnection, dbCommand);
        }

        public IAsyncEnumerable<MovieDTO> GetMoviesAfterStreaming(int count, int after)
        {
            DbConnection dbConnection = CreateDbConnection();
            DbCommand dbCommand = CreateCommandForGetAfter(dbConnection, count, after);
            return GetStreaming(dbConnection, dbCommand);
        }

        private static async IAsyncEnumerable<MovieDTO> GetStreaming(DbConnection dbConnection, DbCommand dbCommand)
        {
            DbDataReader dbDataReader = null;
            try
            {
                await dbConnection.OpenAsync().ConfigureAwait(false);
                dbDataReader = await dbCommand.ExecuteReaderAsync().ConfigureAwait(false);

                MovieDTO movie = new();
                while (dbDataReader.Read())
                {
                    movie.Id = (long)dbDataReader[0];
                    movie.ImdbId = (string)dbDataReader[1];
                    movie.Title = (string)dbDataReader[2];
                    movie.CoverUrl = (string)dbDataReader[3];
                    movie.Year = (int)dbDataReader[4];
                    movie.OriginalAirDate = (string)dbDataReader[5];
                    movie.Kind = (string)dbDataReader[6];
                    movie.Rating = (decimal)dbDataReader[7];
                    movie.Plot = (string)(dbDataReader[8] == DBNull.Value ? string.Empty : dbDataReader[8]);
                    movie.Top250Rank = (int)dbDataReader[9];
                    yield return movie;
                }
            }
            finally
            {
                dbDataReader?.Dispose();
                dbCommand?.Dispose();
                dbConnection.Dispose();
            }
        }

        private static DbCommand CreateCommand(DbConnection dbConnection, string query)
        {
            DbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandType = CommandType.Text;
            dbCommand.CommandText = query;
            return dbCommand;
        }

        private static DbCommand CreateCommandForGetAll(DbConnection dbConnection)
        {
            string query = "SELECT * FROM movies";
            return CreateCommand(dbConnection, query);
        }

        private static DbCommand CreateCommandForGetFirstCount(DbConnection dbConnection, int count)
        {
            string query = $"SELECT TOP({count}) * FROM movies ORDER BY top_250_rank ASC";
            return CreateCommand(dbConnection, query);
        }

        private static DbCommand CreateCommandForGetBefore(DbConnection dbConnection, int count, int before)
        {
            string query = $"SELECT TOP({count}) * FROM movies WHERE top_250_rank < {before} ORDER BY top_250_rank DESC";
            return CreateCommand(dbConnection, query);
        }

        private static DbCommand CreateCommandForGetAfter(DbConnection dbConnection, int count, int after)
        {
            string query = $"SELECT TOP({count}) * FROM movies WHERE top_250_rank > {after} ORDER BY top_250_rank ASC";
            return CreateCommand(dbConnection, query);
        }
    }
}
