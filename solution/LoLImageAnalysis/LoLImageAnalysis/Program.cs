using System;
using System.Drawing;

namespace LoLImageAnalysis
{
    class Program
    {
        const String filePath = "D:/dojo/mspaint/1.jpg";
  
        

        static void Main(string[] args)
        {
            Screenshot screenshot = new Screenshot(filePath);
            Bitmap minimap = screenshot.getMiniMap();

            minimap.Save("D:/dojo/resultat.jpg");

            Console.Write("END");
            Console.ReadKey();

        }
    }


    public class Screenshot
    {
        private const double pixelRatio = 0.031;
        private const double offsetRatio = 0.0013;
        
        private Bitmap bmp;

        public Screenshot(String url)
        {
            bmp = new Bitmap(url);
        }

        public Bitmap getMiniMap()
        {
            int miniMapSize = (int)Math.Round(Math.Sqrt(bmp.Width * bmp.Height * pixelRatio));
            int offset = (int)Math.Round(bmp.Width * bmp.Height * offsetRatio / miniMapSize);

            int canvasX = bmp.Width - miniMapSize - offset;
            int canvasY = bmp.Height - miniMapSize - offset;

            Rectangle rect = new Rectangle(canvasX, canvasY, miniMapSize, miniMapSize);
            return bmp.Clone(rect, bmp.PixelFormat);
        }
    }


/*
    public class Processing
    {
        public Bitmap invertColors(Bitmap bmp)
        {
            return;
        }
    }

 */

}
