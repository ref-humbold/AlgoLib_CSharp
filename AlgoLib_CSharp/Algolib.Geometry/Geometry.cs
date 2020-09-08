// Basic geometric operations
using System;

namespace Algolib.Geometry
{
    public sealed class Geometry
    {
        public static double Distance(Point2D p1, Point2D p2) =>
            Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));

        public static double Distance(Point3D p1, Point3D p2) =>
            Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y)
                      + (p2.Z - p1.Z) * (p2.Z - p1.Z));

        public static Point2D Translate(Point2D p, Vector2D v) =>
            new Point2D(p.X + v.X, p.Y + v.Y);

        public static Point3D Translate(Point3D p, Vector3D v) =>
            new Point3D(p.X + v.X, p.Y + v.Y, p.Z + v.Z);
    }
}
