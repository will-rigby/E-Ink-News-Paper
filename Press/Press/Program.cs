using System;
using System.IO;


namespace TheABCNews
{
    class Program
    {
        static void Main(string[] arguments)
        {
            
            Console.WriteLine("Hello World");

            Reporter reporter = new Reporter();
            String source = System.IO.File.ReadAllText("E:\\repos\\E-Ink-News-Paper\\Press\\Press\\ExampleStories\\StandardStory.htm");
            Story story = reporter.AnalyseArticle(source);
            Console.WriteLine("Title: " + story.title);
            Console.WriteLine("By Line: " + story.byLine);
            if(story.imageURL != null)
            {
                Console.WriteLine("Image URL: " + story.imageURL);
            }
            
        }
    }
}