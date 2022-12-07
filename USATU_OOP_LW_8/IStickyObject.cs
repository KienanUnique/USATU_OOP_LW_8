using System.Drawing;

namespace USATU_OOP_LW_8;

public interface IStickyObject
{
    public Point[] ContourStickyPoints { get; }
    public bool IsAnyPointInsideStickyObject(Point[] pointsToCheck);
}