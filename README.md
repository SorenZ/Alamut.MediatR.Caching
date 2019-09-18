# MediatR Caching Behavior  
### [Behaviors](https://github.com/jbogard/MediatR/wiki/Behaviors) allow you to build your own pipleline directly inside of MediatR.
A pipeline behavior is an implementation of `IPipelineBehavior<TRequest, TResponse>`. It represents a similar pattern to filters in ASP.NET MVC/Web API or pipeline behaviors in NServiceBus.  

The simplest implementation, that does nothing but call the next possible behavior.
```csharp
public class MyPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next)
    {
        return await next();
    }
}
```

**Chaching Behavior** is an implemented Behavior that Caches your response based on [ICacheable](https://github.com/SorenZ/Alamut.Abstractions/blob/master/src/Alamut.Abstractions/Caching/ICacheable.cs) request.

### Installing Alamut.MediatR.Caching

You should install [Alamut.MediatR.Caching with NuGet](https://www.nuget.org/packages/Alamut.MediatR.Caching):

    Install-Package Alamut.MediatR.Caching
    
Or via the .NET Core command line interface:

    dotnet add package Alamut.MediatR.Caching   


