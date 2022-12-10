using System;
using System.Drawing;

namespace USATU_OOP_LW_8;

public class FiguresFactory : FiguresAbstractFactory
{
    public override Figure ParseFigure(Enum typeOfObject, Color color, Point location)
    {
        Figure newFigure = typeOfObject switch
        {
            GraphicObjectsTypes.Circle => new Circle(color, location),
            GraphicObjectsTypes.Square => new Square(color, location),
            GraphicObjectsTypes.Triangle => new Triangle(color, location),
            GraphicObjectsTypes.Pentagon => new Pentagon(color, location),
            _ => null
        };

        return newFigure;
    }
}