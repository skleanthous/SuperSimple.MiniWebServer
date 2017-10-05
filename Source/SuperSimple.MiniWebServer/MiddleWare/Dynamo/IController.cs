namespace SuperSimple.MiniWebServer.MiddleWare.Dynamo
{
    using System.Threading.Tasks;
    using Environment = SuperSimple.MiniWebServer.Environment;

    internal interface IController
    {
        Task Handle(Environment environment);
    }
}
