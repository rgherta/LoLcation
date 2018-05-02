using System;
using System.Drawing;

/*  
 *  
 *  2018-05-01 / RG
 *  captures minimap from screenshot
 *  uses a predefined pixeRatio deduced from test samples
 *  offset ratio is used to trim the minimap margins
 *  
 */



namespace LoLImageAnalysis
{
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
}
