using System.Drawing;
using System.IO;
using System.Linq;

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
    public abstract Point[] ContourPoints { get; }
    protected const string PrefixGraphicObjectsType = "Type: ";
    private readonly StickyShapesObservable _stickyShapesObservable = new();
    public bool CanBeNotifiedMoved { get; private set; } = true;
    public abstract bool IsGroup { get; }

    public void ResetCanBeNotifiedMoved()
    {
        CanBeNotifiedMoved = true;
        _stickyShapesObservable.ResetAllCanBeNotifiedMovedFlags();
    }

    public abstract bool IsFigureOutside(Size backgroundSize);
    public abstract void Color(Color newColor);
    public abstract bool IsResizePossible(int sizeK, ResizeAction resizeAction, Size backgroundSize);
    public abstract void Resize(int sizeK, ResizeAction resizeAction);
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
        _stickyShapesObservable.NotifyStuckObjectsAboutMoving(Id, moveVector, backgroundSize);
        MoveWithoutNotifying(moveVector);
        CanBeNotifiedMoved = true;
        _stickyShapesObservable.ResetAllCanBeNotifiedMovedFlags();
    }

    public bool IsAnyPointInside(Point[] pointsToCheck)
    {
        return pointsToCheck.Any(point => IsPointInside(point));
    }

    public void TryMove(Point moveVector, Size backgroundSize)
    {
        if (!CanBeNotifiedMoved) return;
        CanBeNotifiedMoved = false;
        _stickyShapesObservable.NotifyStuckObjectsAboutMoving(Id, moveVector, backgroundSize);
        if (IsMovePossible(moveVector, backgroundSize))
        {
            MoveWithoutNotifying(moveVector);
        }
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
        if (!IsObjectAlreadyStuck(newGraphicObject.Id))
        {
            _stickyShapesObservable.StickNewGraphicObject(newGraphicObject);
        }
    }

    public bool IsObjectAlreadyStuck(int graphicObjectIdToCheck)
    {
        return _stickyShapesObservable.IsObjectAlreadyStuck(graphicObjectIdToCheck);
    }

    public void UnstickGraphicObjectById(int unstuckGraphicObjectId)
    {
        _stickyShapesObservable.UnstickGraphicObjectById(unstuckGraphicObjectId);
    }

    public void UnstickFromStuckGraphicObjects()
    {
        _stickyShapesObservable.UnstickFromStuckGraphicObjects(Id);
    }
}

public class StickyShapesObservable
{
    private readonly GraphicObjectsList _stuckObjectsList = new();

    public void StickNewGraphicObject(GraphicObject newGraphicObject)
    {
        _stuckObjectsList.Add(newGraphicObject);
    }

    public bool IsObjectAlreadyStuck(int graphicObjectIdToCheck)
    {
        for (var i = _stuckObjectsList.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            if (i.Current.Id == graphicObjectIdToCheck)
            {
                return true;
            }
        }

        return false;
    }

    public void UnstickGraphicObjectById(int unstuckGraphicObjectId)
    {
        for (var i = _stuckObjectsList.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            if (i.Current.Id == unstuckGraphicObjectId)
            {
                _stuckObjectsList.RemovePointerElement(i);
            }
        }
    }

    public void UnstickFromStuckGraphicObjects(int thisId)
    {
        var pointer = _stuckObjectsList.GetPointerOnBeginning();
        while (!pointer.IsBorderReached())
        {
            pointer.Current.UnstickGraphicObjectById(thisId);
            _stuckObjectsList.RemovePointerElement(pointer);
            pointer.MoveNext();
        }
    }

    public void NotifyStuckObjectsAboutMoving(int thisId, Point moveVector, Size backgroundSize)
    {
        for (var i = _stuckObjectsList.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            if (i.Current.Id != thisId && i.Current.CanBeNotifiedMoved)
            {
                i.Current.TryMove(moveVector, backgroundSize);
            }
        }
    }

    public void ResetAllCanBeNotifiedMovedFlags()
    {
        for (var i = _stuckObjectsList.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            if (!i.Current.CanBeNotifiedMoved)
            {
                i.Current.ResetCanBeNotifiedMoved();
            }
        }
    }
}