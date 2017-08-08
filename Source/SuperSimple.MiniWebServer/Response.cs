namespace SuperSimple.MiniWebServer
{
    using System;
    using System.Text;
    using Newtonsoft.Json;

    public class Response
    {
        private string content;
        private byte[] contentBytes;

        public string StatusCode { get; }

        internal byte[] ContentBytes => contentBytes;
        internal Encoding ContentEncoding => Encoding.UTF8;
        public string Content
        {
            get => content;
            protected set
            {
                content = value;
                contentBytes = ContentEncoding.GetBytes(content);
            }
        }
        public string ContentType { get; protected set; }

        public Response(string content, string contentType, string statusCode = "200")
        {
            StatusCode = statusCode;
            Content = content ?? throw new ArgumentNullException(nameof(content));
            ContentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
        }

        public Response(object responseObject, string contentType = "application/json", string statusCode = "200")
        {
            StatusCode = statusCode;
            Content = JsonConvert.SerializeObject(responseObject);
            ContentType = contentType;
        }
    }
}
