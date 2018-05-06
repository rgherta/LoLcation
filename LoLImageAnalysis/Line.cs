using System;

namespace LoLImageAnalysis
{
    public class Line
    {
        
        private int coordX;
        private int coordY;
        private int width;
        private lineType type;

        public int CoordX { get => coordX; set => coordX = value; }
        public int CoordY { get => coordY; set => coordY = value; }
        public int Width { get => width; set => width = value; }
        public lineType Type { get => type; set => type = value; }

        public Line(int X, int Y, int W,  lineType T)
        {
            this.coordX = X;
            this.coordY = Y;
            this.width = W;
            this.Type = T;
        }

        public override String ToString()
        {
            return this.coordX + ";" + this.CoordY + ";" + this.Width + ";" + this.Type;
        }
    }
}
