using System.Drawing;
using USATU_OOP_LW_8.Enums;

namespace USATU_OOP_LW_8.Figures
{
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

        public Pentagon(FigureCreationArguments figureCreationArguments) : base(figureCreationArguments)
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