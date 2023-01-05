using System;

namespace USATU_OOP_LW_8;

public class FiguresFactory : FiguresAbstractFactory
{
    public override Figure ParseFigure(Enum typeOfObject, FigureArgument figureArgument)
    {
        Figure newFigure = typeOfObject switch
        {
            GraphicObjectsTypes.Circle => new Circle(figureArgument),
            GraphicObjectsTypes.Square => new Square(figureArgument),
            GraphicObjectsTypes.Triangle => new Triangle(figureArgument),
            GraphicObjectsTypes.Pentagon => new Pentagon(figureArgument),
            _ => null
        };

        return newFigure;
    }
}