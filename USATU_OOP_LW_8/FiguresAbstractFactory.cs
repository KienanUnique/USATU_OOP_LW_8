using System;
using System.Drawing;

namespace USATU_OOP_LW_8;

public abstract class FiguresAbstractFactory
{
    public abstract Figure ParseFigure(Enum typeOfObject, Color color, Point location);
}