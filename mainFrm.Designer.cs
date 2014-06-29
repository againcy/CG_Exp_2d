namespace CG_Exp_2D
{
    partial class mainFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_workspace = new System.Windows.Forms.Panel();
            this.textBox_curCoords = new System.Windows.Forms.TextBox();
            this.label_curCoords = new System.Windows.Forms.Label();
            this.button_startDrawPolygon = new System.Windows.Forms.Button();
            this.button_endDrawPolygon = new System.Windows.Forms.Button();
            this.button_startDrawLine = new System.Windows.Forms.Button();
            this.panel_curColor = new System.Windows.Forms.Panel();
            this.button_drawCircle = new System.Windows.Forms.Button();
            this.textBox_circleRadius = new System.Windows.Forms.TextBox();
            this.textBox_X = new System.Windows.Forms.TextBox();
            this.textBox_Y = new System.Windows.Forms.TextBox();
            this.button_setCoor = new System.Windows.Forms.Button();
            this.listBox_graphics = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.button_drawRectangle = new System.Windows.Forms.Button();
            this.button_clipLines = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_chooseItem = new System.Windows.Forms.TextBox();
            this.button_output = new System.Windows.Forms.Button();
            this.button_clipPolygon = new System.Windows.Forms.Button();
            this.button_findIntersections = new System.Windows.Forms.Button();
            this.button_checkInPolygon = new System.Windows.Forms.Button();
            this.button_clearCanvas = new System.Windows.Forms.Button();
            this.button_drawControlPolygon = new System.Windows.Forms.Button();
            this.button_drawBezier = new System.Windows.Forms.Button();
            this.button_switchShowControl = new System.Windows.Forms.Button();
            this.button_fillArea = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panel_workspace
            // 
            this.panel_workspace.Location = new System.Drawing.Point(12, 12);
            this.panel_workspace.Name = "panel_workspace";
            this.panel_workspace.Size = new System.Drawing.Size(600, 600);
            this.panel_workspace.TabIndex = 0;
            this.panel_workspace.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_workspace_Paint);
            this.panel_workspace.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_workspace_MouseDown);
            // 
            // textBox_curCoords
            // 
            this.textBox_curCoords.Location = new System.Drawing.Point(677, 12);
            this.textBox_curCoords.Name = "textBox_curCoords";
            this.textBox_curCoords.Size = new System.Drawing.Size(100, 21);
            this.textBox_curCoords.TabIndex = 1;
            // 
            // label_curCoords
            // 
            this.label_curCoords.AutoSize = true;
            this.label_curCoords.Location = new System.Drawing.Point(618, 15);
            this.label_curCoords.Name = "label_curCoords";
            this.label_curCoords.Size = new System.Drawing.Size(53, 12);
            this.label_curCoords.TabIndex = 2;
            this.label_curCoords.Text = "当前坐标";
            // 
            // button_startDrawPolygon
            // 
            this.button_startDrawPolygon.Location = new System.Drawing.Point(618, 72);
            this.button_startDrawPolygon.Name = "button_startDrawPolygon";
            this.button_startDrawPolygon.Size = new System.Drawing.Size(100, 30);
            this.button_startDrawPolygon.TabIndex = 3;
            this.button_startDrawPolygon.Text = "绘制多边形";
            this.button_startDrawPolygon.UseVisualStyleBackColor = true;
            this.button_startDrawPolygon.Click += new System.EventHandler(this.button_startDrawPolygon_Click);
            // 
            // button_endDrawPolygon
            // 
            this.button_endDrawPolygon.Location = new System.Drawing.Point(724, 72);
            this.button_endDrawPolygon.Name = "button_endDrawPolygon";
            this.button_endDrawPolygon.Size = new System.Drawing.Size(100, 30);
            this.button_endDrawPolygon.TabIndex = 4;
            this.button_endDrawPolygon.Text = "多边形绘制结束";
            this.button_endDrawPolygon.UseVisualStyleBackColor = true;
            this.button_endDrawPolygon.Click += new System.EventHandler(this.button_endDrawPolygon_Click);
            // 
            // button_startDrawLine
            // 
            this.button_startDrawLine.Location = new System.Drawing.Point(618, 108);
            this.button_startDrawLine.Name = "button_startDrawLine";
            this.button_startDrawLine.Size = new System.Drawing.Size(100, 30);
            this.button_startDrawLine.TabIndex = 5;
            this.button_startDrawLine.Text = "绘制直线";
            this.button_startDrawLine.UseVisualStyleBackColor = true;
            this.button_startDrawLine.Click += new System.EventHandler(this.button_startDrawLine_Click);
            // 
            // panel_curColor
            // 
            this.panel_curColor.Location = new System.Drawing.Point(688, 582);
            this.panel_curColor.Name = "panel_curColor";
            this.panel_curColor.Size = new System.Drawing.Size(30, 30);
            this.panel_curColor.TabIndex = 7;
            this.panel_curColor.Click += new System.EventHandler(this.panel_curColor_Click);
            this.panel_curColor.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_curColor_Paint);
            // 
            // button_drawCircle
            // 
            this.button_drawCircle.Location = new System.Drawing.Point(618, 144);
            this.button_drawCircle.Name = "button_drawCircle";
            this.button_drawCircle.Size = new System.Drawing.Size(100, 30);
            this.button_drawCircle.TabIndex = 8;
            this.button_drawCircle.Text = "绘制圆";
            this.button_drawCircle.UseVisualStyleBackColor = true;
            this.button_drawCircle.Click += new System.EventHandler(this.button_drawCircle_Click);
            // 
            // textBox_circleRadius
            // 
            this.textBox_circleRadius.Location = new System.Drawing.Point(724, 150);
            this.textBox_circleRadius.Name = "textBox_circleRadius";
            this.textBox_circleRadius.Size = new System.Drawing.Size(100, 21);
            this.textBox_circleRadius.TabIndex = 9;
            this.textBox_circleRadius.Text = "请输入半径";
            this.textBox_circleRadius.Enter += new System.EventHandler(this.textBox_circleRadius_Enter);
            this.textBox_circleRadius.Leave += new System.EventHandler(this.textBox_circleRadius_Leave);
            // 
            // textBox_X
            // 
            this.textBox_X.Location = new System.Drawing.Point(620, 45);
            this.textBox_X.Name = "textBox_X";
            this.textBox_X.Size = new System.Drawing.Size(51, 21);
            this.textBox_X.TabIndex = 10;
            // 
            // textBox_Y
            // 
            this.textBox_Y.Location = new System.Drawing.Point(677, 45);
            this.textBox_Y.Name = "textBox_Y";
            this.textBox_Y.Size = new System.Drawing.Size(51, 21);
            this.textBox_Y.TabIndex = 11;
            // 
            // button_setCoor
            // 
            this.button_setCoor.Location = new System.Drawing.Point(734, 39);
            this.button_setCoor.Name = "button_setCoor";
            this.button_setCoor.Size = new System.Drawing.Size(76, 30);
            this.button_setCoor.TabIndex = 12;
            this.button_setCoor.Text = "设置坐标";
            this.button_setCoor.UseVisualStyleBackColor = true;
            this.button_setCoor.Click += new System.EventHandler(this.button_setCoor_Click);
            // 
            // listBox_graphics
            // 
            this.listBox_graphics.FormattingEnabled = true;
            this.listBox_graphics.ItemHeight = 12;
            this.listBox_graphics.Location = new System.Drawing.Point(834, 108);
            this.listBox_graphics.Name = "listBox_graphics";
            this.listBox_graphics.Size = new System.Drawing.Size(120, 232);
            this.listBox_graphics.TabIndex = 13;
            this.listBox_graphics.SelectedIndexChanged += new System.EventHandler(this.listBox_graphics_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(858, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "已绘制图元";
            // 
            // button_drawRectangle
            // 
            this.button_drawRectangle.Location = new System.Drawing.Point(618, 180);
            this.button_drawRectangle.Name = "button_drawRectangle";
            this.button_drawRectangle.Size = new System.Drawing.Size(100, 30);
            this.button_drawRectangle.TabIndex = 16;
            this.button_drawRectangle.Text = "绘制矩形";
            this.button_drawRectangle.UseVisualStyleBackColor = true;
            this.button_drawRectangle.Click += new System.EventHandler(this.button_drawRectangle_Click);
            // 
            // button_clipLines
            // 
            this.button_clipLines.Location = new System.Drawing.Point(618, 329);
            this.button_clipLines.Name = "button_clipLines";
            this.button_clipLines.Size = new System.Drawing.Size(100, 30);
            this.button_clipLines.TabIndex = 17;
            this.button_clipLines.Text = "裁剪直线";
            this.button_clipLines.UseVisualStyleBackColor = true;
            this.button_clipLines.Click += new System.EventHandler(this.button_clipLines_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(860, 347);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "选定图元";
            // 
            // textBox_chooseItem
            // 
            this.textBox_chooseItem.Location = new System.Drawing.Point(834, 367);
            this.textBox_chooseItem.Name = "textBox_chooseItem";
            this.textBox_chooseItem.Size = new System.Drawing.Size(120, 21);
            this.textBox_chooseItem.TabIndex = 19;
            // 
            // button_output
            // 
            this.button_output.Location = new System.Drawing.Point(618, 546);
            this.button_output.Name = "button_output";
            this.button_output.Size = new System.Drawing.Size(100, 30);
            this.button_output.TabIndex = 20;
            this.button_output.Text = "导出";
            this.button_output.UseVisualStyleBackColor = true;
            this.button_output.Click += new System.EventHandler(this.button_output_Click);
            // 
            // button_clipPolygon
            // 
            this.button_clipPolygon.Location = new System.Drawing.Point(618, 365);
            this.button_clipPolygon.Name = "button_clipPolygon";
            this.button_clipPolygon.Size = new System.Drawing.Size(100, 30);
            this.button_clipPolygon.TabIndex = 21;
            this.button_clipPolygon.Text = "裁剪多边形";
            this.button_clipPolygon.UseVisualStyleBackColor = true;
            this.button_clipPolygon.Click += new System.EventHandler(this.button_clipPolygon_Click);
            // 
            // button_findIntersections
            // 
            this.button_findIntersections.Location = new System.Drawing.Point(618, 401);
            this.button_findIntersections.Name = "button_findIntersections";
            this.button_findIntersections.Size = new System.Drawing.Size(100, 30);
            this.button_findIntersections.TabIndex = 22;
            this.button_findIntersections.Text = "求直线交点";
            this.button_findIntersections.UseVisualStyleBackColor = true;
            this.button_findIntersections.Click += new System.EventHandler(this.button_findIntersections_Click);
            // 
            // button_checkInPolygon
            // 
            this.button_checkInPolygon.Location = new System.Drawing.Point(816, 6);
            this.button_checkInPolygon.Name = "button_checkInPolygon";
            this.button_checkInPolygon.Size = new System.Drawing.Size(147, 30);
            this.button_checkInPolygon.TabIndex = 23;
            this.button_checkInPolygon.Text = "点是否在当前多边形内";
            this.button_checkInPolygon.UseVisualStyleBackColor = true;
            this.button_checkInPolygon.Click += new System.EventHandler(this.button_checkInPolygon_Click);
            // 
            // button_clearCanvas
            // 
            this.button_clearCanvas.Location = new System.Drawing.Point(618, 510);
            this.button_clearCanvas.Name = "button_clearCanvas";
            this.button_clearCanvas.Size = new System.Drawing.Size(100, 30);
            this.button_clearCanvas.TabIndex = 24;
            this.button_clearCanvas.Text = "清空画布";
            this.button_clearCanvas.UseVisualStyleBackColor = true;
            this.button_clearCanvas.Click += new System.EventHandler(this.button_clearCanvas_Click);
            // 
            // button_drawControlPolygon
            // 
            this.button_drawControlPolygon.Location = new System.Drawing.Point(618, 216);
            this.button_drawControlPolygon.Name = "button_drawControlPolygon";
            this.button_drawControlPolygon.Size = new System.Drawing.Size(100, 30);
            this.button_drawControlPolygon.TabIndex = 25;
            this.button_drawControlPolygon.Text = "绘制控制点";
            this.button_drawControlPolygon.UseVisualStyleBackColor = true;
            this.button_drawControlPolygon.Click += new System.EventHandler(this.button_drawControlPolygon_Click);
            // 
            // button_drawBezier
            // 
            this.button_drawBezier.Location = new System.Drawing.Point(724, 216);
            this.button_drawBezier.Name = "button_drawBezier";
            this.button_drawBezier.Size = new System.Drawing.Size(100, 30);
            this.button_drawBezier.TabIndex = 26;
            this.button_drawBezier.Text = "生成贝塞尔曲线";
            this.button_drawBezier.UseVisualStyleBackColor = true;
            this.button_drawBezier.Click += new System.EventHandler(this.button_drawBezier_Click);
            // 
            // button_switchShowControl
            // 
            this.button_switchShowControl.Location = new System.Drawing.Point(724, 252);
            this.button_switchShowControl.Name = "button_switchShowControl";
            this.button_switchShowControl.Size = new System.Drawing.Size(100, 30);
            this.button_switchShowControl.TabIndex = 27;
            this.button_switchShowControl.Text = "取消显示控制点";
            this.button_switchShowControl.UseVisualStyleBackColor = true;
            this.button_switchShowControl.Click += new System.EventHandler(this.button_switchShowControl_Click);
            // 
            // button_fillArea
            // 
            this.button_fillArea.Location = new System.Drawing.Point(618, 459);
            this.button_fillArea.Name = "button_fillArea";
            this.button_fillArea.Size = new System.Drawing.Size(100, 30);
            this.button_fillArea.TabIndex = 28;
            this.button_fillArea.Text = "区域填充";
            this.button_fillArea.UseVisualStyleBackColor = true;
            this.button_fillArea.Click += new System.EventHandler(this.button_fillArea_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(618, 594);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "当前颜色";
            // 
            // mainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 615);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button_fillArea);
            this.Controls.Add(this.button_switchShowControl);
            this.Controls.Add(this.button_drawBezier);
            this.Controls.Add(this.button_drawControlPolygon);
            this.Controls.Add(this.button_clearCanvas);
            this.Controls.Add(this.button_checkInPolygon);
            this.Controls.Add(this.button_findIntersections);
            this.Controls.Add(this.button_clipPolygon);
            this.Controls.Add(this.button_output);
            this.Controls.Add(this.textBox_chooseItem);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_clipLines);
            this.Controls.Add(this.button_drawRectangle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBox_graphics);
            this.Controls.Add(this.button_setCoor);
            this.Controls.Add(this.textBox_Y);
            this.Controls.Add(this.textBox_X);
            this.Controls.Add(this.textBox_circleRadius);
            this.Controls.Add(this.button_drawCircle);
            this.Controls.Add(this.panel_curColor);
            this.Controls.Add(this.button_startDrawLine);
            this.Controls.Add(this.button_endDrawPolygon);
            this.Controls.Add(this.button_startDrawPolygon);
            this.Controls.Add(this.label_curCoords);
            this.Controls.Add(this.textBox_curCoords);
            this.Controls.Add(this.panel_workspace);
            this.Name = "mainFrm";
            this.Text = "CG_Exp_2d";
            this.Load += new System.EventHandler(this.mainFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_workspace;
        private System.Windows.Forms.TextBox textBox_curCoords;
        private System.Windows.Forms.Label label_curCoords;
        private System.Windows.Forms.Button button_startDrawPolygon;
        private System.Windows.Forms.Button button_endDrawPolygon;
        private System.Windows.Forms.Button button_startDrawLine;
        private System.Windows.Forms.Panel panel_curColor;
        private System.Windows.Forms.Button button_drawCircle;
        private System.Windows.Forms.TextBox textBox_circleRadius;
        private System.Windows.Forms.TextBox textBox_X;
        private System.Windows.Forms.TextBox textBox_Y;
        private System.Windows.Forms.Button button_setCoor;
        private System.Windows.Forms.ListBox listBox_graphics;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button button_drawRectangle;
        private System.Windows.Forms.Button button_clipLines;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_chooseItem;
        private System.Windows.Forms.Button button_output;
        private System.Windows.Forms.Button button_clipPolygon;
        private System.Windows.Forms.Button button_findIntersections;
        private System.Windows.Forms.Button button_checkInPolygon;
        private System.Windows.Forms.Button button_clearCanvas;
        private System.Windows.Forms.Button button_drawControlPolygon;
        private System.Windows.Forms.Button button_drawBezier;
        private System.Windows.Forms.Button button_switchShowControl;
        private System.Windows.Forms.Button button_fillArea;
        private System.Windows.Forms.Label label3;
    }
}

