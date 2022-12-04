using System;
using System.Drawing;

namespace USATU_OOP_LW_8;

public class GraphicObjectsHandler
{
    private readonly GraphicObjectsList _graphicObjects = new();
    private bool _isMultipleSelectionEnabled;
    private readonly Size _backgroundSize;
    private readonly GraphicObjectsAbstractFactory _graphicObjectsFactory = new GraphicObjectsFactory();
    private readonly StorageTools _storageTools;

    public GraphicObjectsHandler(Size backgroundSize, string selectedFile)
    {
        _backgroundSize = backgroundSize;
        _storageTools = new StorageTools(selectedFile);
        if (_storageTools.IsFileExists())
        {
            try
            {
                _graphicObjects.ParseGraphicObjects(_storageTools.GetFormattedDataFromStorage(),
                    _graphicObjectsFactory);
            }
            catch (Exception e)
            {
                // ignored
            }
        }
    }

    public void JoinSelectedGraphicObject()
    {
        if (IsOnlySingleGeometricObjectSelected())
        {
            return;
        }

        var newGraphicObjectGroup = new GraphicObjectGroup(_graphicObjectsFactory);
        for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            if (i.Current.IsObjectSelected())
            {
                newGraphicObjectGroup.AddGraphicObject(i.Current);
                _graphicObjects.RemovePointerElement(i);
            }
        }

        _graphicObjects.Add(newGraphicObjectGroup);
        UnselectAll();
    }

    public void SeparateSelectedGraphicObjects()
    {
        for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            if (i.Current.IsObjectSelected() && i.Current.IsGroup())
            {
                var currentGroupList = ((GraphicObjectGroup) i.Current).GetAllGraphicObjects();
                _graphicObjects.InsertListBeforePointer(currentGroupList, i);
                _graphicObjects.RemovePointerElement(i);
            }
        }

        UnselectAll();
    }

    public void DrawOnGraphics(Graphics graphics)
    {
        for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            i.Current.DrawOnGraphics(graphics);
        }
    }

    public void EnableMultipleSelection()
    {
        _isMultipleSelectionEnabled = true;
    }

    public void DisableMultipleSelection()
    {
        _isMultipleSelectionEnabled = false;
    }

    public bool TryProcessSelectionClick(Point clickPoint)
    {
        bool wasOnObject = false;
        for (var i = _graphicObjects.GetPointerOnEnd(); !i.IsBorderReached(); i.MovePrevious())
        {
            if (i.Current.IsPointInside(clickPoint))
            {
                wasOnObject = true;
                if (!i.Current.IsObjectSelected() && !_isMultipleSelectionEnabled)
                {
                    UnselectAll();
                }

                i.Current.ProcessClick();
                break;
            }
        }

        return wasOnObject;
    }

    public void AddFigure(GraphicObjectsTypes graphicObjectsTypeType, Color color, Point location)
    {
        Figure newFigure = null;
        switch (graphicObjectsTypeType)
        {
            case GraphicObjectsTypes.Circle:
                newFigure = new Circle(color, location);
                break;
            case GraphicObjectsTypes.Square:
                newFigure = new Square(color, location);
                break;
            case GraphicObjectsTypes.Triangle:
                newFigure = new Triangle(color, location);
                break;
            case GraphicObjectsTypes.Pentagon:
                newFigure = new Pentagon(color, location);
                break;
        }

        if (!newFigure.IsFigureOutside(_backgroundSize))
        {
            _graphicObjects.Add(newFigure);
        }

        UnselectAll();
    }

    public void ProcessColorClick(Point clickLocation, Color color)
    {
        bool wasColored = false;
        for (var i = _graphicObjects.GetPointerOnEnd(); !i.IsBorderReached(); i.MovePrevious())
        {
            if (i.Current.IsPointInside(clickLocation))
            {
                if (i.Current.IsObjectSelected())
                {
                    for (var k = _graphicObjects.GetPointerOnBeginning(); !k.IsBorderReached(); k.MoveNext())
                    {
                        if (k.Current.IsObjectSelected())
                        {
                            k.Current.Color(color);
                        }
                    }
                }
                else
                {
                    i.Current.Color(color);
                    UnselectAll();
                    i.Current.Select();
                }

                wasColored = true;
                break;
            }
        }

        if (!wasColored)
        {
            UnselectAll();
        }
    }

    public void ResizeSelectedFigures(int changeSizeK, ResizeAction resizeAction)
    {
        for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            if (i.Current.IsObjectSelected() && i.Current.IsResizePossible(changeSizeK, resizeAction, _backgroundSize))
            {
                i.Current.Resize(changeSizeK, resizeAction);
            }
        }
    }

    public void MoveSelectedFigures(Point moveVector)
    {
        for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            if (i.Current.IsObjectSelected() && i.Current.IsMovePossible(moveVector, _backgroundSize))
            {
                i.Current.Move(moveVector);
            }
        }
    }

    public void DeleteAllSelected()
    {
        for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            if (i.Current.IsObjectSelected())
            {
                _graphicObjects.RemovePointerElement(i);
            }
        }
    }

    public void StoreData()
    {
        _storageTools.WriteDataToStorage(_graphicObjects.PrepareDataToStore());
    }

    private void UnselectAll()
    {
        for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            if (i.Current.IsObjectSelected())
            {
                i.Current.Unselect();
            }
        }
    }

    private bool IsOnlySingleGeometricObjectSelected()
    {
        bool wasOneSelectedObjectPassed = false;
        for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            if (!i.Current.IsObjectSelected()) continue;
            if (wasOneSelectedObjectPassed)
            {
                return false;
            }
            else
            {
                wasOneSelectedObjectPassed = true;
            }
        }

        return true;
    }
}