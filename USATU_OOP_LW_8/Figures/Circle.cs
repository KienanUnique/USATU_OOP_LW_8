using System;
using System.Drawing;
using USATU_OOP_LW_8.Enums;

namespace USATU_OOP_LW_8.Figures;

public class Circle : Figure
{
    private const int CountOfPoints = 16;
    private int CircleRadius => FigureRectangle.Width / 2;

    private Point CircleCenter => new Point(FigureRectangle.X + FigureRectangle.Width / 2,
        FigureRectangle.Y + FigureRectangle.Height / 2);

    protected override GraphicObjectsTypes GraphicObjectsType => GraphicObjectsTypes.Circle;
    protected override string NamePrefix => "Circle";

    public override Point[] ContourPoints
    {
        get
        {
            var contourPoints = new Point[CountOfPoints];
            double currentAngle = 0;
            for (int i = 0; i < CountOfPoints; i++)
            {
                contourPoints[i] = new Point((int) (CircleRadius * Math.Sin(currentAngle) + CircleCenter.X),
                    (int) ((CircleRadius * Math.Cos(currentAngle) + CircleCenter.Y)));
                currentAngle += Math.PI * 2 / CountOfPoints;
            }

            return contourPoints;
        }
    }

    public Circle(FigureCreationArguments figureCreationArguments) : base(figureCreationArguments)
    {
    }

    public Circle() : base()
    {
    }

    public override bool IsPointInside(Point pointToCheck)
    {
        var tmpX = pointToCheck.X - CircleCenter.X;
        var tmpY = pointToCheck.Y - CircleCenter.Y;
        return tmpX * tmpX + tmpY * tmpY <= CircleRadius * CircleRadius;
    }

    protected override void DrawFigureOnGraphics(Graphics graphics)
    {
        graphics.FillEllipse(CurrentBrush, FigureRectangle);
    }
}