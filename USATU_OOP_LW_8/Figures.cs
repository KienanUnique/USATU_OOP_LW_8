using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace USATU_OOP_LW_8
{
    public enum GraphicObjectsTypes
    {
        None,
        Circle,
        Triangle,
        Square,
        Pentagon,
        Group
    }

    public static class SelectionBorder
    {
        private const int SelectionBorderWidth = 5;
        private static readonly Color SelectionColor = Color.Black;
        private static readonly float[] DashValues = {1, 1};
        private static readonly Pen BorderPen;

        static SelectionBorder()
        {
            BorderPen = new Pen(SelectionColor, SelectionBorderWidth);
            BorderPen.DashPattern = DashValues;
        }

        public static void DrawSelectionBorder(Graphics graphics, Rectangle figureRectangle)
        {
            graphics.DrawRectangle(BorderPen, figureRectangle);
        }
    }

    public abstract class Figure : GraphicObject
    {
        protected Rectangle FigureRectangle;
        protected readonly SolidBrush CurrentBrush;
        protected abstract GraphicObjectsTypes GraphicObjectsType { get; }

        private readonly Size _defaultSize = new Size(50, 50);
        private readonly Point _defaultLocation = new Point(0, 0);
        private readonly Color _defaultColor = System.Drawing.Color.Black;
        private readonly Size _minimumSize = new Size(10, 10);

        private const string PrefixSizeWidth = "Size width: ";
        private const string PrefixSizeHeight = "Size height: ";
        private const string PrefixLeftTopPointX = "Left top point X: ";
        private const string PrefixLeftTopPointY = "Left top point Y: ";
        private const string PrefixColor = "Color: ";
        private const string PrefixFigureType = "Figure type: ";

        protected Figure()
        {
            FigureRectangle = new Rectangle(_defaultLocation, _defaultSize);
            CurrentBrush = new SolidBrush(_defaultColor);
        }

        ~Figure()
        {
        }

        protected Figure(Color color, Point centerLocation)
        {
            var leftTopPoint = new Point(centerLocation.X - _defaultSize.Width / 2,
                centerLocation.Y - _defaultSize.Height / 2);
            FigureRectangle = new Rectangle(leftTopPoint, _defaultSize);
            CurrentBrush = new SolidBrush(color);
        }

        public override void LoadData(StringReader dataStringReader)
        {
            int.TryParse(dataStringReader.ReadLine(), out int readWidth);
            int.TryParse(dataStringReader.ReadLine(), out int readHeight);
            int.TryParse(dataStringReader.ReadLine(), out int readLocationX);
            int.TryParse(dataStringReader.ReadLine(), out int readLocationY);
            int.TryParse(dataStringReader.ReadLine(), out int readColor);
            FigureRectangle.Size = new Size(readWidth, readHeight);
            FigureRectangle.Location = new Point(readLocationX, readLocationY);
            CurrentBrush.Color = ColorTranslator.FromOle(readColor);
        }

        public override bool IsFigureOutside(Size backgroundSize)
        {
            return IsFigureOutside(FigureRectangle, backgroundSize);
        }

        public override void Color(Color newColor) => CurrentBrush.Color = newColor;

        private Rectangle GetResizedFigureRectangle(int sizeK, ResizeAction resizeAction)
        {
            var newFigureRectangle = new Rectangle();
            switch (resizeAction)
            {
                case ResizeAction.Increase:
                    newFigureRectangle = new Rectangle(FigureRectangle.Location,
                        new Size(FigureRectangle.Size.Width * sizeK, FigureRectangle.Size.Height * sizeK));
                    break;
                case ResizeAction.Decrease:
                    newFigureRectangle = new Rectangle(FigureRectangle.Location,
                        new Size(FigureRectangle.Size.Width / sizeK, FigureRectangle.Size.Height / sizeK));
                    break;
            }

            return newFigureRectangle;
        }

        public override bool IsResizePossible(int sizeK, ResizeAction resizeAction, Size backgroundSize)
        {
            var newFigureRectangle = GetResizedFigureRectangle(sizeK, resizeAction);
            if (IsFigureOutside(newFigureRectangle, backgroundSize) ||
                newFigureRectangle.Size.Height < _minimumSize.Height ||
                newFigureRectangle.Size.Width < _minimumSize.Width) return false;
            return true;
        }

        public override void Resize(int sizeK, ResizeAction resizeAction)
        {
            FigureRectangle = GetResizedFigureRectangle(sizeK, resizeAction);
        }

        private Rectangle GetMovedFigureRectangle(Point moveVector)
        {
            return new Rectangle(
                new Point(FigureRectangle.Location.X + moveVector.X, FigureRectangle.Location.Y + moveVector.Y),
                FigureRectangle.Size);
        }

        public override bool IsMovePossible(Point moveVector, Size backgroundSize)
        {
            return !(IsFigureOutside(GetMovedFigureRectangle(moveVector), backgroundSize));
        }

        public override void Move(Point moveVector)
        {
            FigureRectangle = GetMovedFigureRectangle(moveVector);
        }

        public override void DrawOnGraphics(Graphics graphics)
        {
            DrawFigureOnGraphics(graphics);
            if (IsSelected)
            {
                DrawSelectionBorders(graphics);
            }
        }

        public override bool IsObjectSelected()
        {
            return IsSelected;
        }

        public override void Select()
        {
            IsSelected = true;
        }

        public override void Unselect()
        {
            IsSelected = false;
        }

        public override void ProcessClick()
        {
            IsSelected = !IsSelected;
        }

        protected abstract void DrawFigureOnGraphics(Graphics graphics);

        protected static bool IsUnderLine(Point firstLinePoint, Point secondLinePoint, Point checkPoint)
        {
            return (checkPoint.X - firstLinePoint.X) * (secondLinePoint.Y - firstLinePoint.Y) -
                (checkPoint.Y - firstLinePoint.Y) * (secondLinePoint.X - firstLinePoint.X) <= 0;
        }

        private void DrawSelectionBorders(Graphics graphics)
        {
            SelectionBorder.DrawSelectionBorder(graphics, FigureRectangle);
        }

        private static bool IsFigureOutside(Rectangle figureRectangle, Size backgroundSize)
        {
            return 0 > figureRectangle.Left || figureRectangle.Right > backgroundSize.Width ||
                   backgroundSize.Height < figureRectangle.Bottom || figureRectangle.Top < 0;
        }

        public override bool IsGroup()
        {
            return false;
        }

        public override string PrepareDataToStore()
        {
            var dataStringBuilder = new StringBuilder();
            dataStringBuilder.AppendLine(PrefixFigureType + GraphicObjectsType);
            dataStringBuilder.AppendLine(PrefixSizeWidth + FigureRectangle.Size.Width);
            dataStringBuilder.AppendLine(PrefixSizeHeight + FigureRectangle.Size.Height);
            dataStringBuilder.AppendLine(PrefixLeftTopPointX + FigureRectangle.Location.X);
            dataStringBuilder.AppendLine(PrefixLeftTopPointY + FigureRectangle.Location.Y);
            dataStringBuilder.AppendLine(PrefixColor + ColorTranslator.ToOle(CurrentBrush.Color));
            return dataStringBuilder.ToString();
        }
    }

    public class Circle : Figure
    {
        protected override GraphicObjectsTypes GraphicObjectsType => GraphicObjectsTypes.Circle;
        protected override string NamePrefix => "Circle";

        public Circle(Color color, Point location) : base(color, location)
        {
        }

        public Circle() : base()
        {
        }

        public override bool IsPointInside(Point pointToCheck)
        {
            var circleRadius = FigureRectangle.Width / 2;
            var tmpX = pointToCheck.X - FigureRectangle.Location.X - circleRadius;
            var tmpY = pointToCheck.Y - FigureRectangle.Location.Y - circleRadius;
            return tmpX * tmpX + tmpY * tmpY <= circleRadius * circleRadius;
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

        public Triangle(Color color, Point location) : base(color, location)
        {
        }

        public Triangle() : base()
        {
        }

        public override bool IsPointInside(Point pointToCheck)
        {
            return (FigureRectangle.Contains(pointToCheck) && IsUnderLine(FigureRectangle.Location,
                new Point(FigureRectangle.Location.X + FigureRectangle.Width,
                    FigureRectangle.Location.Y + FigureRectangle.Height), pointToCheck));
        }

        protected override void DrawFigureOnGraphics(Graphics graphics)
        {
            Point[] points =
            {
                FigureRectangle.Location, new Point(FigureRectangle.Left, FigureRectangle.Bottom),
                new Point(FigureRectangle.Right, FigureRectangle.Bottom)
            };
            graphics.FillPolygon(CurrentBrush, points);
        }
    }

    public class Pentagon : Figure
    {
        protected override GraphicObjectsTypes GraphicObjectsType => GraphicObjectsTypes.Pentagon;
        protected override string NamePrefix => "Pentagon";

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
            Point[] points =
            {
                new Point(FigureRectangle.Left, FigureRectangle.Bottom - FigureRectangle.Height / 2),
                new Point(FigureRectangle.Right - FigureRectangle.Width / 2, FigureRectangle.Top),
                new Point(FigureRectangle.Right, FigureRectangle.Bottom - FigureRectangle.Height / 2),
                new Point(FigureRectangle.Right, FigureRectangle.Bottom),
                new Point(FigureRectangle.Left, FigureRectangle.Bottom),
            };
            graphics.FillPolygon(CurrentBrush, points);
        }
    }
}