using System.Drawing;

namespace USATU_OOP_LW_8.Figures;

public struct FigureCreationArguments
{
    public readonly Color FillColor;
    public readonly Point CenterLocation;

    public FigureCreationArguments(Color fillColor, Point centerLocation) =>
        (FillColor, CenterLocation) = (fillColor, centerLocation);
}