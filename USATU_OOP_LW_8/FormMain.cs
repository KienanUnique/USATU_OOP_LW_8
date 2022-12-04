using System;
using System.Drawing;
using System.Windows.Forms;

namespace USATU_OOP_LW_8
{
    public partial class FormMain : Form
    {
        private const int ChangeSizeK = 2;
        private const int MoveLength = 10;
        private readonly Color _startColor = Color.Coral;
        private GraphicObjectsHandler _graphicObjectsHandler;
        private bool _wasControlAlreadyPressed;
        private bool _wasFileLoaded = false;

        public FormMain()
        {
            InitializeComponent();
            this.KeyPreview = true;
            colorDialog.Color = _startColor;
            controlCurrentColor.BackColor = _startColor;
            buttonSave.Enabled = false;
            panelAllPaintObjects.Enabled = false;
        }

        private void OpenLoadFromFileDialog()
        {
            panelAllPaintObjects.Enabled = false;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "C:\\Users\\wi\\Documents\\OOP editor saved files";
            openFileDialog.Filter = "Database files (*.txt)|*.txt";
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _graphicObjectsHandler =
                    new GraphicObjectsHandler(panelForDrawing.DisplayRectangle.Size, openFileDialog.FileName);
                panelForDrawing.Paint += panelForDrawing_Paint;
                _wasFileLoaded = true;
                buttonSave.Enabled = true;
                panelAllPaintObjects.Enabled = true;
            }
            else
            {
                if (_wasFileLoaded)
                {
                    panelAllPaintObjects.Enabled = true;
                }
            }
        }

        private void panelForDrawing_Paint(object sender, PaintEventArgs e)
        {
            _graphicObjectsHandler.DrawOnGraphics(e.Graphics);
        }

        private void panelForDrawing_Update()
        {
            panelForDrawing.Invalidate();
        }

        private GraphicObjectsTypes GetSelectedFigureEnum()
        {
            if (radioButtonCircle.Checked)
            {
                return GraphicObjectsTypes.Circle;
            }
            else if (radioButtonTriangle.Checked)
            {
                return GraphicObjectsTypes.Triangle;
            }
            else if (radioButtonSquare.Checked)
            {
                return GraphicObjectsTypes.Square;
            }
            else if (radioButtonPentagon.Checked)
            {
                return GraphicObjectsTypes.Pentagon;
            }

            return GraphicObjectsTypes.None;
        }

        private void panelForDrawing_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!_graphicObjectsHandler.TryProcessSelectionClick(e.Location))
                {
                    _graphicObjectsHandler.AddFigure(GetSelectedFigureEnum(), colorDialog.Color, e.Location);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                _graphicObjectsHandler.ProcessColorClick(e.Location, colorDialog.Color);
            }

            panelForDrawing_Update();
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey when !_wasControlAlreadyPressed:
                    _graphicObjectsHandler.EnableMultipleSelection();
                    _wasControlAlreadyPressed = true;
                    break;
                case Keys.W:
                    _graphicObjectsHandler.MoveSelectedFigures(new Point(0, -1 * MoveLength));
                    panelForDrawing_Update();
                    break;
                case Keys.S:
                    _graphicObjectsHandler.MoveSelectedFigures(new Point(0, MoveLength));
                    panelForDrawing_Update();
                    break;
                case Keys.A:
                    _graphicObjectsHandler.MoveSelectedFigures(new Point(-1 * MoveLength, 0));
                    panelForDrawing_Update();
                    break;
                case Keys.D:
                    _graphicObjectsHandler.MoveSelectedFigures(new Point(MoveLength, 0));
                    panelForDrawing_Update();
                    break;
            }
        }

        private void FormMain_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey:
                    _graphicObjectsHandler.DisableMultipleSelection();
                    _wasControlAlreadyPressed = false;
                    break;
                case Keys.Delete:
                    _graphicObjectsHandler.DeleteAllSelected();
                    panelForDrawing_Update();
                    break;
                case Keys.Oemplus:
                    _graphicObjectsHandler.ResizeSelectedFigures(ChangeSizeK, ResizeAction.Increase);
                    panelForDrawing_Update();
                    break;
                case Keys.OemMinus:
                    _graphicObjectsHandler.ResizeSelectedFigures(ChangeSizeK, ResizeAction.Decrease);
                    panelForDrawing_Update();
                    break;
                case Keys.J:
                    _graphicObjectsHandler.JoinSelectedGraphicObject();
                    panelForDrawing_Update();
                    break;
                case Keys.U:
                    _graphicObjectsHandler.SeparateSelectedGraphicObjects();
                    panelForDrawing_Update();
                    break;
            }
        }

        private void buttonChooseColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                controlCurrentColor.BackColor = colorDialog.Color;
            }
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_wasFileLoaded)
            {
                _graphicObjectsHandler.StoreData();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            _graphicObjectsHandler.StoreData();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenLoadFromFileDialog();
        }
    }
}