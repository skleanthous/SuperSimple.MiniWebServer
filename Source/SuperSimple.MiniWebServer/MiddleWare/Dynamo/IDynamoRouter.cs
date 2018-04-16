namespace SuperSimple.MiniWebServer.MiddleWare.Dynamo
{
    using System.Threading.Tasks;
    using Environment = SuperSimple.MiniWebServer.Environment;

    internal interface IDynamoRouter
    {
        IController GetFrom(string route, string method);

        Task Setup(Environment environment);
    }
}
