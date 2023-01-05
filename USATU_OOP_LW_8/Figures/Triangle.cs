using System.Drawing;
using USATU_OOP_LW_8.Enums;

namespace USATU_OOP_LW_8.Figures;

public class Triangle : Figure
{
    protected override GraphicObjectsTypes GraphicObjectsType => GraphicObjectsTypes.Triangle;
    protected override string NamePrefix => "Triangle";

    public override Point[] ContourPoints => new Point[]
    {
        new Point(FigureRectangle.Left, FigureRectangle.Top),
        new Point(FigureRectangle.Left, FigureRectangle.Bottom),
        new Point(FigureRectangle.Right, FigureRectangle.Bottom)
    };

    public Triangle(FigureCreationArguments figureCreationArguments) : base(figureCreationArguments)
    {
    }

    public Triangle() : base()
    {
    }

    public override bool IsPointInside(Point pointToCheck)
    {
        return (FigureRectangle.Contains(pointToCheck) && IsUnderLine(FigureRectangle.Location,
            new Point(FigureRectangle.Right, FigureRectangle.Bottom), pointToCheck));
    }

    protected override void DrawFigureOnGraphics(Graphics graphics)
    {
        graphics.FillPolygon(CurrentBrush, ContourPoints);
    }
}