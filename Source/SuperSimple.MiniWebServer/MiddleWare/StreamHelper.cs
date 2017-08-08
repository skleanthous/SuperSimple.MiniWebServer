namespace SuperSimple.MiniWebServer.MiddleWare
{
    using System.IO;
    using System.Threading.Tasks;

    internal static class StreamHelper
    {
        public static async Task<byte[]> ReadAllBytes(this Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                await input.CopyToAsync(ms);

                return ms.ToArray();
            }
        }
    }
}
