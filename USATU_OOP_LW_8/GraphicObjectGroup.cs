using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace USATU_OOP_LW_8
{
    public class GraphicObjectGroup : GraphicObject, IStickyObject
    {
        protected override string NamePrefix => "Group";
        public override bool IsGroup => true;
        public override bool IsSticky => _isSticky;
        private bool _isSticky;
        private readonly GraphicObjectsList _graphicObjects = new();
        private readonly GraphicObjectsAbstractFactory _graphicObjectsFactory;

        public override Point[] ContourPoints
        {
            get
            {
                var points = new List<Point>();
                for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
                {
                    points.AddRange(i.Current.ContourPoints);
                }

                return points.ToArray();
            }
        }

        public Point[] ContourStickyPoints
        {
            get
            {
                var points = new List<Point>();
                for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
                {
                    if (!i.Current.IsSticky) continue;
                    var stickyObject = (IStickyObject) i.Current;
                    points.AddRange(stickyObject.ContourStickyPoints);
                }

                return points.ToArray();
            }
        }

        public bool IsAnyPointInsideStickyObject(Point[] pointsToCheck)
        {
            for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                if (!i.Current.IsSticky) continue;
                var stickyObject = (IStickyObject) i.Current;
                if (stickyObject.IsAnyPointInsideStickyObject(pointsToCheck))
                {
                    return true;
                }
            }
            return false;
        }

        public GraphicObjectGroup(GraphicObjectsAbstractFactory graphicObjectsFactory)
        {
            _graphicObjectsFactory = graphicObjectsFactory;
            IsSelected = false;
        }

        public override void LoadData(StringReader dataStringReader)
        {
            IsSelected = false;
            _isSticky = false;
            _graphicObjects.ParseGraphicObjects(dataStringReader, _graphicObjectsFactory);
            for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                if (i.Current.IsSticky)
                {
                    _isSticky = true;
                    break;
                }
            }
        }

        public override bool IsFigureOutside(Size backgroundSize)
        {
            for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                if (i.Current.IsFigureOutside(backgroundSize))
                {
                    return true;
                }
            }

            return false;
        }

        public override void Color(Color newColor)
        {
            for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                i.Current.Color(newColor);
            }
        }

        public override bool IsResizePossible(int sizeK, ResizeAction resizeAction, Size backgroundSize)
        {
            for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                if (!i.Current.IsResizePossible(sizeK, resizeAction, backgroundSize))
                {
                    return false;
                }
            }

            return true;
        }

        public override void Resize(int sizeK, ResizeAction resizeAction)
        {
            for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                i.Current.Resize(sizeK, resizeAction);
            }
        }

        public override bool IsMovePossible(Point moveVector, Size backgroundSize)
        {
            for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                if (!i.Current.IsMovePossible(moveVector, backgroundSize))
                {
                    return false;
                }
            }

            return true;
        }

        public override void MoveWithoutNotifying(Point moveVector)
        {
            for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                i.Current.MoveWithoutNotifying(moveVector);
            }
        }

        public override void DrawOnGraphics(Graphics graphics)
        {
            for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                i.Current.DrawOnGraphics(graphics);
            }
        }

        public override bool IsObjectSelected()
        {
            return IsSelected;
        }

        public override void Select()
        {
            IsSelected = true;
            ChangeAllSelection(IsSelected);
        }

        public override void Unselect()
        {
            IsSelected = false;
            ChangeAllSelection(IsSelected);
        }

        public override void ProcessClick()
        {
            IsSelected = !IsSelected;
            ChangeAllSelection(IsSelected);
        }

        public override bool IsPointInside(Point pointToCheck)
        {
            for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                if (i.Current.IsPointInside(pointToCheck))
                {
                    return true;
                }
            }

            return false;
        }

        public override string PrepareDataToStore()
        {
            var dataStringBuilder = new StringBuilder();
            dataStringBuilder.AppendLine(PrefixGraphicObjectsType + GraphicObjectsTypes.Group);
            dataStringBuilder.Append(_graphicObjects.PrepareDataToStore());
            return dataStringBuilder.ToString();
        }

        public GraphicObjectsList GetAllGraphicObjects()
        {
            return _graphicObjects;
        }

        public void AddGraphicObject(GraphicObject newGraphicObject)
        {
            newGraphicObject.Unselect();
            if (newGraphicObject.IsSticky)
            {
                _isSticky = true;
            }

            _graphicObjects.Add(newGraphicObject);
        }

        public void ReturnOnlyGroupIdToBank()
        {
            base.ReturnIdToBank();
        }

        public override void ReturnIdToBank()
        {
            for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                i.Current.ReturnIdToBank();
            }

            base.ReturnIdToBank();
        }

        private void ChangeAllSelection(bool newIsSelected)
        {
            for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                if (!newIsSelected && i.Current.IsObjectSelected())
                {
                    i.Current.Unselect();
                }
                else if (newIsSelected && !i.Current.IsObjectSelected())
                {
                    i.Current.Select();
                }
            }
        }
    }
}