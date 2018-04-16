namespace SuperSimple.MiniWebServer
{
    using System.Threading.Tasks;

    public interface IMiddleware
    {
        //TODO: Revisit middleware chain mechanist (currently from return value)
        Task<MiddlewareInvocationEnum> Invoke(Environment environment);
    }
}
