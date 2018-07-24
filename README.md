# robo-middleware-framework

This project demonstrates when we invoking some method we can pass the method as a parameter to another method.
If we design the application using middleware pipeline. we can success method calls with Separating of Concerns (SoC)
we can catch all methods in middleware pipeline, and we can execute before middleware and after middleware

## Middleware Example:

```csharp

public static void Main(string[] args)
{
    IApplicationBuilder app = Application.Current.appBuilder;

    app.UseMiddleware<LoggerManager>("param1", "param2"); //Example Middleware

    app.UseMiddleware<CacheManager>(); //Example Middleware

    app.Use(next =>
    {
        return async invokeContext =>
        {
            Console.WriteLine("Begin Inline middleware");

            await next(invokeContext);

            Console.WriteLine("End Inline middleware");
        };
    }); //Example inline defined Middleware

    app.UseMiddleware<Invoker>(); //Last Middleware, is used for method invocation

    Application.Current.Build(); //builds middleware invocation order

    //// Example Method Call ////

    PersonService personService = new PersonService();

    var result = personService.Invoke<string>(() => personService.Method1(3));

    Console.ReadKey();
}
```

Example Service class/interface:

```csharp
public interface IPersonService
{
    string Method1(int x);
}

public class PersonService : BaseService, IPersonService
{
    public virtual string Method1(int x)
    {
        return $"input is {x}";
    }
}
```


Example middleware class:

```csharp
public class LoggerManager
{
    private readonly InvokeDelegate _next;

    public LoggerManager(InvokeDelegate next)
    {
        _next = next;
    }

    public LoggerManager(InvokeDelegate next, string param1, string param2)
    {
        _next = next;
    }

    public async Task Invoke(InvokeContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        Console.WriteLine("Begin LoggerManager.");

        await _next.Invoke(context);

        Console.WriteLine("End LoggerManager.");
    }
}

```
