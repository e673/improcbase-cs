using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace ImageReadCS
{
    class Program
    {
        static void FlipImage(GrayscaleFloatImage image)
        {
            for (int y = 0; y < image.Height; y++)
                for (int x = 0; x < image.Width / 2; x++)
                {
                    float p = image[x, y];
                    image[x, y] = image[image.Width - 1 - x, y];
                    image[image.Width - 1 - x, y] = p;
                }
        }


        static void Main(string[] args)
        {
            if (args.Length < 2)
                return;
            string InputFileName = args[0], OutputFileName = args[1];
            if (!File.Exists(InputFileName))
                return;

            GrayscaleFloatImage image = ImageIO.FileToGrayscaleFloatImage(InputFileName);

            FlipImage(image);

            ImageIO.ImageToFile(image, OutputFileName);


        }
    }
}
