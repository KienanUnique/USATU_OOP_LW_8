using System;
using USATU_OOP_LW_8.Enums;
using USATU_OOP_LW_8.Figures;

namespace USATU_OOP_LW_8.Factories;

public class FiguresFactory : FiguresAbstractFactory
{
    public override Figure ParseFigure(Enum typeOfObject, FigureCreationArguments figureCreationArguments)
    {
        Figure newFigure = typeOfObject switch
        {
            GraphicObjectsTypes.Circle => new Circle(figureCreationArguments),
            GraphicObjectsTypes.Square => new Square(figureCreationArguments),
            GraphicObjectsTypes.Triangle => new Triangle(figureCreationArguments),
            GraphicObjectsTypes.Pentagon => new Pentagon(figureCreationArguments),
            _ => null
        };

        return newFigure;
    }
}