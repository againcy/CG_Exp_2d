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
            this.button_chooseColor = new System.Windows.Forms.Button();
            this.panel_curColor = new System.Windows.Forms.Panel();
            this.button_drawCircle = new System.Windows.Forms.Button();
            this.textBox_circleRadius = new System.Windows.Forms.TextBox();
            this.textBox_X = new System.Windows.Forms.TextBox();
            this.textBox_Y = new System.Windows.Forms.TextBox();
            this.button_setCoor = new System.Windows.Forms.Button();
            this.listBox_graphics = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
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
            // button_chooseColor
            // 
            this.button_chooseColor.Location = new System.Drawing.Point(620, 582);
            this.button_chooseColor.Name = "button_chooseColor";
            this.button_chooseColor.Size = new System.Drawing.Size(100, 30);
            this.button_chooseColor.TabIndex = 6;
            this.button_chooseColor.Text = "选择颜色";
            this.button_chooseColor.UseVisualStyleBackColor = true;
            this.button_chooseColor.Click += new System.EventHandler(this.button_chooseColor_Click);
            // 
            // panel_curColor
            // 
            this.panel_curColor.Location = new System.Drawing.Point(738, 582);
            this.panel_curColor.Name = "panel_curColor";
            this.panel_curColor.Size = new System.Drawing.Size(30, 30);
            this.panel_curColor.TabIndex = 7;
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
            // mainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 615);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBox_graphics);
            this.Controls.Add(this.button_setCoor);
            this.Controls.Add(this.textBox_Y);
            this.Controls.Add(this.textBox_X);
            this.Controls.Add(this.textBox_circleRadius);
            this.Controls.Add(this.button_drawCircle);
            this.Controls.Add(this.panel_curColor);
            this.Controls.Add(this.button_chooseColor);
            this.Controls.Add(this.button_startDrawLine);
            this.Controls.Add(this.button_endDrawPolygon);
            this.Controls.Add(this.button_startDrawPolygon);
            this.Controls.Add(this.label_curCoords);
            this.Controls.Add(this.textBox_curCoords);
            this.Controls.Add(this.panel_workspace);
            this.Name = "mainFrm";
            this.Text = "Form1";
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
        private System.Windows.Forms.Button button_chooseColor;
        private System.Windows.Forms.Panel panel_curColor;
        private System.Windows.Forms.Button button_drawCircle;
        private System.Windows.Forms.TextBox textBox_circleRadius;
        private System.Windows.Forms.TextBox textBox_X;
        private System.Windows.Forms.TextBox textBox_Y;
        private System.Windows.Forms.Button button_setCoor;
        private System.Windows.Forms.ListBox listBox_graphics;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}

