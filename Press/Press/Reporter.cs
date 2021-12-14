using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheABCNews
{
    /* Class that gets news stories */
    internal class Reporter
    {
        public Reporter()
        {

        }

        public Story AnalyseArticle(string source)
        {
            List<String> body = new List<String>();
            String title = GetTitle(source);
            String byline = GetByLine(source);
            String? imageURL = GetImageURL(source);
            return new Story("Title", "Author", body, imageURL);
        }

        private List<String> GetDataInTags(string source, string tag)
        {
            List<String> dataList = new List<String>();

            int i = 0;

            string openTag = "<" + tag + ">";
            string closeTag = "</" + tag + ">";
            string dataInTags = "";
            Boolean tagFound = false;

            while (i < (source.Length - closeTag.Length))
            {
                if (tagFound)
                {
                    if (source.Substring(i, closeTag.Length).Equals(closeTag))
                    {
                        dataList.Add(dataInTags);
                        dataInTags = "";
                        tagFound = false;
                        i += closeTag.Length;
                    }
                    else
                    {
                        dataInTags += source[i];
                        i++;
                    }
                }
                else
                {
                    if (source.Substring(i, openTag.Length).Equals(openTag))
                    {
                        i += openTag.Length;
                        tagFound = true;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            return dataList;
        }

        public String? GetContentInTag(string source, string openTag, string closeTag)
        {
            int i = 0;
            Boolean tagFound = false;
            string dataInTag = "";

            int largeTagSize;
            if (openTag.Length > closeTag.Length)
            {
                largeTagSize = openTag.Length;
            } else
            {
                largeTagSize = closeTag.Length;
            }
            
            while (i < (source.Length - largeTagSize))
            {
                if (tagFound)
                {
                    String checkString = source.Substring(i, closeTag.Length);
                    if (checkString.Equals(closeTag))
                    {                        
                        return dataInTag;
                        
                    }
                    else
                    {
                        dataInTag += source[i];
                        i++;
                    }
                }
                else
                {
                    if (source.Substring(i, openTag.Length).Equals(openTag))
                    {
                        i += openTag.Length;
                        tagFound = true;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            return null;
        }

        public String GetTitle(string source)
        {
            string? titleTag = GetContentInTag(source, "<title>", "</title>");
            if(titleTag != null) {
                return titleTag.Replace(" - ABC News", "");
            } else
            {
                throw new Exception("Invalid Title");
            }
        }

        public String GetByLine(string source)
        {
            string? byLineURL = GetContentInTag(source, "<meta data-react-helmet=\"true\" property=\"article:author\" content=\"", "\">");
            if (byLineURL != null)
            {
                byLineURL = byLineURL.Replace("https://www.abc.net.au/news/", "");
                string byLine = "";
                int i = 0;
                while (byLineURL[i] != '/')
                {
                    byLine += byLineURL[i++];

                    if (i == byLineURL.Length)
                    {
                        throw new Exception("By Line invalid format exception");
                    }
                }
                
                return byLine.Replace("-", " ");
            }
            else
            {
                throw new Exception("Invalid ByLine");
            }
        }

        public String? GetImageURL(string source)
        {
            string? imageDiv = GetContentInTag(source, "img alt", "\"image\"");
            if(imageDiv != null)
            {
                return GetContentInTag(source, "src=\"", "\" "); 
            }
            return null;
        }
    }
}
