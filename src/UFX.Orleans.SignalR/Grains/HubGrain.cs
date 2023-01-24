﻿using Microsoft.Extensions.Options;
using Orleans.Runtime;

namespace UFX.Orleans.SignalR.Grains;

internal interface IHubGrain : ISignalrGrain
{
    Task SendAllAsync(string methodName, object?[] args);
    Task SendAllExceptAsync(string methodName, object?[] args, IReadOnlyList<string> excludedConnectionIds);
}

internal class HubGrain : SignalrBaseGrain, IHubGrain
{
    public HubGrain(
        [PersistentState(Constants.StateName, Constants.StorageName)] IPersistentState<SubscriptionState> persistedSubs,
        IOptions<SignalrOrleansOptions> options)
        : base(persistedSubs, options)
    {
    }

    public Task SendAllAsync(string methodName, object?[] args) 
        => InformObserversAsync(observer => observer.SendAllAsync(methodName, args));

    public Task SendAllExceptAsync(string methodName, object?[] args, IReadOnlyList<string> excludedConnectionIds) 
        => InformObserversAsync(observer => observer.SendAllExceptAsync(methodName, args, excludedConnectionIds));
}