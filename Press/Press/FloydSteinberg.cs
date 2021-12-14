using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheABCNews
{
    internal class Colour
    {
        public int red;
        public int green;
        public int blue;

        public Colour(int red, int green, int blue)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }

        public void Set(int red, int green, int blue)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }
    }

    internal class Image
    {
        public Colour[,] pixels;

        public Image(Colour[,] pixels)
        {
            this.pixels = pixels;
        }

        public Image(int height, int width){
            this.pixels = new Colour[width, height];
        }
    }


    internal class FloydSteinberg
    {
        List<Colour> colPal;
        public FloydSteinberg(List<Colour> colourPalette)
        {
            colPal = colourPalette;
        }

        private int MagnitudeQuantError(Colour colA, Colour colB)
        {
            Double rDif = colA.red - colB.red;
            Double gDif = colA.green - colB.green;
            Double bDif = colA.blue -  colB.blue;

            Double result = Math.Sqrt((rDif*rDif)+(gDif*gDif)+(bDif*bDif));

            return Convert.ToInt32(result);
        }

        private Colour QuantError(Colour colA, Colour colB)
        {
            int rDif = colA.red - colB.red;
            int gDif = colA.green - colB.green;
            int bDif = colA.blue - colB.blue;

            return new Colour(rDif, gDif, bDif);
        }

        private Colour CalculateDitherForCell(Colour oldPixel, Colour quantError, Double transformFraction)
        {
            int red = oldPixel.red + Convert.ToInt32(quantError.red * transformFraction);
            int green = oldPixel.green + Convert.ToInt32(quantError.green * transformFraction);
            int blue = oldPixel.blue + Convert.ToInt32(quantError.blue * transformFraction);

            return new Colour(red, green, blue);
        }

        private int FindClosestPaletteColourIndex(Colour colour)
        {
            int match = 0;
            int minDif = MagnitudeQuantError(colPal[0], colour);
            for(int palIndex = 1; palIndex < colPal.Count; palIndex++)
            {
                if(MagnitudeQuantError(colour, colPal[palIndex]) < minDif)
                {
                    match = palIndex;
                }
            }
            return match;
        }

        public Image Dither(Image image)
        {
            
            for(int y = 0; y < image.pixels.GetLength(1); y++)
            {
                for(int x = 0; x <image.pixels.GetLength(0); x++)
                {
                    Colour oldPixel = image.pixels[x,y];
                    Colour newPixel = colPal[FindClosestPaletteColourIndex(oldPixel)];
                    Colour quantError = QuantError(oldPixel, newPixel);
                    image.pixels[x,y] = newPixel;
                    if((x+ 1) < image.pixels.GetLength(0))
                    {
                        image.pixels[x + 1, y] = CalculateDitherForCell(image.pixels[x + 1, y], quantError, 7.0 / 16.0);
                    }
                    
                    if((y+1) < image.pixels.GetLength(1))
                    {
                        if(x > 0)
                        {
                            image.pixels[x - 1, y + 1] = CalculateDitherForCell(image.pixels[x - 1, y + 1], quantError, 3.0 / 16.0);
                        }
                        
                        image.pixels[x , y+1] = CalculateDitherForCell(image.pixels[x, y+1], quantError, 5.0 / 16.0);
                        if ((x + 1) < image.pixels.GetLength(0))
                        {
                            image.pixels[x + 1, y + 1] = CalculateDitherForCell(image.pixels[x + 1, y + 1], quantError, 1.0 / 16.0);
                        }
                    }
                    

                }
            }

            return image;
        }

        
    }
}
