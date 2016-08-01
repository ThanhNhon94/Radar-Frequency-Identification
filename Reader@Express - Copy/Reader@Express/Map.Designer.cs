namespace Reader_Express
{
    partial class Map
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
            this.panelMap = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panelControl = new System.Windows.Forms.Panel();
            this.btnSetting = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnRectangle3D = new System.Windows.Forms.Button();
            this.btnPolygon = new System.Windows.Forms.Button();
            this.chbRaw = new System.Windows.Forms.CheckBox();
            this.labelControl = new System.Windows.Forms.Label();
            this.btnRectangle = new System.Windows.Forms.Button();
            this.btnEclipse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelMap.SuspendLayout();
            this.panelControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMap
            // 
            this.panelMap.BackColor = System.Drawing.Color.White;
            this.panelMap.Controls.Add(this.label3);
            this.panelMap.Controls.Add(this.label4);
            this.panelMap.Location = new System.Drawing.Point(111, 31);
            this.panelMap.Name = "panelMap";
            this.panelMap.Size = new System.Drawing.Size(472, 532);
            this.panelMap.TabIndex = 0;
            this.panelMap.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMap_Paint);
            this.panelMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MoveDown);
            this.panelMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseMove);
            this.panelMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(191, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "ant1";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "ant4";
            // 
            // panelControl
            // 
            this.panelControl.BackColor = System.Drawing.Color.Cyan;
            this.panelControl.Controls.Add(this.btnSetting);
            this.panelControl.Controls.Add(this.btnClear);
            this.panelControl.Controls.Add(this.btnRectangle3D);
            this.panelControl.Controls.Add(this.btnPolygon);
            this.panelControl.Controls.Add(this.chbRaw);
            this.panelControl.Controls.Add(this.labelControl);
            this.panelControl.Controls.Add(this.btnRectangle);
            this.panelControl.Controls.Add(this.btnEclipse);
            this.panelControl.Location = new System.Drawing.Point(0, 1);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(105, 321);
            this.panelControl.TabIndex = 1;
            // 
            // btnSetting
            // 
            this.btnSetting.Location = new System.Drawing.Point(0, 245);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(105, 32);
            this.btnSetting.TabIndex = 5;
            this.btnSetting.Text = "Setting";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(0, 283);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(105, 34);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnRectangle3D
            // 
            this.btnRectangle3D.Location = new System.Drawing.Point(0, 201);
            this.btnRectangle3D.Name = "btnRectangle3D";
            this.btnRectangle3D.Size = new System.Drawing.Size(105, 37);
            this.btnRectangle3D.TabIndex = 3;
            this.btnRectangle3D.Text = "Start";
            this.btnRectangle3D.UseVisualStyleBackColor = true;
            this.btnRectangle3D.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnPolygon
            // 
            this.btnPolygon.Location = new System.Drawing.Point(0, 155);
            this.btnPolygon.Name = "btnPolygon";
            this.btnPolygon.Size = new System.Drawing.Size(105, 39);
            this.btnPolygon.TabIndex = 2;
            this.btnPolygon.Text = "Stop";
            this.btnPolygon.UseVisualStyleBackColor = true;
            this.btnPolygon.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // chbRaw
            // 
            this.chbRaw.AutoSize = true;
            this.chbRaw.Location = new System.Drawing.Point(3, 42);
            this.chbRaw.Name = "chbRaw";
            this.chbRaw.Size = new System.Drawing.Size(48, 17);
            this.chbRaw.TabIndex = 1;
            this.chbRaw.Text = "Raw";
            this.chbRaw.UseVisualStyleBackColor = true;
            // 
            // labelControl
            // 
            this.labelControl.AutoSize = true;
            this.labelControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl.ForeColor = System.Drawing.Color.Blue;
            this.labelControl.Location = new System.Drawing.Point(28, 11);
            this.labelControl.Name = "labelControl";
            this.labelControl.Size = new System.Drawing.Size(50, 24);
            this.labelControl.TabIndex = 0;
            this.labelControl.Text = "Map";
            // 
            // btnRectangle
            // 
            this.btnRectangle.Location = new System.Drawing.Point(0, 65);
            this.btnRectangle.Name = "btnRectangle";
            this.btnRectangle.Size = new System.Drawing.Size(105, 40);
            this.btnRectangle.TabIndex = 0;
            this.btnRectangle.Text = "Rectangle";
            this.btnRectangle.UseVisualStyleBackColor = true;
            this.btnRectangle.Click += new System.EventHandler(this.btnRectangle_Click);
            // 
            // btnEclipse
            // 
            this.btnEclipse.Location = new System.Drawing.Point(0, 111);
            this.btnEclipse.Name = "btnEclipse";
            this.btnEclipse.Size = new System.Drawing.Size(105, 38);
            this.btnEclipse.TabIndex = 0;
            this.btnEclipse.Text = "Path";
            this.btnEclipse.UseVisualStyleBackColor = true;
            this.btnEclipse.Click += new System.EventHandler(this.btnCircle_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(111, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ant3";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(302, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "ant2";
            // 
            // Map
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelControl);
            this.Controls.Add(this.panelMap);
            this.Name = "Map";
            this.Text = "Map";
            this.Load += new System.EventHandler(this.Map_Load);
            this.panelMap.ResumeLayout(false);
            this.panelMap.PerformLayout();
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelMap;
        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.Label labelControl;
        private System.Windows.Forms.Button btnRectangle;
        private System.Windows.Forms.Button btnEclipse;
        private System.Windows.Forms.CheckBox chbRaw;
        private System.Windows.Forms.Button btnPolygon;
        private System.Windows.Forms.Button btnRectangle3D;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}