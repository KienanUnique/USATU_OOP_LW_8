using System.Drawing;
using System.IO;

namespace USATU_OOP_LW_8;

public enum ResizeAction
{
    Increase,
    Decrease
}

public abstract class GraphicObject
{
    public readonly int Id = BankOfIds.GetInstance().GetId();
    protected bool IsSelected;
    protected abstract string NamePrefix { get; }
    protected const string PrefixGraphicObjectsType = "Type: ";
    public abstract bool IsFigureOutside(Size backgroundSize);
    public abstract void Color(Color newColor);
    public abstract bool IsResizePossible(int sizeK, ResizeAction resizeAction, Size backgroundSize);
    public abstract void Resize(int sizeK, ResizeAction resizeAction);
    public abstract bool IsMovePossible(Point moveVector, Size backgroundSize);
    public abstract void Move(Point moveVector);
    public abstract void DrawOnGraphics(Graphics graphics);
    public abstract bool IsObjectSelected();
    public abstract void Select();
    public abstract void Unselect();
    public abstract void ProcessClick();
    public abstract bool IsPointInside(Point pointToCheck);
    public abstract bool IsGroup();
    public abstract string PrepareDataToStore();
    public abstract void LoadData(StringReader dataStringReader);

    public string GetName()
    {
        return NamePrefix + " (" + Id + ")";
    }
    public void ReturnIdToBank()
    {
        BankOfIds.GetInstance().ReturnId(Id);
    }
}