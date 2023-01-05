using System.Drawing;
using System.IO;
using System.Linq;
using USATU_OOP_LW_8.Enums;

namespace USATU_OOP_LW_8;

public abstract class GraphicObject
{
    public readonly int Id = BankOfIds.GetInstance().GetId();
    protected bool IsSelected;
    protected abstract string NamePrefix { get; }
    public abstract Point[] ContourPoints { get; }
    protected const string PrefixGraphicObjectsType = "Type: ";
    private readonly StickyShapesObserver _stickyShapesObserver = new();
    public bool CanBeNotifiedMoved { get; private set; } = true;
    public abstract bool IsGroup { get; }

    public void ResetCanBeNotifiedMoved()
    {
        CanBeNotifiedMoved = true;
        _stickyShapesObserver.ResetAllCanBeNotifiedMovedFlags();
    }

    public abstract bool IsFigureOutside(Size backgroundSize);
    public abstract void Color(Color newColor);
    public abstract bool IsResizePossible(int sizeK, ResizeActionTypes resizeActionTypes, Size backgroundSize);
    public abstract void Resize(int sizeK, ResizeActionTypes resizeActionTypes);
    public abstract bool IsMovePossible(Point moveVector, Size backgroundSize);
    public abstract void MoveWithoutNotifying(Point moveVector);
    public abstract void DrawOnGraphics(Graphics graphics);
    public abstract bool IsObjectSelected();
    public abstract void Select();
    public abstract void Unselect();
    public abstract void ProcessClick();
    public abstract bool IsPointInside(Point pointToCheck);
    public abstract string PrepareDataToStore();
    public abstract void LoadData(StringReader dataStringReader);

    public void Move(Point moveVector, Size backgroundSize)
    {
        CanBeNotifiedMoved = false;
        _stickyShapesObserver.NotifyStuckObjectsAboutMoving(moveVector, backgroundSize);
        MoveWithoutNotifying(moveVector);
        ResetCanBeNotifiedMoved();
    }

    public void TryMove(Point moveVector, Size backgroundSize)
    {
        if (!CanBeNotifiedMoved) return;
        CanBeNotifiedMoved = false;
        _stickyShapesObserver.NotifyStuckObjectsAboutMoving(moveVector, backgroundSize);
        if (IsMovePossible(moveVector, backgroundSize))
        {
            MoveWithoutNotifying(moveVector);
        }
    }

    public bool IsAnyPointInside(Point[] pointsToCheck)
    {
        return pointsToCheck.Any(IsPointInside);
    }

    public string GetName()
    {
        return NamePrefix + " (" + Id + ")";
    }

    public virtual void ReturnIdToBank()
    {
        BankOfIds.GetInstance().ReturnId(Id);
    }

    public void StickNewGraphicObject(GraphicObject newGraphicObject)
    {
        _stickyShapesObserver.StickNewGraphicObject(newGraphicObject);
    }

    public bool IsObjectAlreadyStuck(int graphicObjectIdToCheck)
    {
        return _stickyShapesObserver.IsObjectAlreadyStuck(graphicObjectIdToCheck);
    }

    public void UnstickGraphicObjectById(int unstuckGraphicObjectId)
    {
        _stickyShapesObserver.UnstickGraphicObjectById(unstuckGraphicObjectId);
    }

    public void UnstickFromAllStuckGraphicObjects()
    {
        _stickyShapesObserver.UnstickFromAllStuckGraphicObjects(Id);
    }
}