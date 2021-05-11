using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Movies_DAL.DTO.Movies;

namespace Movies_WebAPI.Hubs.App
{
    public interface IAppHub
    {
        IAsyncEnumerable<MovieDTO> GetAllMoviesStreamingWithIAsyncEnumerable(int delay, CancellationToken cancellationToken);
        ChannelReader<MovieDTO> GetAllMoviesStreamingWithChannelReader(int delay, CancellationToken cancellationToken);
        IAsyncEnumerable<MovieDTO> GetPaginatedMoviesStreamingWithIAsyncEnumerable(int delay, int? count, int? before, int? after, CancellationToken cancellationToken);
        ChannelReader<MovieDTO> GetPaginatedMoviesStreamingWithChannelReader(int delay, int? count, int? before, int? after, CancellationToken cancellationToken);
    }
}
