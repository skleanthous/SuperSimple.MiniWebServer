namespace SuperSimple.MiniWebServer
{
    using System.Collections.Generic;

    public class ResponseHeaders : WrappedDictionary<string, string[]>
    {
        private const string CONTENT_TYPE = "Content-Type";
        public string ContentType
        {
            get { return this[CONTENT_TYPE]?[0]; }
            set { this[CONTENT_TYPE] = new[] { value }; }
        }

        private const string CONTENT_LENGTH = "Content-Length";
        public int ContentLength
        {
            get { return int.Parse(this[CONTENT_TYPE]?[0]); }
            set { this[CONTENT_TYPE] = new[] { value.ToString() }; }
        }

        private const string CONTENT_DISPOSITION = "Content-Disposition";
        public ContentDispositionData ContentDisposition
        {
            get { return ContentDispositionData.Parse(this[CONTENT_DISPOSITION]); }
            set { this[CONTENT_DISPOSITION] = value.ToStringArray(); }
        }

        internal ResponseHeaders(IDictionary<string, string[]> original)
            : base(original)
        { }
    }
}
