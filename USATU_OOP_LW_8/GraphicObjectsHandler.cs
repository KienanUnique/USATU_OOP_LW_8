using System;
using System.Drawing;
using System.Windows.Forms;
using USATU_OOP_LW_8.Enums;
using USATU_OOP_LW_8.Factories;
using USATU_OOP_LW_8.Figures;
using USATU_OOP_LW_8.Observers;

namespace USATU_OOP_LW_8;

public class GraphicObjectsHandler
{
    public delegate void OnTreeNeedUpdate(TreeNode treeNode);

    public event OnTreeNeedUpdate TreeNeedUpdate;

    private GraphicObjectsList _graphicObjects = new();
    private bool _isMultipleSelectionEnabled;
    private readonly Size _backgroundSize;
    private readonly GraphicObjectsAbstractFactory _graphicObjectsFactory = new GraphicObjectsFactory();
    private readonly FiguresAbstractFactory _figuresFactory = new FiguresFactory();
    private StorageTools _storageTools;
    private GraphicObjectsListObserverTreeViewUpdater _graphicObjectsListObserver;

    public GraphicObjectsHandler(Size backgroundSize)
    {
        _backgroundSize = backgroundSize;
    }

    public void ReadDataFromStorage(string selectedFile)
    {
        _storageTools = new StorageTools(selectedFile);
        if (!_storageTools.IsFileExists()) return;
        BankOfIds.GetInstance().Clear();
        try
        {
            _graphicObjects = new GraphicObjectsList();
            _graphicObjectsListObserver = new GraphicObjectsListObserverTreeViewUpdater(_graphicObjects);
            _graphicObjectsListObserver.TreeNeedUpdate += ThrowTreeUpdate;
            _graphicObjects.ParseGraphicObjects(_storageTools.GetFormattedDataFromStorage(),
                _graphicObjectsFactory);
        }
        catch (Exception)
        {
            // ignored
        }

        ProcessGraphicObjectsIntersections();
        _graphicObjectsListObserver.UpdateChanges();
    }

    private void ThrowTreeUpdate(TreeNode treeNode)
    {
        TreeNeedUpdate?.Invoke(treeNode);
    }

    public void ProcessTreeActionToObjects(TreeNode selectedNode, bool isChecked)
    {
        _graphicObjectsListObserver.ProcessTreeSelectionToObjects(selectedNode, isChecked);
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
        _graphicObjectsListObserver.UpdateChanges();
    }

    public void SeparateSelectedGraphicObjects()
    {
        for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            if (i.Current.IsObjectSelected() && i.Current.IsGroup)
            {
                var currentGroup = ((GraphicObjectGroup) i.Current);
                _graphicObjects.InsertListBeforePointer(currentGroup.GetAllGraphicObjects(), i);
                currentGroup.ReturnOnlyGroupIdToBank();
                _graphicObjects.RemovePointerElement(i);
            }
        }

        UnselectAll();
        _graphicObjectsListObserver.UpdateChanges();
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
                _graphicObjectsListObserver.UpdateChanges();
                break;
            }
        }

        return wasOnObject;
    }

    public void AddFigure(Enum graphicObjectsTypeType, Color color, Point location)
    {
        Figure newFigure = _figuresFactory.ParseFigure(graphicObjectsTypeType, new FigureCreationArguments(color, location));
        if (!newFigure.IsFigureOutside(_backgroundSize))
        {
            _graphicObjects.Add(newFigure);
            ProcessGraphicObjectsIntersections();
        }

        UnselectAll();
        _graphicObjectsListObserver.UpdateChanges();
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

    public void ResizeSelectedFigures(int changeSizeK, ResizeActionTypes resizeActionTypes)
    {
        for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            if (i.Current.IsObjectSelected() && i.Current.IsResizePossible(changeSizeK, resizeActionTypes, _backgroundSize))
            {
                i.Current.Resize(changeSizeK, resizeActionTypes);
                ProcessGraphicObjectsIntersections();
            }
        }
    }

    public void MoveSelectedFigures(Point moveVector)
    {
        for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            if (i.Current.IsObjectSelected() && i.Current.IsMovePossible(moveVector, _backgroundSize))
            {
                i.Current.Move(moveVector, _backgroundSize);
                ProcessGraphicObjectsIntersections();
            }
        }
    }

    public void DeleteAllSelected()
    {
        for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            if (i.Current.IsObjectSelected())
            {
                i.Current.ReturnIdToBank();
                i.Current.UnstickFromAllStuckGraphicObjects();
                _graphicObjects.RemovePointerElement(i);
            }
        }

        _graphicObjectsListObserver.UpdateChanges();
    }

    public void StoreData()
    {
        _storageTools.WriteDataToStorage(_graphicObjects.PrepareDataToStore());
    }

    private void UnselectAll()
    {
        bool needObserverUpdate = false;
        for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            if (i.Current.IsObjectSelected())
            {
                i.Current.Unselect();
                needObserverUpdate = true;
            }
        }

        if (needObserverUpdate)
        {
            _graphicObjectsListObserver.UpdateChanges();
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

    private void ProcessGraphicObjectsIntersections()
    {
        for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            for (var j = i.GetPointerOnNextElement(); !j.IsBorderReached(); j.MoveNext())
            {
                if (j.Current.Id != i.Current.Id &&
                    (!i.Current.IsObjectAlreadyStuck(j.Current.Id) ||
                     !j.Current.IsObjectAlreadyStuck(i.Current.Id)) &&
                    (i.Current.IsAnyPointInside(j.Current.ContourPoints) ||
                     j.Current.IsAnyPointInside(i.Current.ContourPoints)))
                {
                    j.Current.StickNewGraphicObject(i.Current);
                    i.Current.StickNewGraphicObject(j.Current);
                }
                else if (j.Current.Id != i.Current.Id &&
                         (i.Current.IsObjectAlreadyStuck(j.Current.Id) ||
                          j.Current.IsObjectAlreadyStuck(i.Current.Id)) &&
                         !i.Current.IsAnyPointInside(j.Current.ContourPoints) &&
                         !j.Current.IsAnyPointInside(i.Current.ContourPoints))
                {
                    j.Current.UnstickGraphicObjectById(i.Current.Id);
                    i.Current.UnstickGraphicObjectById(j.Current.Id);
                }
            }
        }
    }
}