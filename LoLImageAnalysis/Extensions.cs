using System;
using System.Drawing;

/*  
 *  
 *  2018-05-02 / RG
 *  extended Bitmap and Color types
 *  
 */


namespace LoLImageAnalysis
{
    
public static class Extensions
    {
        
        //Bitmap Extensions

        public static Bitmap InvertColors(this Bitmap bmp)
        {
            for (int i = 0; i < bmp.Width; i++)
                for (int j = 0; j < bmp.Height; j++)

                {
                    Color pixel = bmp.GetPixel(i, j);

                    bmp.SetPixel(i, j, pixel.InvertedColor());
                }

            return bmp;
        }


        public static Bitmap FilterBlack(this Bitmap bmp)
        {
            for (int i = 0; i < bmp.Width; i++)
                for (int j = 0; j < bmp.Height; j++)

                {
                    Color pixel = bmp.GetPixel(i, j);

                    if (!pixel.IsDark())
                    {
                        pixel = Color.FromArgb(255, 255, 255);
                        bmp.SetPixel(i, j, pixel);
                    }
                }

            return bmp;
        }


        //Color Extensions

        public static Color InvertedColor(this Color color)
        {
            const int max = 255;

            color = Color.FromArgb(max - color.R
                                    , max - color.G
                                    , max - color.B
                                  );
            return color;
        }

        public static Boolean IsDark(this Color pixel)
        {
            const int threshold = 30;

            Boolean response = (pixel.R > threshold || pixel.G > threshold || pixel.B > threshold) ? false : true;

            return response;
        }

    }

}
