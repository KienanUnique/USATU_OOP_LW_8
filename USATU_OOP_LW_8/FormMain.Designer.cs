namespace USATU_OOP_LW_8
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

         #region Windows Form Designer generated code

         /// <summary>
         /// Required method for Designer support - do not modify
         /// the contents of this method with the code editor.
         /// </summary>
         private void InitializeComponent()
         {
             System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
             this.colorDialog = new System.Windows.Forms.ColorDialog();
             this.panelAllPaintObjects = new System.Windows.Forms.Panel();
             this.treeViewGraphicObjects = new System.Windows.Forms.TreeView();
             this.textBoxControlPromts = new System.Windows.Forms.TextBox();
             this.groupBoxChooseFigure = new System.Windows.Forms.GroupBox();
             this.radioButtonPentagon = new System.Windows.Forms.RadioButton();
             this.radioButtonTriangle = new System.Windows.Forms.RadioButton();
             this.radioButtonSquare = new System.Windows.Forms.RadioButton();
             this.radioButtonCircle = new System.Windows.Forms.RadioButton();
             this.panelColorChoose = new System.Windows.Forms.Panel();
             this.buttonChooseColor = new System.Windows.Forms.Button();
             this.controlCurrentColor = new System.Windows.Forms.Control();
             this.labelCurrentColor = new System.Windows.Forms.Label();
             this.panelForDrawing = new System.Windows.Forms.Panel();
             this.buttonSave = new System.Windows.Forms.Button();
             this.buttonLoad = new System.Windows.Forms.Button();
             this.radioButtonStickySquare = new System.Windows.Forms.RadioButton();
             this.panelAllPaintObjects.SuspendLayout();
             this.groupBoxChooseFigure.SuspendLayout();
             this.panelColorChoose.SuspendLayout();
             this.SuspendLayout();
             // 
             // panelAllPaintObjects
             // 
             this.panelAllPaintObjects.Controls.Add(this.treeViewGraphicObjects);
             this.panelAllPaintObjects.Controls.Add(this.textBoxControlPromts);
             this.panelAllPaintObjects.Controls.Add(this.groupBoxChooseFigure);
             this.panelAllPaintObjects.Controls.Add(this.panelColorChoose);
             this.panelAllPaintObjects.Controls.Add(this.panelForDrawing);
             this.panelAllPaintObjects.Location = new System.Drawing.Point(12, 12);
             this.panelAllPaintObjects.Name = "panelAllPaintObjects";
             this.panelAllPaintObjects.Size = new System.Drawing.Size(1074, 440);
             this.panelAllPaintObjects.TabIndex = 0;
             // 
             // treeViewGraphicObjects
             // 
             this.treeViewGraphicObjects.CheckBoxes = true;
             this.treeViewGraphicObjects.Location = new System.Drawing.Point(3, 3);
             this.treeViewGraphicObjects.Name = "treeViewGraphicObjects";
             this.treeViewGraphicObjects.Size = new System.Drawing.Size(280, 426);
             this.treeViewGraphicObjects.TabIndex = 8;
             this.treeViewGraphicObjects.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewGraphicObjects_AfterCheck);
             // 
             // textBoxControlPromts
             // 
             this.textBoxControlPromts.Location = new System.Drawing.Point(915, 251);
             this.textBoxControlPromts.Multiline = true;
             this.textBoxControlPromts.Name = "textBoxControlPromts";
             this.textBoxControlPromts.ReadOnly = true;
             this.textBoxControlPromts.Size = new System.Drawing.Size(150, 178);
             this.textBoxControlPromts.TabIndex = 7;
             this.textBoxControlPromts.Text = resources.GetString("textBoxControlPromts.Text");
             this.textBoxControlPromts.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
             // 
             // groupBoxChooseFigure
             // 
             this.groupBoxChooseFigure.Controls.Add(this.radioButtonStickySquare);
             this.groupBoxChooseFigure.Controls.Add(this.radioButtonPentagon);
             this.groupBoxChooseFigure.Controls.Add(this.radioButtonTriangle);
             this.groupBoxChooseFigure.Controls.Add(this.radioButtonSquare);
             this.groupBoxChooseFigure.Controls.Add(this.radioButtonCircle);
             this.groupBoxChooseFigure.Location = new System.Drawing.Point(915, 76);
             this.groupBoxChooseFigure.Name = "groupBoxChooseFigure";
             this.groupBoxChooseFigure.Size = new System.Drawing.Size(150, 169);
             this.groupBoxChooseFigure.TabIndex = 6;
             this.groupBoxChooseFigure.TabStop = false;
             this.groupBoxChooseFigure.Text = "Current figure:";
             // 
             // radioButtonPentagon
             // 
             this.radioButtonPentagon.Location = new System.Drawing.Point(6, 109);
             this.radioButtonPentagon.Name = "radioButtonPentagon";
             this.radioButtonPentagon.Size = new System.Drawing.Size(104, 24);
             this.radioButtonPentagon.TabIndex = 0;
             this.radioButtonPentagon.Text = "Pentagon";
             this.radioButtonPentagon.UseVisualStyleBackColor = true;
             // 
             // radioButtonTriangle
             // 
             this.radioButtonTriangle.Location = new System.Drawing.Point(6, 49);
             this.radioButtonTriangle.Name = "radioButtonTriangle";
             this.radioButtonTriangle.Size = new System.Drawing.Size(104, 24);
             this.radioButtonTriangle.TabIndex = 0;
             this.radioButtonTriangle.Text = "Triangle";
             this.radioButtonTriangle.UseVisualStyleBackColor = true;
             // 
             // radioButtonSquare
             // 
             this.radioButtonSquare.Location = new System.Drawing.Point(6, 79);
             this.radioButtonSquare.Name = "radioButtonSquare";
             this.radioButtonSquare.Size = new System.Drawing.Size(104, 24);
             this.radioButtonSquare.TabIndex = 0;
             this.radioButtonSquare.Text = "Square";
             this.radioButtonSquare.UseVisualStyleBackColor = true;
             // 
             // radioButtonCircle
             // 
             this.radioButtonCircle.Checked = true;
             this.radioButtonCircle.Location = new System.Drawing.Point(6, 19);
             this.radioButtonCircle.Name = "radioButtonCircle";
             this.radioButtonCircle.Size = new System.Drawing.Size(104, 24);
             this.radioButtonCircle.TabIndex = 0;
             this.radioButtonCircle.TabStop = true;
             this.radioButtonCircle.Text = "Circle";
             this.radioButtonCircle.UseVisualStyleBackColor = true;
             // 
             // panelColorChoose
             // 
             this.panelColorChoose.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
             this.panelColorChoose.Controls.Add(this.buttonChooseColor);
             this.panelColorChoose.Controls.Add(this.controlCurrentColor);
             this.panelColorChoose.Controls.Add(this.labelCurrentColor);
             this.panelColorChoose.Location = new System.Drawing.Point(915, 3);
             this.panelColorChoose.Name = "panelColorChoose";
             this.panelColorChoose.Size = new System.Drawing.Size(150, 67);
             this.panelColorChoose.TabIndex = 5;
             // 
             // buttonChooseColor
             // 
             this.buttonChooseColor.Location = new System.Drawing.Point(3, 34);
             this.buttonChooseColor.Name = "buttonChooseColor";
             this.buttonChooseColor.Size = new System.Drawing.Size(144, 23);
             this.buttonChooseColor.TabIndex = 2;
             this.buttonChooseColor.Text = "Change color";
             this.buttonChooseColor.UseVisualStyleBackColor = true;
             this.buttonChooseColor.Click += new System.EventHandler(this.buttonChooseColor_Click);
             // 
             // controlCurrentColor
             // 
             this.controlCurrentColor.BackColor = System.Drawing.Color.Black;
             this.controlCurrentColor.Location = new System.Drawing.Point(82, 3);
             this.controlCurrentColor.Name = "controlCurrentColor";
             this.controlCurrentColor.Size = new System.Drawing.Size(25, 25);
             this.controlCurrentColor.TabIndex = 1;
             // 
             // labelCurrentColor
             // 
             this.labelCurrentColor.Location = new System.Drawing.Point(3, 5);
             this.labelCurrentColor.Name = "labelCurrentColor";
             this.labelCurrentColor.Size = new System.Drawing.Size(73, 23);
             this.labelCurrentColor.TabIndex = 0;
             this.labelCurrentColor.Text = "Current color:";
             this.labelCurrentColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
             // 
             // panelForDrawing
             // 
             this.panelForDrawing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
             this.panelForDrawing.Location = new System.Drawing.Point(289, 3);
             this.panelForDrawing.Name = "panelForDrawing";
             this.panelForDrawing.Size = new System.Drawing.Size(620, 426);
             this.panelForDrawing.TabIndex = 4;
             this.panelForDrawing.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelForDrawing_MouseClick);
             // 
             // buttonSave
             // 
             this.buttonSave.Location = new System.Drawing.Point(15, 463);
             this.buttonSave.Name = "buttonSave";
             this.buttonSave.Size = new System.Drawing.Size(131, 23);
             this.buttonSave.TabIndex = 1;
             this.buttonSave.Text = "Save";
             this.buttonSave.UseVisualStyleBackColor = true;
             this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
             // 
             // buttonLoad
             // 
             this.buttonLoad.Location = new System.Drawing.Point(152, 463);
             this.buttonLoad.Name = "buttonLoad";
             this.buttonLoad.Size = new System.Drawing.Size(131, 23);
             this.buttonLoad.TabIndex = 1;
             this.buttonLoad.Text = "Load";
             this.buttonLoad.UseVisualStyleBackColor = true;
             this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
             // 
             // radioButtonStickySquare
             // 
             this.radioButtonStickySquare.Location = new System.Drawing.Point(6, 139);
             this.radioButtonStickySquare.Name = "radioButtonStickySquare";
             this.radioButtonStickySquare.Size = new System.Drawing.Size(104, 24);
             this.radioButtonStickySquare.TabIndex = 0;
             this.radioButtonStickySquare.Text = "Sticky square";
             this.radioButtonStickySquare.UseVisualStyleBackColor = true;
             // 
             // FormMain
             // 
             this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
             this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
             this.BackColor = System.Drawing.SystemColors.Control;
             this.ClientSize = new System.Drawing.Size(1098, 498);
             this.Controls.Add(this.buttonLoad);
             this.Controls.Add(this.buttonSave);
             this.Controls.Add(this.panelAllPaintObjects);
             this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
             this.Location = new System.Drawing.Point(15, 15);
             this.Name = "FormMain";
             this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
             this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
             this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyUp);
             this.panelAllPaintObjects.ResumeLayout(false);
             this.panelAllPaintObjects.PerformLayout();
             this.groupBoxChooseFigure.ResumeLayout(false);
             this.panelColorChoose.ResumeLayout(false);
             this.ResumeLayout(false);
         }

         private System.Windows.Forms.RadioButton radioButtonStickySquare;

         private System.Windows.Forms.TreeView treeViewGraphicObjects;

         private System.Windows.Forms.Panel panelAllPaintObjects;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonLoad;

        private System.Windows.Forms.TextBox textBoxControlPromts;

        private System.Windows.Forms.RadioButton radioButtonPentagon;

        private System.Windows.Forms.Button buttonChooseColor;

        private System.Windows.Forms.GroupBox groupBoxChooseFigure;
        private System.Windows.Forms.RadioButton radioButtonCircle;
        private System.Windows.Forms.RadioButton radioButtonSquare;
        private System.Windows.Forms.RadioButton radioButtonTriangle;

        private System.Windows.Forms.Panel panelColorChoose;
        private System.Windows.Forms.Label labelCurrentColor;
        private System.Windows.Forms.Control controlCurrentColor;

        private System.Windows.Forms.ColorDialog colorDialog;

        private System.Windows.Forms.Panel panelForDrawing;

        #endregion
    }
}