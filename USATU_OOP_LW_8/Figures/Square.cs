using System.Drawing;
using USATU_OOP_LW_8.Enums;

namespace USATU_OOP_LW_8.Figures;

public class Square : Figure
{
    protected override GraphicObjectsTypes GraphicObjectsType => GraphicObjectsTypes.Square;
    protected override string NamePrefix => "Square";

    public override Point[] ContourPoints => new Point[]
    {
        new Point(FigureRectangle.Left, FigureRectangle.Top),
        new Point(FigureRectangle.Right, FigureRectangle.Top),
        new Point(FigureRectangle.Left, FigureRectangle.Bottom),
        new Point(FigureRectangle.Right, FigureRectangle.Bottom),
    };

    public Square(FigureCreationArguments figureCreationArguments) : base(figureCreationArguments)
    {
    }

    public Square() : base()
    {
    }

    public override bool IsPointInside(Point pointToCheck)
    {
        return FigureRectangle.Contains(pointToCheck);
    }

    protected override void DrawFigureOnGraphics(Graphics graphics)
    {
        graphics.FillRectangle(CurrentBrush, FigureRectangle);
    }
}