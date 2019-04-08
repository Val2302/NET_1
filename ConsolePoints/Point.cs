
namespace ConsolePoints
{
    public class Point
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public Point ( double x, double y )
        {
            X = x;
            Y = y;
        }

        public override string ToString ( )
        {
            return $"X:{X,8:F4} Y:{Y,8:F4}";
        }
    }
}
