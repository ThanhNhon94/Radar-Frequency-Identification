namespace Reader_Express
{
    partial class RoomParameter
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbWidth = new System.Windows.Forms.ComboBox();
            this.cbbHeight = new System.Windows.Forms.ComboBox();
            this.cbbDeep = new System.Windows.Forms.ComboBox();
            this.cbbRatio = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.chb2D = new System.Windows.Forms.CheckBox();
            this.chb3D = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Height";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Deep";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Ratio";
            // 
            // cbbWidth
            // 
            this.cbbWidth.FormattingEnabled = true;
            this.cbbWidth.Items.AddRange(new object[] {
            "90"});
            this.cbbWidth.Location = new System.Drawing.Point(65, 40);
            this.cbbWidth.Name = "cbbWidth";
            this.cbbWidth.Size = new System.Drawing.Size(182, 21);
            this.cbbWidth.TabIndex = 4;
            // 
            // cbbHeight
            // 
            this.cbbHeight.FormattingEnabled = true;
            this.cbbHeight.Items.AddRange(new object[] {
            "90"});
            this.cbbHeight.Location = new System.Drawing.Point(65, 74);
            this.cbbHeight.Name = "cbbHeight";
            this.cbbHeight.Size = new System.Drawing.Size(182, 21);
            this.cbbHeight.TabIndex = 5;
            // 
            // cbbDeep
            // 
            this.cbbDeep.FormattingEnabled = true;
            this.cbbDeep.Items.AddRange(new object[] {
            "90"});
            this.cbbDeep.Location = new System.Drawing.Point(65, 110);
            this.cbbDeep.Name = "cbbDeep";
            this.cbbDeep.Size = new System.Drawing.Size(182, 21);
            this.cbbDeep.TabIndex = 6;
            // 
            // cbbRatio
            // 
            this.cbbRatio.FormattingEnabled = true;
            this.cbbRatio.Items.AddRange(new object[] {
            "90"});
            this.cbbRatio.Location = new System.Drawing.Point(65, 144);
            this.cbbRatio.Name = "cbbRatio";
            this.cbbRatio.Size = new System.Drawing.Size(182, 21);
            this.cbbRatio.TabIndex = 7;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(94, 171);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(90, 38);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chb2D
            // 
            this.chb2D.AutoSize = true;
            this.chb2D.Location = new System.Drawing.Point(65, 13);
            this.chb2D.Name = "chb2D";
            this.chb2D.Size = new System.Drawing.Size(40, 17);
            this.chb2D.TabIndex = 9;
            this.chb2D.Text = "2D";
            this.chb2D.UseVisualStyleBackColor = true;
            // 
            // chb3D
            // 
            this.chb3D.AutoSize = true;
            this.chb3D.Location = new System.Drawing.Point(207, 13);
            this.chb3D.Name = "chb3D";
            this.chb3D.Size = new System.Drawing.Size(40, 17);
            this.chb3D.TabIndex = 10;
            this.chb3D.Text = "3D";
            this.chb3D.UseVisualStyleBackColor = true;
            // 
            // RoomParameter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 219);
            this.Controls.Add(this.chb3D);
            this.Controls.Add(this.chb2D);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cbbRatio);
            this.Controls.Add(this.cbbDeep);
            this.Controls.Add(this.cbbHeight);
            this.Controls.Add(this.cbbWidth);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "RoomParameter";
            this.Text = "Room Parameter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbbWidth;
        private System.Windows.Forms.ComboBox cbbHeight;
        private System.Windows.Forms.ComboBox cbbDeep;
        private System.Windows.Forms.ComboBox cbbRatio;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.CheckBox chb2D;
        private System.Windows.Forms.CheckBox chb3D;
    }
}