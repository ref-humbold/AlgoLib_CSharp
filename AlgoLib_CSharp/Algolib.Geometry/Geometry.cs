// Basic geometric operations

namespace Algolib.Geometry
{
    public sealed class Geometry
    {
        public static Point2D Translate(Point2D p, Vector2D v) =>
            new Point2D(p.X + v.X, p.Y + v.Y);

        public static Point3D Translate(Point3D p, Vector3D v) =>
            new Point3D(p.X + v.X, p.Y + v.Y, p.Z + v.Z);
    }
}
