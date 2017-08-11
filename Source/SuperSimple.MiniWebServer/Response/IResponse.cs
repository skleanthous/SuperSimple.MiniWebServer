namespace SuperSimple.MiniWebServer.Response
{
    using System.Threading.Tasks;

    internal interface IResponse
    {
        Task WriteContentTo(Environment environment);
    }
}
