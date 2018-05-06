using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

/*  
 *  
 *  2018-05-06 / RG
 *  finding horizontal and vertical lines
 *  evaluate multiple lines by neighborhood criteria
 *  normalize results
 */

namespace LoLImageAnalysis
{
    public enum lineType { Horizontal, Vertical };
 
    class Processing
    {
        const int nbhood = 5;
        public static int normWidth = 320;


        //Rectangle Detection
        public static MapRectangle GetMapRectangle(Bitmap bmp)
        {
            int[] result = new int[2];

            List<Line> lines = new List<Line>();
            lines.AddRange(FindVertical(bmp));
            lines.AddRange(FindHorizontal(bmp));


            //normalize results(multiple lines due to line width)
            NormalizeResults(lines);

            //intersect lines and find rectangle corners
            List<int[]> rectanglePoints = ComputeIntersection(lines);


            /* for checking purposes
            foreach (int[] point in rectanglePoints)
            {
                Console.WriteLine(point[0] + " " + point[1]);
            }
            */



            MapRectangle mapRectangle = new MapRectangle(rectanglePoints);

            return mapRectangle;
        }





        public static List<Line> FindVertical(Bitmap bmp)
        {
            //output in the form of (x,y,w,h)
            List<Line> result = new List<Line>();
            int k = 0;


            //loop up-down
            for (int i = 0; i < bmp.Width; i++)
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color pixel = bmp.GetPixel(i, j);

                    if (pixel.IsDark())
                    {

                        int lineLength = 1;
                        Line line = new Line(i, j, lineLength, lineType.Vertical);

                        //find vertical lines
                        for (k = j + 1; k < bmp.Height; k++)
                        {

                            Color nextPixel = bmp.GetPixel(i, k);


                            if (nextPixel.IsDark())
                            {
                                lineLength = k - j;
                                line.Width = lineLength;
                            }
                            else if (k - (j + lineLength) - 1 < nbhood)
                            {

                                continue;
                            }
                            else if (lineLength >= nbhood + 5)
                            {
                                result.Add(line);
                                //Console.WriteLine(line.ToString());
                                break;
                            }
                            else
                            {
                                break;
                            }

                        }

                        j = k;
                    }
                }
            return result;
        }







        public static List<Line> FindHorizontal(Bitmap bmp)
        {
            List<Line> result = new List<Line>();
            int k = 0;

            //loop left-right
            for (int j = 0; j < bmp.Height; j++)
                for (int i = 0; i < bmp.Width; i++)
                
                {
                    Color pixel = bmp.GetPixel(i, j);

                    if (pixel.IsDark())
                    {

                        int lineLength = 1;
                        Line line = new Line(i, j, lineLength, lineType.Horizontal);

                        //find horizontal lines
                        for (k = i + 1; k < bmp.Width; k++)
                        {

                            Color nextPixel = bmp.GetPixel(k, j);

                            if (nextPixel.IsDark())
                            {
                                lineLength = k - i;
                                line.Width = lineLength;
                            }
                            else if (k - i  - 1 < nbhood)
                            {

                                continue;
                            }
                            else if (lineLength >= nbhood + 5)
                            {
                                result.Add(line);
                                //Console.WriteLine(line.ToString());
                                break;
                            }
                            else
                            {
                                break;
                            }
                            

                        }

                        i = k;
                    }
                }
            return result;
        }


        public static List<int[]> ComputeIntersection(List<Line> lines)
        {
            List<int[]> points = new List<int[]>();

            var horizontals = lines.Where(ln => ln.Type == lineType.Horizontal);
            var verticals = lines.Where(ln => ln.Type == lineType.Vertical);
 
            foreach (Line elh in horizontals)
            {
                foreach (Line elv in verticals)
                {
                    points.Add(new int[] { elv.CoordX, elh.CoordY });
                }
            }

            //treat individual cases where rectangle is not fully visible by adding mising points
            if (horizontals.Count() == 1 && verticals.Count() == 2 && horizontals.ElementAt(0).CoordY < normWidth/2 )
            {   //top
                points.Add(new int[] { verticals.ElementAt(0).CoordX, 0 });
                points.Add(new int[] { verticals.ElementAt(1).CoordX, 0 });

            } else if (horizontals.Count() == 1 && verticals.Count() == 2 && horizontals.ElementAt(0).CoordY > normWidth / 2)
            {   //bottom
                points.Add(new int[] { verticals.ElementAt(0).CoordX, normWidth });
                points.Add(new int[] { verticals.ElementAt(1).CoordX, normWidth });

            } else if (verticals.Count() == 1 && horizontals.Count() == 2 && verticals.ElementAt(0).CoordX < normWidth / 2)
            {   //left
                points.Add(new int[] { 0, horizontals.ElementAt(0).CoordY });
                points.Add(new int[] { 0, horizontals.ElementAt(1).CoordY });

            } else if (verticals.Count() == 1 && horizontals.Count() == 2 && verticals.ElementAt(0).CoordX > normWidth / 2)
            {   //right
                points.Add(new int[] { normWidth, horizontals.ElementAt(0).CoordY });
                points.Add(new int[] { normWidth, horizontals.ElementAt(1).CoordY });

            }
                  

            //top left
            else if (verticals.Count() == 1 && horizontals.Count() == 1 
                && verticals.ElementAt(0).CoordX < normWidth / 2 && horizontals.ElementAt(0).CoordY < normWidth / 2)
            {
                points.Add(new int[] { 0, 0 });
                points.Add(new int[] { 0, horizontals.ElementAt(0).CoordY });
                points.Add(new int[] { verticals.ElementAt(0).CoordX, 0 });
            //bottom left
            } else if (verticals.Count() == 1 && horizontals.Count() == 1
                && verticals.ElementAt(0).CoordX < normWidth / 2 && horizontals.ElementAt(0).CoordY > normWidth / 2)
            {
                points.Add(new int[] { 0, normWidth });
                points.Add(new int[] { 0, horizontals.ElementAt(0).CoordY });
                points.Add(new int[] { verticals.ElementAt(0).CoordX, normWidth });
            //top right
            } else if (verticals.Count() == 1 && horizontals.Count() == 1
               && verticals.ElementAt(0).CoordX > normWidth / 2 && horizontals.ElementAt(0).CoordY < normWidth / 2)
            {
                points.Add(new int[] { normWidth, 0 });
                points.Add(new int[] { normWidth, horizontals.ElementAt(0).CoordY });
                points.Add(new int[] { verticals.ElementAt(0).CoordX, 0 });
            }  //bottom right
            else if (verticals.Count() == 1 && horizontals.Count() == 1
               && verticals.ElementAt(0).CoordX > normWidth / 2 && horizontals.ElementAt(0).CoordY > normWidth / 2)
            {
                points.Add(new int[] { normWidth, normWidth });
                points.Add(new int[] { normWidth, horizontals.ElementAt(0).CoordY });
                points.Add(new int[] { verticals.ElementAt(0).CoordX, normWidth });
            }

            else if (verticals.Count() == 0 || horizontals.Count() == 0)
            {
                throw new LineNumberException();
            };


            return points.OrderBy(el => el[0]).ThenBy(el => el[1]).ToList();

        }




        public static void NormalizeResults(List<Line> lines)
        {
            for (int i = 0; i < lines.Count; i++)
            {

                switch (lines[i].Type)
                {
                    case lineType.Vertical:
                        int CoordX;
                        for (int j = 0; (j < lines.Count) && (j != i); j++)
                        {

                            if (lines[j].Type == lineType.Vertical && lines[i].CoordX - lines[j].CoordX <= nbhood)
                            {
                                CoordX = Math.Min(lines[i].CoordX, lines[j].CoordX);
                                lines[i].CoordX = CoordX;
                                lines.Remove(lines[j]);
                                j = j - 1;
                                i = i - 1;
                            }
                        }
                        break;

                    case lineType.Horizontal:
                        int CoordY;
                        int CdX;
                        for (int j = 0; (j < lines.Count) && (j != i); j++)
                        {

                            if (lines[j].Type == lineType.Horizontal && lines[i].CoordY - lines[j].CoordY <= nbhood)
                            {
                                CoordY = Math.Min(lines[i].CoordY, lines[j].CoordY);
                                CdX = Math.Min(lines[i].CoordX, lines[j].CoordX);
                                lines[i].CoordY = CoordY;
                                lines[i].CoordX = CdX;
                                lines.Remove(lines[j]);
                                j = j - 1;
                                i = i - 1;
                            }
                        }
                        break;

                }


            }
        }
    }
}
