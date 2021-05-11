﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Movies_DAL.DTO.Movies;
using Movies_DAL.Services.Movies;

namespace Movies_WebAPI.Hubs.App
{
    public class AppHub : Hub, IAppHub
    {
        private readonly IMoviesService moviesService;

        public AppHub(IMoviesService moviesService)
        {
            this.moviesService = moviesService;
        }

        public async IAsyncEnumerable<MovieDTO> GetAllMoviesStreamingWithIAsyncEnumerable(int delay, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await foreach (MovieDTO movie in moviesService.GetAllStreaming())
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return movie;
                await Task.Delay(delay, cancellationToken);
            }
        }

        public ChannelReader<MovieDTO> GetAllMoviesStreamingWithChannelReader(int delay, CancellationToken cancellationToken)
        {
            Channel<MovieDTO> channel = Channel.CreateUnbounded<MovieDTO>();
            _ = WriteItemAsync(channel.Writer, delay, cancellationToken);
            return channel.Reader;
        }

        public async IAsyncEnumerable<MovieDTO> GetPaginatedMoviesStreamingWithIAsyncEnumerable(int delay, int? count, int? before, int? after, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await foreach (MovieDTO movie in moviesService.GetAllStreamingWithPointer(count, before, after))
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return movie;
                await Task.Delay(delay, cancellationToken);
            }
        }

        public ChannelReader<MovieDTO> GetPaginatedMoviesStreamingWithChannelReader(int delay, int? count, int? before, int? after, CancellationToken cancellationToken)
        {
            Channel<MovieDTO> channel = Channel.CreateUnbounded<MovieDTO>();
            _ = WritePaginatedItemAsync(channel.Writer, delay, count, before, after, cancellationToken);
            return channel.Reader;
        }

        private async Task WriteItemAsync(ChannelWriter<MovieDTO> writer, int delay, CancellationToken cancellationToken)
        {
            Exception localException = null;
            try
            {
                await foreach (MovieDTO movie in moviesService.GetAllStreaming())
                {
                    await writer.WriteAsync(movie, cancellationToken);
                    await Task.Delay(delay, cancellationToken);
                }
            }
            catch (Exception exc)
            {
                localException = exc;
            }
            finally
            {
                writer.Complete(localException);
            }
        }

        private async Task WritePaginatedItemAsync(ChannelWriter<MovieDTO> writer, int delay, int? count, int? before, int? after, CancellationToken cancellationToken)
        {
            Exception localException = null;
            try
            {
                await foreach (MovieDTO movie in moviesService.GetAllStreamingWithPointer(count, before, after))
                {
                    await writer.WriteAsync(movie, cancellationToken);
                    await Task.Delay(delay, cancellationToken);
                }
            }
            catch (Exception exc)
            {
                localException = exc;
            }
            finally
            {
                writer.Complete(localException);
            }
        }
    }
}
