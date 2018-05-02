using System;
using System.Drawing;

/*  
 *  
 *  2018-05-02 / RG
 *  tool to filter out inverted picture
 *  to have a well defined search feature
 *    
 */

namespace LoLImageAnalysis
{
    public class Processing
    {
        public static Bitmap invertColors(Bitmap bmp)
        {
            for (int i = 0; i < bmp.Width; i++)
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color pixel = bmp.GetPixel(i, j);
                    Color invertedPixel = invertedColor(pixel);

                    bmp.SetPixel(i, j, invertedPixel);
                }

            return bmp;
        }

        private static Color invertedColor(Color color)
        {
            const int max = 255;

            color = Color.FromArgb(max - color.R
                                    , max - color.G
                                    , max - color.B
                                  );
            return color;
        }
    }
}
