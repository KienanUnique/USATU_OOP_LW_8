using System;
using USATU_OOP_LW_8.Enums;
using USATU_OOP_LW_8.Figures;

namespace USATU_OOP_LW_8.Factories;

public class GraphicObjectsFactory : GraphicObjectsAbstractFactory
{
    public override GraphicObject ParseGraphicObject(string typeOfObject)
    {
        Enum.TryParse(typeOfObject, out GraphicObjectsTypes objectType);
        GraphicObject newGraphicObject = objectType switch
        {
            GraphicObjectsTypes.Group => new GraphicObjectGroup(this),
            GraphicObjectsTypes.Circle => new Circle(),
            GraphicObjectsTypes.Square => new Square(),
            GraphicObjectsTypes.Triangle => new Triangle(),
            GraphicObjectsTypes.Pentagon => new Pentagon(),
            _ => null
        };

        return newGraphicObject;
    }
}