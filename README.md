# Chaos Tesing with .Net/C#
Some starter code for Chaos Testing .Net Microservices. The best way to get started is to just copy the classes into your own code and tweak them as needed.

The code sample complies with NetStandard 1.4, so it should run in just about any project.

## ChaosDelegatingHandler

This is a DelegatingHandler you can use with HttpClient or server-side to introduce failures either when calling
another HTTP service (HttpClient) or at your own service boundary.

### Client
``` csharp
var client = new HttpClient(new ChaosDelegatingHandler() { InnerHandler = new HttpClientHandler()});
```

### Server
``` csharp
config.MessageHandlers.Add(new ChaosDelegatingHandler());
```

## ChaosConfig
A simple static config class to enable/disable the handler.
The default is false.

``` csharp
ChaosConfig.EnableChaos = true;
``` 