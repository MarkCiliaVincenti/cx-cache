﻿using System.Data.Common;
using EFCoreCache.Interfaces;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EFCoreCache;
public class EFCoreCacheInterceptor : DbCommandInterceptor
{
    private readonly ILockProvider _lockProvider;
    private readonly IDbCommandInterceptorProcessor _processor;

    /// <summary>
    ///     Entity Framework Core Second Level Caching Library
    ///     Please use
    ///     services.AddDbContextPool&lt;ApplicationDbContext&gt;((serviceProvider, optionsBuilder) =&gt;
    ///     optionsBuilder.UseSqlServer(...).AddInterceptors(serviceProvider.GetRequiredService&lt;SecondLevelCacheInterceptor
    ///     &gt;()));
    ///     to register it.
    /// </summary>
    public EFCoreCacheInterceptor(IDbCommandInterceptorProcessor processor, ILockProvider lockProvider)
    {
        _processor = processor;
        _lockProvider = lockProvider;
    }

    /// <summary>
    ///     Called immediately after EF calls System.Data.Common.DbCommand.ExecuteNonQuery
    /// </summary>
    public override int NonQueryExecuted(
        DbCommand command,
        CommandExecutedEventData eventData,
        int result)
    {
        var (shouldSkipProcessing, cachePolicy) = _processor.ShouldSkipProcessing(command, eventData?.Context);
        if (shouldSkipProcessing)
        {
            return result;
        }

        using var @lock = _lockProvider.Lock();
        return _processor.ProcessExecutedCommands(command, eventData?.Context, result, cachePolicy);
    }


    /// <summary>
    ///     Called immediately after EF calls System.Data.Common.DbCommand.ExecuteNonQueryAsync.
    /// </summary>
    //// NET8_0 || NET7_0 || NET6_0 || NET5_0 || NETSTANDARD2_1
    public override async ValueTask<int> NonQueryExecutedAsync(
        DbCommand command,
        CommandExecutedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        var (shouldSkipProcessing, cachePolicy) = _processor.ShouldSkipProcessing(
         command, eventData?.Context, cancellationToken);
        if (shouldSkipProcessing)
        {
            return result;
        }

        using var lockAsync = await _lockProvider.LockAsync(cancellationToken);
        return _processor.ProcessExecutedCommands(command, eventData?.Context, result, cachePolicy);
    }

    /// <summary>
    ///     Called just before EF intends to call System.Data.Common.DbCommand.ExecuteNonQuery.
    /// </summary>
    public override InterceptionResult<int> NonQueryExecuting(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<int> result)
    {
        var (shouldSkipProcessing, cachePolicy) = _processor.ShouldSkipProcessing(command, eventData?.Context);
        if (shouldSkipProcessing)
        {
            return result;
        }

        using var @lock = _lockProvider.Lock();
        return _processor.ProcessExecutingCommands(command, eventData?.Context, result, cachePolicy);
    }

    /// <summary>
    ///     Called just before EF intends to call System.Data.Common.DbCommand.ExecuteNonQueryAsync.
    /// </summary>
    //// NET8_0 || NET7_0 || NET6_0 || NET5_0 || NETSTANDARD2_1
    public override async ValueTask<InterceptionResult<int>> NonQueryExecutingAsync(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var (shouldSkipProcessing, cachePolicy) = _processor.ShouldSkipProcessing(
         command, eventData?.Context, cancellationToken);
        if (shouldSkipProcessing)
        {
            return result;
        }

        using var lockAsync = await _lockProvider.LockAsync(cancellationToken);
        return _processor.ProcessExecutingCommands(command, eventData?.Context, result, cachePolicy);
    }

    /// <summary>
    ///     Called immediately after EF calls System.Data.Common.DbCommand.ExecuteReader.
    /// </summary>
    public override DbDataReader ReaderExecuted(
        DbCommand command,
        CommandExecutedEventData eventData,
        DbDataReader result)
    {
        var (shouldSkipProcessing, cachePolicy) = _processor.ShouldSkipProcessing(command, eventData?.Context);
        if (shouldSkipProcessing)
        {
            return result;
        }

        using var @lock = _lockProvider.Lock();
        return _processor.ProcessExecutedCommands(command, eventData?.Context, result, cachePolicy);
    }

    /// <summary>
    ///     Called immediately after EF calls System.Data.Common.DbCommand.ExecuteReaderAsync.
    /// </summary>
    //// NET8_0 || NET7_0 || NET6_0 || NET5_0 || NETSTANDARD2_1
    public override async ValueTask<DbDataReader> ReaderExecutedAsync(
        DbCommand command,
        CommandExecutedEventData eventData,
        DbDataReader result,
        CancellationToken cancellationToken = default)
    {
        var (shouldSkipProcessing, cachePolicy) = _processor.ShouldSkipProcessing(
         command, eventData?.Context, cancellationToken);
        if (shouldSkipProcessing)
        {
            return result;
        }

        using var lockAsync = await _lockProvider.LockAsync(cancellationToken);
        return _processor.ProcessExecutedCommands(command, eventData?.Context, result, cachePolicy);
    }

    /// <summary>
    ///     Called just before EF intends to call System.Data.Common.DbCommand.ExecuteReader.
    /// </summary>
    public override InterceptionResult<DbDataReader> ReaderExecuting(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<DbDataReader> result)
    {
        var (shouldSkipProcessing, cachePolicy) = _processor.ShouldSkipProcessing(command, eventData?.Context);
        if (shouldSkipProcessing)
        {
            return result;
        }

        using var @lock = _lockProvider.Lock();
        return _processor.ProcessExecutingCommands(command, eventData?.Context, result, cachePolicy);
    }

    /// <summary>
    ///     Called just before EF intends to call System.Data.Common.DbCommand.ExecuteReaderAsync.
    /// </summary>
    //// NET8_0 || NET7_0 || NET6_0 || NET5_0 || NETSTANDARD2_1
    public override async ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<DbDataReader> result,
        CancellationToken cancellationToken = default)
    {
        var (shouldSkipProcessing, cachePolicy) = _processor.ShouldSkipProcessing(
         command, eventData?.Context, cancellationToken);
        if (shouldSkipProcessing)
        {
            return result;
        }

        using var lockAsync = await _lockProvider.LockAsync(cancellationToken);
        return _processor.ProcessExecutingCommands(command, eventData?.Context, result, cachePolicy);
    }

    /// <summary>
    ///     Called immediately after EF calls System.Data.Common.DbCommand.ExecuteScalar.
    /// </summary>
    public override object? ScalarExecuted(
        DbCommand command,
        CommandExecutedEventData eventData,
        object? result)
    {
        var (shouldSkipProcessing, cachePolicy) = _processor.ShouldSkipProcessing(command, eventData?.Context);
        if (shouldSkipProcessing)
        {
            return result;
        }

        using var @lock = _lockProvider.Lock();
        return _processor.ProcessExecutedCommands(command, eventData?.Context, result, cachePolicy);
    }

    /// <summary>
    ///     Called immediately after EF calls System.Data.Common.DbCommand.ExecuteScalarAsync.
    /// </summary>
    //// NET8_0 || NET7_0 || NET6_0 || NET5_0 || NETSTANDARD2_1
    public override async ValueTask<object?> ScalarExecutedAsync(
        DbCommand command,
        CommandExecutedEventData eventData,
        object? result,
        CancellationToken cancellationToken = default)
    {
        var (shouldSkipProcessing, cachePolicy) = _processor.ShouldSkipProcessing(
         command, eventData?.Context, cancellationToken);
        if (shouldSkipProcessing)
        {
            return result;
        }

        using var lockAsync = await _lockProvider.LockAsync(cancellationToken);
        return _processor.ProcessExecutedCommands(command, eventData?.Context, result, cachePolicy);
    }

    /// <summary>
    ///     Called just before EF intends to call System.Data.Common.DbCommand.ExecuteScalar.
    /// </summary>
    public override InterceptionResult<object> ScalarExecuting(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<object> result)
    {
        var (shouldSkipProcessing, cachePolicy) = _processor.ShouldSkipProcessing(command, eventData?.Context);
        if (shouldSkipProcessing)
        {
            return result;
        }

        using var @lock = _lockProvider.Lock();
        return _processor.ProcessExecutingCommands(command, eventData?.Context, result, cachePolicy);
    }

    /// <summary>
    ///     Called just before EF intends to call System.Data.Common.DbCommand.ExecuteScalarAsync.
    /// </summary>
    //// NET8_0 || NET7_0 || NET6_0 || NET5_0 || NETSTANDARD2_1
    public override async ValueTask<InterceptionResult<object>> ScalarExecutingAsync(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<object> result,
        CancellationToken cancellationToken = default)
    {
        var (shouldSkipProcessing, cachePolicy) = _processor.ShouldSkipProcessing(
         command, eventData?.Context, cancellationToken);
        if (shouldSkipProcessing)
        {
            return result;
        }

        using var lockAsync = await _lockProvider.LockAsync(cancellationToken);
        return _processor.ProcessExecutingCommands(command, eventData?.Context, result, cachePolicy);
    }
}
