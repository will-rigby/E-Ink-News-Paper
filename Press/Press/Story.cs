using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheABCNews
{
    /* Class to represent news story */
    internal class Story
    {
        public readonly string title;
        public readonly string byLine;
        public readonly List<string> body;
        public readonly string? imageURL;
        public Story(string title, string byLine, List<String> body, string? imageURL)
        {
            this.title = title;
            this.byLine = byLine;
            this.body = body;
            this.imageURL = imageURL;
        }
    }
}
