using System;
using System.Drawing;
using System.IO;
using static LoLImageAnalysis.Processing;

/*  
 *  
 *  2018-05-02 / RG
 *  main app
 *  
 *  
 */

namespace LoLImageAnalysis
{
    class Program
    {
        protected static String appName = System.IO.Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
        

        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                switch (args[0])
                {

                    case "-h":
                    case "--help":
                        Documentation();
                        break;



                    case "-s":
                    case "--src":
                        if (args.Length == 2)
                        {
                            String src = args[1];

                            if (File.Exists(src))
                            {

                                RunProgram(src);

                            }
                            else if (Directory.Exists(src))
                            {

                                foreach (string imageFileName in Directory.GetFiles(src, "*.jpg"))
                                {
                                    RunProgram(imageFileName);
                                }

                            }
                            else
                            {

                                NotRecognized();

                            }
                        }
                        else
                        {
                            NotRecognized();
                        };
                        break;




                    default:
                        NotRecognized();
                        break;
                }
            } else
            {
                NotRecognized();
            }


        }


        private static void RunProgram(String srcPath)
        {
            try
            {
                Screenshot screenshot = new Screenshot(srcPath);
                Bitmap minimap = screenshot.GetMinimap();

                //preprocessing
                minimap.InvertColors();
                minimap.FilterBlack();
                minimap = new Bitmap(minimap, new Size(normWidth, normWidth));

                //saving
                //minimap.Save("D:/dojo/resultat.jpg");

                //processing
                MapRectangle mapRectangle = Processing.GetMapRectangle(minimap);

                Console.WriteLine("[" + srcPath + "]          ");
                Console.WriteLine("x: " + mapRectangle.CoordX + ", y: "
                             + mapRectangle.CoordY + ", w: " + mapRectangle.Width
                             + ", h: " + mapRectangle.Height + ", location: (" + mapRectangle.Location[0] + ", " + mapRectangle.Location[1] + ")");
                Console.WriteLine();
            }
            catch (LineNumberException ln)
            {
                Console.WriteLine("[Rectangle Exception] Could not identify rectangle");
            }
            catch (Exception e)
            {
                NewException();
            }

        }





        //TODO: Finish documentation for different cases
        private static void Documentation()
        {
            Console.WriteLine();
            Console.WriteLine("-------------");
            Console.WriteLine("DOCUMENTATION");
            Console.WriteLine("-------------");
            Console.WriteLine();
            Console.WriteLine("[" + appName + "] is a console tool that identifies player location based on LeagueOfLegends screenshots;");
            Console.WriteLine();
            Console.WriteLine("[-s] [--src]   can be an individual image file or a folder with images;");
            Console.WriteLine("               Example:    --src path\to\my\file.jpg");
            Console.WriteLine("                           --src path\to\my\folder");
   
        }

        private static void NotRecognized()
        {
            Console.WriteLine("[Argument not recognized] To consult documentation type -h or --help.");
        }

        private static void NewException()
        {
            Console.WriteLine("[Uncaught Exception] Something went wrong.");
        }

    } 

}
