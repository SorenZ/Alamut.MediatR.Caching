# MediatR Caching Behavior  
### [Behaviors](https://github.com/jbogard/MediatR/wiki/Behaviors) allow you to build your own pipeline directly inside of MediatR.
A pipeline behavior is an implementation of `IPipelineBehavior<TRequest, TResponse>`. It represents a similar pattern to filters in ASP.NET MVC/Web API or pipeline behaviors in NServiceBus.  

The simplest implementation that does nothing but calls the next possible behavior.
```csharp
public class MyPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next)
    {
        return await next();
    }
}
```

**Caching Behavior** is an implemented Behavior that Caches your response based on [ICacheable](https://github.com/SorenZ/Alamut.Abstractions/blob/master/src/Alamut.Abstractions/Caching/ICacheable.cs) request.

### Registering Caching Behavior 
You should register caching-behavior to cache your Request. 
If you're using ASP.NET Code DI you can install [Alamut.MediatR.Caching.DependencyInjection](https://www.nuget.org/packages/Alamut.MediatR.Caching.DependencyInjection/) package and register it by calling `services.AddCachingBehavior();` (take a look at [Startup.cs](https://github.com/SorenZ/Alamut.MediatR.Caching/blob/master/sample/Alamut.MediatR.Caching.SampleApi/Startup.cs) for more info).  
Alternatively you can install [Alamut.MediatR.Caching](https://www.nuget.org/packages/Alamut.MediatR.Caching/) and register the behavior `services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));`

### Setting up Caching
Any Request(query) that Implement [ICacheable](https://github.com/SorenZ/Alamut.Abstractions/blob/master/src/Alamut.Abstractions/Caching/ICacheable.cs) (its a part of [Alamut.Abstraction](https://github.com/SorenZ/Alamut.Abstractions) package) are eligibel to be cached.
```csharp 
public class GetFooByIdQuery : IRequest<FooModel>, ICacheable
    {
        public GetFooByIdQuery(int id)
        {
            Id = id;
            Key = $"Foo_{id}";
            Options = new ExpirationOptions(TimeSpan.FromSeconds(60));
        }

        public int Id { get;  }
        public string Key { get; }
        public ExpirationOptions Options { get; }
    }
```
By implementing ICacheable you should provide a (unique) key for the cache object and [ExpirationOptions](https://github.com/SorenZ/Alamut.Abstractions/blob/master/src/Alamut.Abstractions/Caching/ExpirationOptions.cs). 

That's It! It couldn't be any easier.   
It's highly recommended to study the ASP.NET Web API [sample](https://github.com/SorenZ/Alamut.MediatR.Caching/tree/master/sample/Alamut.MediatR.Caching.SampleApi)

### Acknowledgements
Alamut.MediatR.Caching is built using the following great open source projects and free services:

* [MediatR](https://github.com/jbogard/MediatR)
* [MessagePack](https://github.com/neuecc/MessagePack-CSharp)
* [Alamut.Extensions.Caching](https://www.nuget.org/packages/Alamut.Extensions.Caching/)
