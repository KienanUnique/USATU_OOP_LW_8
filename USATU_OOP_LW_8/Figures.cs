using System;
using System.Drawing;

namespace USATU_OOP_LW_8
{
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

        public Circle(Color color, Point location) : base(color, location)
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

        public Square(Color color, Point location) : base(color, location)
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

        public Triangle(Color color, Point location) : base(color, location)
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

    public class Pentagon : Figure
    {
        protected override GraphicObjectsTypes GraphicObjectsType => GraphicObjectsTypes.Pentagon;
        protected override string NamePrefix => "Pentagon";

        public override Point[] ContourPoints => new Point[]
        {
            new Point(FigureRectangle.Left, FigureRectangle.Bottom - FigureRectangle.Height / 2),
            new Point(FigureRectangle.Right - FigureRectangle.Width / 2, FigureRectangle.Top),
            new Point(FigureRectangle.Right, FigureRectangle.Bottom - FigureRectangle.Height / 2),
            new Point(FigureRectangle.Right, FigureRectangle.Bottom),
            new Point(FigureRectangle.Left, FigureRectangle.Bottom),
        };

        public Pentagon(Color color, Point location) : base(color, location)
        {
        }

        public Pentagon() : base()
        {
        }

        public override bool IsPointInside(Point pointToCheck)
        {
            var isUnderLeftCornerLine = IsUnderLine(
                new Point(FigureRectangle.Left, FigureRectangle.Bottom - FigureRectangle.Height / 2),
                new Point(FigureRectangle.Right - FigureRectangle.Width / 2, FigureRectangle.Top), pointToCheck);
            var isUnderRightCornerLine = IsUnderLine(
                new Point(FigureRectangle.Right - FigureRectangle.Width / 2, FigureRectangle.Top),
                new Point(FigureRectangle.Right, FigureRectangle.Bottom - FigureRectangle.Height / 2), pointToCheck);
            return FigureRectangle.Contains(pointToCheck) && isUnderLeftCornerLine && isUnderRightCornerLine;
        }

        protected override void DrawFigureOnGraphics(Graphics graphics)
        {
            graphics.FillPolygon(CurrentBrush, ContourPoints);
        }
    }
}