using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSimple.MiniWebServer
{
    public struct ContentDispositionData
    {
        public string Disposition { get; set; }
        public string Filename { get; set; }

        public static ContentDispositionData Parse(string[] data)
        {
            //TODO: Finish with parsing this
            return new ContentDispositionData()
            {
                Disposition = data[0],
            };
        }

        public string[] ToStringArray()
        {
            //TODO: Optimize this a bit maybe?
            var items = new List<string>();

            items.Add($"{Disposition}");

            if (Filename != null)
                items.Add($"filename=\"{Filename}\"");

            return items.ToArray();
        }
    }
}
