namespace SuperSimple.MiniWebServer
{
    using System;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public class Request : IEquatable<Request>
    {
        public string RequestMethod { get; }
        public string RequestPath { get; }
        public string RequestPathBase { get; }
        public string RequestQueryString { get; }
        public string Content { get; }

        public Request(string method, string path, string pathBase = null, string queryString = null, string content = null)
        {
            RequestMethod = method;
            RequestPath = path;
            RequestPathBase = pathBase;
            RequestQueryString = queryString;
            Content = content;
        }

        public T GetContentAs<T>() => JsonConvert.DeserializeObject<T>(Content);

        public static async Task<Request> FromEnvironment(Environment env)
            => new Request(env.RequestMethod, env.RequestPath, env.RequestPathBase, env.RequestQueryString, env.RequestContent);

        public static bool operator !=(Request left, Request right) => !(left == right);
        public static bool operator ==(Request left, Request right)
        {
            if ((object)left != null) return left.Equals(right);

            return right == null;
        }


        public bool Equals(Request other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(RequestMethod, other.RequestMethod, StringComparison.OrdinalIgnoreCase) && string.Equals(RequestPath, other.RequestPath, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Request) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((RequestMethod != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(RequestMethod) : 0) * 397) ^ (RequestPath != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(RequestPath) : 0);
            }
        }
    }
}