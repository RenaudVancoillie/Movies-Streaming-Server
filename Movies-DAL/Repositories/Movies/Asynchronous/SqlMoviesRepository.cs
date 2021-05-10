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

        public async IAsyncEnumerable<MovieDTO> GetAllStreaming()
        {
            DbConnection dbConnection = CreateDbConnection();
            DbCommand dbCommand = null;
            DbDataReader dbDataReader = null;
            try
            {
                await dbConnection.OpenAsync().ConfigureAwait(false);
                dbCommand = CreateCommandForGetAll(dbConnection);
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

        private static DbCommand CreateCommandForGetAll(DbConnection dbConnection)
        {
            DbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = "SELECT * FROM movies";
            dbCommand.CommandType = CommandType.Text;
            return dbCommand;
        }
    }
}
