using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using CustomDoublyLinkedListLibrary;

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
    private readonly StickyShapesObservable _stickyShapesObservable = new StickyShapesObservable();
    public abstract bool IsSticky { get; }
    public abstract bool IsGroup { get; }
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
        _stickyShapesObservable.NotifyAllAboutMoving(Id, moveVector, backgroundSize);
        MoveWithoutNotifying(moveVector);
    }

    public bool IsAnyPointInside(Point[] pointsToCheck)
    {
        return pointsToCheck.Any(point => IsPointInside(point));
    }

    public void NotifiedTryMoveWithExceptions(Point moveVector, Size backgroundSize,
        CustomDoublyLinkedList<int> otherExceptions)
    {
        _stickyShapesObservable.NotifyAllAboutMovingWithExceptions(Id, moveVector, backgroundSize, otherExceptions);
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
        _stickyShapesObservable.StickNewGraphicObject(Id, newGraphicObject);
    }


    public bool IntersectWithCommonObject(GraphicObjectsList objectsToCheck)
    {
        for (var i = _stickyShapesObservable.GetAllStuckObjects().GetPointerOnBeginning();
             !i.IsBorderReached();
             i.MoveNext())
        {
            for (var j = objectsToCheck.GetPointerOnBeginning(); !j.IsBorderReached(); j.MoveNext())
            {
                if (i.Current.Id == j.Current.Id && (i.Current.IsAnyPointInside(ContourPoints) ||
                                                     IsAnyPointInside(i.Current.ContourPoints)))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void NotifiedStickNewGraphicObjectWithExceptions(GraphicObject newGraphicObject,
        CustomDoublyLinkedList<int> otherExceptions)
    {
        _stickyShapesObservable.NotifyAllAboutNewObjectStickWithExceptions(Id, newGraphicObject, otherExceptions);
        _stickyShapesObservable.StickNewGraphicObjectWithoutNotify(Id, newGraphicObject);
    }

    public bool IsObjectAlreadyStuck(int graphicObjectIdToCheck)
    {
        return _stickyShapesObservable.IsObjectAlreadyStuck(graphicObjectIdToCheck);
    }

    public void UnstickGraphicObjectById(int unstuckGraphicObjectId)
    {
        _stickyShapesObservable.UnstickGraphicObjectById(unstuckGraphicObjectId);
    }

    public void UnstickGraphicObjectByIdWithoutNotify(int unstuckGraphicObjectId)
    {
        _stickyShapesObservable.UnstickGraphicObjectByIdWithoutNotify(unstuckGraphicObjectId);
    }

    public void UnstickFromStuckGraphicObjects()
    {
        _stickyShapesObservable.UnstickFromStuckGraphicObjects(Id);
    }

    public GraphicObjectsList GetAllStuckObjects()
    {
        return _stickyShapesObservable.GetAllStuckObjects();
    }
}

public class StickyShapesObservable
{
    private readonly GraphicObjectsList _stuckObjectsList = new();

    public void StickNewGraphicObject(int thisId, GraphicObject newGraphicObject)
    {
        NotifyAllAboutNewObjectStick(thisId, newGraphicObject);
        StickNewGraphicObjectWithoutNotify(thisId, newGraphicObject);
    }

    public void StickNewGraphicObjectWithoutNotify(int thisId, GraphicObject newGraphicObject)
    {
        _stuckObjectsList.Add(newGraphicObject);
        for (var i = newGraphicObject.GetAllStuckObjects().GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            if (thisId != i.Current.Id && !IsObjectAlreadyStuck(i.Current.Id))
            {
                _stuckObjectsList.Add(i.Current);
            }
        }
    }

    public GraphicObjectsList GetAllStuckObjects()
    {
        return _stuckObjectsList;
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
        bool isUnstuckGraphicObjectSticky = false;
        bool isUnstuckGraphicObjectStickyOnlyOne = true;
        for (var i = _stuckObjectsList.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            if (i.Current.IsSticky)
            {
                if (unstuckGraphicObjectId == i.Current.Id)
                {
                    isUnstuckGraphicObjectSticky = true;
                }
                else
                {
                    isUnstuckGraphicObjectStickyOnlyOne = false;
                }
            }
        }

        if (isUnstuckGraphicObjectSticky && isUnstuckGraphicObjectStickyOnlyOne)
        {
            var pointer = _stuckObjectsList.GetPointerOnBeginning();
            while (!pointer.IsBorderReached())
            {
                _stuckObjectsList.RemovePointerElement(pointer);
                pointer.MoveNext();
            }
        }
        else
        {
            NotifyAllAboutUnstuck(unstuckGraphicObjectId);
            UnstickGraphicObjectByIdWithoutNotify(unstuckGraphicObjectId);
        }
    }

    public void UnstickGraphicObjectByIdWithoutNotify(int unstuckGraphicObjectId)
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

    public void NotifyAllAboutMoving(int thisId, Point moveVector, Size backgroundSize)
    {
        for (var i = _stuckObjectsList.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            i.Current.NotifiedTryMoveWithExceptions(moveVector, backgroundSize, GetExceptionsIdsList(thisId));
        }
    }

    public void NotifyAllAboutMovingWithExceptions(int thisId, Point moveVector, Size backgroundSize,
        CustomDoublyLinkedList<int> notifyExceptionIdsList)
    {
        for (var i = _stuckObjectsList.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            if (!notifyExceptionIdsList.Contains(i.Current.Id))
            {
                i.Current.NotifiedTryMoveWithExceptions(moveVector, backgroundSize,
                    GetExceptionsIdsList(thisId, notifyExceptionIdsList));
            }
        }
    }

    private void NotifyAllAboutNewObjectStick(int thisId, GraphicObject newGraphicObject)
    {
        for (var i = _stuckObjectsList.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            i.Current.NotifiedStickNewGraphicObjectWithExceptions(newGraphicObject, GetExceptionsIdsList(thisId));
        }
    }

    public void NotifyAllAboutNewObjectStickWithExceptions(int thisId, GraphicObject newGraphicObject,
        CustomDoublyLinkedList<int> notifyExceptionIdsList)
    {
        for (var i = _stuckObjectsList.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            if (!notifyExceptionIdsList.Contains(i.Current.Id))
            {
                i.Current.NotifiedStickNewGraphicObjectWithExceptions(newGraphicObject,
                    GetExceptionsIdsList(thisId, notifyExceptionIdsList));
            }
        }
    }

    private CustomDoublyLinkedList<int> GetStuckIdsList()
    {
        var idList = new CustomDoublyLinkedList<int>();
        for (var i = _stuckObjectsList.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            idList.Add(i.Current.Id);
        }

        return idList;
    }

    private CustomDoublyLinkedList<int> GetExceptionsIdsList(int thisId)
    {
        var idList = GetStuckIdsList();
        idList.Add(thisId);
        return idList;
    }

    private CustomDoublyLinkedList<int> GetExceptionsIdsList(int thisId, CustomDoublyLinkedList<int> otherExceptions)
    {
        for (var i = _stuckObjectsList.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            if (!otherExceptions.Contains(i.Current.Id))
            {
                otherExceptions.Add(i.Current.Id);
            }
        }

        if (!otherExceptions.Contains(thisId))
        {
            otherExceptions.Add(thisId);
        }

        return otherExceptions;
    }

    private void NotifyAllAboutUnstuck(int unstuckGraphicObjectId)
    {
        for (var i = _stuckObjectsList.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            i.Current.UnstickGraphicObjectByIdWithoutNotify(unstuckGraphicObjectId);
        }
    }
}