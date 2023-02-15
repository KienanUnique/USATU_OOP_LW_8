using System;
using USATU_OOP_LW_8.Figures;

namespace USATU_OOP_LW_8.Factories;

public abstract class FiguresAbstractFactory
{
    public abstract Figure ParseFigure(Enum typeOfObject, FigureCreationArguments figureCreationArguments);
}