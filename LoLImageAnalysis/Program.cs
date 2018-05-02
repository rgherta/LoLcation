using System;
using System.Drawing;

namespace LoLImageAnalysis
{
    class Program
    {
        const String filePath = "D:/dojo/mspaint/1.jpg";
  
        static void Main(string[] args)
        {
            //preprocessing
            Screenshot screenshot = new Screenshot(filePath);
            Bitmap minimap = screenshot.getMiniMap();
            minimap = Processing.invertColors(minimap);

            //saving
            minimap.Save("D:/dojo/resultat.jpg");

            Console.Write("END");
            Console.ReadKey();

        }
    } 

}
