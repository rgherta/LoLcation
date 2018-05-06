using System;
using System.Collections.Generic;
using System.Linq;
using static LoLImageAnalysis.Processing;

namespace LoLImageAnalysis
{
    public class MapRectangle
    {

        private int coordX;
        private int coordY;
        private int width;
        private int height;
        private double[] location;


        public int CoordX { get => coordX; set => coordX = value; }
        public int CoordY { get => coordY; set => coordY = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public double[] Location { get => location; set => location = value; }

        public MapRectangle(List<int[]> points)
        {
            this.CoordX = points[0][0];
            this.CoordY = points[0][1];
            this.Width = Math.Abs(points[0][0] - points[2][0]);
            this.Height = Math.Abs(points[0][1] - points[1][1]);

            this.Location = new double[] { 0, 0};
            this.Location[0] = Math.Round( ((double) (points.Sum(p => p[0]) / points.Count()) / normWidth) ,2);
            this.Location[1] = Math.Round( ((double) (points.Sum(p => p[1]) / points.Count()) / normWidth) ,2);
        }

        
    }
}
