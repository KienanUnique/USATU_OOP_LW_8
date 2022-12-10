using System.Drawing;

namespace USATU_OOP_LW_8;

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

    public void UnstickFromAllStuckGraphicObjects(int thisId)
    {
        var pointer = _stuckObjectsList.GetPointerOnBeginning();
        while (!pointer.IsBorderReached())
        {
            pointer.Current.UnstickGraphicObjectById(thisId);
            _stuckObjectsList.RemovePointerElement(pointer);
            pointer.MoveNext();
        }
    }

    public void NotifyStuckObjectsAboutMoving(Point moveVector, Size backgroundSize)
    {
        for (var i = _stuckObjectsList.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            if (i.Current.CanBeNotifiedMoved)
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