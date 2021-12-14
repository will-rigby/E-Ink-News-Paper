using System;
using System.IO;
using System;
using System.Drawing;

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

            Bitmap bmp = new Bitmap("E:\\st-lucia.bmp");
            Image image = new Image(bmp.Height, bmp.Width);
            for(int x = 0; x < bmp.Width; x++)
            {
                for(int y = 0; y < bmp.Height; y++)
                {
                    image.pixels[x, y] = new Colour(Convert.ToInt32(bmp.GetPixel(x, y).R), Convert.ToInt32(bmp.GetPixel(x, y).G), Convert.ToInt32(bmp.GetPixel(x, y).B));
                }

                
            }
            Colour red = new Colour(255, 0, 0);
            Colour green = new Colour(0, 255, 0);
            Colour blue = new Colour(0, 0, 255);
            Colour white = new Colour(255, 255, 255);
            Colour black = new Colour(0, 0, 0);

            List<Colour> palette = new List<Colour>();
            palette.Add(red);
            palette.Add(green);
            palette.Add(blue);
            palette.Add(white);
            //palette.Add(black);
            
            FloydSteinberg fs = new FloydSteinberg(palette);
            Image ditheredImage = fs.Dither(image);
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color color = Color.FromArgb(ditheredImage.pixels[x, y].red, ditheredImage.pixels[x, y].green, ditheredImage.pixels[x, y].blue);
                    bmp.SetPixel(x, y, color);
                }


            }
            bmp.Save("E:\\dithered.bmp");
        }
    }
}