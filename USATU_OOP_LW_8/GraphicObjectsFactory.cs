using System;

namespace USATU_OOP_LW_8;

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
            GraphicObjectsTypes.StickySquare => new StickySquare(),
            _ => null
        };

        return newGraphicObject;
    }
}