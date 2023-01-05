namespace USATU_OOP_LW_8.Factories;

public abstract class GraphicObjectsAbstractFactory
{
    public abstract GraphicObject ParseGraphicObject(string typeOfObject);
}