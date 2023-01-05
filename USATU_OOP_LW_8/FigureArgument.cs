using System.Drawing;

namespace USATU_OOP_LW_8;

public struct FigureArgument
{
    public readonly Color FillColor;
    public readonly Point CenterLocation;

    public FigureArgument(Color fillColor, Point centerLocation) =>
        (FillColor, CenterLocation) = (fillColor, centerLocation);
}